using Convergence.React;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Convergence.Demo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUniversalReactEnvironment _environment;
        public HomeController(IUniversalReactEnvironment environment)
        {
            _environment = environment;
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [Route("math")]
        public async Task<ActionResult> Math()
        {
            // Run scripts from the controller
            var result = await _environment.RunAsync<string>("'2 + 2 = ' + (2 + 2)", "math");
            return View((object)result);
        }
    }
}