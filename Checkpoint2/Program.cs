using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace WCS
{
    class Program
    {
        [Verb("events", HelpText = "agenda events")]
        class EventsAgenda
        {
            [Option('p', "person", Required = true, HelpText = "input id person")]
            public int IdPerson { get; set; }

            [Option('b', "begins", Required = true, HelpText = "start period")]
            public string BeginDate { get; set; }

            [Option('e', "ends", Required = true, HelpText = "end period")]
            public string EndDate { get; set; }
        }

        [Verb("cursus", HelpText = "cursus events and students")]
        class Cursus
        {
            [Option('n', "name", Required = true, HelpText = "input cursus name")]
            public string Cursusname { get; set; }

            [Option('s', "students", Required = false, HelpText = "write only word students")]
            public bool Students { get; set; }

            [Option('q', "quests", Required = false, HelpText = "write only word quests")]
            public bool Quests { get; set; }
        }

        static void Main(string[] args)
        {
            /*Event newEvent = new Event("Important meeting");
            newEvent.StartTime = DateTime.Now;
            newEvent.EndTime = DateTime.Now + TimeSpan.FromHours(1);
            newEvent.Postpone(TimeSpan.FromHours(1));
            Console.WriteLine("Another meeting is postponed");*/

            Parser.Default.ParseArguments<EventsAgenda, Cursus>(args)
                .WithParsed<EventsAgenda>(ShowAgenda)
                .WithParsed<Cursus>(ShowCursus);

            static void ShowAgenda(EventsAgenda options)
            {
                Display.DisplayAgenda(options.IdPerson, options.BeginDate, options.EndDate);
            }

            static void ShowCursus(Cursus options)
            {
                if (options.Students)
                {
                    Display.DisplayCursusStudents(options.Cursusname);
                }
                if (options.Quests)
                {
                    Display.DisplayCursusQuests(options.Cursusname);
                }                
            }
        }
    }
}
