using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Weight_Scales.dto.response;
using Weight_Scales.Services;

namespace Weight_Scales.Controllers
{
    public class ConnectionController : Controller
    {
        private readonly IConnectionService _connectionService;

        public ConnectionController(IConnectionService connectionService)
        {
            _connectionService = connectionService;
        }

        // Action to render the connection form
        public IActionResult Connect()
        {
            return View(new DBConnectionVM());
        }

        [HttpPost]
        public IActionResult Connect(DBConnectionVM model)
        {
            try
            {
                // Ensure the model contains the necessary fields
                _connectionService.SetConnectionString(model.InstanceName, "weightScale", model.Username, model.Password);

                // Attempt to open the connection to validate it
                using (var connection = new SqlConnection(_connectionService.ConnectionString))
                {
                    connection.Open();
                }

                model.IsConnected = true;
                return RedirectToAction("DateRange");
            }
            catch (Exception ex)
            {
                model.ErrorMessage = "Connection failed: " + ex.Message;
                return View(model);
            }
        }


        // Action to display the DateRange view
        public IActionResult DateRange()
        {
            if (!_connectionService.IsConnected)
            {
                return RedirectToAction("Connect");
            }

            return View(new DateRangeViewModel());
        }


        // Action to handle disconnecting from the database
        public IActionResult Disconnect()
        {
            _connectionService.ClearConnection(); // Clear the connection state
            return RedirectToAction("Connect"); // Redirect to the Connect view
        }
    }
}
