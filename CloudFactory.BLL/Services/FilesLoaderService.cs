using CloudFactory.BLL.Interfaces;
using CloudFactory.BLL.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CloudFactory.BLL.Services
{
	public class FilesLoaderService : IFilesLoaderService
	{
		#region private members

		private readonly SiteSettings _siteSettings;
		private readonly string _filesDirectory;
		
		#endregion

		#region constructor

		public FilesLoaderService(IOptionsSnapshot<SiteSettings> siteSettings)
		{
			_siteSettings = siteSettings?.Value ?? throw new ArgumentException(nameof(siteSettings.Value));
			_filesDirectory = $"{Directory.GetCurrentDirectory()}/{_siteSettings.FilesRoot}";
		}

		#endregion

		public IReadOnlyCollection<string> GetAvailableFilenames()
		{
			return Directory
				.EnumerateFiles(_filesDirectory, "*.txt", SearchOption.TopDirectoryOnly)
				.Select(x => x.Split('\\').Last())
				.ToList();
		}
	}
}
