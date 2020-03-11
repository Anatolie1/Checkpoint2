using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Linq;

namespace WCS
{
    [TestFixture]
    public class TestCreatePerson
    {
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public void TestStudentCreation()
        {
            Person student = FactoryPerson.Create("john", false, false);

            Assert.AreSame(typeof(Student), student.GetType());
        }

        [Test]
        public void TestTeacherCreation()
        {
            Person teacher = FactoryPerson.Create("Loh", false, true);

            Assert.AreSame(typeof(Teacher), teacher.GetType());
        }

        [Test]
        public void TestTeacherBossCreation()
        {
            Person teacherboss = FactoryPerson.Create("Boss", true, true);

            Assert.AreSame(typeof(Teacher), teacherboss.GetType());
        }


    }
}
