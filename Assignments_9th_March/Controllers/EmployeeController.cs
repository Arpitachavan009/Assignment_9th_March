using Assignments_9th_March.Database;
using Assignments_9th_March.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Assignments_9th_March.Controllers
{
    public class EmployeeController : Controller
    {
        #region Properties
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        #endregion

        #region Constructors
        public EmployeeController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion

        #region Methods
        // GET: Employees
        public async Task<IActionResult> Index(string searchString, string sortOrder, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DeptSortParm"] = sortOrder == "Dept" ? "dept_desc" : "Dept";

            var employees = from e in _context.tbl_Employees
                            select e;

            if (!string.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(e => e.FirstName.Contains(searchString)
                                       || e.LastName.Contains(searchString)
                                       || e.Email.Contains(searchString)
                                       || e.Department.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    employees = employees.OrderByDescending(e => e.FirstName + e.LastName);
                    break;
                case "Dept":
                    employees = employees.OrderBy(e => e.Department);
                    break;
                case "dept_desc":
                    employees = employees.OrderByDescending(e => e.Department);
                    break;
                default:
                    employees = employees.OrderBy(e => e.FirstName + e.LastName);
                    break;
            }

            int pageSize = 5;
            return View(await PaginatedList<Employee>.CreateAsync(employees.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id ,FirstName ,LastName ,Gender ,DateOfBirth ,Email ,Phone ,Department ,City ,State ,Country ,Pin_Code ,Hobbies")] Employee employee)
        {
            if (ModelState.IsValid)
            {

                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.tbl_Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  Employee employee , IFormFile photo)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.tbl_Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.tbl_Employees.FindAsync(id);
            _context.tbl_Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.tbl_Employees.Any(e => e.Id == id);
        }

       
        #endregion

    }
}
