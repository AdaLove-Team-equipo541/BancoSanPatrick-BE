﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SanPatrick.Application.Dtos.Users;
using SanPatrick.Application.Features.Users.Commands.AuthenticateUser;
using SanPatrick.Application.Features.Users.Commands.RegisterUserCommand;

namespace SanPatrick.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseApiController
    {
        [HttpPost("authenticate")]
        public async Task<IActionResult>AuthenticateAsync(AuthenticationRequest request)
        {
            return Ok(await Mediator.Send(new AuthenticateUserCommand
            {
                Email = request.Email,
                Password = request.Password,
                IpAddress = GetIpAddress()
            }));
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegistrationRequest request)
        {
            return Ok(await Mediator.Send(new RegisterUserCommand
            {
                Email = request.Email,
                Username = request.Username,
                Password = request.Password,
                ConfirmPassword = request.ConfirmPassword,
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName,
                BirthDate = request.BirthDate,
                AvatarString = request.AvatarString,
                MaritalStatus = request.MaritalStatus,
                PhoneNumber = request.PhoneNumber,
                Contry = request.Contry,
                Citizenship = request.Citizenship,
                Occupation = request.Occupation,
                Address = request.Address,
                Origin = Request.Headers["origin"]
            }));
        }

        private string GetIpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                return Request.Headers["X-Forwarded-For"];
            }
            else
            {
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            }
        }
    }
}