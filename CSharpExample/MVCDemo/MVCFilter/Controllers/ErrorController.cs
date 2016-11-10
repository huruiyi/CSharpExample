﻿using System.Web.Mvc;

namespace MVCFilter.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index(string error)
        {
            ViewData["Title"] = "WebSite 网站内部错误";
            ViewData["Description"] = error;
            return View("Index");   //全部路由到Error下的Index视图
        }

        public ActionResult HttpError404(string error)
        {
            ViewData["Title"] = "HTTP 404- 无法找到文件";
            ViewData["Description"] = error;
            return View("Index");
        }

        public ActionResult HttpError500(string error)
        {
            ViewData["Title"] = "HTTP 500 - 内部服务器错误";
            ViewData["Description"] = error;
            return View("Index");
        }

        public ActionResult General(string error)
        {
            ViewData["Title"] = "HTTP 发生错误";
            ViewData["Description"] = error;
            return View("Index");
        }
    }
}