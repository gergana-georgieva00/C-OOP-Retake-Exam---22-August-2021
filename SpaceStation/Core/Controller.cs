using SpaceStation.Core.Contracts;
using SpaceStation.Models.Astronauts;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Mission;
using SpaceStation.Models.Planets;
using SpaceStation.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceStation.Core
{
    public class Controller : IController
    {
        private PlanetRepository planets;
        private AstronautRepository astronauts;

        public Controller()
        {
            planets = new PlanetRepository();
            astronauts = new AstronautRepository();
        }

        public string AddAstronaut(string type, string astronautName)
        {
            IAstronaut astronaut;
            switch (type)
            {
                case "Biologist":
                    astronaut = new Biologist(astronautName);
                    break;
                case "Geodesist":
                    astronaut = new Geodesist(astronautName);
                    break;
                case "Meteorologist":
                    astronaut = new Meteorologist(astronautName);
                    break;
                default:
                    throw new InvalidOperationException("Astronaut type doesn't exists!");
            }

            return $"Successfully added {type}: {astronautName}!";
        }

        public string AddPlanet(string planetName, params string[] items)
        {
            var planet = new Planet(planetName);

            foreach (var item in items)
            {
                planet.Items.Add(item);
            }

            planets.Add(planet);
            return $"Successfully added Planet: {planetName}!";
        }

        public string ExplorePlanet(string planetName)
        {
            var mission = new Mission();
            var suitable = astronauts.Models.Where(a => a.Oxygen > 60).ToList();
            if (suitable.Count == 0)
            {
                throw new InvalidOperationException("You need at least one astronaut to explore the planet");
            }

            mission.Explore(planets.FindByName(planetName), suitable);
            return $"Planet: {planetName} was explored! Exploration finished with {astronauts.Models.Where(a => a.Oxygen > 0).ToList().Count} dead astronauts!";
        }

        public string Report()
        {
            throw new NotImplementedException();
        }

        public string RetireAstronaut(string astronautName)
        {
            if (astronauts.FindByName(astronautName) is null)
            {
                throw new InvalidOperationException("Astronaut {astronautName} doesn't exists!");
            }

            astronauts.Remove(astronauts.FindByName(astronautName));
            return $"Astronaut {astronautName} was retired!";
        }
    }
}
