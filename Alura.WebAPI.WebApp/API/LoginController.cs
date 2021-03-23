using Alura.ListaLeitura.Seguranca;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Alura.WebAPI.WebApp.API
{
    [Route("api/[controller)]")]
    public class LoginController : ControllerBase
    {
        private readonly SignInManager<Usuario> _signInManager;


        public LoginController(SignInManager<Usuario> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Token(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Login, model.Password, true, true);
                if (result.Succeeded)
                {
                    var direitos = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, model.Login),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };

                    var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("alura-webapi-authentication-valid"));

                    var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        issuer: "Alura.WebApp",
                        audience: "Postman",
                        claims: direitos,
                        signingCredentials: credenciais,
                        expires: DateTime.Now.AddMilliseconds(30)
                        );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                    return Ok(tokenString);
                }
                return Unauthorized();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
