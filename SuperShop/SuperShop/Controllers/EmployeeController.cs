using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SuperShop.Helper;
using SuperShop.Models;
using SuperShop.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SuperShop.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public EmployeeController()
        {

        }

        [HttpGet]
        [Route("GetAll/{token}")]
        public async Task<IActionResult> GetEmployees(string token)
        {
            List<EmployeeRegistrationModel> employees = new List<EmployeeRegistrationModel>();
            //var accessToken = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var genurl = ApplicationConstant.SecurityAPI + "Employee/GetEmployees";

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                var httpResponse = await httpClient.GetAsync(genurl);
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    dynamic sAccess = JsonConvert.DeserializeObject<Object>(await httpResponse.Content.ReadAsStringAsync());
                    Object sv = sAccess.employees;
                    employees = JsonConvert.DeserializeObject<List<EmployeeRegistrationModel>>(sv.ToString());
                    return Ok(new
                    {
                        employees = employees
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Invalid Token.");
            }
            return Ok();

        }

        [HttpPost]
        [Route("Save")]
        public async Task<IActionResult> Save(EmployeeRegistrationModel employee)
        {
            ResponseModel response = new ResponseModel();
            var json = JsonConvert.SerializeObject(employee);
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", employee.Token);
            try
            {
                var httpResponse = await httpClient.PostAsync(ApplicationConstant.SecurityAPI + "Employee/Save", new StringContent(json, Encoding.UTF8, "application/json"));
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    response = JsonConvert.DeserializeObject<ResponseModel>(await httpResponse.Content.ReadAsStringAsync());
                    return Ok(new
                    {
                        response_code = response.responseCode,
                        response_message = response.responseMessage,
                        response_data = response?.responseData
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

        [HttpGet]
        [Route("GetRoles/{token}")]
        public async Task<IActionResult> GetRoles(string token)
        {
            List<RoleModel> roles = new List<RoleModel>();
            //var accessToken = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var genurl = ApplicationConstant.SecurityAPI + "Role/GetRoles";

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                var httpResponse = await httpClient.GetAsync(genurl);
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    dynamic sAccess = JsonConvert.DeserializeObject<Object>(await httpResponse.Content.ReadAsStringAsync());
                    Object sv = sAccess.roles;
                    roles = JsonConvert.DeserializeObject<List<RoleModel>>(sv.ToString());
                    return Ok(new
                    {
                        roles = roles
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
        [Route("GetMenus/{userId}/{token}")]
        public async Task<IActionResult> GetUserMenu(string userId, string token)
        {
            SecurityAccess securityAccess = new SecurityAccess();
            //var accessToken = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var genurl = ApplicationConstant.SecurityAPI + "Employee/getMenuForEmployee?username=" + userId;

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

    }
}
