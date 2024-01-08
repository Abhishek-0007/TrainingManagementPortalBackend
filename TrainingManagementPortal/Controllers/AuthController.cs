using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using TrainingManagementPortal.Models.RequestViewModels;
using TrainingManagementPortal.Models.ResponseViewModels;
using TrainingManagementPortal.Services;

namespace TrainingManagementPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IHttpService _httpService;
        private readonly IConfiguration _config;

        public AuthController(IServiceProvider service)
        {
            _httpService = service.GetRequiredService<IHttpService>();
            _config = service.GetRequiredService<IConfiguration>();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel vm)
        { 
            var response = new ApiResponseModel();
            try
            { 
                HttpResponseMessage httpResponse = await _httpService.SendAsync(new HttpCallRequestModel
                {
                    Url = _config["APIs:Identity:BaseURL"] + _config["APIs:Identity:Login"],
                    Method = HttpMethod.Post.Method,
                    Body = vm
                });
                JToken jsonResponse = JToken.Parse(await httpResponse.Content.ReadAsStringAsync());
                if (jsonResponse != null && jsonResponse.SelectToken(".code").ToString().Equals("200"))
                    response.Body = JObject.Parse(jsonResponse.SelectToken(".body").ToString())?["jwtToken"].ToString();
                else
                    throw new Exception(jsonResponse.ToString());

            }
            catch(Exception ex)
            {
                response.Code = "500";
                response.Message = ex.Message;  
            }

            return Ok(response);
        }
    }
}
