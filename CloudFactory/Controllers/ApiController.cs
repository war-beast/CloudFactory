using CloudFactory.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
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
		public IActionResult GetFilesList()
		{
			var files = _filesLoaderService.GetAvailableFilenames();

			return Ok(JsonConvert.SerializeObject(files, Formatting.None, new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver(),
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore
			}));
		}
	}
}
