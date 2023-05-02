using CandidateSoW.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://urldefense.com/v3/__https://go.microsoft.com/fwlink/?LinkID=397860__;!!LpKI!g4v4VJTRThuo2_KoXMfcGo8tEjp6x0XE-l9bCD0ak8sctmjmdwbY0A0KMPwSHCakAEKzP2NCdhL0ICYm9OMUCA1FwByAWCZL2A$  [go[.]microsoft[.]com]

namespace SowController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SowController : ControllerBase
    {
        private IConfiguration configuration;
        public SowController(IConfiguration iConfig)
        {
            configuration = iConfig;
        }
        string msg = string.Empty;

        [HttpGet]
        public List<Sow> Get()
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            Sow sp = new Sow();
            sp.Type = "get";
            DataSet ds = dop.sowtableget(sp, msg);
            List<Sow> list = new List<Sow>();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        list.Add(new Sow
                        {
                            SowId = Convert.ToInt32(dr["SowId"]),
                            SoName = dr["SoName"].ToString(),
                            JRCode = dr["JRCode"].ToString(),
                            RequestCreationDate = ((DateTime)dr["RequestCreationDate"]),
                            AccountId = Convert.ToInt32(dr["AccountId"]),
                            TechnologyId = Convert.ToInt32(dr["TechnologyId"]),
                            Role = dr["Role"].ToString(),
                            LocationId = Convert.ToInt32(dr["LocationId"]),
                            RegionId = Convert.ToInt32(dr["RegionId"]),
                            TargetOpenPositions = Convert.ToInt32(dr["TargetOpenPositions"]),
                            PositionsTobeClosed = Convert.ToInt32(dr["PositionsTobeClosed"]),
                            USTPOCId = Convert.ToInt32(dr["USTPOCId"]),
                            RecruiterId = Convert.ToInt32(dr["RecruiterId"]),
                            USTTPMId = Convert.ToInt32(dr["USTTPMId"]),
                            DellManagerId = Convert.ToInt32(dr["DellManagerId"]),
                            StatusId = Convert.ToInt32(dr["StatusId"]),
                            Band = dr["Band"].ToString(),
                            ProjectId = dr["ProjectId"].ToString(),
                            AccountManager = dr["AccountManager"].ToString(),
                            ExternalResource = dr["ExternalResource"].ToString(),
                            InternalResource = dr["InternalResource"].ToString(),
                            AccountName = dr["AccountName"].ToString(),
                            TechnologyName = dr["TechnologyName"].ToString(),
                            Location = dr["Location"].ToString(),
                            Region = dr["Region"].ToString(),
                            USTPOCName = dr["USTPOCName"].ToString(),
                            RecruiterName = dr["RecruiterName"].ToString(),
                            USTTPMName = dr["USTTPMName"].ToString(),
                            DellManagerName = dr["DellManagerName"].ToString(),
                            StatusName = dr["StatusName"].ToString(),
                            // Type = dr["Type"].ToString(),
                        });
                    }
                }
            }
            return list;
        }

        // GET api/<Sow_Controller>/5
        [HttpGet("{id}")]
        public List<Sow> Get(int id)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            Sow sp = new Sow();
            sp.Type = "getid";
            sp.SowId = id;
            DataSet ds = dop.sowtableget(sp, msg);
            List<Sow> list = new List<Sow>();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        list.Add(new Sow
                        {
                            SowId = Convert.ToInt32(dr["SowId"]),
                            SoName = dr["SoName"].ToString(),
                            JRCode = dr["JRCode"].ToString(),
                            RequestCreationDate = ((DateTime)dr["RequestCreationDate"]),
                            AccountId = Convert.ToInt32(dr["AccountId"]),
                            TechnologyId = Convert.ToInt32(dr["TechnologyId"]),
                            Role = dr["Role"].ToString(),
                            LocationId = Convert.ToInt32(dr["LocationId"]),
                            RegionId = Convert.ToInt32(dr["RegionId"]),
                            TargetOpenPositions = Convert.ToInt32(dr["TargetOpenPositions"]),
                            PositionsTobeClosed = Convert.ToInt32(dr["PositionsTobeClosed"]),
                            USTPOCId = Convert.ToInt32(dr["USTPOCId"]),
                            RecruiterId = Convert.ToInt32(dr["RecruiterId"]),
                            USTTPMId = Convert.ToInt32(dr["USTTPMId"]),
                            DellManagerId = Convert.ToInt32(dr["DellManagerId"]),
                            StatusId = Convert.ToInt32(dr["StatusId"]),
                            Band = dr["Band"].ToString(),
                            ProjectId = dr["ProjectId"].ToString(),
                            AccountManager = dr["AccountManager"].ToString(),
                            ExternalResource = dr["ExternalResource"].ToString(),
                            InternalResource = dr["InternalResource"].ToString(),
                            AccountName = dr["AccountName"].ToString(),
                            TechnologyName = dr["TechnologyName"].ToString(),
                            Location = dr["Location"].ToString(),
                            Region = dr["Region"].ToString(),
                            USTPOCName = dr["USTPOCName"].ToString(),
                            RecruiterName = dr["RecruiterName"].ToString(),
                            USTTPMName = dr["USTTPMName"].ToString(),
                            DellManagerName = dr["DellManagerName"].ToString(),
                            StatusName = dr["StatusName"].ToString(),
                            // Type = dr["Type"].ToString(),
                        });
                    }
                }
            }
            return list;
        }

        // POST api/<Sow_Controller>
        [HttpPost]
        public void Post([FromBody] Sow sp)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;
            try
            {
                msg = dop.sowtable(sp);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
        }

        // update api/<Sow_Controller>/5
        [HttpPut("{id}")]
        public void update([FromBody] Sow sp, int id)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;
            try
            {
                sp.Type = "update";
                sp.SowId = id;
                msg = dop.sowtable(sp);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
        }

        // DELETE api/<Sow_Controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;
            try
            {
                Sow sp = new Sow();
                sp.Type = "Delete";
                sp.SowId = id;
                msg = dop.sowtable(sp);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
        }
        [HttpGet]
        [Route("Getdate")]
        public List<Sow> Getdate(DateTime Startdate, DateTime Enddate)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            Sow sp = new Sow();
            sp.Type = "getdate";
            //sp.SowId = id;
            DataSet ds = dop.Sowgetdate(sp, Startdate, Enddate);
            List<Sow> list = new List<Sow>();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        list.Add(new Sow
                        {
                            SowId = Convert.ToInt32(dr["SowId"]),
                            SoName = dr["SoName"].ToString(),
                            JRCode = dr["JRCode"].ToString(),
                            RequestCreationDate = ((DateTime)dr["RequestCreationDate"]),
                            AccountId = Convert.ToInt32(dr["AccountId"]),
                            TechnologyId = Convert.ToInt32(dr["TechnologyId"]),
                            Role = dr["Role"].ToString(),
                            LocationId = Convert.ToInt32(dr["LocationId"]),
                            RegionId = Convert.ToInt32(dr["RegionId"]),
                            TargetOpenPositions = Convert.ToInt32(dr["TargetOpenPositions"]),
                            PositionsTobeClosed = Convert.ToInt32(dr["PositionsTobeClosed"]),
                            USTPOCId = Convert.ToInt32(dr["USTPOCId"]),
                            RecruiterId = Convert.ToInt32(dr["RecruiterId"]),
                            USTTPMId = Convert.ToInt32(dr["USTTPMId"]),
                            DellManagerId = Convert.ToInt32(dr["DellManagerId"]),
                            StatusId = Convert.ToInt32(dr["StatusId"]),
                            Band = dr["Band"].ToString(),
                            ProjectId = dr["ProjectId"].ToString(),
                            AccountManager = dr["AccountManager"].ToString(),
                            ExternalResource = dr["ExternalResource"].ToString(),
                            InternalResource = dr["InternalResource"].ToString(),
                            AccountName = dr["AccountName"].ToString(),
                            TechnologyName = dr["TechnologyName"].ToString(),
                            Location = dr["Location"].ToString(),
                            Region = dr["Region"].ToString(),
                            USTPOCName = dr["USTPOCName"].ToString(),
                            RecruiterName = dr["RecruiterName"].ToString(),
                            USTTPMName = dr["USTTPMName"].ToString(),
                            DellManagerName = dr["DellManagerName"].ToString(),
                            StatusName = dr["StatusName"].ToString(),

                            // Type = dr["Type"].ToString(),
                        });
                    }
                }
            }
            return list;
        }
        [HttpPost]
        [Route("SOWImportData")]
        public void ImportCsowData([FromBody] Sowdata json_data)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;
            try
            {
                string data = JsonConvert.SerializeObject(json_data.SOW);
                msg = dop.imp_sowData(data);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
        }
    }
}