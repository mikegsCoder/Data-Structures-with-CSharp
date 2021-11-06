namespace _01.Classroom
{
    using System.Collections.Generic;

    public interface IClassroom
    {
        void RegisterStudent(Student student, Class classToAdd);

        void CreateClass(string name);

        bool Exists(Student student);

        bool Exists(Class classToSearch);

        Student GetStudent(string name, Class studentClass);

        Student RemoveStudent(string name, Class studentClass);

        IEnumerable<Student> GetStudentsByClass(Class studentsClass);

        IEnumerable<Student> GetStudentByTown(string town);

        void MoveClass(Class oldClass, Class newClass, string studentName);

        IEnumerable<Student> GetAllOrderedByHeightDescThenByNameAscThenByTownNameDesc();

        IEnumerable<Student> GetStudentByAge(int age);

        IEnumerable<Student> GetStudentsInHeightRange(int low, int hi);
    }
}