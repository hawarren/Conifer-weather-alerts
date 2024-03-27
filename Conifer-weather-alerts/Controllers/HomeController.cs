using Conifer_weather_alerts.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Conifer_weather_alerts.Controllers
{
    public class HomeController : Controller
    {
        private readonly GeoService _geoService;
        private object apiResult;

        public HomeController(GeoService geoService)
        {
            _geoService = geoService;
        }

        public IActionResult Index()
        {
            var zones = _geoService.GetRegionStatesViewModel(); // Get the list of zones from the GeoService

            return View(zones);
        }

        public IActionResult ViewZones(string parameter)
        {
            // Call the API with the parameter
            // Get the result from the API

            // Pass the result to the Zones page
            return RedirectToAction("Zones", new { result = apiResult });
        }

        public IActionResult Zones(int selectedRegionId)
        {
            List<StateZonesViewModel> StatesandZones = _geoService.GetStatesAndCitiesByRegionId(selectedRegionId);
            // Display the result on the Zones page
            ViewData["Result"] = selectedRegionId;
            return View(StatesandZones);
        }
    }
}
