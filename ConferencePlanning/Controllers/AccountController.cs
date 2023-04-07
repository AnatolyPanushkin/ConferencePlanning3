using ConferencePlanning.Data.Entities;
using ConferencePlanning.DTO;
using ConferencePlanning.IdentityServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanning.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class AccountController:ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly TokenService _tokenService;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        //_tokenService = tokenService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        
        var user = await _userManager.FindByEmailAsync(loginDto.Email);

        if (user == null)
        {
            return Unauthorized();
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

        //var token = _tokenService.CreateToken(user);
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        
        await _userManager.ConfirmEmailAsync(user, token);
        await _signInManager.SignInAsync(user,isPersistent:false);
        
        
        if (result.Succeeded)
        {
            return new UserDto
            {
                DisplayName = user.DisplayName,
                Token = token,
                UserName = user.UserName
            };
        }
        
        
        return Unauthorized();
    }


    [HttpPost("registration")]
    public async Task<ActionResult<UserDto>> Registration(RegisterDto registerDto)
    {
        if (await _userManager.Users.AnyAsync(user => user.UserName == registerDto.UserName))
        {
            return BadRequest("UserName is taken");
        }
        
        if (await _userManager.Users.AnyAsync(user => user.Email == registerDto.Email))
        {
            return BadRequest("Email is taken");
        }

        var user = new User
        {
            DisplayName = registerDto.DisplayName,
            UserName = registerDto.UserName,
            Email = registerDto.Email,
            Bio = registerDto.UserName
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        if (result.Succeeded)
        {
            return new UserDto
            {
                DisplayName = user.DisplayName,
                Token = token,
                UserName = user.UserName
            };
        }

        return BadRequest("Error signup");
    }
    
}