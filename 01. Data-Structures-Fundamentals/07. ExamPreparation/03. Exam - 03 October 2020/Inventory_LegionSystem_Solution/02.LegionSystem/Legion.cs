namespace _02.LegionSystem
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Wintellect.PowerCollections;
    using _02.LegionSystem.Interfaces;

    public class Legion : IArmy
    {
        OrderedSet<IEnemy> enemies;

        public Legion()
        {
            this.enemies = new OrderedSet<IEnemy>();
        }

        public int Size => this.enemies.Count;

        public bool Contains(IEnemy enemy)
        {
            return this.enemies.Contains(enemy);
        }

        public void Create(IEnemy enemy)
        {
            this.enemies.Add(enemy);
        }

        public IEnemy GetByAttackSpeed(int speed)
        {
            return this.enemies.FirstOrDefault(x => x.AttackSpeed == speed);
        }

        private void CheckIsEmpty()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException("Legion has no enemies!");
            }
        }

        public List<IEnemy> GetFaster(int speed)
        {
            return this.enemies.Where(x => x.AttackSpeed > speed).ToList();
        }

        public IEnemy GetFastest()
        {
            this.CheckIsEmpty();

            return this.enemies.GetFirst();
        }

        public IEnemy[] GetOrderedByHealth()
        {
            return this.enemies.OrderByDescending(x => x.Health).ToArray();
        }

        public List<IEnemy> GetSlower(int speed)
        {
            return this.enemies.Where(x => x.AttackSpeed < speed).ToList();
        }

        public IEnemy GetSlowest()
        {
            this.CheckIsEmpty();

            return this.enemies.GetLast();
        }

        public void ShootFastest()
        {
            this.CheckIsEmpty();

            this.enemies.RemoveFirst();
        }

        public void ShootSlowest()
        {
            this.CheckIsEmpty();

            this.enemies.RemoveLast();
        }
    }
}
