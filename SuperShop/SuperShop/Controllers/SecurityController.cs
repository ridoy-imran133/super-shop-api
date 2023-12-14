using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using SuperShop.Helper;
using SuperShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SuperShop.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        [HttpGet]
        [Route("GetUserMenu/{userId}/{token}")]
        public async Task<IActionResult> GetUserMenu(string userId, string token)
        {
            SecurityAccess securityAccess = new SecurityAccess();
            //var accessToken = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var genurl = ApplicationConstant.SecurityAPI + "Config/getMenu?username=" + userId;

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                var httpResponse = await httpClient.GetAsync(genurl);
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    
                    //securityAccess = JsonConvert.DeserializeObject<SecurityAccess>(await httpResponse.Content.ReadAsStringAsync());
                    //List <Menu> menus = securityAccess.responseData;
                    dynamic sAccess = JsonConvert.DeserializeObject<Object>(await httpResponse.Content.ReadAsStringAsync());
                    Object sv = sAccess.menus;
                    securityAccess = JsonConvert.DeserializeObject<SecurityAccess>(sv.ToString());
                    //dynamic menus = menuacccess.responseData;
                    //if (menuacccess != null)
                    //{
                    //    return Ok(new
                    //    {
                    //        menu = menuacccess.menuInfoList
                    //    });
                    //}
                    return Ok(new
                    {
                        menus = securityAccess.responseData
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Invalid Token.");
            }
            return Ok();

        }

        [HttpGet]
        [Route("GetUserProfile/{userId}/{token}")]
        public async Task<IActionResult> GetUserProfile(string userId, string token)
        {
            UserProfile profile = new UserProfile();
            var genurl = ApplicationConstant.SecurityAPI + "User/UserProfile?pUserName=" + userId;

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                var httpResponse = await httpClient.GetAsync(genurl);
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    dynamic sAccess = JsonConvert.DeserializeObject<Object>(await httpResponse.Content.ReadAsStringAsync());
                    Object sv = sAccess.profile;
                    profile = JsonConvert.DeserializeObject<UserProfile>(sv.ToString());
                    return Ok(new
                    {
                        profile = profile
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Invalid Token.");
            }
            return Ok();

        }
    }
}
