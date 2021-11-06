namespace _02.DistrictManager
{
    public  class District 
    {
        public District(int id, string name, int sqMeters) 
        {
            this.Id = id;
            this.Name = name;
            this.SqMeters = sqMeters;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int SqMeters { get; set; }

        public Country Country { get; set; }
    }
}