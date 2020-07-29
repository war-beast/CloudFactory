using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudFactory.BLL.Interfaces
{
	public interface IFilesLoaderService
	{
		/// <summary>
		/// Получение списка файлов, доступных для чтения
		/// </summary>
		/// <returns>Список файлов</returns>
		Task<IReadOnlyCollection<string>> GetAvailableFilenames();

		/// <summary>
		/// Получение содержимого текстового файла по его имени
		/// </summary>
		/// <param name="fileName">Имя файла</param>
		/// <returns>Содержимое файла</returns>
		Task<byte[]> GetFile(string fileName);
	}
}
