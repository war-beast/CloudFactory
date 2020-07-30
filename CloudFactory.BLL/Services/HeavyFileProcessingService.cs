using CloudFactory.BLL.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CloudFactory.BLL.Services
{
	public class HeavyFileProcessingService : IHeavyFileProcessingService
	{
		#region private members

		private const int FileProcessingDelayMilliseconds = 2000;
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
			byte[] cachedArray = _distributedCache.Get(fileFullPath);
			if (cachedArray != null)
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

			_distributedCache.Set(fileFullPath, 
				array, 
				new DistributedCacheEntryOptions
				{
					SlidingExpiration = TimeSpan.FromMinutes(20)
				});

			return array;
		}

		#endregion
	}
}
