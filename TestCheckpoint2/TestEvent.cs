using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WCS
{
	[TestFixture]
	public class TestEvent
	{
		[Test]
		public void TestEventPostponed()
		{
			Event newEvent = new Event("TestPresent");
			newEvent.StartTime = DateTime.Now - TimeSpan.FromMinutes(1);
			newEvent.EndTime = newEvent.StartTime + TimeSpan.FromHours(10);
			DateTime startDateBeforePostpone = newEvent.StartTime;
			DateTime endDateBeforePostpone = newEvent.EndTime;

			newEvent.Postpone(TimeSpan.FromDays(1));

			Assert.AreEqual(startDateBeforePostpone, newEvent.StartTime - TimeSpan.FromDays(1));
			Assert.AreEqual(endDateBeforePostpone, newEvent.EndTime - TimeSpan.FromDays(1));
		}

		[Test]
		public void TestEventAgenda()
		{
			DatabaseReader database = new DatabaseReader();
			string query = $"EXECUTE sp_Event 3, '2019', '2021'";
			List<object[]> dataDB = database.ReturnDataDB(query);

			int number_columns = dataDB.First().Length;
			int number_events = dataDB.Count();
			string first_event_name = dataDB.First()[0].ToString();		

			Assert.AreEqual(number_columns, 2);
			Assert.AreEqual(number_events, 75);
			Assert.AreEqual(first_event_name, "PHP1");
		}
		[Test]
		public void TestCalendarEvent()
		{
			DatabaseReader database = new DatabaseReader();
			string query = $"EXECUTE sp_Calendar_Cursus '2020-04-14', '2020-05-30'";
			List<object[]> dataDB = database.ReturnDataDB(query);

			int number_columns = dataDB.First().Length;
			int number_events = dataDB.Count();
			string first_event_name = dataDB.First()[0].ToString();

			Assert.AreEqual(number_columns, 3);
			Assert.AreEqual(number_events, 62);
			Assert.AreEqual(first_event_name, "PHP");
		}
		[Test]
		public void TestTeacherStudents()
		{
			DatabaseReader database = new DatabaseReader();
			string query = $"EXECUTE sp_Teacher_Students 'TeacherPHPJAVA'";
			List<object[]> dataDB = database.ReturnDataDB(query);

			int number_columns = dataDB.First().Length;
			int number_events = dataDB.Count();
			string first_event_name = dataDB.First()[0].ToString();

			Assert.AreEqual(number_columns, 1);
			Assert.AreEqual(number_events, 10);
			Assert.AreEqual(first_event_name, "Student11");
		}
		[Test]
		public void TestQuestExpedition()
		{
			DatabaseReader database = new DatabaseReader();
			string query = $"EXECUTE sp_Quest_Expedition 'JAVA', '2020-05-23', '2020-05-30'";
			List<object[]> dataDB = database.ReturnDataDB(query);

			int number_columns = dataDB.First().Length;
			int number_events = dataDB.Count();
			string first_event_name = dataDB.First()[0].ToString();

			Assert.AreEqual(number_columns, 2);
			Assert.AreEqual(number_events, 4);
			Assert.AreEqual(first_event_name, "JAVA43");
		}
	}
}
