using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebMvcNorthWind.Models;
using WebMvcNorthWind.Models.Dals;

namespace WebMvcNorthWind.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        protected CustomerDal _Dal;

        public CustomerController(CustomerDal dal)
        {
            _Dal = dal;
        }

        /// <summary>
        /// Get
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerModel>>> getTest()
        {
            try
            {
                var customers = await _Dal.GetCustomer();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        /// <summary>
        /// Post
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CustomerModel>> PostTest(CustomerModel ts)
        {
            return Ok("POST success");
        }
    }
}
