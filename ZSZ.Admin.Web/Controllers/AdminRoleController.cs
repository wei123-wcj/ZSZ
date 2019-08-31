using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZSZ.IService;
using ZSZ.Service;

namespace ZSZ.Admin.Web.Controllers
{
    public class AdminRoleController : Controller
    {
        public IRoleService Irole { get; set; }
        // GET: AdminRole
        //查询全部角色
        public ActionResult Index()
        {
            ViewBag.Tocount = Irole.GetTotalCount();
            return View(Irole.GetAll());
        }
        //添加角色
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(RoleEntity role)
        {
            using (MyContext my=new MyContext())
            {
                my.Roles.Add(role);
                int i = my.SaveChanges();
                if(i>0)
                {
                    ViewBag.Tocount = Irole.GetTotalCount();
                    return Json("ok");
                }
                else
                {
                    return Json("no");
                }
            }
        }
        //修改角色
        [HttpGet]
        public ActionResult Update(long id)
        {
            var data = Irole.GetById(id);
            return View(data);
        }
        [HttpPost]
        public ActionResult Update(RoleEntity role)
        {
            using (MyContext my=new MyContext())
            {
                var data = my.Roles.Find(role.Id);
                data.Name = role.Name;
                int i = my.SaveChanges();
                if(i>0)
                {
                    ViewBag.Tocount = Irole.GetTotalCount();
                    return Json("ok");
                }
                else
                {
                    return Json("no");
                }
            }
           
        }
        //删除角色
        [HttpPost]
        public ActionResult Delete(long id)
        {
            bool i = Irole.MarkDeleted(id);
            if (i)
            {
                ViewBag.Tocount = Irole.GetTotalCount();
                return Json("ok");
            }
            else
            {
                return Json("no");
            }
        }
        public ActionResult DeleteAll(long[] selectIDs)
        {
            for (int i = 0; i < selectIDs.Length; i++)
            {
                Irole.MarkDeleted(selectIDs[i]);
            }
            ViewBag.Tocount = Irole.GetTotalCount();
            return Json("ok");
        }
    }
}