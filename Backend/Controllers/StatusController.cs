using CandidateSoW.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CandidateSoW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private IConfiguration configuration;
        public StatusController(IConfiguration iConfig)
        {
            configuration = iConfig;
        }

        string msg = string.Empty;

        // GET: api/<StatusController>
        [HttpGet]
        public List<StatusModel> Get()
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            StatusModel st = new StatusModel();
            st.Type = "get";
            DataSet ds = dop.Statusget(st, msg);
            List<StatusModel> list = new List<StatusModel>();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        list.Add(new StatusModel
                        {
                            StatusId = Convert.ToInt32(dr["StatusId"]),
                            StatusName = dr["StatusName"].ToString(),
                        });
                    }
                }
            }
            return list;
        }

        // GET api/<StatusController>/5
        [HttpGet("{id}")]
        public List<StatusModel> Get(int id)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            StatusModel st = new StatusModel();
            st.Type = "get";
            st.StatusId = id;
            DataSet ds = dop.Statusget(st, msg);
            List<StatusModel> list = new List<StatusModel>();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        list.Add(new StatusModel
                        {
                            StatusId = Convert.ToInt32(dr["StatusId"]),
                            StatusName = dr["StatusName"].ToString(),
                        });
                    }
                }
            }
            return list;
        }

        // POST api/<StatusController>
        [HttpPost]
        public string Post([FromBody] StatusModel st)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;
            try
            {
                msg = dop.Statustable(st);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return JsonConvert.SerializeObject(msg);
        }

        // PUT api/<StatusController>/5
        [HttpPut("{id}")]
        public string Put(int id, [FromBody] StatusModel st)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;
            try
            {
                st.Type = "update";
                st.StatusId = id;
                msg = dop.Statustable(st);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return JsonConvert.SerializeObject(msg);
        }

        // DELETE api/<StatusController>/5
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;
            try
            {
                StatusModel st = new StatusModel();
                st.Type = "Delete";
                st.StatusId = id;
                msg = dop.Statustable(st);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return JsonConvert.SerializeObject(msg);
        }

        [HttpGet]
        [Route("GetStatusByType")]
        //[Route("GetStatusByType/{id}")]
        public List<StatusModel> GetStatusByType(int id)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            StatusModel st = new StatusModel();
            st.Type = "get";
            st.StatusType =Convert.ToString((Status)id);
            DataSet ds = dop.Statusget(st, msg);
            List<StatusModel> list = new List<StatusModel>();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        list.Add(new StatusModel
                        {
                            StatusId = Convert.ToInt32(dr["StatusId"]),
                            StatusName = dr["StatusName"].ToString(),
                        });
                    }
                }
            }
            return list;
        }
    }

    public enum Status
    {
        SO = 0,
        Candidate = 1
    }
}
