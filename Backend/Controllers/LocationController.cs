using CandidateSoW.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CandidateSoW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private IConfiguration configuration;
        public LocationController(IConfiguration iConfig)
        {
            configuration = iConfig;
        }

        string msg = string.Empty;
        // GET: api/<LocationController>
        [HttpGet]
        public List<LocationModel> Get()
        {

            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            LocationModel ls = new LocationModel();
            ls.Type = "get";
            DataSet ds = dop.locationtableget(ls, msg);
            List<LocationModel> list = new List<LocationModel>();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        list.Add(new LocationModel
                        {
                            LocationId = Convert.ToInt32(dr["LocationId"]),
                            Location = dr["Location"].ToString(),
                            RegionId = Convert.ToInt32(dr["RegionId"]),
                        });
                    }
                }
            }
            return list;
        }

        // GET api/<LocationController>/5
        [HttpGet("{id}")]
        public List<LocationModel> Get(int id)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            LocationModel ls = new LocationModel();
            ls.Type = "getid";
            ls.LocationId = id;
            DataSet ds = dop.locationtableget(ls, msg);
            List<LocationModel> list = new List<LocationModel>();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        list.Add(new LocationModel
                        {
                            LocationId = Convert.ToInt32(dr["LocationId"]),
                            Location = dr["Location"].ToString(),
                            RegionId = Convert.ToInt32(dr["RegionId"]),
                        });
                    }
                }
            }
            return list;
        }

        // POST api/<LocationController>
        [HttpPost]
        public String Post([FromBody] LocationModel ls)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;
            try
            {
                msg = dop.locationtable(ls);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            //return msg;
            return JsonConvert.SerializeObject(msg);
        }

        // PUT api/<LocationController>/5
        [HttpPut("{id}")]
        public String Put(int id, [FromBody] LocationModel ls)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;
            try
            {
                ls.Type = "update";
                ls.LocationId = id;
                msg = dop.locationtable(ls);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            //return msg;
            return JsonConvert.SerializeObject(msg);
        }

        // DELETE api/<LocationController>/5
        [HttpDelete("{id}")]
        public String Delete(int id)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;
            try
            {
                LocationModel ls = new LocationModel();
                ls.Type = "Delete";
                ls.LocationId = id;
                msg = dop.locationtable(ls);
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
