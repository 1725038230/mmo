﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Entities;
using SkillBridge.Message;

namespace Managers
{
    interface IEntityNotify
    {
        void OnEntityRemoved();
        void OnEntityEvent(EntityEvent @event);
        void OnEntityChanged(Entity entity);
    }
    class EntityManager:Singleton<EntityManager>
    {
        Dictionary<int, Entity> entities = new Dictionary<int, Entity>();
        Dictionary<int, IEntityNotify> notifiers = new Dictionary<int, IEntityNotify>();

        public void RegisterEntityChanegNotify(int entityId,IEntityNotify notify)
        {
            this.notifiers[entityId] = notify;
        }

        public void AddEntity(Entity entity)
        {
            entities[entity.entityId] = entity;
        }

        public void RemoveEntity(NEntity entity)
        {
            entities.Remove(entity.Id);
            if(notifiers.ContainsKey(entity.Id))
            {
                notifiers[entity.Id].OnEntityRemoved();
                notifiers.Remove(entity.Id);
            }
        }

        internal void OnEntitySync(NEntitySync data)
        {
            Entity entity = null;
            entities.TryGetValue(data.Id,out entity);
            if(entity!=null)
            {
                if (data.Entity != null)
                    entity.EntityData = data.Entity;
            }
            if(notifiers.ContainsKey(data.Id))
            {
                notifiers[entity.entityId].OnEntityChanged(entity);
                notifiers[entity.entityId].OnEntityEvent(data.Event);
            }
        }
    }
}
