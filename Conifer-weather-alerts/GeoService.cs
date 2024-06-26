using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class GeoService
{
    public List<Region> GetRegions()
    {
        string regionsJson = File.ReadAllText("regions.json");
        List<Region> regionsList = JsonSerializer.Deserialize<List<Region>>(regionsJson);
        return regionsList;
    }

    public List<Zones> GetZones()
    {
        string citiesJson = File.ReadAllText("cities_and_zones.json");
        List<Zones> citiesList = JsonSerializer.Deserialize<List<Zones>>(citiesJson);
        return citiesList;
    }

    public List<State> GetStates()
    {
        string citiesJson = File.ReadAllText("states.json");
        List<State> statesList = JsonSerializer.Deserialize<List<State>>(citiesJson);
        return statesList;
    }

    public List<RegionStatesViewModel> GetRegionStatesViewModel()
    {
        List<Region> regions = GetRegions();
        List<State> states = GetStates();

        var regionStatesViewModel = regions.Select(region => new RegionStatesViewModel
        {
            RegionId = region.Id,
            RegionName = region.Name,
            States = string.Join(", ", states.Where(state => state.RegionId == region.Id).Select(state => state.Name))
        }).ToList();

        return regionStatesViewModel;
    }
    public StateZonesViewModel GetStateZonesViewModel(string state)
    {
        List<State> states = GetStates();
        List<Zones> zones = GetZones();
        var uniqueCities = zones.Where(item => item.State == state).Select(item => item.City).Distinct();
        List<CityZonesViewModel> myList = new List<CityZonesViewModel>();
        foreach (var city in uniqueCities)
        {
            CityZonesViewModel cityZonesViewModel = new CityZonesViewModel
            {
                City = city,
                CityZones = zones.Where(zone => zone.City == city).ToList()
            };
            myList.Add(cityZonesViewModel);
        }
        StateZonesViewModel stateZonesViewModel = new StateZonesViewModel
        {
            State = state,
            Cities = myList
        };
        return stateZonesViewModel;
    }
    public List<StateZonesViewModel> GetStatesAndCitiesByRegionId(int regionId)
    {
        List<State> states = GetStates();
        List<Zones> zones = GetZones();

        List<StateZonesViewModel> statesAndCities = states
            .Where(state => state.RegionId == regionId)
            .Select(state => GetStateZonesViewModel(state.Name))            
            .ToList();

        return statesAndCities;
    }
}

public class Region
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class Zones
{
    public string City { get; set; }
    public string State { get; set; }
    public string StateAbbreviation { get; set; }
    public string FIPS { get; set; }
    public string County { get; set; }
    public string Zone { get; set; }
    public string ZoneName { get; set; }
}

public class State
{
    public string Name { get; set; }
    public string Abbreviation { get; set; }
    public int RegionId { get; set; }
}

public class RegionStatesViewModel
{
    public int RegionId { get; set; }
    public string States { get; set; }
    public string RegionName { get; set; }
}

public class StateZonesViewModel
{
    public string State { get; set; }
    public List<CityZonesViewModel> Cities { get; set; }
}

public class CityZonesViewModel
{
    public string City { get; set; }
    public List<Zones> CityZones { get; set; }

}