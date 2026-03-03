using System;
using UnityEngine;

public class PlanetSystemController
{
    TimeModel timeModel;
    IPlanetEphemerisService ephemeris;

    PlanetView[] planets;

    public PlanetSystemController(
        TimeModel timeModel,
        IPlanetEphemerisService ephemeris,
        PlanetView[] planets)
    {
        this.timeModel = timeModel;
        this.ephemeris = ephemeris;
        this.planets = planets;

        timeModel.OnTimeChanged += UpdatePlanets;
    }

    void UpdatePlanets(DateTime time)
    {
        Debug.Log("[TIME] Updating planets " + time);

        foreach (var planet in planets)
        {
            Vector3 pos = ephemeris.GetPlanetPosition(planet.planet, time);
            planet.SetPosition(pos);
        }
    }

    public void UpdateTrajectories(DateTime centerTime)
    {
        int samples = 128;
        double daysSpan = 365.0;
        double dt = daysSpan / samples;

        foreach (var planet in planets)
        {
            var points = new Vector3[samples];
            for (int i = 0; i < samples; i++)
            {
                var t = centerTime.AddDays(-daysSpan / 2.0 + i * dt);
                points[i] = ephemeris.GetPlanetPosition(planet.planet, t);
            }
            planet.SetTrajectory(points);
        }
    }
}
