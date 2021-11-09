public class Competitor
{
    public Competitor(int id, string name)
    {
        this.Id = id;
        this.Name = name;
        this.TotalScore = 0;
    }

    public int Id { get; }

    public string Name { get; set; }

    public long TotalScore { get; set; }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }

        return this.Id == ((Competitor) obj).Id;
    }

    public override int GetHashCode()
    {
        return this.Id.GetHashCode();
    }

    public override string ToString()
    {
        return $"{this.Name} {this.TotalScore}";
    }
}
