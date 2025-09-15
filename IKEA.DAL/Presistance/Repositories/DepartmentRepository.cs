using IKEA.DAL.Models;
using IKEA.DAL.Presistance.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Presistance.Repositories
{

    public class DepartmentRepository(ApplicationDbContext dbContext) : IDepartmentRepository
    {

        // private readonly ApplicationDbContext _dbContext;

        //CRUD
        //public DepartmentRepository(ApplicationDbContext dbContext) {


        //    _dbContext = dbContext;
        //}



        //ApplicationDbContext dbContext=new ApplicationDbContext();
        //Get All Department
        public IEnumerable<Department> GetAll(bool withTracking = false)
        {
            if (withTracking)
            {
                return dbContext.Departments.ToList();
            }
            else
            {
                return dbContext.Departments.AsNoTracking().ToList();
            }
        }
        //Get By Id
        public Department? GetById(int id/*,ApplicationDbContext dbContext*/)
        => dbContext.Departments.Find(id);
        //Add
        public int Add(Department department)
        {
            dbContext.Departments.Add(department);
            return dbContext.SaveChanges();
        }
        //Delete
        public int Remove(Department department)
        {
            dbContext.Departments.Remove(department);
            return dbContext.SaveChanges();
        }

        //Update
        public int Update(Department department)
        {
            dbContext.Departments.Update(department);
            return dbContext.SaveChanges();
        }
    }
}
