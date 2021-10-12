using _02.Data.Interfaces;
using System;
using System.Collections.Generic;
using Wintellect.PowerCollections;
using System.Linq;
using _02.Data.Models;

namespace _02.Data
{
    public class Data : IRepository
    {
        private Deque<IEntity> entities;

        public Data()
        {
            this.entities = new Deque<IEntity>();
        }

        public Data(Data copy)
        {
            this.entities = copy.entities;
        }

        public int Size => this.entities.Count;

        public void Add(IEntity entity)
        {
            this.entities.Add(entity);

            int parentId = (int)entity.ParentId;
            var parent = this.GetById(parentId);

            if (parent != null)
            {
                parent.Children.Add(entity);
            }
        }

        public IRepository Copy()
        {
            Data result = (Data)this.MemberwiseClone();

            return new Data(result);
        }

        public IEntity DequeueMostRecent()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException("Operation on empty Data");
            }

            return this.entities.RemoveFromBack();
        }

        public List<IEntity> GetAll()
        {
            return new List<IEntity>(this.entities);
        }

        public List<IEntity> GetAllByType(string type)
        {
            if (type != typeof(Invoice).Name && type != typeof(StoreClient).Name && type != typeof(User).Name)
            {
                throw new InvalidOperationException("Invalid type: " + type);
            }

            var result = new List<IEntity>();

            foreach (var entity in this.entities)
            {
                if (entity.GetType().Name == type)
                {
                    result.Add(entity);
                }
            }

            return result;
        }

        public IEntity GetById(int id)
        {            
            if (id < 0 || id >= this.Size)
            {
                return null;
            }

            return entities[id];
        }

        public List<IEntity> GetByParentId(int parentId)
        {
            var parent = this.GetById(parentId);
            return parent != null ? parent.Children : new List<IEntity>();
        }
        
        public IEntity PeekMostRecent()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException("Operation on empty Data");
            }

            return this.entities.Last();
        }
    }
}
