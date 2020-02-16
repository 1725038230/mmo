﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using GameServer.Entities;

namespace GameServer.Managers
{
    class EntityManager:Singleton<EntityManager>
    {
        private int idx = 0;
        public List<Entity> AllEentities = new List<Entity>();
        public Dictionary<int, List<Entity>> MapEntities = new Dictionary<int, List<Entity>>();

        public void AddEntity(int mapId,Entity entity)
        {
            AllEentities.Add(entity);
            entity.EntityData.Id = ++this.idx;
            List<Entity> entities = null; 
            if(!MapEntities.TryGetValue(mapId,out entities))
            {
                entities = new List<Entity>();
                MapEntities[mapId] = entities;
            }
            entities.Add(entity);
        }

        public void RemoveEntity(int mapId,Entity entity)
        {
            this.AllEentities.Remove(entity);
            this.MapEntities[mapId].Remove(entity);
        }
    }
}
