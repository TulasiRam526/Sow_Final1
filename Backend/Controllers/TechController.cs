using CandidateSoW.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CandidateSoW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechController : ControllerBase
    {
        private IConfiguration configuration;
        public TechController(IConfiguration iConfig)
        {
            configuration = iConfig;
        }

        string msg = string.Empty;

        // GET: api/<TechController>
        [HttpGet]
        public List<TechnologyModel> Get()
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            TechnologyModel ts = new TechnologyModel();
            ts.Type = "get";
            DataSet ds = dop.techtableget(ts, msg);
            List<TechnologyModel> list = new List<TechnologyModel>();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        list.Add(new TechnologyModel
                        {
                            TechnologyId = Convert.ToInt32(dr["TechnologyId"]),
                            TechnologyName = dr["TechnologyName"].ToString(),
                            DomainId = Convert.ToInt32(dr["DomainId"]),
                            DomainName = dr["DomainName"].ToString(),
                        });
                    }
                }
            }
            return list;
        }

        // GET api/<TechController>/5
        [HttpGet("{id}")]
        public List<TechnologyModel> Get(int id)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            TechnologyModel ts = new TechnologyModel();
            ts.Type = "getid";
            ts.TechnologyId = id;
            DataSet ds = dop.techtableget(ts, msg);
            List<TechnologyModel> list = new List<TechnologyModel>();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        list.Add(new TechnologyModel
                        {
                            TechnologyId = Convert.ToInt32(dr["TechnologyId"]),
                            TechnologyName = dr["TechnologyName"].ToString(),
                            DomainId = Convert.ToInt32(dr["DomainId"]),
                        });
                    }
                }
            }
            return list;
        }

        // POST api/<TechController>
        [HttpPost]
        public String Post([FromBody] TechnologyModel ts)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;
            try
            {
                msg = dop.Techtable(ts);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            //return msg;
            return JsonConvert.SerializeObject(msg);
        }

        // PUT api/<TechController>/5
        [HttpPut("{id}")]
        public String Put(int id, [FromBody] TechnologyModel ts)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;
            try
            {
                ts.Type = "update";
                ts.TechnologyId = id;
                msg = dop.Techtable(ts);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            //return msg;
            return JsonConvert.SerializeObject(msg);
        }

        // DELETE api/<TechController>/5
        [HttpDelete("{id}")]
        public String Delete(int id)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;
            try
            {
                TechnologyModel ts = new TechnologyModel();
                ts.Type = "Delete";
                ts.TechnologyId = id;
                msg = dop.Techtable(ts);
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
