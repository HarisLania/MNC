using MNC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MNC.DAL
{
    public class DepartmentRepository
    {
        private readonly MNCDBContext _dbContext;
        public DepartmentRepository(MNCDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Department> GetAllDepartments()
        {
            return _dbContext.Departments.ToList();
        }

        public Department GetDepartmentById(int id)
        {
            return _dbContext.Departments.Find(id);
        }

        public void AddDepartment(Department department)
        {
            _dbContext.Departments.Add(department);
            _dbContext.SaveChanges();
        }

        public void UpdateDepartment(Department department)
        {
            _dbContext.Entry(department).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void DeleteDepartment(int id)
        {
            Department department = _dbContext.Departments.Find(id);
            if (department != null)
            {
                _dbContext.Departments.Remove(department);
                _dbContext.SaveChanges();
            }
        }
    }
}