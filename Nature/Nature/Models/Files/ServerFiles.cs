using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Nature.Models
{
	public static class ServerFiles
	{
		public const string img = "img";
		public const string services = "services";
		public const string doctors = "doctors";
		public const string news = "news";

		public static async Task<string> SaveImageToLocalFiles(IWebHostEnvironment serverEnvironment, IFormFile file, string folderName = "")
		{
			var imagePath = @"\img\";
			if (!string.IsNullOrWhiteSpace(folderName))
				imagePath += folderName + @"\";

			var uploadPath = serverEnvironment.WebRootPath + imagePath;
			if (Directory.Exists(uploadPath))
				Directory.CreateDirectory(uploadPath);

			var uniqFileName = Guid.NewGuid().ToString();
			var fileExtension = file.FileName.Split(".")[1].ToLower();
			var fileName = Path.GetFileName(uniqFileName + "." + fileExtension);
			string fullPath = uploadPath + fileName;

			imagePath += @"\";
			var filePath = @".." + Path.Combine(imagePath, fileName);

			using (var fileStream = new FileStream(fullPath, FileMode.Create))
			{
				await file.CopyToAsync(fileStream);
			}

			return filePath;
		}

		public static void DeleteImageFromLocalFiles(string webRootPath, string imagePath, string folderName = "")
		{
			try
			{
				string fileName = imagePath.Split("\\")[4];
				string filePath = Path.Combine(webRootPath, "img", folderName, fileName);
				File.Delete(filePath);
			}
			catch (DirectoryNotFoundException ex)
			{
				Trace.WriteLine(ex.Message);
				throw;
			}
		}
	}
}
