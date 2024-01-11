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
        private readonly ILogger<CustomerController> _Logger;
        protected CustomerDal _Dal;

        public CustomerController(ILogger<CustomerController> logger, CustomerDal dal)
        {
            this._Logger = logger;
            this._Dal = dal;
        }

        /// <summary>
        /// 取得全部的顧客資料
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerModel>>> GetCustomer()
        {
            _Logger.LogInformation("CustomerController GetCustomer start");
            try
            {
                var customers = await _Dal.GetCustomer();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex.Message);
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
            _Logger.LogInformation("CustomerController GetCustomerByID start");
            try
            {
                var customer = await _Dal.GetCustomerByID(p_sQryId);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 新增顧客
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CustomerModel>> AddCustomer(CustomerModel p_oAdd)
        {
            _Logger.LogInformation("CustomerController AddCustomer start");
            try
            {
                var addItemID = p_oAdd.CustomerID;
                // 確認有無重複
                var queryItem = _Dal.GetCustomerByID(addItemID);
                if (queryItem != null)
                {
                    var result = await _Dal.AddCustomer(p_oAdd);
                    return CreatedAtAction(nameof(GetCustomerByID), new { p_sQryId = p_oAdd.CustomerID }, p_oAdd);
                } 
                else
                { return BadRequest("Data has been create"); }
                
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 修改顧客
        /// </summary>
        /// <param name="p_sQryId"></param>
        /// <param name="p_oUpd"></param>
        /// <returns></returns>
        [HttpPut("{p_sQryId}")]
        public async Task<ActionResult<CustomerModel>> UpdateCustomer(string p_sQryId, CustomerModel p_oUpd)
        {
            _Logger.LogInformation("CustomerController UpdateCustomer start");
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
                _Logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 刪除顧客
        /// </summary>
        /// <param name="p_sQryId"></param>
        /// <returns></returns>
        [HttpDelete("{p_sQryId}")]
        public async Task<ActionResult<CustomerModel>> DeleteCustomer(string p_sQryId)
        {
            _Logger.LogInformation("CustomerController DeleteCustomer start");
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
                _Logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

    }
}
