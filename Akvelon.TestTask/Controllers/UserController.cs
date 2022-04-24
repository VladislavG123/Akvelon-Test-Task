using Akvelon.TestTask.Contracts.ViewModels;
using Akvelon.TestTask.LogicLevel.Abstract;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace Akvelon.TestTask.Controllers;

[Route("/api/user")]
public class UserController : ControllerBase
{
    private readonly IUserBllService _userBllService;
    private readonly IMapper _mapper;

    public UserController(IUserBllService userBllService, IMapper mapper)
    {
        _userBllService = userBllService;
        _mapper = mapper;
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] UserAuthenticationViewModel parameter)
    {
        try
        {
            var token = await _userBllService.SignUp(parameter.Login, parameter.Password);
            return Ok(token);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] UserAuthenticationViewModel parameter)
    {
        try
        {
            var token = await _userBllService.SignIn(parameter.Login, parameter.Password);
            return Ok(token);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserDataByToken()
    {
        try
        {
            var data = await _userBllService
                .GetUserByToken(Request.Headers[HeaderNames.Authorization]
                    .ToArray()[0].Replace("Bearer ", ""));

            return Ok(_mapper.Map<UserViewModel>(data));
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }
}