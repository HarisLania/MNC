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
    public class CustomerController : Controller
    {
        private readonly CustomerRepository _customerRepository;

        public CustomerController(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public ActionResult Index()
        {
            var customers = _customerRepository.GetAllCustomers();
            var viewModel = new CustomerViewModel
            {
                Customers = customers
            };
            return View(viewModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerRepository.AddCustomer(customer);
                return RedirectToAction("Index");
            }

            return View(customer);
        }


        public ActionResult Edit(int id)
        {
            Customer customer = _customerRepository.GetCustomerById(id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }

        [HttpPost]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerRepository.UpdateCustomer(customer);
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        public ActionResult Delete(int id)
        {
            Customer customer = _customerRepository.GetCustomerById(id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Customer customer)
        {
            _customerRepository.DeleteCustomer(customer.Id);
            return RedirectToAction("Index");
        }
    }
}