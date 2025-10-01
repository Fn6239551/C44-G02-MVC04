using IKEA.DAL.Presistance.Data.Contexts;
using IKEA.DAL.Presistance.Repositories.Interface;
using IKEA.DAL.Presistance.Repositories.Classess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Presistance.Repositories.Classess
{
    public class UnitOfWork : IunitOfWork
    {
        private readonly Lazy< IEmployeeRepository> _employeeRepository;
        private readonly  ApplicationDbContext _dbContext;
        private readonly Lazy< IDepartmentRepository > _departmentRepository;
        public UnitOfWork(ApplicationDbContext dbContext,IEmployeeRepository employeeRepository,IDepartmentRepository departmentRepository)
        {
            _dbContext = dbContext;
            _employeeRepository = new Lazy<IEmployeeRepository>(()=>new EmployeeRepository(_dbContext));
            _departmentRepository = new Lazy<IDepartmentRepository>(()=>new DepartmentRepository(_dbContext));
        }
        public IEmployeeRepository EmployeeRepository => _employeeRepository.Value;

        public IDepartmentRepository DepartmentRepository => _departmentRepository.Value;

        public int SaveChanges()
        {
           return _dbContext.SaveChanges();
        }
    }
}
