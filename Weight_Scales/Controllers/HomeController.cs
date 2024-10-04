using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using System.Threading.Tasks;
using Weight_Scales.dto.response;
using Weight_Scales.Services;

namespace Weight_Scales.Controllers
{
    public class HomeController : Controller
    {
        private readonly dapperQuery _dapperQuery;

        private readonly IConnectionService _connectionService;

        public HomeController(dapperQuery dapperQuery, IConnectionService connectionService)
        {
            _dapperQuery = dapperQuery;
            _connectionService = connectionService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> getCompanyList()
        {
            try
            {
                if (_connectionService.IsConnected && !string.IsNullOrEmpty(_connectionService.ConnectionString))
                {
                string cmd = "SELECT * FROM view_companyList";
                var companyList = await _dapperQuery.QryAsync<Company>(cmd);  // This retrieves the list of companies.
                return View(companyList);
                }
                else
                {
                    return View("Error", new { errorMessage = "Not connected to the database." });
                }
            }
            catch (Exception e)
            {
                return View("Error", new { errorMessage = e.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> getWeightRecord(DateTime fromDate, DateTime toDate)
        {
            try
            {
                if (_connectionService.IsConnected && !string.IsNullOrEmpty(_connectionService.ConnectionString))
                {
                    if (fromDate > toDate)
                    {
                        return View("Error", new { errorMessage = "From Date must be earlier than To Date." });
                    }

                    // Prepare the SQL query
                    string cmd = $"SELECT * FROM view_weightRecord WHERE Date BETWEEN '{fromDate:yyyy-MM-dd}' AND '{toDate:yyyy-MM-dd}'";
                    var weightRecords = await _dapperQuery.QryAsync<WeightRecord>(cmd);  // This retrieves the list of weight records.

                    if (weightRecords == null || !weightRecords.Any())
                    {
                        return View("NoRecords"); // Create a view to handle "No records found" message
                    }

                    return View(weightRecords);  // Pass the list to the View.
                }
                else
                {
                    // Handle case where no database connection is established
                    return View("Error", new { errorMessage = "Not connected to the database." });
                }
            }
            catch (Exception e)
            {
                return View("Error", new { errorMessage = e.Message });  // Display an error page if something goes wrong.
            }
        }

    }
}
