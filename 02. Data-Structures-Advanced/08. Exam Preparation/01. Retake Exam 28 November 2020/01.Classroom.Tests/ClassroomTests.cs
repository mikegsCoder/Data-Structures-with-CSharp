namespace _01.Classroom.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;
    using _01.Classroom;

    public class ClassroomTests
    {
        private Class c1;
        private Class c2;
        private Class c3;

        private Student s1;
        private Student s2;
        private Student s3;
        private Student s4;
        private Student s5;

        private IClassroom classroom;

        [SetUp]
        public void Setup()
        {
            this.c1 = new Class("a");
            this.c2 = new Class("b");
            this.c3 = new Class("c");

            this.s1 = new Student(1, "a", 150, 12, "plovdiv");
            this.s2 = new Student(2, "a", 120, 10, "sliven");
            this.s3 = new Student(3, "b", 170, 18, "asenovgrad");
            this.s4 = new Student(4, "b", 140, 18, "gabrovo");
            this.s5 = new Student(5, "c", 130, 117, "sofia");

            this.classroom = new Classroom();
        }

        [Test]
        public void TestCreateClass()
        {
            this.classroom.CreateClass(c1.Name);
            Assert.True(this.classroom.Exists(c1));
        }

        [Test]
        public void TestNotCreatingClassShouldNotExist()
        {
            Assert.False(this.classroom.Exists(c1));
        }

        [Test]
        public void TestAddStudent()
        {
            this.classroom.CreateClass(c1.Name);
            this.classroom.RegisterStudent(s1, c1);

            Assert.True(this.classroom.Exists(s1));
        }

        [Test]
        public void TestAddStudentAndGet()
        {
            this.classroom.CreateClass(c1.Name);
            this.classroom.RegisterStudent(s1, c1);

            var s = this.classroom.GetStudent(s1.Name, c1);
            Assert.AreEqual(s, s1);
        }

        [Test]
        public void TestAddStudentAndGetFromAnotherClassThrowException()
        {
            this.classroom.CreateClass(c1.Name);
            this.classroom.RegisterStudent(s1, c1);

            Assert.Throws<ArgumentException>(() => this.classroom.GetStudent(s1.Name, c2));
        }


        [Test]
        public void TestAddStudentAndGetWithDifferentNameThrowException()
        {
            this.classroom.CreateClass(c1.Name);
            this.classroom.RegisterStudent(s1, c1);

            Assert.Throws<ArgumentException>(() => this.classroom.GetStudent(s5.Name, c1));
        }

        [Test]
        public void TestAddStudentWithSameId()
        {
            this.classroom.CreateClass(c1.Name);
            this.classroom.RegisterStudent(s1, c1);

            Assert.Throws<ArgumentException>(() => this.classroom.RegisterStudent(s1, c1));
        }

        [Test]
        public void TestExistsStudent()
        {
            this.classroom.CreateClass(c1.Name);
            this.classroom.RegisterStudent(s1, c1);

            Assert.True(this.classroom.Exists(s1));
        }

        [Test]
        public void TestExistsStudentFalse()
        {
            this.classroom.CreateClass(c1.Name);
            this.classroom.RegisterStudent(s1, c1);

            Assert.False(this.classroom.Exists(s2));
        }

        [Test]
        public void TestExistsClass()
        {
            this.classroom.CreateClass(c1.Name);
            this.classroom.RegisterStudent(s1, c1);

            Assert.True(this.classroom.Exists(c1));
        }

        [Test]
        public void TestExistsClassFalse()
        {
            this.classroom.CreateClass(c1.Name);
            this.classroom.RegisterStudent(s1, c1);

            Assert.False(this.classroom.Exists(c2));
        }

        [Test]
        public void TestGetStudentUnexistent()
        {
            this.classroom.CreateClass(c1.Name);
            Assert.Throws<ArgumentException>(() => this.classroom.GetStudent(s5.Name, c1));
        }

        [Test]
        public void TestGetStudentUnexistentClass()
        {
            this.classroom.CreateClass(c1.Name);
            this.classroom.RegisterStudent(s1, c1);
            Assert.Throws<ArgumentException>(() => this.classroom.GetStudent(s1.Name, c2));
        }

        [Test]
        public void TestGetStudentFromWrongClass()
        {
            this.classroom.CreateClass(c1.Name);
            this.classroom.CreateClass(c2.Name);
            this.classroom.RegisterStudent(s1, c1);
            Assert.Throws<ArgumentException>(() => this.classroom.GetStudent(s1.Name, c2));
        }

        [Test]
        public void TestGetStudentCorrect()
        {
            this.classroom.CreateClass(c1.Name);
            this.classroom.RegisterStudent(s1, c1);

            Assert.AreEqual(s1, this.classroom.GetStudent(s1.Name, c1));
        }

        [Test]
        public void TestRemoveStudentUnexistent()
        {
            this.classroom.CreateClass(c1.Name);
            Assert.Throws<ArgumentException>(() => this.classroom.RemoveStudent(s5.Name, c1));
        }

        [Test]
        public void TestRemoveStudentUnexistentClass()
        {
            this.classroom.CreateClass(c1.Name);
            this.classroom.RegisterStudent(s1, c1);
            Assert.Throws<ArgumentException>(() => this.classroom.RemoveStudent(s1.Name, c2));
        }

        [Test]
        public void TestRemoveStudentWrongClass()
        {
            this.classroom.CreateClass(c1.Name);
            this.classroom.CreateClass(c2.Name);
            this.classroom.RegisterStudent(s1, c1);
            Assert.Throws<ArgumentException>(() => this.classroom.RemoveStudent(s1.Name, c2));
        }

        [Test]
        public void TestRemoveStudentCorrect()
        {
            this.classroom.CreateClass(c1.Name);
            this.classroom.CreateClass(c2.Name);
            this.classroom.RegisterStudent(s1, c1);
            Assert.AreEqual(s1, this.classroom.RemoveStudent(s1.Name, c1));
        }

        [Test]
        public void TestMoveClassV1()
        {
            Assert.Throws<ArgumentException>(() => this.classroom.MoveClass(c1, c2, s1.Name));
        }

        [Test]
        public void TestMoveClassV2()
        {
            this.classroom.CreateClass(c1.Name);
            this.classroom.RegisterStudent(s1, c1);
            Assert.Throws<ArgumentException>(() => this.classroom.MoveClass(c1, c2, s1.Name));
        }

        [Test]
        public void TestMoveClassV3Correct()
        {
            this.classroom.CreateClass(c1.Name);
            this.classroom.CreateClass(c2.Name);
            this.classroom.RegisterStudent(s1, c1);
            this.classroom.MoveClass(c1, c2, s1.Name);

            Assert.AreEqual(s1, this.classroom.GetStudent(s1.Name, c2));
        }

        [Test]
        public void TestMoveClassV4()
        {
            this.classroom.CreateClass(c1.Name);
            this.classroom.CreateClass(c2.Name);
            this.classroom.RegisterStudent(s1, c1);
            this.classroom.RegisterStudent(s2, c2);
            Assert.Throws<ArgumentException>(() => this.classroom.MoveClass(c1, c2, s1.Name));
        }

        [Test]
        public void TestGetStudentByClassNoStudents()
        {
            this.classroom.CreateClass(c1.Name);
            Assert.AreEqual(0, this.classroom.GetStudentsByClass(c1).ToList().Count);
        }

        [Test]
        public void TestGetStudentByClassNoClass()
        {
            Assert.Throws<ArgumentException>(() => this.classroom.GetStudentsByClass(c1));
        }

        [Test]
        public void TestGetStudentByClassCorrect()
        {
            this.classroom.CreateClass(c1.Name);
            this.classroom.RegisterStudent(s1, c1);
            this.classroom.RegisterStudent(s3, c1);
            this.classroom.RegisterStudent(s5, c1);

            var result = this.classroom.GetStudentsByClass(c1).ToList();
            var expected = new List<Student>() { s1, s3, s5 };
            CollectionAssert.AreEquivalent(expected, result);
        }

        [Test]
        public void TestGetStudentByTownNoStudent()
        {
            this.classroom.CreateClass(c1.Name);
            var res = this.classroom.GetStudentByTown("sofia").ToList();

            Assert.AreEqual(0, res.Count);
        }

        [Test]
        public void TestGetStudentByTownOnlyOneStudent()
        {
            this.classroom.CreateClass(c1.Name);
            this.classroom.RegisterStudent(s1, c1);
            this.classroom.RegisterStudent(s3, c1);
            var res = this.classroom.GetStudentByTown(s1.Town).ToList();

            Assert.AreEqual(s1, res[0]);
        }

        [Test]
        public void TestGetStudentByTownOnlyManyStudent()
        {
            this.classroom.CreateClass(c1.Name);
            this.classroom.RegisterStudent(s1, c1);
            var ss1 = new Student(11, "asd", 123, 123, s1.Town);
            var ss2 = new Student(12, "asdf", 123, 123, s1.Town);
            var ss3 = new Student(13, "asdfg", 123, 123, s1.Town);
            this.classroom.RegisterStudent(ss1, c1);
            this.classroom.RegisterStudent(ss2, c1);
            this.classroom.RegisterStudent(ss3, c1);
            var res = this.classroom.GetStudentByTown(s1.Town).ToList();
            var exp = new List<Student>() { s1, ss1, ss2, ss3 };

            Assert.AreEqual(4, res.Count);
            CollectionAssert.AreEquivalent(res, exp);
        }

        [Test]
        public void TestGetAllOrderedByHeightDescThenByNameAscThenByTownNameDescEmpty()
        {
            var res = this.classroom.GetAllOrderedByHeightDescThenByNameAscThenByTownNameDesc().ToList();
            Assert.AreEqual(0, res.Count);
        }

        [Test]
        public void TestGetAllOrderedByHeightDescThenByNameAscThenByTownNameDescOne()
        {
            this.classroom.CreateClass(c1.Name);
            this.classroom.RegisterStudent(s1, c1);

            var res = this.classroom.GetAllOrderedByHeightDescThenByNameAscThenByTownNameDesc().ToList();
            Assert.AreEqual(1, res.Count);
            Assert.AreEqual(s1, res[0]);
        }

        [Test]
        public void TestGetAllOrderedByHeightDescThenByNameAscThenByTownNameDescMany()
        {
            this.classroom.CreateClass(c1.Name);
            this.classroom.CreateClass(c2.Name);
            this.classroom.CreateClass(c3.Name);


            this.classroom.RegisterStudent(s1, c1);
            this.classroom.RegisterStudent(s2, c2);
            this.classroom.RegisterStudent(s3, c3);
            this.classroom.RegisterStudent(s4, c1);
            this.classroom.RegisterStudent(s5, c2);

            var res = this.classroom.GetAllOrderedByHeightDescThenByNameAscThenByTownNameDesc().ToList();
            var exp = new List<Student>() { s3, s1, s4, s5, s2 };
            CollectionAssert.AreEqual(res, exp);
        }

        [Test]
        public void TestGetStudentByAgeNoStudents()
        {
            var res = this.classroom.GetStudentByAge(1).ToList();
            Assert.AreEqual(0, res.Count);
        }


        [Test]
        public void TestGetStudentByAgeOneStudent()
        {
            this.classroom.CreateClass(c1.Name);
            this.classroom.RegisterStudent(s1, c1);

            var res = this.classroom.GetStudentByAge(s1.Age).ToList();
            Assert.AreEqual(1, res.Count);
            Assert.AreEqual(s1, res[0]);
        }

        [Test]
        public void TestGetStudentByAgeManyStudents()
        {
            this.classroom.CreateClass(c1.Name);

            var sss1 = new Student(123, "asd", 123, 1, "sofia");
            var sss2 = new Student(12, "asdf", 123, 1, "sofia");
            var sss3 = new Student(1245, "asdfg", 123, 1, "sofia");
            var sss4 = new Student(121233, "asdfgh", 123, 1, "sofia");
            this.classroom.RegisterStudent(sss1, c1);
            this.classroom.RegisterStudent(sss2, c1);
            this.classroom.RegisterStudent(sss3, c1);
            this.classroom.RegisterStudent(sss4, c1);
            this.classroom.RegisterStudent(s1, c1);

            var res = this.classroom.GetStudentByAge(1);
            var exp = new List<Student>() { sss1, sss2, sss3, sss4 };
            CollectionAssert.AreEquivalent(res, exp);
        }
    }
}