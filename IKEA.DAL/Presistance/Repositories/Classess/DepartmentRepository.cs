using IKEA.DAL.Models.DepartmentModule;
using IKEA.DAL.Presistance.Data.Contexts;
using IKEA.DAL.Presistance.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Presistance.Repositories.Classess
{

    public class DepartmentRepository(ApplicationDbContext dbContext) : GenericRepository<Department>(dbContext), IDepartmentRepository
    {



        //CRUD
        //public DepartmentRepository(ApplicationDbContext dbContext) {


        //    _dbContext = dbContext;
        //}



        //ApplicationDbContext dbContext=new ApplicationDbContext();
        //Get All Department
        private readonly ApplicationDbContext _dbContext = dbContext;
    }
}
