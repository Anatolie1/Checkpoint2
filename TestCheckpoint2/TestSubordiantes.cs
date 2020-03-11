using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Linq;


namespace WCS
{
	[TestFixture]
	class TestSubordiantes
	{
		Teacher AAA = new Teacher("AAA", true, true);
		Teacher Bbb = new Teacher("Bbb", true, false);
		Teacher Ccc = new Teacher("Ccc", true, false);
		Student s1 = new Student("s1");
		Student s2 = new Student("s2");
		Student s3 = new Student("s3");
		Student s4 = new Student("s4");

		[SetUp]
		public void Setup()
		{
			AAA.Subordinates.Add(Bbb);
			AAA.Subordinates.Add(Ccc);
			Bbb.Subordinates.Add(s1);
			Bbb.Subordinates.Add(s2);
			Bbb.Subordinates.Add(s3);
			Ccc.Subordinates.Add(s4);
		}
		[Test]
		public void TestSubordinates()
		{
			List<Person> peoplesschool = AAA.DivisionSubordinates.ToList();
			peoplesschool.Add(AAA);
			Int32 numberpeople = peoplesschool.Count;
			Int32 numberpeople1 = peoplesschool.Where(x => x.HasStudent).Count();
			Int32 numberpeople2 = peoplesschool.Where(x => x.HasTeacher).Count();

			Assert.AreEqual(7, numberpeople);
			Assert.AreEqual(3, numberpeople1);
			Assert.AreEqual(1, numberpeople2);
		}
	}
}
