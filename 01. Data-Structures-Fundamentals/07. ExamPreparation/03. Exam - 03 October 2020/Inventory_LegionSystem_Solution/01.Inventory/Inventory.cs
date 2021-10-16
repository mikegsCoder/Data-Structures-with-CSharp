namespace _01.Inventory
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using _01.Inventory.Models;
    using _01.Inventory.Interfaces;

    public class Inventory : IHolder
    {
        private List<IWeapon> weapons;

        public Inventory()
        {
            this.weapons = new List<IWeapon>();
        }

        public int Capacity => this.weapons.Count;

        public void Add(IWeapon weapon)
        {
            this.weapons.Add(weapon);
        }

        public void Clear()
        {
            this.weapons.Clear();
        }

        public bool Contains(IWeapon weapon)
        {
            return this.weapons.Contains(weapon);
        }

        public void EmptyArsenal(Category category)
        {
            int cat = (int)category;

            for (int i = 0; i < this.weapons.Count; i++)
            {
                if ((int)this.weapons[i].Category == cat)
                {
                    this.weapons[i].Ammunition = 0;
                }
            }
        }

        public bool Fire(IWeapon weapon, int ammunition)
        {
            if (!this.Contains(weapon))
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }

            if (ammunition <= weapon.Ammunition)
            {
                weapon.Ammunition -= ammunition;
                return true;
            }

            return false;
        }

        public IWeapon GetById(int id)
        {
            foreach (var entity in weapons)
            {
                if (entity.Id == id)
                {
                    return entity;
                }
            }

            return null;
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var weapon in this.weapons)
            {
                yield return weapon;
            }
        }

        public int Refill(IWeapon weapon, int ammunition)
        {
            if (!this.Contains(weapon))
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }

            weapon.Ammunition += ammunition;
            return weapon.Ammunition;
        }
        
        public IWeapon RemoveById(int id)
        {
            var result = this.GetById(id);

            if (result == null)
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }

            this.weapons.Remove(result);
            return result;
        }

        public int RemoveHeavy()
        {
            List<IWeapon> result = new List<IWeapon>();

            for (int i = 0; i < this.weapons.Count; i++)
            {
                if (this.weapons[i].Category != Category.Heavy)
                {
                    result.Add(this.weapons[i]);
                }
            }

            int count = this.weapons.Count - result.Count;
            this.weapons = result;

            return count;
        }

        public List<IWeapon> RetrieveAll()
        {
            return new List<IWeapon>(this.weapons);
        }

        public List<IWeapon> RetriveInRange(Category lower, Category upper)
        {
            int low = (int)lower;
            int up = (int)upper;

            List<IWeapon> result = new List<IWeapon>();

            for (int i = 0; i < this.weapons.Count; i++)
            {
                if ((int)weapons[i].Category >= low && (int)weapons[i].Category <= up)
                {
                    result.Add(weapons[i]);
                }
            }

            return result;
        }

        public void Swap(IWeapon firstWeapon, IWeapon secondWeapon)
        {
            int firstIndex = this.ValidateEntityIndex(firstWeapon);
            int secondIndex = this.ValidateEntityIndex(secondWeapon);

            this.weapons[firstIndex] = secondWeapon;
            this.weapons[secondIndex] = firstWeapon;
        }

        private int ValidateEntityIndex(IWeapon weapon)
        {
            int index = this.weapons.IndexOf(this.FindById(weapon.Id));

            if (index == -1)
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }

            return index;
        }

        private IWeapon FindById(int id)
        {
            for (int i = 0; i < weapons.Count; i++)
            {
                if (weapons[i].Id == id)
                {
                    return weapons[i];
                }
            }

            return null;
        }
    }
}
