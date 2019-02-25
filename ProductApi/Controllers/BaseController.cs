using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ProductApi.Controllers
{
    public abstract class BaseController : Controller
    {
        public string ConnectionString => this.Configuration.GetConnectionString("Default");

        public IDbConnection DbConnection => new SqlConnection(this.ConnectionString);

        protected BaseController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
    }
}


