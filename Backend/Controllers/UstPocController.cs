using CandidateSoW.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CandidateSoW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UstPocController : ControllerBase
    {
        private IConfiguration configuration;
        public UstPocController(IConfiguration iConfig)
        {
            configuration = iConfig;
        }

        string msg = string.Empty;
        // GET: api/<UstPocController>
        [HttpGet]
        public List<USTPOCModel> Get()
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            USTPOCModel us = new USTPOCModel();
            us.Type = "get";
            DataSet ds = dop.ustpocget(us, msg);
            List<USTPOCModel> list = new List<USTPOCModel>();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        list.Add(new USTPOCModel
                        {
                            USTPOCId = Convert.ToInt32(dr["USTPOCId"]),
                            USTPOCName = dr["USTPOCName"].ToString(),
                        });
                    }
                }
            }
            return list; ;
        }

        // GET api/<UstPocController>/5
        [HttpGet("{id}")]
        public List<USTPOCModel> Get(int id)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            USTPOCModel us = new USTPOCModel();
            us.Type = "getid";
            us.USTPOCId = id;
            DataSet ds = dop.ustpocget(us, msg);
            List<USTPOCModel> list = new List<USTPOCModel>();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        list.Add(new USTPOCModel
                        {
                            USTPOCId = Convert.ToInt32(dr["USTPOCId"]),
                            USTPOCName = dr["USTPOCName"].ToString(),
                        });
                    }
                }
            }
            return list;
        }

        // POST api/<UstPocController>
        [HttpPost]
        public String Post([FromBody] USTPOCModel us)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;
            try
            {
                msg = dop.ustpoctable(us);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            //return msg;
            return JsonConvert.SerializeObject(msg);
        }

        // PUT api/<UstPocController>/5
        [HttpPut("{id}")]
        public String Put(int id, [FromBody] USTPOCModel us)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;
            try
            {
                us.Type = "update";
                us.USTPOCId = id;
                msg = dop.ustpoctable(us);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            //return msg;
            return JsonConvert.SerializeObject(msg);
        }

        // DELETE api/<UstPocController>/5
        [HttpDelete("{id}")]
        public String Delete(int id)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;
            try
            {
                USTPOCModel us = new USTPOCModel();
                us.Type = "Delete";
                us.USTPOCId = id;
                msg = dop.ustpoctable(us);
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
