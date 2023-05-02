using CandidateSoW.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CandidateSoW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private IConfiguration configuration;
        public RegionController(IConfiguration iConfig)
        {
            configuration = iConfig;
        }

        string msg = string.Empty;
        // GET: api/<RegionController>
        [HttpGet]
        public List<RegionModel> Get()
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            RegionModel rs = new RegionModel();
            rs.Type = "get";
            DataSet ds = dop.Regiontableget(rs, msg);
            List<RegionModel> list = new List<RegionModel>();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        list.Add(new RegionModel
                        {
                            RegionId = Convert.ToInt32(dr["RegionId"]),
                            Region = dr["Region"].ToString(),
                        });
                    }
                }
            }
            return list;
        }

        // GET api/<RegionController>/5
        [HttpGet("{id}")]
        public List<RegionModel> Get(int id)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            RegionModel rs = new RegionModel();
            rs.Type = "getid";
            rs.RegionId = id;
            DataSet ds = dop.Regiontableget(rs, msg);
            List<RegionModel> list = new List<RegionModel>();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        list.Add(new RegionModel
                        {
                            RegionId = Convert.ToInt32(dr["RegionId"]),
                            Region = dr["Region"].ToString(),
                        });
                    }
                }
            }
            return list;
        }

        // POST api/<RegionController>
        [HttpPost]
        public String Post([FromBody] RegionModel rs)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;
            try
            {
                msg = dop.Regiontable(rs);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            //return msg;
            return JsonConvert.SerializeObject(msg);
        }

        // PUT api/<RegionController>/5
        [HttpPut("{id}")]
        public String Put(int id, [FromBody] RegionModel rs)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;
            try
            {
                rs.Type = "update";
                rs.RegionId = id;
                msg = dop.Regiontable(rs);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            //return msg;
            return JsonConvert.SerializeObject(msg);
        }

        // DELETE api/<RegionController>/5
        [HttpDelete("{id}")]
        public String Delete(int id)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;
            try
            {
                RegionModel rs = new RegionModel();
                rs.Type = "Delete";
                rs.RegionId = id;
                msg = dop.Regiontable(rs);
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
