using CandidateSoW.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CandidateSoW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IConfiguration configuration;
        public AccountController(IConfiguration iConfig)
        {
            configuration = iConfig;
        }

        string msg = string.Empty;

        // GET: api/<AccountController>
        [HttpGet]
        public List<AccountModel> Get()
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            AccountModel ac = new AccountModel();
            ac.Type = "get";
            DataSet ds = dop.Accounttableget(ac, msg);
            List<AccountModel> list = new List<AccountModel>();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        list.Add(new AccountModel
                        {
                            AccountId = Convert.ToInt32(dr["AccountId"]),
                            AccountName = dr["AccountName"].ToString(),
                        });
                    }
                }
            }
            return list;
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public List<AccountModel> Get(int id)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            AccountModel ac = new AccountModel();
            ac.Type = "getid";
            ac.AccountId = id;
            DataSet ds = dop.Accounttableget(ac, msg);
            List<AccountModel> list = new List<AccountModel>();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        list.Add(new AccountModel
                        {
                            AccountId = Convert.ToInt32(dr["AccountId"]),
                            AccountName = dr["AccountName"].ToString(),
                        });
                    }
                }
            }
            return list;
        }

        // POST api/<AccountController>
        [HttpPost]
        public String Post([FromBody] AccountModel ac)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;
            try
            {
                msg = dop.Accounttable(ac);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            //return msg;
            return JsonConvert.SerializeObject(msg);
        }

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public String Put(int id, [FromBody] AccountModel ac)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;
            try
            {
                ac.Type = "update";
                ac.AccountId = id;
                msg = dop.Accounttable(ac);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            //return msg;
            return JsonConvert.SerializeObject(msg);
        }

        // DELETE api/<AccountController>/5
        [HttpDelete("{id}")]
        public String Delete(int id)
        {
            string dbConn = configuration.GetSection("ConnectionStrings").GetSection("DbConnection").Value;
            Db dop = new Db(dbConn);
            string msg = string.Empty;

            try
            {
                AccountModel ac = new AccountModel();
                ac.Type = "Delete";
                ac.AccountId = id;
                msg = dop.Accounttable(ac);
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
