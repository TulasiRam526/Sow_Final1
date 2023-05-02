using CandidateSoW.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
namespace CandidateSoW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardStatsController : ControllerBase
    {
        private IConfiguration configuration;
        public DashboardStatsController(IConfiguration iConfig)
        {
            configuration = iConfig;
        }

        [HttpGet]
        public List<DashboardStatsModel> Get(string period)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            DataSet ds = dop.DashboardStatsget(period);
            List<DashboardStatsModel> list = new List<DashboardStatsModel>();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        list.Add(new DashboardStatsModel
                        {
                            Count = Convert.ToInt32(dr["Count"]),
                            Name = dr["Name"].ToString(),
                            Category = dr["Category"].ToString(),
                        });
                    }
                }
            }
            return list;
        }

        [HttpGet]
        [Route("GetCandidatesStats")]
        public List<DashboardStatsModel> GetCandidatesStats(string period)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            DataSet ds = dop.DashboardCandidateStatsget(period);
            List<DashboardStatsModel> list = new List<DashboardStatsModel>();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        list.Add(new DashboardStatsModel
                        {

                            Category = dr["Category"].ToString(),
                            Name = dr["Name"].ToString(),
                            Count = Convert.ToInt32(dr["Count"])
                        });
                    }
                }
            }
            return list;
        }


    }
}