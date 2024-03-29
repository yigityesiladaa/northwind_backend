﻿using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : Controller
	{
		IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpPost("login")]
		public ActionResult Login(UserForLoginDto userForLoginDto)
		{
			var userToLogin = _authService.Login(userForLoginDto);
			if (!userToLogin.Success)
			{
				return BadRequest(userToLogin.Message);
			}
			var result = _authService.CreateAccessToken(userToLogin.Data);
			if (!result.Success)
			{
				return BadRequest(result.Message);
			}

			return Ok(result.Data);
		}

		[HttpPost("register")]
		public ActionResult Register(UserForRegisterDto userForRegisterDto)
		{
			var userExist = _authService.UserExist(userForRegisterDto.Email);
			if (!userExist.Success) 
			{
				return BadRequest(userExist.Message);
			}

			var registerResult = _authService.Register(userForRegisterDto);
			var result = _authService.CreateAccessToken(registerResult.Data);
			if (!result.Success)
			{
				return BadRequest(result.Message);
			}

			return Ok(result.Data);
		}

	}
}
