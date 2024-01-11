using Microsoft.Data.SqlClient;
using System.Data;

namespace WebMvcNorthWind.Models
{
    public class BaseDal : IDisposable
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        protected SqlConnection _cn;


        public BaseDal (IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("Base_DB_ConnectString");
            try
            {
                if (_cn == null)
                {
                    _cn = new SqlConnection(_connectionString);
                }
                if (_cn.State == ConnectionState.Closed)
                {
                    _cn.Open();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void Dispose()
        {
            try
            {
                if (_cn != null)
                {
                    _cn.Close();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
