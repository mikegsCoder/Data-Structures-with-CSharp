public class Card
{
    public Card(string name, int damage, int score, int level)
    {
        this.Name = name;
        this.Damage = damage;
        this.Score = score;
        this.Level = level;
        this.Health = 20;
    }

    public string Name { get; }

    public int Damage { get; set; }

    public int Score { get; set; }

    public int Health { get; set; }

    public int Level { get; set; }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }

        return this.Name == ((Card) obj).Name;
    }

    public override int GetHashCode()
    {
        return this.Name.GetHashCode();
    }

    public override string ToString()
    {
        return $"{this.Name} {this.Score}";
    }
}