using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SuperShop.Helper;
using SuperShop.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SuperShop.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        [Route("UserLogin")]
        public async Task<IActionResult> UserLogin(LoginModel loginModel)
        {
            ResponseModel response = new ResponseModel();
            var json = JsonConvert.SerializeObject(loginModel);
            var httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            try
            {
                var httpResponse = await httpClient.PostAsync(ApplicationConstant.SecurityAPI + "Auth/UserLogin", new StringContent(json, Encoding.UTF8, "application/json"));
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    dynamic logindata = new ExpandoObject();
                    response = JsonConvert.DeserializeObject<ResponseModel>(await httpResponse.Content.ReadAsStringAsync());
                    if(response.responseData.token != null)
                    {
                        var claims = new[]
                        {
                            new Claim(ClaimTypes.NameIdentifier, response.responseData.userName)
                            //new Claim(ClaimTypes.Name, pUserName)
                        };
                        string checkVal = _config.GetSection("AppSettings:Token").Value;
                        byte[] val = Encoding.UTF8.GetBytes(checkVal);
                        var key = new SymmetricSecurityKey(val);
                        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(claims),
                            Expires = DateTime.Now.AddDays(1),
                            SigningCredentials = credentials
                        };

                        var tokenHandler = new JwtSecurityTokenHandler();
                        var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
                        logindata.a_token = response.responseData.token;
                        logindata.a_expireIn = response.responseData.token;
                        logindata.s_token = token;
                        logindata.s_expireIn = DateTime.Now.AddDays(1);
                        logindata.userName = response.responseData.userName;
                    }
                    return Ok(new
                    {
                        response_code = response.responseCode,
                        response_message = response.responseMessage,
                        response_data = logindata
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Invalid Token.");
            }
            return BadRequest("Invalid Token.");
            //return Ok();
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegistrationModel registration)
        {
            ResponseModel response = new ResponseModel();
            var json = JsonConvert.SerializeObject(registration);
            var httpClient = new HttpClient();
            try
            {
                var httpResponse = await httpClient.PostAsync("https://localhost:44392/Auth/Register", new StringContent(json, Encoding.UTF8, "application/json"));
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    response = JsonConvert.DeserializeObject<ResponseModel>(await httpResponse.Content.ReadAsStringAsync());
                    return Ok(new
                    {
                        response_code = response.responseCode,
                        response_message = response.responseMessage,
                        response_data = response.responseData
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Invalid Token.");
            }
            return BadRequest("Invalid Token.");
            return Ok();
        }
    }
}
