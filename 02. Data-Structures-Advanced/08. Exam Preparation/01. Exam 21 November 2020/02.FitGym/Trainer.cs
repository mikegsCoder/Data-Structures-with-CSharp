namespace _02.FitGym
{
    public class Trainer
    {
        public Trainer(int id, string name, int popularity)
        {
            this.Id = id;
            this.Name = name;
            this.Popularity = popularity;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int Popularity { get; set; }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null) return false;
            Trainer that = (Trainer)obj;
            return Id == that.Id;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}