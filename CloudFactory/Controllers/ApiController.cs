using CloudFactory.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CloudFactory.Controllers
{
	[Route("api/files")]
	[ApiController]
	public class ApiController : ControllerBase
	{
		#region private members

		private readonly IFilesLoaderService _filesLoaderService;

		#endregion

		#region constructor

		public ApiController(IFilesLoaderService filesLoaderService)
		{
			_filesLoaderService = filesLoaderService ?? throw new ArgumentException(nameof(filesLoaderService));
		}

		#endregion

		[HttpGet]
		[Route("list")]
		public async Task<IActionResult> GetFilesList()
		{
			var files = await _filesLoaderService.GetAvailableFilenames();

			return Ok(JsonConvert.SerializeObject(files, Formatting.None, new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver(),
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore
			}));
		}

		[HttpGet]
		[Route("")]
		public async Task<IActionResult> GetFile(string fileName)
		{
			#region validation

			if (string.IsNullOrWhiteSpace(fileName))
				return NotFound();

			#endregion

			var fileContent = await _filesLoaderService.GetFile(fileName, CancellationToken.None);

			return File(fileContent, "text/txt", fileName);
		}
	}
}
