using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        public readonly EmployeeDbContext Context;
        public EmployeeController(EmployeeDbContext context)
        {
            Context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employee = await  Context.Employees.ToListAsync();
            return View(employee);
        }

        [HttpGet]
        public async Task<IActionResult>  Details(Guid id)
        {
            var employee = await Context.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (employee != null)
            {
                var Viewmodel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    salary = employee.salary,
                    DateOfBirth = employee.DateOfBirth,
                    Department = employee.Department,
                };
                return View(Viewmodel);

            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel model)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Email = model.Email,
                salary = model.salary,
                DateOfBirth = model.DateOfBirth,
                Department = model.Department,

            };

            await Context.Employees.AddAsync(employee);
            await Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var employee = await Context.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if(employee != null) 
            {
                var Viewmodel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    salary = employee.salary,
                    DateOfBirth = employee.DateOfBirth,
                    Department = employee.Department,
                };
                return View(Viewmodel);

            }
            return RedirectToAction("Index");
           
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateEmployeeViewModel model)
        {
            var employee = await Context.Employees.FindAsync(model.Id);
            if(employee != null) 
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.DateOfBirth = model.DateOfBirth;
                employee.salary = model.salary;
                employee.Department = model.Department;

                await Context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await Context.Employees.FindAsync(model.Id);
            if (employee != null)
            {
                Context.Employees.Remove(employee);
                await Context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }



    }
}
