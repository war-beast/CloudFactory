using System.Threading;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace CloudFactory.BLL.Interfaces
{
	public interface IHeavyFileProcessingService
	{
		Task<byte[]> GetFile(string fileFullPath, CancellationToken token);

		Task RemoveKey(string key, CancellationToken token);
	}
}
