using CandidateSoW.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
namespace CandidateSoW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : Controller
    {
        private IConfiguration configuration;
        public RegistrationController(IConfiguration iConfig)
        {
            configuration = iConfig;
        }
        string msg = string.Empty;

        [HttpGet]
        public List<RegistrationModel> Get()
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            RegistrationModel rc = new RegistrationModel();
            rc.Type = "get";
            DataSet ds = dop.getRoles(rc, msg);
            List<RegistrationModel> list = new List<RegistrationModel>();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        list.Add(new RegistrationModel
                        {
                            RoleId = Convert.ToInt32(dr["RoleId"]),
                            RoleName = dr["RoleName"].ToString(),
                        });
                    }
                }
            }
            return list;
        }

        [HttpPost]
        public String Post([FromBody] RegistrationModel rc)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            string password = configuration.GetSection("InitialPassword").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;
            try
            {
                rc.Type = "post";
                rc.FailureAttempts = 0;
                rc.LoginPassword = password;
                msg = dop.Userdetails(rc);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            //return msg;
            return JsonConvert.SerializeObject(msg);
        }




        [HttpGet]
        [Route("GetLoginDetails")]
        public List<RegistrationModel> GetLoginDetails()
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            RegistrationModel rc = new RegistrationModel();
            rc.Type = "getuser";
            DataSet ds = dop.getLoginDetails(rc, msg);
            List<RegistrationModel> list = new List<RegistrationModel>();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        list.Add(new RegistrationModel
                        {
                            LoginId = Convert.ToInt32(dr["LoginId"]),
                            LoginName = dr["LoginName"].ToString(),
                            EmailId = dr["EmailId"].ToString(),
                            RoleName = dr["RoleName"].ToString(),
                            RoleId = Convert.ToInt32(dr["RoleId"]),

                        });
                    }
                }
            }
            return list;
        }

        //[HttpGet]
        //[Route("GetLoginDetails/{loginId}")]
        //public RegistrationModel GetLoginDetails(int loginId)
        //{
        //    string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
        //    Db dop = new Db(dbConn);
        //    RegistrationModel rc = new RegistrationModel();
        //    rc.Type = "getuser";
        //    rc.LoginId = loginId; 
        //    DataSet ds = dop.getLoginDetails(rc, msg);
        //    RegistrationModel result = null;
        //    if (ds.Tables.Count > 0)
        //    {
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            DataRow dr = ds.Tables[0].Rows[0];
        //            result = new RegistrationModel
        //            {
        //                LoginId = Convert.ToInt32(dr["LoginId"]),
        //                LoginName = dr["LoginName"].ToString(),
        //                EmailId = dr["EmailId"].ToString(),
        //                RoleName = dr["RoleName"].ToString(),
        //                RoleId = Convert.ToInt32(dr["RoleId"])
        //            };
        //        }
        //    }
        //    return result;
        //}

        [HttpPut("{id}")]
        public String Put(int id, [FromBody] RegistrationModel rc)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;
            try
            {
                rc.FailureAttempts = 0;
                rc.Type = "update";
                rc.LoginId = id;
                msg = dop.UpdateUser(rc);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            //return msg;
            return JsonConvert.SerializeObject(msg);
        }

        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;
            try
            {
                RegistrationModel rc = new RegistrationModel();
                rc.FailureAttempts = 0;
                rc.Type = "delete";
                rc.LoginId = id;
                msg = dop.DeleteUser(rc);
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
