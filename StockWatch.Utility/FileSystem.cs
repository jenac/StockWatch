using System;
using System.IO;

namespace StockWatch.Utility
{
	public static class FileSystem
	{
		/*
		public static string GetSWFolderOnGoogleDrive()
		{
			string value = Directory.GetParent(
				Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName;
			if (Environment.OSVersion.Version.Major >= 6)
			{
				value = Directory.GetParent(value).FullName;
			}
			return Path.Combine(value, "Google Drive", "StockWatch");
		}*/

		public static void EnsureFolder(string folder)
		{
			if (!Directory.Exists(folder))
				Directory.CreateDirectory(folder);
		}

		public static string GetSWLogFolder()
        {
            return Path.Combine(GetSWFolder(), "Logs");
        }

        public static string GetSWTempFolder()
        {
            return Path.Combine(GetSWFolder(), "Temp");
        }

        private static string GetSWFolder()
        {
            string value = Environment.GetEnvironmentVariable("ALLUSERSPROFILE");
            return Path.Combine(value, "StockWatch");
        }
        public static string ToUnixPath(string windowsPath)
        {
            return windowsPath.Replace('\\', '/');
        }

		/*
		public static void ZipFolder(string sourceFolder, string targetFile)
		{
			ZipFile.CreateFromDirectory(sourceFolder, targetFile);
		}

		public static void UnzipFile(string sourceFile, string targetFolder)
		{
			ZipFile.ExtractToDirectory(sourceFile, targetFolder);
		}*/

	}
}

