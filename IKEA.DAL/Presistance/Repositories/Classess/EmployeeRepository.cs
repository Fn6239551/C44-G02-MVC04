using IKEA.DAL.Models.EmployeeModule;
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
    public class EmployeeRepository(ApplicationDbContext dbContext) : GenericRepository<Employee>(dbContext), IEmployeeRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
    }
}
