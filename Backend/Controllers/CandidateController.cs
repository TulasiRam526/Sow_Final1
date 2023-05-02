using CandidateSoW.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Runtime.Intrinsics.Arm;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CandidateSoW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private IConfiguration configuration;
        public CandidateController(IConfiguration iConfig)
        {
            configuration = iConfig;
        }
      
        // GET: api/<CandidateController>
        [HttpGet]
        public List<CandidateModel> Get()
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            //return new string[] { "value1", "value2" };
            return dop.GetAllCandidateData();
        }

        // GET api/<CandidateController>/5
        [HttpGet("{id}")]
        public List<CandidateModel> Get(int id)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            //return dop.GetAllCandidateData().Where(e=>e.CandidateId == id).ToList();
            return dop.GetCandidateById(id);
        }

        // POST api/<CandidateController>
        [HttpPost]
        public void Post([FromBody] CandidateModel candidate)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;
            try
            {
                msg = dop.candidateTable(candidate);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
        }

        // PUT api/<CandidateController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] CandidateModel candidate)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg= string.Empty;
            try
            {
                msg= dop.updateCandidateTable(id,candidate);
            }
            catch(Exception ex)
            {
                msg=ex.Message;
            }
        }

        // DELETE api/<CandidateController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;
            try
            {
                msg = dop.deleteCandidateData(id);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
        }
        [HttpGet]
        [Route("Getdate")]
        public List<CandidateModel> Getdate(DateTime StartDate, DateTime EndDate)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            //return dop.GetAllCandidateData().Where(e=>e.CandidateId == id).ToList();
            return dop.GetCandidateDate(StartDate, EndDate);
        }
        [HttpPost]
        [Route("ImportData")]
        public string ImportCandidateData([FromBody] CandidateData json_data)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;
            try
            {
                string data = JsonConvert.SerializeObject(json_data.candidates);
                return msg = dop.imp_candidateData(data);
            }
            catch (Exception ex)
            {
                return msg = ex.Message;
            }

        }
    }
}
