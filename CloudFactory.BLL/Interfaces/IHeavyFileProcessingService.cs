using System.Threading.Tasks;

namespace CloudFactory.BLL.Interfaces
{
	public interface IHeavyFileProcessingService
	{
		byte[] GetFile(string fileFullPath);
	}
}
