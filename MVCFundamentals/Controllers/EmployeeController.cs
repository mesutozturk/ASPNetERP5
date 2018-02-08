using MVCFundamentals.Models;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MVCFundamentals.Controllers
{
    public class EmployeeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var db = new NorthwindEntities();
            var model = await db.Employees
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ToListAsync();

            return View(model);
        }
    }
}