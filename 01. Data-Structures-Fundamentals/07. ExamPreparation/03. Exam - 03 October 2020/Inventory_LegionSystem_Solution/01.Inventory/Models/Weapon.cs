namespace _01.Inventory.Models
{
    using _01.Inventory.Interfaces;

    public abstract class Weapon : IWeapon
    {
        private int ammunition;

        public Weapon(int id, int maxCapacity, int ammunition)
        {
            this.Id = id;
            this.MaxCapacity = maxCapacity;
            this.Ammunition = ammunition;
        }

        public int Id { get; private set; }

        public int Ammunition 
        {
            get { return this.ammunition; }
            set
            {
                if (value > this.MaxCapacity)
                {
                    value = MaxCapacity;
                }

                this.ammunition = value;
            }
        }
        public int MaxCapacity { get; set; }

        public Category Category { get; set; }

        public int CompareTo(object obj)
        {
            var other = (Weapon)obj;

            return other.Id - this.Id;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null) return false;
            Weapon that = (Weapon)obj;
            return Id == that.Id;
        }
    }
}
