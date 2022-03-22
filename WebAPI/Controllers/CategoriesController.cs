using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		ICategoryService _categoryService;

		public CategoriesController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		[HttpGet("getall")]
		public IActionResult GetList()
		{
			var result = _categoryService.GetList();
			if (result.Success) return Ok(result.Data);
			return BadRequest(result.Message);
		}

		[HttpGet("getbyid")]
		public IActionResult GetById(int categoryId)
		{
			var result = _categoryService.GetById(categoryId);
			if (result.Success) return Ok(result.Data);
			return BadRequest(result.Message);
		}
	}
}
