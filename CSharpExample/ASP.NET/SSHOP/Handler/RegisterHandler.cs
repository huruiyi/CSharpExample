﻿using System;
using System.Data.SqlClient;
using System.Web;
using System.Web.SessionState;

namespace SSHOP
{
    public class RegisterHandler : IHttpHandler, IRequiresSessionState
    {
        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            string admin = context.Request.Form["username"];
            string pwd = context.Request.Form["password"];
            string address = context.Request.Form["Address"]; ;
            string phone = context.Request.Form["Phone"];

            #region 生成UserID

            SqlDataReader sdr = SQLHelper.ExecuteReader("select userId from T_Admin ");
            string userid = "";
            while (sdr.Read())
            {
                userid = sdr["userId"].ToString();
            }
            if (userid == null || userid == "")
            {
                userid = "U001";
            }
            else
            {
                userid = userid.Substring(1);
                userid = "U" + (Convert.ToInt32(userid) + 1).ToString();
            }

            #endregion 生成UserID

            string sql = @"insert into T_Admin (userId,userName,userPwd,userPhoneNum,userAddress) values
                        (@userId,@userName,@userPwd,@userPhoneNum,@userAddress)";

            int registerResult = SQLHelper.ExecuteNonQuery(sql, new SqlParameter("@userId", userid),
                                              new SqlParameter("@userName", admin),
                                              new SqlParameter("@userPwd", pwd),
                                              new SqlParameter("@userPhoneNum", phone),
                                              new SqlParameter("@userAddress", address)
                                              );
            if (registerResult == 1)
            {
                context.Session["sadmin"] = admin;
                context.Session["spwd"] = pwd;
                context.Response.Redirect("GoodsDetails.html");
            }
            else
            {
                context.Response.Write("注册失败");
                context.Response.Redirect("Register.html");
            }
        }
    }
}