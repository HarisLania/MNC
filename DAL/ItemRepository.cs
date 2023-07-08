using MNC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MNC.DAL
{
    public class ItemRepository
    {
        private readonly MNCDBContext _dbContext;
        public ItemRepository(MNCDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Item> GetAllItems()
        {
            return _dbContext.Items;
        }

        public Item GetItemById(int id)
        {
            return _dbContext.Items.Find(id);
        }

        public void AddItem(Item item)
        {
            _dbContext.Items.Add(item);
            _dbContext.SaveChanges();
        }

        public void UpdateItem(Item item)
        {
            _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void DeleteItem(int id)
        {
            var item = _dbContext.Items.Find(id);
            if (item != null)
            {
                _dbContext.Items.Remove(item);
                _dbContext.SaveChanges();
            }
        }
    }
}