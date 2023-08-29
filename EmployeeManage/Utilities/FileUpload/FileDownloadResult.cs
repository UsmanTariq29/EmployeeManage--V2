using System;
using System.IO;
using Microsoft.AspNetCore.StaticFiles;

namespace EmployeeManage.Utilities.FileUpload
{
    /// <summary>
    /// Helper class for transferring files. Automatically determines MIME type via filename.
    /// </summary>
    public class FileDownloadResult
    {
        public byte[] Data { get; }

        public string ContentType { get; }
        
        public string Name { get; }

        /// <summary>
        /// Stores file data and tries to generate MIME type based on file name. If MIME type cannot be found,
        /// it stores an empty string instead.
        /// </summary>
        /// <param name="data">File data</param>
        /// <param name="filename">File name</param>
        /// <exception cref="ArgumentNullException">When filename is invalid, or has no extension.</exception>
        public FileDownloadResult(byte[] data, string filename)
        {
            var extension = Path.GetExtension(filename).ToLowerInvariant();

            if (string.IsNullOrEmpty(extension))
            {
                throw new ArgumentNullException($"{nameof(filename)} not valid!");
            }

            new FileExtensionContentTypeProvider().TryGetContentType(filename, out var contentType);
            ContentType = contentType ?? string.Empty;
            Data = data;
            Name = filename;
        }

        public MemoryStream GetStream()
        {
            if (Data == null)
            {
                throw new InvalidOperationException($"{nameof(Data)} is null!");
            }
            return new MemoryStream(Data);
        }
    }
}