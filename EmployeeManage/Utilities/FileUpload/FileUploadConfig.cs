namespace EmployeeManage.Utilities.FileUpload
{
	public class FileUploadConfig
	{
		public string RelativeUploadPath { get; set; }

		public int? MaximumFileCount { get; set; }

		public string[] AllowedExtensions { get; set; }

		public long MaxFileSizeBytes { get; set; }
	}
}