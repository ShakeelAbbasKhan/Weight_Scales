using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using System.Threading.Tasks;
using Weight_Scales.dto.response;
using Weight_Scales.Services;

namespace Weight_Scales.Controllers
{
    public class WeightScaleController : Controller
    {
        private readonly dapperQuery _dapperQuery;

        private readonly IConnectionService _connectionService;

        public WeightScaleController(dapperQuery dapperQuery, IConnectionService connectionService)
        {
            _dapperQuery = dapperQuery;
            _connectionService = connectionService;
        }

        [HttpPost]
        public async Task<IActionResult> getWeightRecord(DateTime fromDate, DateTime toDate)
        {
            try
            {
                if (_connectionService.IsConnected && !string.IsNullOrEmpty(_connectionService.ConnectionString))
                {
                    // Ensure that the fromDate is earlier than toDate
                    if (fromDate > toDate)
                    {
                        return View("Error", new { errorMessage = "From Date must be earlier than To Date." });
                    }

                    // Prepare the SQL query
                    string cmd = $"SELECT * FROM view_weightRecord WHERE Date BETWEEN '{fromDate:yyyy-MM-dd}' AND '{toDate:yyyy-MM-dd}'";
                    var weightRecords = await _dapperQuery.QryAsync<WeightRecord>(cmd);

                    if (weightRecords == null || !weightRecords.Any())
                    {
                        return View("NoRecords");
                    }

                    return View(weightRecords);
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

    }
}
