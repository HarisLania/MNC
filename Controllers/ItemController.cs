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
    public class ItemController : Controller
    {
        private readonly ItemRepository _itemRepository;

        public ItemController(ItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        public ActionResult Index()
        {
            var items = _itemRepository.GetAllItems();
            
            return View(items);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Item items)
        {
            if (ModelState.IsValid)
            {
                _itemRepository.AddItem(items);
                return RedirectToAction("Index");
            }

            return View(items);
        }


        public ActionResult Edit(int id)
        {
            Item items = _itemRepository.GetItemById(id);

            if (items == null)
            {
                return HttpNotFound();
            }

            return View(items);
        }

        [HttpPost]
        public ActionResult Edit(Item items)
        {
            if (ModelState.IsValid)
            {
                _itemRepository.UpdateItem(items);
                return RedirectToAction("Index");
            }

            return View(items);
        }

        public ActionResult Delete(int id)
        {
            Item items = _itemRepository.GetItemById(id);

            if (items == null)
            {
                return HttpNotFound();
            }

            return View(items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Item items)
        {
            _itemRepository.DeleteItem(items.Id);
            return RedirectToAction("Index");
        }
    }
}