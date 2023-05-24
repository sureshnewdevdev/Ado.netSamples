using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using WebApplication12.Models;

namespace WebApplication12.Controllers
{
    public class HomeController : Controller
    {
        System.Data.SqlClient.SqlConnection _con;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
                    _logger = logger;
        }

        public IActionResult ViewCustomer()
        {
            return View("ViewCustomer");
        }

        [HttpPost]
        public IActionResult ViewCustomer(Customer customer)
        {
            _con = new System.Data.SqlClient.SqlConnection("Data Source=.;Initial Catalog=CustomerDBCollection;Integrated Security=True");
            
            if (_con.State != System.Data.ConnectionState.Broken | _con.State != System.Data.ConnectionState.Closed)
            {
                _con.Open();
            }

            DataSet ds =new DataSet();

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM CUSTOMERTBL", _con);

            sqlDataAdapter.InsertCommand = new SqlCommand($"INSERT INTO CUSTOMERTBL VALUES ({customer.CustId},'{customer.Name}',{customer.Salary}  )",_con);
            
            sqlDataAdapter.Fill(ds);

            int result = sqlDataAdapter.InsertCommand.ExecuteNonQuery();

            if (result == 1)
                ViewBag.Result = "Record Inserted Successfully";
            else
                ViewBag.Result = "Record not Inserted ";
            
            if (_con.State == ConnectionState.Open)
                _con.Close();

            return View("ViewCustomer",customer);
        }

        [HttpPost]
        public IActionResult EmpView(Employee emp)
        {

            return View(emp);
        }

        public IActionResult EmpView()
        {
            return View();
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}