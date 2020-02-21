using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Nature.Models
{
	public static class ServerFiles
	{
		public static async Task<string> SaveImageToLocalFiles(IWebHostEnvironment serverEnvironment, IFormFile file, string folderName = "")
		{
			var imagePath = @"\img\";
			if (!string.IsNullOrWhiteSpace(folderName))
				imagePath += folderName + @"\";

			var uploadPath = serverEnvironment.WebRootPath + imagePath;
			if (Directory.Exists(uploadPath))
				Directory.CreateDirectory(uploadPath);

			var uniqFileName = Guid.NewGuid().ToString();
			var fileName = Path.GetFileName(uniqFileName + "." + file.FileName.Split(".")[1].ToLower());
			string fullPath = uploadPath + fileName;

			imagePath += @"\";
			var filePath = @".." + Path.Combine(imagePath, fileName);

			using (var fileStream = new FileStream(fullPath, FileMode.Create))
			{
				await file.CopyToAsync(fileStream);
			}

			return filePath;
		}
	}
}
