﻿using System;

namespace EcsRx.Entities
{
    public class DefaultEntityFactory : IEntityFactory
    {
        public IIdPool IdPool { get; }

        public DefaultEntityFactory(IIdPool idPool)
        {
            IdPool = idPool;
        }

        public int GetId(int? id = null)
        {
            if(!id.HasValue)
            { return IdPool.Claim(); }

            IdPool.ClaimSpecific(id.Value);
            return id.Value;
        }
        
        public IEntity Create(int? id = null)
        {
            if(id.HasValue && id.Value == 0)
            { throw new ArgumentException("id must be null or > 0"); }
            
            var usedId = GetId(id);
            return new Entity(usedId);
        }

        public void Destroy(int entityId)
        { IdPool.Free(entityId); }
    }
}