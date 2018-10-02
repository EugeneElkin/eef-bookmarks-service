namespace BookmarksAPI.Controllers
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using BookmarksAPI.Exceptions;
    using BookmarksAPI.Models;
    using BookmarksAPI.Services.Interfaces;
    using DataWorkShop.Entities;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [ApiController]
    [Produces("application/json")]
    [Route("api/users")]
    public class UsersController : Controller
    {
        private readonly IUserService userService;
        private readonly ITokenService tokenService;

        public UsersController(IUserService userService, ITokenService tokenService)
        {
            this.userService = userService;
            this.tokenService = tokenService;
        }

        [AllowAnonymous]
        // POST: api/users
        [HttpPost]
        public async Task<IActionResult> Register([FromBody]NewUserViewModel newUser)
        {
            var user = Mapper.Map<User>(newUser);

            try
            {
                user.Id = Guid.NewGuid().ToString();
                await this.userService.CreateAsync(user, newUser.Password);
                return Ok();
            }
            catch (CustomException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        // POST: api/users/token
        [HttpPost("token")]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticatingUserViewModel authenticatingUser)
        {
            try
            {
                var dbUser = await this.userService.AuthenticateAsync(authenticatingUser.UserName, authenticatingUser.Password);
                var token = this.tokenService.CreateAccessToken(dbUser);

                return Ok(
                    new AuthenticatedUserViewModel
                    {
                        Id = dbUser.Id,
                        UserName = dbUser.UserName,
                        FirstName = dbUser.FirstName,
                        LastName = dbUser.LastName,
                        TokenInfo = new TokenViewModel {
                            AccessToken = token.AccessToken,
                            RefreshToken = token.RefreshToken
                        }
                    });
            }
            catch (CustomException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        // POST: api/users/newtokens
        [HttpPost("newtoken")]
        public async Task<IActionResult> RefreshTokens([FromBody]TokenRequestViewModel tokenRequest)
        {
            try
            {
                var tokenInfo = await Task.FromResult(this.tokenService.UpdateAccessToken(tokenRequest.UserId, tokenRequest.RefreshToken));
                return Ok(tokenInfo);
            }
            catch (CustomException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
