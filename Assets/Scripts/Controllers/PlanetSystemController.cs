using System;
using UnityEngine;

public class PlanetSystemController
{
    TimeModel timeModel;
    IPlanetEphemerisService ephemeris;

    PlanetViewV2[] planets;

    private bool showLogs = false;

    public PlanetSystemController(
        TimeModel timeModel,
        IPlanetEphemerisService ephemeris,
        PlanetViewV2[] planets)
    {
        this.timeModel = timeModel;
        this.ephemeris = ephemeris;
        this.planets = planets;

        timeModel.OnTimeChanged += UpdatePlanets;
    }

    void UpdatePlanets(DateTime time)
    {
        if (showLogs) Debug.Log("[TIME] Updating planets " + time);

        foreach (var planet in planets)
        {
            Vector3 pos = ephemeris.GetPlanetPosition(planet.planet, time);
            planet.SetPosition(pos);
        }
    }

    public void UpdateTrajectories(DateTime centerTime)
    {
        int samples = 128;

        foreach (var planet in planets)
        {
            double daysSpan = GetOrbitalPeriod(planet.planet);
            double dt = daysSpan / samples;
            var points = new Vector3[samples];
            for (int i = 0; i < samples; i++)
            {
                var t = centerTime.AddDays(i * dt);
                points[i] = ephemeris.GetPlanetPosition(planet.planet, t);
            }
            planet.SetTrajectory(points);
        }
    }

    private double GetOrbitalPeriod(PlanetData.Planet planet)
    {
        switch (planet)
        {
            case PlanetData.Planet.Mercury: return 88;
            case PlanetData.Planet.Venus: return 225;
            case PlanetData.Planet.Earth: return 365;
            case PlanetData.Planet.Mars: return 687;
            case PlanetData.Planet.Jupiter: return 4333;
            case PlanetData.Planet.Saturn: return 10759;
            case PlanetData.Planet.Uranus: return 30687;
            case PlanetData.Planet.Neptune: return 60190;
            default: throw new ArgumentException("Unknown planet");
        }
    }
}
