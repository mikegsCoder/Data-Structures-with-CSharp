using System.Collections.Generic;

namespace _01.Classroom
{
    public class Class
    {
        public Class(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }

        public Dictionary<string, Student> Students { get; set; } = new Dictionary<string, Student>();
    }
}