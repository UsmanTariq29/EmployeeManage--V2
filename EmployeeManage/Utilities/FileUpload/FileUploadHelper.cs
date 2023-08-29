using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace EmployeeManage.Utilities.FileUpload
{
    public static class FileUploadHelper
    {
        private static readonly Dictionary<string, List<byte[]>> FileBoMs = new Dictionary<string, List<byte[]>>
        {
            {
                ".jpg",
                new List<byte[]>
                {
                    new byte[] {0xFF, 0xD8, 0xFF, 0xE0},
                    new byte[] {0xFF, 0xD8, 0xFF, 0xE1},
                    new byte[] {0xFF, 0xD8, 0xFF, 0xE8},
                }
            },
            {
                ".jpeg",
                new List<byte[]>
                {
                    new byte[] {0xFF, 0xD8, 0xFF, 0xE0},
                    new byte[] {0xFF, 0xD8, 0xFF, 0xE2},
                    new byte[] {0xFF, 0xD8, 0xFF, 0xE3},
                }
            },
            {
                ".xlsx",
                new List<byte[]>
                {
                    new byte[] {0x50, 0x4B, 0x03, 0x04},
                    new byte[] {0x50, 0x4B, 0x03, 0x04, 0x14, 0x00, 0x06, 0x00},
                }
            },
            {
                ".docx",
                new List<byte[]>
                {
                    new byte[] {0x50, 0x4B, 0x03, 0x04},
                    new byte[] {0x50, 0x4B, 0x03, 0x04, 0x14, 0x00, 0x06, 0x00},
                }
            },
            {
                ".doc",
                new List<byte[]>
                {
                    new byte[] {0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1},
                    new byte[] {0x0D, 0x44, 0x4F, 0x43},
                    new byte[] {0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1, 0x00},
                    new byte[] {0xDB, 0xA5, 0x2D, 0x00},
                    new byte[] {0xEC, 0xA5, 0xC1, 0x00},
                }
            },
            {
                ".png",
                new List<byte[]>
                {
                    new byte[] {0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A}
                }
            },
            {
                ".gif",
                new List<byte[]>
                {
                    new byte[] {0x47, 0x49, 0x46, 0x38}
                }
            },
            {
                ".pdf",
                new List<byte[]>
                {
                    new byte[] {0x25, 0x50, 0x44, 0x46}
                }
            },
            {
                ".svg",
                new List<byte[]>
                {
                    new byte[]
                    {
                        0x3c, 0x3f, 0x78, 0x6d, 0x6c, 0x20, 0x76, 0x65, 0x72, 0x73, 0x69, 0x6f, 0x6e, 0x3d, 0x22, 0x31,
                        0x2e, 0x30, 0x22
                    }, // for files beginning with XML version
                    new byte[] {0x3c, 0x73, 0x76, 0x67} // for files beginning with SVG tag declaration
                }
            }
        };


        public static void VerifyUploadedFiles(FileUploadConfig config, IList<IFormFile> uploads)
        {
            if (uploads.Count > config.MaximumFileCount)
            {
                //throw new APIException(
                //    ErrorCodesEnum.ERROR_MAXIMUM_FILE_COUNT_EXCEEDED,
                //    StatusCodes.Status400BadRequest,
                //    arguments: new object[] {config.MaximumFileCount}
                //);
            }

            foreach (var file in uploads)
            {
                // check file length (with BOM)
                if (file.Length == 0)
                {
                    //throw new APIException(
                    //    ErrorCodesEnum.ERROR_INVALID_OR_EMPTY_FILE,
                    //    StatusCodes.Status400BadRequest
                    //);
                }

                if (file.Length > config.MaxFileSizeBytes)
                {
                    var kiloBytesFileSize = config.MaxFileSizeBytes / 1024;
                    //throw new APIException(
                    //    ErrorCodesEnum.ERROR_MAXIMUM_FILE_SIZE_EXCEEDED,
                    //    StatusCodes.Status400BadRequest,
                    //    arguments: new object[] {$"{kiloBytesFileSize:N1}"}
                    //);
                }

                 using var memoryStream = new MemoryStream();
                 file.CopyToAsync(memoryStream);
                // check file (without BOM) length
                if (memoryStream.Length == 0)
                {
                    //throw new APIException(
                    //    ErrorCodesEnum.ERROR_INVALID_OR_EMPTY_FILE,
                    //    StatusCodes.Status400BadRequest
                    //);
                }

                if (!IsValidFileType(file.FileName, memoryStream, config.AllowedExtensions))
                {
                    //throw new APIException(
                    //    ErrorCodesEnum.ERROR_UNSUPPORTED_FILE_TYPE,
                    //    StatusCodes.Status400BadRequest
                    //);
                }
            }
        }

        public static string SaveFile(IFormFile file, string directory, string name = null)
        {
            if (file == null)
            {
                throw new ArgumentNullException($"{nameof(file)} cannot be empty!");
            }

            if (string.IsNullOrEmpty(directory))
            {
                throw new ArgumentNullException($"{nameof(directory)} cannot be empty!");
            }

            name ??= Path.GetRandomFileName();
            var path = Path.Combine(directory, name);
            path = Path.ChangeExtension(path, Path.GetExtension(file.FileName)?.ToLowerInvariant());

            using var stream = File.Create(path);
            file.CopyToAsync(stream);
            return Path.GetFileName(path);
        }

        /// <summary>Loads and returns a physical file as a byte array.</summary>
        /// <param name="directory">File's directory. Should be retrieved using <see cref="GetUploadDirectory"/></param>
        /// <param name="filename">File's full name, including extension, if any.</param>
        /// <param name="token">Asynchronous cancellation token.</param>
        /// <returns>File's content as a byte array.</returns>
        /// <exception cref="FileNotFoundException">File does not exist in the specified location.</exception>
        public static FileDownloadResult GetSavedFile(string directory, string filename)
        {
            var filePath = Path.Combine(directory, filename);
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException();
            }

            return new FileDownloadResult(File.ReadAllBytes(filePath), filename);
        }

        public static byte[] GetFileData(IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            file.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }

        public static string GetUploadDirectory(FileUploadConfig config, IConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            if (config == null || string.IsNullOrEmpty(config.RelativeUploadPath))
            {
                throw new ArgumentException(nameof(config));
            }

            var baseUploadPath = configuration.GetValue<string>("FileUpload:RootPath");
            return Path.Combine(baseUploadPath, config.RelativeUploadPath);
        }

        private static bool IsValidFileType(string fileName, Stream data, IEnumerable<string> allowedExtensions)
        {
            if (string.IsNullOrEmpty(fileName) || data == null || data.Length == 0)
            {
                return false;
            }

            var fileExtension = Path.GetExtension(fileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(fileExtension) || !allowedExtensions.Contains(fileExtension))
            {
                return false;
            }

            data.Position = 0;
            using var reader = new BinaryReader(data);
            var fileExtensionBoMs = FileBoMs[fileExtension];
            var boMHeader = reader.ReadBytes(fileExtensionBoMs.Max(b => b.Length));

            return fileExtensionBoMs.Any(b => boMHeader.Take(b.Length).SequenceEqual(b));
        }
    }
}