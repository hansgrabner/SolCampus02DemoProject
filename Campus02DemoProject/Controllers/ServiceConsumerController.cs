using Campus02DemoProject.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Campus02DemoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceConsumerController : ControllerBase
    {

        //https://dummy.restapiexample.com/
        [HttpGet]
        public async Task<IActionResult> GetRestAsync()
        {
            Root myRoot = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://dummy.restapiexample.com/api/v1/employee/1"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    myRoot = JsonConvert.DeserializeObject<Root>(apiResponse);
                }
                return Ok(myRoot);
            }
        }
    }

    }
