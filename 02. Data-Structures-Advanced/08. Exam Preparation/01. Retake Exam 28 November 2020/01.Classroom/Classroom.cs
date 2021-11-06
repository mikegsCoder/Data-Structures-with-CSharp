namespace _01.Classroom
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Classroom : IClassroom
    {
        Dictionary<int, Student> students = new Dictionary<int, Student>();
        Dictionary<string, Class> classes = new Dictionary<string, Class>();
        Dictionary<string, HashSet<Student>> byTown = new Dictionary<string, HashSet<Student>>();
        Dictionary<int, HashSet<Student>> byHeight = new Dictionary<int, HashSet<Student>>();
        Dictionary<int, HashSet<Student>> byAge = new Dictionary<int, HashSet<Student>>();

        public void RegisterStudent(Student student, Class classToAdd)
        {
            if (students.ContainsKey(student.Id) || !classes.ContainsKey(classToAdd.Name))
            {
                throw new ArgumentException();
            }

            if (classToAdd.Students.ContainsKey(student.Name))
            {
                throw new ArgumentException();
            }

            student.Class = classToAdd;
            students.Add(student.Id, student);

            classToAdd.Students.Add(student.Name, student);
            classes[classToAdd.Name] = classToAdd;

            if (!byTown.ContainsKey(student.Town))
            {
                byTown[student.Town] = new HashSet<Student>();
            }

            byTown[student.Town].Add(student);

            if (!byHeight.ContainsKey(student.Height))
            {
                byHeight[student.Height] = new HashSet<Student>();
            }

            byHeight[student.Height].Add(student);

            if (!byAge.ContainsKey(student.Age))
            {
                byAge[student.Age] = new HashSet<Student>();
            }

            byAge[student.Age].Add(student);
        }

        public void CreateClass(string name)
        {
            if (classes.ContainsKey(name))
            {
                throw new ArgumentException();
            }

            classes.Add(name, new Class(name));
        }

        public bool Exists(Student student)
        {
            return students.ContainsKey(student.Id);
        }

        public bool Exists(Class classToSearch)
        {
            return classes.ContainsKey(classToSearch.Name);
        }

        public Student GetStudent(string name, Class studentClass)
        {
            if (!classes.ContainsKey(studentClass.Name) || !classes[studentClass.Name].Students.ContainsKey(name))
            {
                throw new ArgumentException();
            }

            return classes[studentClass.Name].Students[name];
        }

        public Student RemoveStudent(string name, Class studentClass)
        {
            var studentToRemove = GetStudent(name, studentClass);

            students.Remove(studentToRemove.Id);
            classes[studentClass.Name].Students.Remove(name);

            byTown[studentToRemove.Town].Remove(studentToRemove);

            if (byTown[studentToRemove.Town].Count == 0)
            {
                byTown.Remove(studentToRemove.Town);
            }

            byHeight[studentToRemove.Height].Remove(studentToRemove);

            if (byHeight[studentToRemove.Height].Count == 0)
            {
                byHeight.Remove(studentToRemove.Height);
            }

            byAge[studentToRemove.Age].Remove(studentToRemove);

            if (byAge[studentToRemove.Age].Count == 0)
            {
                byAge.Remove(studentToRemove.Age);
            }

            return studentToRemove;
        }

        public IEnumerable<Student> GetStudentsByClass(Class studentsClass)
        {
            if (!classes.ContainsKey(studentsClass.Name))
            {
                throw new ArgumentException();
            }

            return classes[studentsClass.Name].Students.Values.ToList();
        }

        public IEnumerable<Student> GetStudentByTown(string town)
        {
            if (!byTown.ContainsKey(town))
            {
                return new List<Student>();
            }

            return byTown[town].ToList();
        }

        public void MoveClass(Class oldClass, Class newClass, string studentName)
        {
            if (!classes.ContainsKey(oldClass.Name) || !classes.ContainsKey(newClass.Name) || !classes[oldClass.Name].Students.ContainsKey(studentName))
            {
                throw new ArgumentException();
            }

            var studentToMove = classes[oldClass.Name].Students[studentName];
            studentToMove.Class = newClass;
            classes[oldClass.Name].Students.Remove(studentName);
            classes[newClass.Name].Students.Add(studentName, studentToMove);
        }

        public IEnumerable<Student> GetAllOrderedByHeightDescThenByNameAscThenByTownNameDesc()
        {            
            return students.Select(s => s.Value).OrderByDescending(s => s.Height).ThenBy(s => s.Name).ThenByDescending(s => s.Town).ToList();
        }

        public IEnumerable<Student> GetStudentByAge(int age)
        {
            if (!byAge.ContainsKey(age))
            {
                return new List<Student>();
            }

            return byAge[age].ToList();
        }

        public IEnumerable<Student> GetStudentsInHeightRange(int low, int hi)
        {
            return byHeight.Where(s => low <= s.Key && s.Key <= hi).SelectMany(s => s.Value).ToList();
        }
    }
}