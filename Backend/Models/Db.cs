
using System.Data.SqlClient;
using System.Data;
using System;
using System.Drawing;
using CandidateSoW.Models;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using CandidateSoW.Common;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace CandidateSoW.Models
{
    public class Db
    {

        private string connstr ;

        public Db(string dbConn)
        {
            connstr = dbConn;
        }

        public string sowtable(Sow sp)
        {
            SqlConnection con = new SqlConnection(connstr);
            string msg = string.Empty;
            try
            {
                SqlCommand cmd = new SqlCommand("Sow_proc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SowId", sp.SowId);
                cmd.Parameters.AddWithValue("@SoName", sp.SoName);
                cmd.Parameters.AddWithValue("@JRCode", sp.JRCode);
                cmd.Parameters.AddWithValue("@RequestCreationDate", sp.RequestCreationDate);
                cmd.Parameters.AddWithValue("@AccountId", sp.AccountId);
                cmd.Parameters.AddWithValue("@TechnologyId", sp.TechnologyId);
                cmd.Parameters.AddWithValue("@Role", sp.Role);
                cmd.Parameters.AddWithValue("@LocationId", sp.LocationId);
                cmd.Parameters.AddWithValue("@RegionId", sp.RegionId);
                cmd.Parameters.AddWithValue("@TargetOpenPositions", sp.TargetOpenPositions);
                cmd.Parameters.AddWithValue("@PositionsTobeClosed", sp.PositionsTobeClosed);
                cmd.Parameters.AddWithValue("@USTPOCId", sp.USTPOCId);
                cmd.Parameters.AddWithValue("@RecruiterId", sp.RecruiterId);
                cmd.Parameters.AddWithValue("@USTTPMId", sp.USTTPMId);
                cmd.Parameters.AddWithValue("@DellManagerId", sp.DellManagerId);
                cmd.Parameters.AddWithValue("@StatusId", sp.StatusId);
                cmd.Parameters.AddWithValue("@Band", sp.Band);
                cmd.Parameters.AddWithValue("@ProjectId", sp.ProjectId);
                cmd.Parameters.AddWithValue("@AccountManager", sp.AccountManager);
                cmd.Parameters.AddWithValue("@ExternalResource", sp.ExternalResource);
                cmd.Parameters.AddWithValue("@InternalResource", sp.InternalResource);
                cmd.Parameters.AddWithValue("@Type", sp.Type);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                msg = "Success";

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }
            return msg;
        }
        public string imp_candidateData(string jsonData)
        {
            StringBuilder sb = new StringBuilder();
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                string msg = string.Empty;
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_candidateDataImport", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@json", jsonData);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    // msg = "Success";
                    sb.Append("Success");
                }
                catch (Exception ex)
                {
                    //msg = ex.Message;
                    sb.Append(ex.Message);
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }

                }
                return sb.ToString();
            }
        }
        public string imp_sowData(string jsonData)
        {

            using (SqlConnection conn = new SqlConnection(connstr))
            {
                string msg = string.Empty;
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_sowDataImport", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@json", jsonData);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    msg = "Success";
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }



                }
                return msg;
            }
        }

        //get record
        public DataSet sowtableget(Sow sp, string msg)
        {
            msg = string.Empty;
            SqlConnection con = new SqlConnection(connstr);
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Sow_proc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SowId", sp.SowId);
                cmd.Parameters.AddWithValue("@SoName", sp.SoName);
                cmd.Parameters.AddWithValue("@JRCode", sp.JRCode);
                cmd.Parameters.AddWithValue("@RequestCreationDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@AccountId", sp.AccountId);
                cmd.Parameters.AddWithValue("@TechnologyId", sp.TechnologyId);
                cmd.Parameters.AddWithValue("@Role", sp.Role);
                cmd.Parameters.AddWithValue("@LocationId", sp.LocationId);
                cmd.Parameters.AddWithValue("@RegionId", sp.RegionId);
                cmd.Parameters.AddWithValue("@TargetOpenPositions", sp.TargetOpenPositions);
                cmd.Parameters.AddWithValue("@PositionsTobeClosed", sp.PositionsTobeClosed);
                cmd.Parameters.AddWithValue("@USTPOCId", sp.USTPOCId);
                cmd.Parameters.AddWithValue("@RecruiterId", sp.RecruiterId);
                cmd.Parameters.AddWithValue("@USTTPMId", sp.USTTPMId);
                cmd.Parameters.AddWithValue("@DellManagerId", sp.DellManagerId);
                cmd.Parameters.AddWithValue("@StatusId", sp.StatusId);
                cmd.Parameters.AddWithValue("@Band", sp.Band);
                cmd.Parameters.AddWithValue("@ProjectId", sp.ProjectId);
                cmd.Parameters.AddWithValue("@AccountManager", sp.AccountManager);
                cmd.Parameters.AddWithValue("@ExternalResource", sp.ExternalResource);
                cmd.Parameters.AddWithValue("@InternalResource", sp.InternalResource);
                cmd.Parameters.AddWithValue("@Type", sp.Type);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                msg = "Success";
                return ds;

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return ds;
        }
        public List<CandidateModel> GetAllCandidateData()
        {
            string msg = string.Empty;
            List<CandidateModel> lstmain = new List<CandidateModel>();
            SqlConnection con = new SqlConnection(connstr);
            try
            {
                SqlCommand cmd = new SqlCommand("usp_getAllCandidateData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CandidateModel obj = new CandidateModel();
                    obj.CandidateId = Int32.Parse(dt.Rows[i]["CandidateId"].ToString() ?? "0");
                    obj.CandidateName = dt.Rows[i]["CandidateName"].ToString();
                    obj.DOB = DateTime.Parse(dt.Rows[i]["DOB"].ToString() ?? "2000-01-01");
                    obj.Address = dt.Rows[i]["Address"].ToString();
                    obj.Status = dt.Rows[i]["Status"].ToString();
                    obj.Pincode = dt.Rows[i]["Pincode"].ToString();
                    obj.Email = dt.Rows[i]["EmailId"].ToString();
                    obj.Skills = dt.Rows[i]["Skills"].ToString();
                    obj.Gender = dt.Rows[i]["Gender"].ToString();
                    obj.JoiningDate = DateTime.Parse(dt.Rows[i]["JoiningDate"].ToString() ?? "2000-01-01");
                    obj.isInternal = Boolean.Parse(dt.Rows[i]["isInternal"].ToString() ?? "false");
                    obj.MobileNo = dt.Rows[i]["MobileNo"].ToString();
                    obj.Location = dt.Rows[i]["Location"].ToString();
                    lstmain.Add(obj);
                }
                return lstmain;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return lstmain;
        }

        public List<CandidateModel> GetCandidateById(int id)
        {
            string msg = string.Empty;
            List<CandidateModel> lstmain = new List<CandidateModel>();
            DataTable dt = new DataTable();
            try
            {
                string query = "select * from Candidate where CandidateId = @CandidateId";

                using (SqlConnection con = new SqlConnection(connstr))
                {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add("@CandidateId", SqlDbType.Int).Value = id;
                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            adp.Fill(dt);
                        }
                    }
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CandidateModel obj = new CandidateModel();
                    obj.CandidateId = Int32.Parse(dt.Rows[i]["CandidateId"].ToString() ?? "0");
                    obj.CandidateName = dt.Rows[i]["CandidateName"].ToString();
                    obj.DOB = DateTime.Parse(dt.Rows[i]["DOB"].ToString() ?? "2000-01-01");
                    obj.Address = dt.Rows[i]["Address"].ToString();
                    obj.Status = dt.Rows[i]["Status"].ToString();
                    obj.Pincode = dt.Rows[i]["Pincode"].ToString();
                    obj.Email = dt.Rows[i]["EmailId"].ToString();
                    obj.Skills = dt.Rows[i]["Skills"].ToString();
                    obj.Gender = dt.Rows[i]["Gender"].ToString();
                    obj.JoiningDate = DateTime.Parse(dt.Rows[i]["JoiningDate"].ToString() ?? "2000-01-01");
                    obj.isInternal = Boolean.Parse(dt.Rows[i]["isInternal"].ToString() ?? "false");
                    obj.MobileNo = dt.Rows[i]["MobileNo"].ToString();
                    obj.Location = dt.Rows[i]["Location"].ToString();
                    lstmain.Add(obj);
                }
                return lstmain;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return lstmain;
        }

        public string candidateTable(CandidateModel candidate)
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                string msg = string.Empty;
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_addCandidateData", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CandidateName", candidate.CandidateName);
                    cmd.Parameters.AddWithValue("@EmailId", candidate.Email);
                    cmd.Parameters.AddWithValue("@MobileNo", candidate.MobileNo);
                    cmd.Parameters.AddWithValue("@DOB", candidate.DOB);
                    cmd.Parameters.AddWithValue("@Location", candidate.Location);
                    cmd.Parameters.AddWithValue("@JoiningDate", candidate.JoiningDate);
                    cmd.Parameters.AddWithValue("@Skills", candidate.Skills);
                    cmd.Parameters.AddWithValue("@IsInternal", candidate.isInternal);
                    cmd.Parameters.AddWithValue("@Address", candidate.Address);
                    cmd.Parameters.AddWithValue("@Pincode", candidate.Pincode);
                    cmd.Parameters.AddWithValue("@Status", candidate.Status);
                    cmd.Parameters.AddWithValue("@Gender", candidate.Gender);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    msg = "Success";
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }

                }
                return msg;
            }
        }
        public string updateCandidateTable(int id, CandidateModel candidate)
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                string msg = string.Empty;
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_editCandidateData", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CandidateId", id);
                    cmd.Parameters.AddWithValue("@CandidateName", candidate.CandidateName);
                    cmd.Parameters.AddWithValue("@EmailId", candidate.Email);
                    cmd.Parameters.AddWithValue("@MobileNo", candidate.MobileNo);
                    cmd.Parameters.AddWithValue("@DOB", candidate.DOB);
                    cmd.Parameters.AddWithValue("@Location", candidate.Location);
                    cmd.Parameters.AddWithValue("@JoiningDate", candidate.JoiningDate);
                    cmd.Parameters.AddWithValue("@Skills", candidate.Skills);
                    cmd.Parameters.AddWithValue("@IsInternal", candidate.isInternal);
                    cmd.Parameters.AddWithValue("@Address", candidate.Address);
                    cmd.Parameters.AddWithValue("@Pincode", candidate.Pincode);
                    cmd.Parameters.AddWithValue("@Status", candidate.Status);
                    cmd.Parameters.AddWithValue("@Gender", candidate.Gender);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    msg = "Success";
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }

                }
                return msg;
            }
        }
        public string deleteCandidateData(int id)
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                string msg = string.Empty;
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_deleteCandidateData", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CandidateId", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    msg = "Success";
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }

                }
                return msg;
            }
        }
        public string sowcandidatetable(SoWCandidateModel sc)
        {
            //var jsonString = JsonConvert.SerializeObject(msg);

            string msg = string.Empty;
            SqlConnection con = new SqlConnection(connstr);
            try
            {
                SqlCommand cmd = new SqlCommand("SowCandidate_proc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SOW_CandidateId", sc.SOW_CandidateId);
                cmd.Parameters.AddWithValue("@SowId", sc.SowId);
                cmd.Parameters.AddWithValue("@CandidateId", sc.CandidateId);
                cmd.Parameters.AddWithValue("@StatusId", sc.StatusId);
                cmd.Parameters.AddWithValue("@Type", sc.Type);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                msg = "success";

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }
            return JsonConvert.SerializeObject(msg);
        }


        //get record
        public DataSet SowcandidatetableGet(SoWCandidateModel sc, string msg)
        {
            msg = string.Empty;

            SqlConnection con = new SqlConnection(connstr);
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("SowCandidate_proc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SOW_CandidateId", sc.SOW_CandidateId);
                cmd.Parameters.AddWithValue("@SowId", sc.SowId);
                cmd.Parameters.AddWithValue("@CandidateId", sc.CandidateId);
                cmd.Parameters.AddWithValue("@StatusId", sc.StatusId);
                cmd.Parameters.AddWithValue("@Type", sc.Type);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                msg = "Success";
                return ds;

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return ds;
        }

        public string Domaintable(DomainModel dm)
        {
            //var jsonString = JsonConvert.SerializeObject(msg);

            string msg = string.Empty;
            SqlConnection con = new SqlConnection(connstr);
            try
            {
                SqlCommand cmd = new SqlCommand("Domain_proc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DomainId", dm.DomainId);
                cmd.Parameters.AddWithValue("@DomainName", dm.DomainName);
                cmd.Parameters.AddWithValue("@Type", dm.Type);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                msg = "success";

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }
            return JsonConvert.SerializeObject(msg);
        }



        public DataSet Domaintableget(DomainModel dm, string msg)
        {
            msg = string.Empty;

            SqlConnection con = new SqlConnection(connstr);
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Domain_proc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DomainId", dm.DomainId);
                cmd.Parameters.AddWithValue("@DomainName", dm.DomainName);
                cmd.Parameters.AddWithValue("@Type", dm.Type);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                msg = "Success";
                return ds;

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return ds;
        }

        public string Techtable(TechnologyModel ts)
        {
            //var jsonString = JsonConvert.SerializeObject(msg);

            string msg = string.Empty;
            SqlConnection con = new SqlConnection(connstr);
            try
            {
                SqlCommand cmd = new SqlCommand("Technology_proc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TechnologyId", ts.TechnologyId);
                cmd.Parameters.AddWithValue("@TechnologyName", ts.TechnologyName);
                cmd.Parameters.AddWithValue("@DomainId", ts.DomainId);
                cmd.Parameters.AddWithValue("@Type", ts.Type);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                msg = "success";

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }
            return JsonConvert.SerializeObject(msg);
        }

        public DataSet techtableget(TechnologyModel ts, string msg)
        {
            msg = string.Empty;
            SqlConnection con = new SqlConnection(connstr);

            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Technology_proc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TechnologyId", ts.TechnologyId);
                cmd.Parameters.AddWithValue("@TechnologyName", ts.TechnologyName);
                cmd.Parameters.AddWithValue("@DomainId", ts.DomainId);
                cmd.Parameters.AddWithValue("@Type", ts.Type);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                msg = "Success";
                return ds;

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return ds;
        }

        public string USTTPMtable(USTTPMModel tpm)
        {
            //var jsonString = JsonConvert.SerializeObject(msg);

            string msg = string.Empty;
            SqlConnection con = new SqlConnection(connstr);
            try
            {
                SqlCommand cmd = new SqlCommand("USTTPM_proc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@USTTPMId", tpm.USTTPMId);
                cmd.Parameters.AddWithValue("@USTTPMName", tpm.USTTPMName);
                cmd.Parameters.AddWithValue("@Type", tpm.Type);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                msg = "success";

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }
            return JsonConvert.SerializeObject(msg);
        }

        public DataSet usttpmget(USTTPMModel tpm, string msg)
        {
            msg = string.Empty;
            SqlConnection con = new SqlConnection(connstr);

            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("USTTPM_proc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@USTTPMId", tpm.USTTPMId);
                cmd.Parameters.AddWithValue("@USTTPMName", tpm.USTTPMName);
                cmd.Parameters.AddWithValue("@Type", tpm.Type);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                msg = "Success";
                return ds;

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return ds;
        }

        public string dellmanagertable(DellManagerModel dl)
        {
            //var jsonString = JsonConvert.SerializeObject(msg);

            string msg = string.Empty;
            SqlConnection con = new SqlConnection(connstr);
            try
            {
                SqlCommand cmd = new SqlCommand("DellManager_proc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DellManagerId", dl.DellManagerId);
                cmd.Parameters.AddWithValue("@DellManagerName", dl.DellManagerName);
                cmd.Parameters.AddWithValue("@Type", dl.Type);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                msg = "success";

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }
            return JsonConvert.SerializeObject(msg);
        }


        public DataSet dellmanagerget(DellManagerModel dl, string msg)
        {
            msg = string.Empty;
            SqlConnection con = new SqlConnection(connstr);

            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("DellManager_proc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DellManagerId", dl.DellManagerId);
                cmd.Parameters.AddWithValue("@DellManagerName", dl.DellManagerName);
                cmd.Parameters.AddWithValue("@Type", dl.Type);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                msg = "Success";
                return ds;

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return ds;
        }

        public string ustpoctable(USTPOCModel us)
        {
            //var jsonString = JsonConvert.SerializeObject(msg);

            string msg = string.Empty;
            SqlConnection con = new SqlConnection(connstr);
            try
            {
                SqlCommand cmd = new SqlCommand("USTPOC_proc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@USTPOCId", us.USTPOCId);
                cmd.Parameters.AddWithValue("@USTPOCName", us.USTPOCName);
                cmd.Parameters.AddWithValue("@Type", us.Type);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                msg = "success";

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }
            return JsonConvert.SerializeObject(msg);
        }

        public DataSet ustpocget(USTPOCModel us, string msg)
        {
            msg = string.Empty;
            SqlConnection con = new SqlConnection(connstr);

            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("USTPOC_proc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@USTPOCId", us.USTPOCId);
                cmd.Parameters.AddWithValue("@USTPOCName", us.USTPOCName);
                cmd.Parameters.AddWithValue("@Type", us.Type);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                msg = "Success";
                return ds;

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return ds;
        }
        public string recruiter(RecruiterModel rc)
        {
            //var jsonString = JsonConvert.SerializeObject(msg);

            string msg = string.Empty;
            SqlConnection con = new SqlConnection(connstr);
            try
            {
                SqlCommand cmd = new SqlCommand("Recruiter_proc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RecruiterId", rc.RecruiterId);
                cmd.Parameters.AddWithValue("@RecruiterName", rc.RecruiterName);
                cmd.Parameters.AddWithValue("@Type", rc.Type);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                msg = "success";

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }
            return JsonConvert.SerializeObject(msg);
        }

        public DataSet recruiterget(RecruiterModel rc, string msg)
        {
            msg = string.Empty;
            SqlConnection con = new SqlConnection(connstr);

            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Recruiter_proc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RecruiterId", rc.RecruiterId);
                cmd.Parameters.AddWithValue("@RecruiterName", rc.RecruiterName);
                cmd.Parameters.AddWithValue("@Type", rc.Type);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                msg = "Success";
                return ds;

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return ds;
        }

        public string Accounttable(AccountModel ac)
        {
            //var jsonString = JsonConvert.SerializeObject(msg);
            SqlConnection con = new SqlConnection(connstr);
            string msg = string.Empty;

            try
            {
                SqlCommand cmd = new SqlCommand("Account_proc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AccountId", ac.AccountId);
                cmd.Parameters.AddWithValue("@AccountName", ac.AccountName);
                cmd.Parameters.AddWithValue("@Type", ac.Type);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                msg = "success";

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }
            return JsonConvert.SerializeObject(msg);
        }


        //get record
        public DataSet Accounttableget(AccountModel ac, string msg)
        {
            msg = string.Empty;
            SqlConnection con = new SqlConnection(connstr);

            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Account_proc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AccountId", ac.AccountId);
                cmd.Parameters.AddWithValue("@AccountName", ac.AccountName);
                cmd.Parameters.AddWithValue("@Type", ac.Type);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                msg = "Success";
                return ds;

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return ds;
        }

        public string Regiontable(RegionModel rs)
        {
            //var jsonString = JsonConvert.SerializeObject(msg);

            string msg = string.Empty;
            SqlConnection con = new SqlConnection(connstr);
            try
            {
                SqlCommand cmd = new SqlCommand("Region_proc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RegionId", rs.RegionId);
                cmd.Parameters.AddWithValue("@Region", rs.Region);
                cmd.Parameters.AddWithValue("@Type", rs.Type);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                msg = "success";

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }
            return JsonConvert.SerializeObject(msg);
        }


        //get record
        public DataSet Regiontableget(RegionModel rs, string msg)
        {
            msg = string.Empty;
            SqlConnection con = new SqlConnection(connstr);

            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Region_proc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RegionId", rs.RegionId);
                cmd.Parameters.AddWithValue("@Region", rs.Region);
                cmd.Parameters.AddWithValue("@Type", rs.Type);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                msg = "Success";
                return ds;

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return ds;
        }


        public string locationtable(LocationModel ls)
        {
            //var jsonString = JsonConvert.SerializeObject(msg);

            string msg = string.Empty;
            SqlConnection con = new SqlConnection(connstr);
            try
            {
                SqlCommand cmd = new SqlCommand("Location_proc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LocationId", ls.LocationId);
                cmd.Parameters.AddWithValue("@Location", ls.Location);
                cmd.Parameters.AddWithValue("@RegionId", ls.RegionId);
                cmd.Parameters.AddWithValue("@Type", ls.Type);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                msg = "success";

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }
            return JsonConvert.SerializeObject(msg);
        }

        public DataSet locationtableget(LocationModel ls, string msg)
        {
            msg = string.Empty;
            SqlConnection con = new SqlConnection(connstr);

            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Location_proc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LocationId", ls.LocationId);
                cmd.Parameters.AddWithValue("@Location", ls.Location);
                cmd.Parameters.AddWithValue("@RegionId", ls.RegionId);
                cmd.Parameters.AddWithValue("@Type", ls.Type);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                msg = "Success";
                return ds;

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return ds;
        }

        public string Statustable(StatusModel st)
        {
            //var jsonString = JsonConvert.SerializeObject(msg);

            string msg = string.Empty;
            SqlConnection con = new SqlConnection(connstr);

            try
            {
                SqlCommand cmd = new SqlCommand("Status_proc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StatusId", st.StatusId);
                cmd.Parameters.AddWithValue("@StatusName", st.StatusName);
                cmd.Parameters.AddWithValue("@Type", st.Type);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                msg = "success";

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }
            return JsonConvert.SerializeObject(msg);
        }

        public DataSet Statusget(StatusModel st, string msg)
        {
            msg = string.Empty;

            SqlConnection con = new SqlConnection(connstr);

            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Status_proc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StatusId", st.StatusId);
                cmd.Parameters.AddWithValue("@StatusName", st.StatusName);
                cmd.Parameters.AddWithValue("@Type", st.Type);
                cmd.Parameters.AddWithValue("@StatusType", st.StatusType);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                msg = "Success";    
                return ds;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return ds;
        }
        public string logintable(LoginModel ln)
        {
            //var jsonString = JsonConvert.SerializeObject(msg);
            SqlConnection con = new SqlConnection(connstr);
            string msg = string.Empty; try
            {
                SqlCommand cmd = new SqlCommand("loginproc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LoginName", ln.LoginName);
                cmd.Parameters.AddWithValue("@EmailId", ln.EmailId);
                cmd.Parameters.AddWithValue("@RoleName", ln.RoleName);
                cmd.Parameters.AddWithValue("@ScreenNames", ln.ScreenNames);
                cmd.Parameters.AddWithValue("@Status", ln.Status);
                cmd.Parameters.AddWithValue("@PermissionName", ln.PermissionName);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                msg = "success";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return JsonConvert.SerializeObject(msg);
        }

        // parameterized queries

        //public string logintable(LoginModel ln)
        //{
        //    string msg = string.Empty;
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(connstr))
        //        {
        //            string query = "INSERT INTO LoginTable (LoginName, EmailId, RoleName, ScreenNames, Status, PermissionName) VALUES (@LoginName VARCHAR(50), @EmailId, @RoleName, @ScreenNames, @Status, @PermissionName)";
        //            using (SqlCommand cmd = new SqlCommand(query, con))
        //            {
        //                cmd.Parameters.AddWithValue("@LoginName", ln.LoginName);
        //                cmd.Parameters.AddWithValue("@EmailId", ln.EmailId);
        //                cmd.Parameters.AddWithValue("@RoleName", ln.RoleName);
        //                cmd.Parameters.AddWithValue("@ScreenNames", ln.ScreenNames);
        //                cmd.Parameters.AddWithValue("@Status", ln.Status);
        //                cmd.Parameters.AddWithValue("@PermissionName", ln.PermissionName);
        //                con.Open();
        //                cmd.ExecuteNonQuery();
        //                con.Close();
        //            }
        //            msg = "success";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        msg = ex.Message;
        //    }
        //    return JsonConvert.SerializeObject(msg);
        //}

        //public DataSet loginget(string LoginName, string LoginPassword)
        //{
        //    string msg = string.Empty;
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(connstr))
        //        {
        //            string query = "SELECT * FROM LoginTable WHERE LoginName = @LoginName AND LoginPassword = @LoginPassword";
        //            using (SqlCommand cmd = new SqlCommand(query, con))
        //            {
        //                cmd.Parameters.AddWithValue("@LoginName", LoginName);
        //                cmd.Parameters.AddWithValue("@LoginPassword", EncryptData.Encrypt(LoginPassword));
        //                SqlDataAdapter da = new SqlDataAdapter(cmd);
        //                da.Fill(ds);
        //            }
        //            msg = "Success";
        //            return ds;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        msg = ex.Message;
        //    }
        //    return ds;
        //}

        //-------------------------------------------------------//

        //store procedure with setiing the datatype and its length
        //public DataSet loginget(string LoginName, string LoginPassword)
        //{
        //    string msg = string.Empty;
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(connstr))
        //        {
        //            SqlCommand cmd = new SqlCommand("loginproc", con);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.Add("@LoginName", SqlDbType.VarChar, 50).Value = LoginName;
        //            cmd.Parameters.Add("@LoginPassword", SqlDbType.VarChar, 50).Value = EncryptData.Encrypt(LoginPassword);

        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            da.Fill(ds);
        //            msg = "Success";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        msg = ex.Message;
        //    }
        //    return ds;
        //}

        //public string logintable(LoginModel ln)
        //{
        //    string msg = string.Empty;
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(connstr))
        //        {
        //            SqlCommand cmd = new SqlCommand("loginproc", con);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.Add("@LoginName", SqlDbType.VarChar, 50).Value = ln.LoginName;
        //            cmd.Parameters.Add("@EmailId", SqlDbType.VarChar, 50).Value = ln.EmailId;
        //            cmd.Parameters.Add("@RoleName", SqlDbType.VarChar, 50).Value = ln.RoleName;
        //            cmd.Parameters.Add("@ScreenNames", SqlDbType.VarChar, 50).Value = ln.ScreenNames;
        //            cmd.Parameters.Add("@Status", SqlDbType.VarChar, 50).Value = ln.Status;
        //            cmd.Parameters.Add("@PermissionName", SqlDbType.VarChar, 50).Value = ln.PermissionName;

        //            con.Open();
        //            cmd.ExecuteNonQuery();
        //            msg = "success";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        msg = ex.Message;
        //    }
        //    return JsonConvert.SerializeObject(msg);
        //}

        //


        public DataSet loginget(string EmailId, string LoginPassword)
        {
            string msg = string.Empty;
            SqlConnection con = new SqlConnection(connstr); DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("loginproc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmailId", EmailId);
                cmd.Parameters.AddWithValue("@LoginPassword", EncryptData.Encrypt(LoginPassword));
                //cmd.Parameters.AddWithValue("@EmailId", ln.EmailId);
                //cmd.Parameters.AddWithValue("@RoleName", ln.RoleName);
                //cmd.Parameters.AddWithValue("@ScreenNames", ln.ScreenNames);
                //cmd.Parameters.AddWithValue("@Status", ln.Status);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                msg = "Success";
                return ds;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return ds;
        }
        public string updateLoginTable(string EmailId, string LoginPassword, LoginModel lm)
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                string msg = string.Empty;
                try
                {
                    SqlCommand cmd = new SqlCommand("editlogin", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmailId", EmailId);
                    cmd.Parameters.AddWithValue("@LoginPassword", EncryptData.Encrypt(LoginPassword));
                    cmd.Parameters.AddWithValue("@FailureAttempts", lm.FailureAttempts);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    msg = "Success";
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
                return msg;
            }
        }




        public DataSet DashboardStatsget(string period)
        {
            string msg = string.Empty;
            SqlConnection con = new SqlConnection(connstr); 
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("DashboardStats", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PeriodType", period);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                msg = "Success";
                return ds;

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return ds;
        }

        public DataSet DashboardCandidateStatsget(string period)
        {
            string msg = string.Empty;
            SqlConnection con = new SqlConnection(connstr);
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("DashboardCandidateStats", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PeriodType", period);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                msg = "Success";
                return ds;

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return ds;
        }
        public DataSet getRoles(RegistrationModel rc, string msg)
        {
            msg = string.Empty;
            SqlConnection con = new SqlConnection(connstr);

            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Registration_proc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LoginName", rc.LoginName);
                cmd.Parameters.AddWithValue("@LoginPassword", rc.LoginPassword);
                cmd.Parameters.AddWithValue("@EmailId", rc.EmailId);
                cmd.Parameters.AddWithValue("@RoleId", rc.RoleId);
                cmd.Parameters.AddWithValue("@LoginId", rc.LoginId);
                cmd.Parameters.AddWithValue("@Type", rc.Type);
                cmd.Parameters.AddWithValue("@FailureAttempts", rc.FailureAttempts);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                msg = "Success";
                return ds;

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return ds;
        }

        public string Userdetails(RegistrationModel rc)
        {
            SqlConnection con = new SqlConnection(connstr);
            string msg = string.Empty;

            try
            {
                SqlCommand cmd = new SqlCommand("Registration_proc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LoginName", rc.LoginName);
                //Encrypting the password before inserting
                cmd.Parameters.AddWithValue("@LoginPassword", EncryptData.Encrypt(rc.LoginPassword));
                cmd.Parameters.AddWithValue("@EmailId", rc.EmailId);
                cmd.Parameters.AddWithValue("@RoleId", rc.RoleId);
                cmd.Parameters.AddWithValue("@LoginId", rc.LoginId);
                cmd.Parameters.AddWithValue("@Type", rc.Type);
                cmd.Parameters.AddWithValue("@FailureAttempts", rc.FailureAttempts);
                
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                msg = "success";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return JsonConvert.SerializeObject(msg);
        }

        public DataSet getLoginDetails(RegistrationModel rc, string msg)
        {
            msg = string.Empty;
            SqlConnection con = new SqlConnection(connstr);

            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Registration_proc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LoginName", rc.LoginName);
                cmd.Parameters.AddWithValue("@LoginPassword", rc.LoginPassword);
                cmd.Parameters.AddWithValue("@EmailId", rc.EmailId);
                cmd.Parameters.AddWithValue("@RoleId", rc.RoleId);
                cmd.Parameters.AddWithValue("@LoginId", rc.LoginId);
                cmd.Parameters.AddWithValue("@Type", rc.Type);
                cmd.Parameters.AddWithValue("@FailureAttempts", rc.FailureAttempts);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                msg = "Success";
                return ds;

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return ds;
        }

        public string UpdateUser(RegistrationModel rc)
        {
            SqlConnection con = new SqlConnection(connstr);
            string msg = string.Empty;

            try
            {
                SqlCommand cmd = new SqlCommand("Registration_proc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LoginName", rc.LoginName);
                cmd.Parameters.AddWithValue("@LoginPassword", rc.LoginPassword);
                cmd.Parameters.AddWithValue("@EmailId", rc.EmailId);
                cmd.Parameters.AddWithValue("@RoleId", rc.RoleId);
                cmd.Parameters.AddWithValue("@LoginId", rc.LoginId);
                cmd.Parameters.AddWithValue("@Type", rc.Type);
                cmd.Parameters.AddWithValue("@FailureAttempts", rc.FailureAttempts);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                msg = "success";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return JsonConvert.SerializeObject(msg);
        }

        public string DeleteUser(RegistrationModel rc)
        {
            string msg = string.Empty;
            SqlConnection con = new SqlConnection(connstr);
            try
            {
                SqlCommand cmd = new SqlCommand("Registration_proc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LoginName", rc.LoginName);
                cmd.Parameters.AddWithValue("@LoginPassword", rc.LoginPassword);
                cmd.Parameters.AddWithValue("@EmailId", rc.EmailId);
                cmd.Parameters.AddWithValue("@RoleId", rc.RoleId);
                cmd.Parameters.AddWithValue("@LoginId", rc.LoginId);
                cmd.Parameters.AddWithValue("@Type", rc.Type);
                cmd.Parameters.AddWithValue("@FailureAttempts", rc.FailureAttempts);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                msg = "success";

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }
            return JsonConvert.SerializeObject(msg);
        }
        public string changePassword(ChangePasswordModel cp)
        {
            SqlConnection con = new SqlConnection(connstr);
            string msg = string.Empty;
            try
            {
                SqlCommand cmd = new SqlCommand("ChangePassword_proc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmailId", cp.LoginName);
                cmd.Parameters.AddWithValue("@OldPassword", EncryptData.Encrypt(cp.OldPassword));
                cmd.Parameters.AddWithValue("@NewPassword", EncryptData.Encrypt(cp.NewPassword));
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    msg = rdr["status"].ToString();
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }
            return JsonConvert.SerializeObject(msg);
        }

        public DataSet Sowgetdate(Sow sp, DateTime Startdate, DateTime Enddate)
        {
            string msg = string.Empty;
            SqlConnection con = new SqlConnection(connstr);
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("sow_date", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SowId", sp.SowId);
                cmd.Parameters.AddWithValue("@SoName", sp.SoName);
                cmd.Parameters.AddWithValue("@JRCode", sp.JRCode);
                cmd.Parameters.AddWithValue("@RequestCreationDate", sp.RequestCreationDate);
                cmd.Parameters.AddWithValue("@AccountId", sp.AccountId);
                cmd.Parameters.AddWithValue("@TechnologyId", sp.TechnologyId);
                cmd.Parameters.AddWithValue("@Role", sp.Role);
                cmd.Parameters.AddWithValue("@LocationId", sp.LocationId);
                cmd.Parameters.AddWithValue("@RegionId", sp.RegionId);
                cmd.Parameters.AddWithValue("@TargetOpenPositions", sp.TargetOpenPositions);
                cmd.Parameters.AddWithValue("@PositionsTobeClosed", sp.PositionsTobeClosed);
                cmd.Parameters.AddWithValue("@USTPOCId", sp.USTPOCId);
                cmd.Parameters.AddWithValue("@RecruiterId", sp.RecruiterId);
                cmd.Parameters.AddWithValue("@USTTPMId", sp.USTTPMId);
                cmd.Parameters.AddWithValue("@DellManagerId", sp.DellManagerId);
                cmd.Parameters.AddWithValue("@StatusId", sp.StatusId);
                cmd.Parameters.AddWithValue("@Band", sp.Band);
                cmd.Parameters.AddWithValue("@ProjectId", sp.ProjectId);
                cmd.Parameters.AddWithValue("@AccountManager", sp.AccountManager);
                cmd.Parameters.AddWithValue("@ExternalResource", sp.ExternalResource);
                cmd.Parameters.AddWithValue("@InternalResource", sp.InternalResource);
                cmd.Parameters.AddWithValue("@StartDate", Startdate);
                cmd.Parameters.AddWithValue("@EndDate", Enddate);
                cmd.Parameters.AddWithValue("@Type", sp.Type);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                msg = "Success";
                return ds;

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return ds;
        }
        public List<CandidateModel> GetCandidateDate(DateTime StartDate, DateTime EndDate)
        {
            string msg = string.Empty;
            List<CandidateModel> lstmain = new List<CandidateModel>();
            DataTable dt = new DataTable();
            try
            {
                string query = "select * from Candidate where CONVERT(date, JoiningDate) between  @StartDate and  @EndDate";



                using (SqlConnection con = new SqlConnection(connstr))
                {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add("@StartDate", SqlDbType.Date).Value = StartDate;
                        cmd.Parameters.Add("@EndDate", SqlDbType.Date).Value = EndDate;
                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            adp.Fill(dt);
                        }
                    }
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CandidateModel obj = new CandidateModel();
                    obj.CandidateId = Int32.Parse(dt.Rows[i]["CandidateId"].ToString() ?? "0");
                    obj.CandidateName = dt.Rows[i]["CandidateName"].ToString();
                    obj.DOB = DateTime.Parse(dt.Rows[i]["DOB"].ToString() ?? "2000-01-01");
                    obj.Address = dt.Rows[i]["Address"].ToString();
                    obj.Status = dt.Rows[i]["Status"].ToString();
                    obj.Pincode = dt.Rows[i]["Pincode"].ToString();
                    obj.Email = dt.Rows[i]["EmailId"].ToString();
                    obj.Skills = dt.Rows[i]["Skills"].ToString();
                    obj.Gender = dt.Rows[i]["Gender"].ToString();
                    obj.JoiningDate = DateTime.Parse(dt.Rows[i]["JoiningDate"].ToString() ?? "2000-01-01");
                    obj.isInternal = Boolean.Parse(dt.Rows[i]["isInternal"].ToString() ?? "false");
                    obj.MobileNo = dt.Rows[i]["MobileNo"].ToString();
                    obj.Location = dt.Rows[i]["Location"].ToString();
                    lstmain.Add(obj);
                }
                return lstmain;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return lstmain;
        }







    }

}