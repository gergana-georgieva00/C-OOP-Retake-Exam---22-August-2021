﻿using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceStation.Repositories
{
    public class AstronautRepository : IRepository<IAstronaut>
    {
        private List<IAstronaut> models;

        public AstronautRepository()
        {
            models = new List<IAstronaut>();
        }

        public IReadOnlyCollection<IAstronaut> Models => models.AsReadOnly();

        public void Add(IAstronaut model)
        {
            models.Add(model);
        }

        public IAstronaut FindByName(string name)
            => models.Find(m => m.Name == name);

        public bool Remove(IAstronaut model)
            => models.Remove(model);
    }
}
