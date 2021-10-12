namespace _01.Loader
{
    using _01.Loader.Interfaces;
    using _01.Loader.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Loader : IBuffer
    {
        private List<IEntity> entities;

        public Loader()
        {
            this.entities = new List<IEntity>();
        }

        public int EntitiesCount => this.entities.Count;

        public void Add(IEntity entity)
        {
            this.entities.Add(entity);
        }

        public void Clear()
        {
            this.entities.Clear();
        }

        public bool Contains(IEntity entity)
        {
            return this.FindById(entity.Id) != null;
        }

        public IEntity Extract(int id)
        {
            var result = this.FindById(id);

            if (result != null)
            {
                this.entities.Remove(result);
            }

            return result;
        }

        private IEntity FindById(int id)
        {
            foreach (var entity in this.entities)
            {
                if (entity.Id == id)
                {
                    return entity;
                }
            }

            return null;
        }

        public IEntity Find(IEntity entity)
        {
            return this.FindById(entity.Id);
        }

        public List<IEntity> GetAll()
        {
            return new List<IEntity>(this.entities);
        }

        public IEnumerator<IEntity> GetEnumerator()
        {
            foreach (var entity in this.entities)
            {
                yield return entity;
            }
        }

        public void RemoveSold()
        {
           var temp = new List<IEntity>();

            foreach (var entity in this.entities)
            {
                if (entity.Status != BaseEntityStatus.Sold)
                {
                    temp.Add(entity);
                }
            }

            this.entities = temp;
        }

        public void Replace(IEntity oldEntity, IEntity newEntity)
        {
            int index = this.ValidateEntityIndex(oldEntity);
            this.entities[index] = newEntity;
        }

        public List<IEntity> RetainAllFromTo(BaseEntityStatus lowerBound, BaseEntityStatus upperBound)
        {
            int lower = (int)lowerBound;
            int upper = (int)upperBound;

            var list = new List<IEntity>(this.EntitiesCount);

            foreach (var entity in entities)
            {
                if (IsStatusInRange(entity.Status, lower, upper))
                {
                    list.Add(entity);
                }
            }

            return list;
        }

        private bool IsStatusInRange(BaseEntityStatus status, int lower, int upper)
        {
            if ((int)status < lower)
            {
                return false;
            }
            if ((int)status > upper)
            {
                return false;
            }

            return true;
        }

        public void Swap(IEntity first, IEntity second)
        {
            int firstIndex = this.ValidateEntityIndex(first);
            int secondIndex = this.ValidateEntityIndex(second);

            this.entities[firstIndex] = second;
            this.entities[secondIndex] = first;
        }

        private int ValidateEntityIndex(IEntity oldEntity)
        {
            int index = this.entities.IndexOf(this.FindById(oldEntity.Id));

            if (index == -1)
            {
                throw new InvalidOperationException("Entity not found");
            }

            return index;
        }

        public IEntity[] ToArray()
        {
            return this.entities.ToArray();
        }

        public void UpdateAll(BaseEntityStatus oldStatus, BaseEntityStatus newStatus)
        {
            foreach (var entity in this.entities)
            {
                if (entity.Status == oldStatus)
                {
                    entity.Status = newStatus;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
