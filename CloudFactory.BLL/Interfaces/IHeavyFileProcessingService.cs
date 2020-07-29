using System.Threading.Tasks;

namespace CloudFactory.BLL.Interfaces
{
	public interface IHeavyFileProcessingService
	{
		Task<byte[]> GetFile(string fileFullPath);
	}
}
