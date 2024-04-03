using Conifer_weather_alerts.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Conifer_weather_alerts.Controllers
{
    public class HomeController : Controller
    {
        private readonly GeoService _geoService;
        

        public HomeController(GeoService geoService)
        {
            _geoService = geoService;
        }

        public IActionResult Index()
        {
            var zones = _geoService.GetRegionStatesViewModel(); // Get the list of zones from the GeoService

            return View(zones);
        }

        public IActionResult Zones(int selectedRegionId)
        {
            List<StateZonesViewModel> StatesandZones = _geoService.GetStatesAndCitiesByRegionId(selectedRegionId);
            return View(StatesandZones);
        }
    }
}
