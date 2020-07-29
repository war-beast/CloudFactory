using CloudFactory.BLL.Interfaces;
using CloudFactory.BLL.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CloudFactory.BLL.Services
{
	public class FilesLoaderService : IFilesLoaderService
	{
		#region private members

		private readonly SiteSettings _siteSettings;
		private readonly string _filesDirectory;
		private readonly IHeavyFileProcessingService _heavyFileProcessingService;

		#endregion

		#region constructor

		public FilesLoaderService(IOptionsSnapshot<SiteSettings> siteSettings, 
			IHeavyFileProcessingService heavyFileProcessingService)
		{
			_heavyFileProcessingService = heavyFileProcessingService ?? throw new ArgumentNullException(nameof(heavyFileProcessingService));
			_siteSettings = siteSettings?.Value ?? throw new ArgumentException(nameof(siteSettings.Value));
			_filesDirectory = $"{Directory.GetCurrentDirectory()}/{_siteSettings.FilesRoot}";
		}

		#endregion

		public async Task<IReadOnlyCollection<string>> GetAvailableFilenames()
		{
			return await Task.Run(() => 
				Directory.EnumerateFiles(_filesDirectory, "*.txt", SearchOption.TopDirectoryOnly)
				.Select(x => x.Split('\\').Last())
				.ToList() );
		}

		public async Task<byte[]> GetFile(string fileName)
		{
			#region validation

			if (string.IsNullOrWhiteSpace(fileName))
				throw new ArgumentNullException(nameof(fileName));

			#endregion

			return await Task.Run(() => _heavyFileProcessingService.GetFile(fileFullPath: $"{_filesDirectory}/{fileName}"));
		}

	}
}
