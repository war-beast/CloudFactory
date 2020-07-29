using CloudFactory.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;

namespace CloudFactory.BLL.Services
{
	public class HeavyFileProcessingService : IHeavyFileProcessingService
	{
		#region private members

		private const int FileProcessingDelayMilliseconds = 2000;
		private readonly object _lockObj = new object();

		private readonly Dictionary<string, byte[]> _filesBuffer = new Dictionary<string, byte[]>();

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
			if (_filesBuffer.ContainsKey(fileFullPath))
			{
				return _filesBuffer[fileFullPath];
			}

			using var fileStream = File.OpenRead(fileFullPath);
			var array = new byte[fileStream.Length];
			fileStream.Read(array);

			//Пауза, иммитирующая длительный процесс
			Task.Delay(FileProcessingDelayMilliseconds)
				.GetAwaiter()
				.GetResult();

			_filesBuffer.Add(fileFullPath, array);

			return array;
		}

		#endregion
	}
}
