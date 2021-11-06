namespace _01.Classroom
{
    public class Student
    {
        public Student(int id, string name, int height, int age, string town)
        {
            this.Id = id;
            this.Name = name;
            this.Height = height;
            this.Age = age;
            this.Town = town;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int Height { get; set; }

        public int Age { get; set; }

        public string Town { get; set; }

        public Class Class { get; set; }
    }
}