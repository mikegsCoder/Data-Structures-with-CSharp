namespace _02.FitGym
{
    using System;

    public class Member
    {
        public Member(int id, string name, DateTime registrationDate, int visits)
        {
            this.Id = id;
            this.Name = name;
            this.RegistrationDate = registrationDate;
            this.Visits = visits;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime RegistrationDate { get; set; }

        public int Visits { get; set; }

        public Trainer Trainer { get; set; }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null) return false;
            Member that = (Member)obj;
            return Id == that.Id;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}