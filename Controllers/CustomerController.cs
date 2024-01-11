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
        /// 取得全部的顧客資料
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerModel>>> GetCustomer()
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
        /// 取得單筆資料
        /// </summary>
        /// <param name="p_sQryId"></param>
        /// <returns></returns>
        [HttpGet("{p_sQryId}")]
        public async Task<ActionResult<CustomerModel>> GetCustomerByID (string p_sQryId)
        {
            try
            {
                var customer = await _Dal.GetCustomerByID(p_sQryId);
                return Ok(customer);
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
        public async Task<ActionResult<CustomerModel>> AddCustomer(CustomerModel p_oAdd)
        {
            try
            {
                var result = await _Dal.AddCustomer(p_oAdd);
                return CreatedAtAction(nameof(GetCustomerByID), new { p_sQryId = p_oAdd.CustomerID}, p_oAdd);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{p_sQryId}")]
        public async Task<ActionResult<CustomerModel>> UpdateCustom(string p_sQryId, CustomerModel p_oUpd)
        {
            try
            {
                var updItem = await _Dal.GetCustomerByID(p_sQryId);
                if (updItem == null)
                {
                    return NotFound();
                }
                var result = await _Dal.UpdateCustomer(p_oUpd);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{p_sQryId}")]
        public async Task<ActionResult<CustomerModel>> DeleteCustom(string p_sQryId)
        {
            try
            {
                var deleteItem = await _Dal.GetCustomerByID(p_sQryId);
                if (deleteItem == null)
                {
                    return NotFound();
                }
                var result = await _Dal.DeleteCustomer(p_sQryId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
