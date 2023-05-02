using CandidateSoW.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CandidateSoW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChangePasswordController : ControllerBase
    {
        private IConfiguration configuration;
        public ChangePasswordController(IConfiguration iConfig)
        {
            configuration = iConfig;
        }

        [HttpPut()]
        public String Put([FromBody] ChangePasswordModel cp)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;
            try
            {
                msg = dop.changePassword(cp);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return JsonConvert.SerializeObject(msg);
        }
    }
}
