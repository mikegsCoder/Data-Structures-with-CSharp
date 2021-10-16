namespace _02.LegionSystem.Models
{
    using _02.LegionSystem.Interfaces;

    public class Enemy : IEnemy
    {
        public Enemy(int attackSpeed, int health)
        {
            this.AttackSpeed = attackSpeed;
            this.Health = health;
        }

        public int AttackSpeed { get; set; }

        public int Health { get; set; }

        public int CompareTo(object obj)
        {
            var other = (IEnemy)obj;

            return other.AttackSpeed - this.AttackSpeed;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var element = (IEnemy)obj;
            var equals = Equals(this.AttackSpeed, element.AttackSpeed);

            return equals;
        }

        public override int GetHashCode()
        {
            return this.AttackSpeed.GetHashCode();
        }
    }
}
