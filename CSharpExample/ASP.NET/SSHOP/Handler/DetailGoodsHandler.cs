﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.SessionState;//********************************重点*********************************

namespace SSHOP
{
    public class DetailGoodsHandler : IHttpHandler, IRequiresSessionState
    {
        public bool IsReusable
        {
            get { return true; }
        }

        public static List<MyShopping> mss;

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.RequestType == "GET")
            {
                string pid = context.Request.QueryString["pid"];

                #region 判断此商品是否已经添加到购物车

                string alreadybuycount = "";
                if (mss != null)
                {
                    for (int i = 0; i < mss.Count; i++)
                    {
                        if (mss[i].proid == pid)
                        {
                            alreadybuycount = mss[i].buyCount;
                            break;
                        }
                    }
                }

                #endregion 判断此商品是否已经添加到购物车

                string sql = "select * from Product where ProductID=@ProductID";
                SqlDataReader sdr = SQLHelper.ExecuteReader(sql, new SqlParameter("@ProductID", pid));
                Product pro = new Product();
                pro.ProductID = pid;
                while (sdr.Read())
                {
                    pro.ProductName = sdr["ProductName"].ToString();
                    pro.UnitPrice = Convert.ToDecimal(sdr["UnitPrice"]);
                    pro.ImagePath = "../Images/" + sdr["ImagePath"];
                    context.Session["pid"] = pid;
                    context.Session["proname"] = pro.ProductName;
                    context.Session["proprice"] = pro.UnitPrice;
                }

                string html = File.ReadAllText(context.Server.MapPath("~/htmls/DetailGoods.htm"));
                html = html.Replace("{产品图片}", string.Format("<img src='{0}' width='300px' height='300px'>", pro.ImagePath));
                html = html.Replace("{产品ID}", pro.ProductID);
                html = html.Replace("{产品名称}", pro.ProductName);
                html = html.Replace("{产品单价}", pro.UnitPrice.ToString());
                html = html.Replace("{联系电话}", "<input typr='text' name='userphone'/>");
                html = html.Replace("{送货地址}", "<input type='text' name='useraddress'/>");
                string bcount = "0";
                for (int i = 1; i < 51; i++)
                {
                    bcount += string.Format("<option value='{0}'>{0}</option>", i);
                }
                html = html.Replace("{购买数量}", bcount);

                if (alreadybuycount != "")
                {
                    html = html.Replace("{已购买数量}", "<p>已购买:" + alreadybuycount + "件</p>");
                }
                else if (alreadybuycount == "")
                {
                    html = html.Replace("{已购买数量}", "");
                }
                context.Response.Write(html);
            }
            else if (context.Request.RequestType == "POST")
            {
                if (context.Session["sadmin"] != null && context.Session["spwd"] != null)
                {
                    string proid = context.Session["pid"].ToString();
                    string proname = context.Session["proname"].ToString();
                    decimal proprice = Convert.ToDecimal(context.Session["proprice"]);
                    string userphone = context.Request.Form["userphone"];
                    string useraddress = context.Request.Form["useraddress"];
                    string buyCount = context.Request.Form["buyCount"];

                    if (mss == null)
                    {
                        mss = new List<MyShopping>();

                        #region 添加首个商品

                        MyShopping ms = new MyShopping();
                        ms.sadmin = context.Session["sadmin"].ToString();
                        ms.proid = proid;
                        ms.proname = proname;
                        ms.proprice = proprice;
                        ms.userphone = userphone;
                        ms.useraddress = useraddress;
                        ms.buyCount = buyCount;
                        mss.Add(ms);

                        #endregion 添加首个商品
                    }
                    else if (mss != null)
                    {
                        foreach (MyShopping ims in mss)
                        {
                            if (ims.proid != proid)//如果此商品ID不存在
                            {
                                MyShopping ms = new MyShopping();
                                ms.sadmin = context.Session["sadmin"].ToString();
                                ms.proid = proid;
                                ms.proname = proname;
                                ms.proprice = proprice;
                                ms.userphone = userphone;
                                ms.useraddress = useraddress;
                                ms.buyCount = buyCount;
                                mss.Add(ms);
                                break;
                            }
                            else//如果商品已购买过,则增加数量,(修改),(添加)新的联系电话,和送货地址
                            {
                                int ibuyCount = Convert.ToInt32(ims.buyCount);
                                ibuyCount += Convert.ToInt32(buyCount);
                                buyCount = ibuyCount.ToString();
                                ims.buyCount = buyCount;
                                ims.userphone = userphone;
                                ims.useraddress = useraddress;
                                break;
                            }
                        }
                    }
                    context.Response.Redirect("SubOrder.html");
                }
                else
                {
                    context.Response.Redirect("Index.htm");
                }
            }
        }
    }
}