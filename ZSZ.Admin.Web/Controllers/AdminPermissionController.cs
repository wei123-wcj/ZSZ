using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZSZ.IService;
using ZSZ.Service;

namespace ZSZ.Admin.Web.Controllers
{
    public class AdminPermissionController : Controller
    {
        public IPermissionService Permission { get; set; }
        // GET: AdminRole
        //查询所有权限
        public ActionResult Index()
        {
            ViewData["count"] = Permission.GetTotalCount();
            return View(Permission.GetAll());
        }
        //添加权限
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(PermissionEntity p)
        {
            PermissionEntity per = new PermissionEntity
            {
                Name = p.Name,
                Description = p.Description
            };
            using (MyContext my = new MyContext() )
            {
                my.Permissions.Add(per);
                int b= my.SaveChanges();
                if (b>0)
                {
                    ViewData["count"] = Permission.GetTotalCount();
                    return Json(b);
                }
            }
            return View();
        }
        //删除权限
        [HttpPost]
        public ActionResult Delete(long id)
        {
           bool b= Permission.MarkDeleted(id);
            if(b)
            {
                ViewData["count"] = Permission.GetTotalCount();
                return Json("ok");
                //return View("Index",Permission.GetAll());
            }else
            {
                return Json("no");
            }
        }
        //批量删除
        [HttpPost]
        public ActionResult DeleteAll(long[] selectIDs)
        {
            for (int i = 0; i < selectIDs.Length; i++)
            {
                bool b = Permission.MarkDeleted(selectIDs[i]);
            }
            return Json("ok");
        }
        //修改权限
        [HttpGet]
        public ActionResult Update(long id)
        {
            var permission = Permission.GetById(id);
            return View(permission);
        }
        [HttpPost]
        public ActionResult Update(PermissionEntity p)
        {
            using (MyContext my = new MyContext())
            {
                var permission = my.Permissions.Find(p.Id); //Permission.GetById(p.Id);
                permission.Name = p.Name;
                permission.Description = p.Description;
                int b = my.SaveChanges();
                if (b > 0)
                {
                    ViewData["count"] = Permission.GetTotalCount();
                    return Json("ok");
                }
                else
                {
                    return Json("no");
                }
            }
        }
        [HttpPost]
        public ActionResult Index(string name)
        {
            var permission = Permission.GetAll().Where(m=>m.Name.Contains(name)).ToArray();
            return View(permission);
        }
    }
}