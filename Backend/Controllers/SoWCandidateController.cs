using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using CandidateSoW.Models;
using System;
using System.Collections.Generic;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CandidateSoW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoWCandidateController : ControllerBase
    {
        private IConfiguration configuration;
        public SoWCandidateController(IConfiguration iConfig)
        {
            configuration = iConfig;
        }
        string msg = string.Empty;
        [HttpGet]
        public List<SoWCandidateModel> Get()
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            SoWCandidateModel sc = new SoWCandidateModel();
            sc.Type = "get";
            DataSet ds = dop.SowcandidatetableGet(sc, msg);
            List<SoWCandidateModel> list = new List<SoWCandidateModel>();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        list.Add(new SoWCandidateModel
                        {
                            SOW_CandidateId = Convert.ToInt32(dr["SOW_CandidateId"]),
                            SowId = Convert.ToInt32(dr["SowId"]),
                            CandidateId = Convert.ToInt32(dr["CandidateId"]),
                            StatusId = Convert.ToInt32(dr["StatusId"]),
                            SoName = Convert.ToString(dr["SoName"]),
                            CandidateName = Convert.ToString(dr["CandidateName"]),
                            StatusName = Convert.ToString(dr["StatusName"]),
                        });
                    }
                }
            }
            return list;
        }

        // GET api/<SoWCandidateController>/5
        [HttpGet("{id}")]
        public List<SoWCandidateModel> Get(int id)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            SoWCandidateModel sc = new SoWCandidateModel();
            sc.Type = "getid";
            sc.SOW_CandidateId = id;
            DataSet ds = dop.SowcandidatetableGet(sc, msg);
            List<SoWCandidateModel> list = new List<SoWCandidateModel>();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        list.Add(new SoWCandidateModel
                        {
                            SOW_CandidateId = Convert.ToInt32(dr["SOW_CandidateId"]),
                            SowId = Convert.ToInt32(dr["SowId"]),
                            CandidateId = Convert.ToInt32(dr["CandidateId"]),
                            StatusId = Convert.ToInt32(dr["StatusId"]),
                            SoName = Convert.ToString(dr["SoName"]),
                            CandidateName = Convert.ToString(dr["CandidateName"]),
                            StatusName = Convert.ToString(dr["StatusName"]),
                        });
                    }
                }
            }
            return list;
        }

        // POST api/<SoWCandidateController>
        [HttpPost]
        public string Post([FromBody] SoWCandidateModel sc)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;
            try
            {
                //sc.StatusId = 1;
                msg = dop.sowcandidatetable(sc);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            //return msg;
            return JsonConvert.SerializeObject(msg);
        }

        // PUT api/<SoWCandidateController>/5
        [HttpPut("{id}")]
        public string Put(int id, [FromBody] SoWCandidateModel sc)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;
            try
            {
                sc.Type = "update";
                sc.SOW_CandidateId = id;
                msg = dop.sowcandidatetable(sc);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            //return msg;
            return JsonConvert.SerializeObject(msg);
        }

        // DELETE api/<SoWCandidateController>/5
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;
            try
            {
                SoWCandidateModel sc = new SoWCandidateModel();
                sc.Type = "Delete";
                sc.SOW_CandidateId = id;
                msg = dop.sowcandidatetable(sc);
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
