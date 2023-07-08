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
    public class EmployeeController : Controller
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly DepartmentRepository _departmentRepository;

        public EmployeeController(EmployeeRepository employeeRepository, DepartmentRepository departmentRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }
        public ActionResult Index()
        {
            var employees = _employeeRepository.GetAllEmployees();
            List<EmployeeViewModel> employeesViewModel = new List<EmployeeViewModel>();
            foreach (var employee in employees)
            {
                employeesViewModel.Add(new EmployeeViewModel
                    {
                        Employee = employee,
                        Department = _departmentRepository.GetDepartmentById(employee.DepartmentId)
                    }
                );
            }
            
            return View(employeesViewModel);
        }

        public ActionResult Create()
        {
            var departments = _departmentRepository.GetAllDepartments();
            ViewBag.Departments = new SelectList(departments, "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeRepository.AddEmployee(employee);
                return RedirectToAction("Index");
            }

            ViewBag.Departments = new SelectList(_departmentRepository.GetAllDepartments(), "Id", "Name");
            return View(employee);
        }


        public ActionResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmployeeById(id);

            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.Departments = new SelectList(_departmentRepository.GetAllDepartments(), "Id", "Name", employee.DepartmentId);
            return View(employee);
        }

        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeRepository.UpdateEmployee(employee);
                return RedirectToAction("Index");
            }
            ViewBag.Departments = new SelectList(_departmentRepository.GetAllDepartments(), "Id", "Name", employee.DepartmentId);
            return View(employee);
        }

        public ActionResult Delete(int id)
        {
            Employee employee = _employeeRepository.GetEmployeeById(id);

            if (employee == null)
            {
                return HttpNotFound();
            }

            ViewBag.Department = _departmentRepository.GetDepartmentById(employee.DepartmentId);

            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Employee employee)
        {
            _employeeRepository.DeleteEmployee(employee.Id);
            return RedirectToAction("Index");
        }
    }
}