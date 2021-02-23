using CloudFactory.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace CloudFactory.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CacheController : ControllerBase
	{
		#region private members

		private readonly IFilesLoaderService _filesLoaderService;
		#endregion

		#region constructor

		public CacheController(IFilesLoaderService filesLoaderService)
		{
			_filesLoaderService = filesLoaderService;
		}

		[HttpGet]
		[Route("clear")]
		public async Task<IActionResult> ClearCache()
		{
			await _filesLoaderService.ClearCache(CancellationToken.None);

			return Ok();
		}

		#endregion
	}
}
