using CandidateSoW.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CandidateSoW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class USTTPMController : ControllerBase
    {
        private IConfiguration configuration;
        public USTTPMController(IConfiguration iConfig)
        {
            configuration = iConfig;
        }

        string msg = string.Empty;

        // GET: api/<USTTPMController>
        [HttpGet]
        public List<USTTPMModel> Get()
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            USTTPMModel tpm = new USTTPMModel();
            tpm.Type = "get";
            DataSet ds = dop.usttpmget(tpm, msg);
            List<USTTPMModel> list = new List<USTTPMModel>();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        list.Add(new USTTPMModel
                        {
                            USTTPMId = Convert.ToInt32(dr["USTTPMId"]),
                            USTTPMName = dr["USTTPMName"].ToString(),
                        });
                    }
                }
            }
            return list;
        }

        // GET api/<USTTPMController>/5
        [HttpGet("{id}")]
        public List<USTTPMModel> Get(int id)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            USTTPMModel tpm = new USTTPMModel();
            tpm.Type = "getid";
            tpm.USTTPMId = id;
            DataSet ds = dop.usttpmget(tpm, msg);
            List<USTTPMModel> list = new List<USTTPMModel>();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        list.Add(new USTTPMModel
                        {
                            USTTPMId = Convert.ToInt32(dr["USTTPMId"]),
                            USTTPMName = dr["USTTPMName"].ToString(),
                        });
                    }
                }
            }
            return list;
        }

        // POST api/<USTTPMController>
        [HttpPost]
        public String Post([FromBody] USTTPMModel tpm)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);

            string msg = string.Empty;
            try
            {
                msg = dop.USTTPMtable(tpm);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            //return msg;
            return JsonConvert.SerializeObject(msg);
        }

        // PUT api/<USTTPMController>/5
        [HttpPut("{id}")]
        public String Put(int id, [FromBody] USTTPMModel tpm)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;
            try
            {
                tpm.Type = "update";
                tpm.USTTPMId = id;
                msg = dop.USTTPMtable(tpm);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            //return msg;
            return JsonConvert.SerializeObject(msg);
        }

        // DELETE api/<USTTPMController>/5
        [HttpDelete("{id}")]
        public String Delete(int id)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;
            try
            {
                USTTPMModel tpm = new USTTPMModel();
                tpm.Type = "Delete";
                tpm.USTTPMId = id;
                msg = dop.USTTPMtable(tpm);
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
