using System.Collections.Generic;

namespace CloudFactory.BLL.Interfaces
{
	public interface IFilesLoaderService
	{
		IReadOnlyCollection<string> GetAvailableFilenames();
	}
}
