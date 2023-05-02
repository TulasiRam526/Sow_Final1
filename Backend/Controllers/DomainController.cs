using CandidateSoW.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CandidateSoW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DomainController : ControllerBase
    {
        private IConfiguration configuration;
        public DomainController(IConfiguration iConfig)
        {
            configuration = iConfig;
        }

        string msg = string.Empty;
        // GET: api/<DomainController>
        [HttpGet]
        public List<DomainModel> Get()
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            DomainModel dm = new DomainModel();
            dm.Type = "get";
            DataSet ds = dop.Domaintableget(dm, msg);
            List<DomainModel> list = new List<DomainModel>();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        list.Add(new DomainModel
                        {
                            DomainId = Convert.ToInt32(dr["DomainId"]),
                            DomainName = dr["DomainName"].ToString(),
                        });
                    }
                }
            }
            return list;
        }

        // GET api/<DomainController>/5
        [HttpGet("{id}")]
        public List<DomainModel> Get(int id)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            DomainModel dm = new DomainModel();
            dm.Type = "getid";
            dm.DomainId = id;
            DataSet ds = dop.Domaintableget(dm, msg);
            List<DomainModel> list = new List<DomainModel>();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        list.Add(new DomainModel
                        {
                            DomainId = Convert.ToInt32(dr["DomainId"]),
                            DomainName = dr["DomainName"].ToString(),
                        });
                    }
                }
            }
            return list;
        }

        // POST api/<DomainController>
        [HttpPost]
        public String Post([FromBody] DomainModel dm)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;
            try
            {
                msg = dop.Domaintable(dm);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            //return msg;
            return JsonConvert.SerializeObject(msg);
        }

        // PUT api/<DomainController>/5
        [HttpPut("{id}")]
        public String Put(int id, [FromBody] DomainModel dm)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;
            try
            {
                dm.Type = "update";
                dm.DomainId = id;
                msg = dop.Domaintable(dm);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            //return msg;
            return JsonConvert.SerializeObject(msg);
        }

        // DELETE api/<DomainController>/5
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;
            try
            {
                DomainModel dm = new DomainModel();
                dm.Type = "Delete";
                dm.DomainId = id;
                msg = dop.Domaintable(dm);
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
