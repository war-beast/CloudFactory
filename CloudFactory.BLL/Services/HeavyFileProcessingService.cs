using CloudFactory.BLL.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CloudFactory.BLL.Services
{
	public class HeavyFileProcessingService : IHeavyFileProcessingService
	{
		#region private members

		private readonly IMemoryCache _cache;
		private const int FileProcessingDelayMilliseconds = 2000;
		private readonly object _lockObj = new object();
		private const int DefaultCacheDurationMinutes = 20;
		
		#endregion

		#region constuctor

		public HeavyFileProcessingService(IMemoryCache cache)
		{
			_cache = cache ?? throw new ArgumentNullException();
		}

		#endregion

		public byte[] GetFile(string fileFullPath)
		{
			#region validation

			if (string.IsNullOrWhiteSpace(fileFullPath))
				throw new ArgumentNullException(nameof(fileFullPath));

			#endregion

			lock (_lockObj)
			{
				var array = LoadFile(fileFullPath);
				return array;
			}
		}

		#region private members

		private byte[] LoadFile(string fileFullPath)
		{
			if (_cache.TryGetValue(fileFullPath, out byte[] cachedArray))
			{
				return cachedArray;
			}

			using var fileStream = File.OpenRead(fileFullPath);
			var array = new byte[fileStream.Length];
			fileStream.Read(array);

			//Пауза, иммитирующая длительный процесс
			Task.Delay(FileProcessingDelayMilliseconds)
				.GetAwaiter()
				.GetResult();

			_cache.Set(fileFullPath, array);

			return array;
		}

		#endregion
	}
}
