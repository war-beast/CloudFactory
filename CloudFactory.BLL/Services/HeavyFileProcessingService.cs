using CloudFactory.BLL.Interfaces;
using CloudFactory.BLL.Models;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CloudFactory.BLL.Services
{
	public class HeavyFileProcessingService : IHeavyFileProcessingService
	{
		#region private members
		
		private const int FileProcessingDelayMilliseconds = 10000;

		#endregion

		#region constructor

		public HeavyFileProcessingService()
		{
			
		}

		#endregion

		public async Task<byte[]> GetFile(string fileFullPath)
		{
			#region validation

			if (string.IsNullOrWhiteSpace(fileFullPath))
				throw new ArgumentNullException(nameof(fileFullPath));

			#endregion

			await using FileStream fileStream = File.OpenRead(fileFullPath);
			var array = new byte[fileStream.Length];
			await fileStream.ReadAsync(array);

			return array;
		}
	}
}
