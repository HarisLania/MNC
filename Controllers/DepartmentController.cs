using MNC.DAL;
using MNC.Models;
using MNC.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MNC.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly DepartmentRepository _departmentRepository;

        public DepartmentController(DepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public ActionResult Index()
        {
            var departments = _departmentRepository.GetAllDepartments();
            var viewModel = new DepartmentViewModel
            {
                Departments = departments
            };
            return View(viewModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                _departmentRepository.AddDepartment(department);
                return RedirectToAction("Index");
            }

            return View(department);
        }


        public ActionResult Edit(int id)
        {
            Department department = _departmentRepository.GetDepartmentById(id);

            if (department == null)
            {
                return HttpNotFound();
            }

            return View(department);
        }

        [HttpPost]
        public ActionResult Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                _departmentRepository.UpdateDepartment(department);
                return RedirectToAction("Index");
            }

            return View(department);
        }

        public ActionResult Delete(int id)
        {
            Department department = _departmentRepository.GetDepartmentById(id);
    
            if (department == null)
            {
                return HttpNotFound();
            }

            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Department department)
        {
            _departmentRepository.DeleteDepartment(department.Id);
            return RedirectToAction("Index");
        }



    }
}