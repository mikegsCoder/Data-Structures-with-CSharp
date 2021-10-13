namespace _01.Microsystem
{
    public class Microsystems : IMicrosystem
    {
        private Dictionary<int,Computer> computers;

        public Microsystems()
        {
            this.computers = new Dictionary<int, Computer>();
        }

        public void CreateComputer(Computer computer)
        {
            if (this.computers.ContainsKey(computer.Number))
            {
                throw new ArgumentException();
            }

            this.computers.Add(computer.Number, computer);
        }

        public bool Contains(int number)
        {
            return this.computers.ContainsKey(number);
        }

        public int Count()
        {
            return this.computers.Count;
        }

        public Computer GetComputer(int number)
        {
            if (!this.Contains(number))
            {
                throw new ArgumentException();
            }

            return this.computers[number];
        }

        public void Remove(int number)
        {
            this.GetComputer(number);

            this.computers.Remove(number);
        }

        public void RemoveWithBrand(Brand brand)
        {
            var toRemove = this.GetAllFromBrand(brand);

            if (toRemove.Count() == 0)
            {
                throw new ArgumentException();
            }

            foreach (var computer in this.GetAllFromBrand(brand))
            {
                this.computers.Remove(computer.Number);
            }
        }

        public void UpgradeRam(int ram, int number)
        {
            var toUpgrade = this.GetComputer(number);

            toUpgrade.RAM = Math.Max(toUpgrade.RAM, ram);
        }

        public IEnumerable<Computer> GetAllFromBrand(Brand brand)
        {
            return this.computers.Values.Where(x => x.Brand == brand).OrderByDescending(x => x.Price);
        }

        public IEnumerable<Computer> GetAllWithScreenSize(double screenSize)
        {
            return this.computers.Values
                .Where(x => x.ScreenSize == screenSize)
                .OrderByDescending(x => x.Number);
        }

        public IEnumerable<Computer> GetAllWithColor(string color)
        {
            return this.computers.Values
                .Where(x => x.Color == color)
                .OrderByDescending(x => x.Price);
        }

        public IEnumerable<Computer> GetInRangePrice(double minPrice, double maxPrice)
        {
            return this.computers.Values
                .Where(x => x.Price >= minPrice && x.Price <= maxPrice)
                .OrderByDescending(x => x.Price);
        }
    }
}
