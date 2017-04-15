﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using schoolsSystems.Models;
using schoolsSystems.Models.DropMenu;

namespace schoolsSystems.Controllers
{
    public class SchoolFormController : Controller
    {
        EMEntities db;
        public SchoolFormController(){
            db=new EMEntities();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Edit(SchoolForm schoolForm)
        {
            if (ModelState.IsValid)
            {
                db.SchoolForm.Add(schoolForm);
                db.SaveChanges();
            }
            return RedirectToAction("Info","School",new {id =schoolForm.SchoolId});
        }
        public ActionResult Create(int SchoolId = 0, int SchoolFormId = 0)
        {
            SchoolForm sf = new SchoolForm();
            sf.SchoolId = SchoolId;
            return View("Edit", sf);
        }
        public PartialViewResult List(int SchoolId)
        {
            List<SchoolForm> forms = db.SchoolForm.Where(sc=>sc.SchoolId==SchoolId).ToList();
            return PartialView(forms);
        }
        public PartialViewResult DropMenu(int SchoolFormId,int SchoolId)
        {
            return PartialView(new DropMenu<SchoolForm> { Id = SchoolFormId, list = db.SchoolForm.Where(sf=>sf.SchoolId==SchoolId).ToList() });
        }
    }
}
