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
    public class OrderController : Controller
    {
        private readonly OrderRepository _orderRepository;
        private readonly EmployeeRepository _employeeRepository;
        private readonly ItemRepository _itemRepository;
        private readonly CustomerRepository _customerRepository;

        public OrderController(OrderRepository orderRepository, 
                               EmployeeRepository departmentRepository,
                               ItemRepository itemRepository,
                               CustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _employeeRepository = departmentRepository;
            _itemRepository = itemRepository;
            _customerRepository = customerRepository;
        }
        public ActionResult Index()
        {
            var orders = _orderRepository.GetAllOrders();
            List<OrderViewModel> ordersViewModel = new List<OrderViewModel>();
            foreach (var order in orders)
            {
                ordersViewModel.Add(new OrderViewModel
                {
                    Order = order,
                    Employee = _employeeRepository.GetEmployeeById(order.EmployeeId),
                    Item = _itemRepository.GetItemById(order.ItemId),
                    Customer = _customerRepository.GetCustomerById(order.CustomerId)
                }
                ); ;
            }

            return View(ordersViewModel);
        }

        public ActionResult Create()
        {
            var employees = _employeeRepository.GetAllEmployees();
            var items = _itemRepository.GetAllItems().ToList();
            var customers = _customerRepository.GetAllCustomers();
            ViewBag.Employees = new SelectList(employees, "Id", "Name");
            ViewBag.Items = new SelectList(items, "Id", "Name", "Quantity");
            ViewBag.Customers = new SelectList(customers, "Id", "Name");
            
            return View();
        }

        [HttpPost]
        public ActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                var item = _itemRepository.GetItemById(order.ItemId);
                if (item.Quantity > order.Quantity)
                {
                    _orderRepository.AddOrder(order);
                    item.Quantity -= order.Quantity;
                    _itemRepository.UpdateItem(item);
                    return RedirectToAction("Index");
                }
                
            }

            ViewBag.Employees = new SelectList(_employeeRepository.GetAllEmployees(), "Id", "Name");
            ViewBag.Items = new SelectList(_itemRepository.GetAllItems().ToList(), "Id", "Name", "Quantity");
            ViewBag.Customers = new SelectList(_customerRepository.GetAllCustomers(), "Id", "Name");
            return View(order);
        }


        public ActionResult Edit(int id)
        {
            Order order = _orderRepository.GetOrderById(id);

            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.Employees = new SelectList(_employeeRepository.GetAllEmployees(), "Id", "Name");
            ViewBag.Items = new SelectList(_itemRepository.GetAllItems().ToList(), "Id", "Name", "Quantity");
            ViewBag.Customers = new SelectList(_customerRepository.GetAllCustomers(), "Id", "Name");
            return View(order);
        }

        [HttpPost]
        public ActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {

                _orderRepository.UpdateOrder(order);
                return RedirectToAction("Index");
            }
            ViewBag.Employees = new SelectList(_employeeRepository.GetAllEmployees(), "Id", "Name");
            ViewBag.Items = new SelectList(_itemRepository.GetAllItems().ToList(), "Id", "Name", "Quantity");
            ViewBag.Customers = new SelectList(_customerRepository.GetAllCustomers(), "Id", "Name");
            return View(order);
        }

        public ActionResult Delete(int id)
        {
            Order order = _orderRepository.GetOrderById(id);

            if (order == null)
            {
                return HttpNotFound();
            }

            ViewBag.Employee = _employeeRepository.GetEmployeeById(order.EmployeeId);
            ViewBag.Item = _itemRepository.GetItemById(order.ItemId);
            ViewBag.Customer = _customerRepository.GetCustomerById(order.CustomerId);

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Order order)
        {
            _orderRepository.DeleteOrder(order.Id);
            return RedirectToAction("Index");
        }
    }
}