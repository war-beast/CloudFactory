using CloudFactory.BLL.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CloudFactory.BLL.Services
{
	public class HeavyFileProcessingService : IHeavyFileProcessingService
	{
		#region private members

		private const int FileProcessingDelayMilliseconds = 5000;
		private readonly object _lockObj = new object();
		private readonly IDistributedCache _distributedCache;
		private const int DefaultCacheDurationMinutes = 20;

		#endregion

		#region constructor

		public HeavyFileProcessingService(IDistributedCache distributedCache)
		{
			_distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
		}

		#endregion

		public async Task<byte[]> GetFile(string fileFullPath, CancellationToken token)
		{
			#region validation

			if (string.IsNullOrWhiteSpace(fileFullPath))
				throw new ArgumentNullException(nameof(fileFullPath));

			#endregion

			Task<byte[]> array;

			lock (_lockObj)
			{
				array = LoadFile(fileFullPath, token);
			}
			
			return await array;
		}

		public Task RemoveKey(string key, CancellationToken token)
		{
			lock (_lockObj)
			{
				return _distributedCache.RemoveAsync(key, token);
			}
		}

		#region private members

		private async Task<byte[]> LoadFile(string fileFullPath, CancellationToken token)
		{
			byte[] cachedArray = await _distributedCache.GetAsync(fileFullPath, token);
			if (cachedArray != null)
			{
				return cachedArray;
			}

			await using var fileStream = File.OpenRead(fileFullPath);
			var array = new byte[fileStream.Length];
			fileStream.Read(array);

			//Пауза, иммитирующая длительный процесс
			await Task.Delay(FileProcessingDelayMilliseconds, token);

			await _distributedCache.SetAsync(fileFullPath, 
				array, 
				new DistributedCacheEntryOptions
				{
					SlidingExpiration = TimeSpan.FromSeconds(30)
				}, token);

			return array;
		}

		#endregion
	}
}
