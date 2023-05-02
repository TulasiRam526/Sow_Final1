using CandidateSoW.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CandidateSoW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DellManagerController : ControllerBase
    {
        private IConfiguration configuration;
        public DellManagerController(IConfiguration iConfig)
        {
            configuration = iConfig;
        }

        string msg = string.Empty;

        // GET: api/<DellManagerController>
        [HttpGet]
        public List<DellManagerModel> Get()
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            DellManagerModel dl = new DellManagerModel();
            dl.Type = "get";
            DataSet ds = dop.dellmanagerget(dl, msg);
            List<DellManagerModel> list = new List<DellManagerModel>();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        list.Add(new DellManagerModel
                        {
                            DellManagerId = Convert.ToInt32(dr["DellManagerId"]),
                            DellManagerName = dr["DellManagerName"].ToString(),
                        });
                    }
                }
            }
            return list;
        }

        // GET api/<DellManagerController>/5
        [HttpGet("{id}")]
        public List<DellManagerModel> Get(int id)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            DellManagerModel dl = new DellManagerModel();
            dl.Type = "getid";

            DataSet ds = dop.dellmanagerget(dl, msg);
            List<DellManagerModel> list = new List<DellManagerModel>();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        list.Add(new DellManagerModel
                        {
                            DellManagerId = Convert.ToInt32(dr["DellManagerId"]),
                            DellManagerName = dr["DellManagerName"].ToString(),


                        });
                    }
                }
            }
            return list;
        }

        // POST api/<DellManagerController>
        [HttpPost]
        public String Post([FromBody] DellManagerModel dl)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;
            try
            {
                msg = dop.dellmanagertable(dl);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            //return msg;
            return JsonConvert.SerializeObject(msg);
        }

        // PUT api/<DellManagerController>/5
        [HttpPut("{id}")]
        public String Put(int id, [FromBody] DellManagerModel dl)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;
            try
            {
                dl.Type = "update";
                dl.DellManagerId = id;
                msg = dop.dellmanagertable(dl);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            //return msg;
            return JsonConvert.SerializeObject(msg);
        }

        // DELETE api/<DellManagerController>/5
        [HttpDelete("{id}")]
        public String Delete(int id)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;
            try
            {
                DellManagerModel dl = new DellManagerModel();
                dl.Type = "Delete";
                dl.DellManagerId = id;
                msg = dop.dellmanagertable(dl);
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
