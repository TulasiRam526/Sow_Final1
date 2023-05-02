using CandidateSoW.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CandidateSoW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecruiterController : ControllerBase
    {
        private IConfiguration configuration;
        public RecruiterController(IConfiguration iConfig)
        {
            configuration = iConfig;
        }

        string msg = string.Empty;
        // GET: api/<RecruiterController>
        [HttpGet]
        public List<RecruiterModel> Get()
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            RecruiterModel rc = new RecruiterModel();
            rc.Type = "get";
            DataSet ds = dop.recruiterget(rc, msg);
            List<RecruiterModel> list = new List<RecruiterModel>();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        list.Add(new RecruiterModel
                        {
                            RecruiterId = Convert.ToInt32(dr["RecruiterId"]),
                            RecruiterName = dr["RecruiterName"].ToString(),
                        });
                    }
                }
            }
            return list;
        }

        // GET api/<RecruiterController>/5
        [HttpGet("{id}")]
        public List<RecruiterModel> Get(int id)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            RecruiterModel rc = new RecruiterModel();
            rc.Type = "getid";
            rc.RecruiterId = id;
            DataSet ds = dop.recruiterget(rc, msg);
            List<RecruiterModel> list = new List<RecruiterModel>();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        list.Add(new RecruiterModel
                        {
                            RecruiterId = Convert.ToInt32(dr["RecruiterId"]),
                            RecruiterName = dr["RecruiterName"].ToString(),
                        });
                    }
                }
            }
            return list;
        }

        // POST api/<RecruiterController>
        [HttpPost]
        public String Post([FromBody] RecruiterModel rc)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;
            try
            {
                msg = dop.recruiter(rc);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            //return msg;
            return JsonConvert.SerializeObject(msg);
        }

        // PUT api/<RecruiterController>/5
        [HttpPut("{id}")]
        public String Put(int id, [FromBody] RecruiterModel rc)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;
            try
            {
                rc.Type = "update";
                rc.RecruiterId = id;
                msg = dop.recruiter(rc);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            //return msg;
            return JsonConvert.SerializeObject(msg);
        }

        // DELETE api/<RecruiterController>/5
        [HttpDelete("{id}")]
        public String Delete(int id)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;
            try
            {
                RecruiterModel rc = new RecruiterModel();
                rc.Type = "Delete";
                rc.RecruiterId = id;
                msg = dop.recruiter(rc);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            //return msg;
            return JsonConvert.SerializeObject(msg);
        }
    }
}
