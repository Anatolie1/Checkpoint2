using System;
using System.Collections.Generic;
using System.Text;

namespace WCS
{
    public abstract class Person
    {
        public string Name { get; set; }
        public bool HasTeacher { get; set; }
        public bool HasStudent { get; set; }

        public List<Person> Subordinates { get; set; }

        public IEnumerable<Person> DivisionSubordinates
        {
            get
            {
                foreach (Person person in Subordinates)
                {
                    yield return person;
                    foreach (Person person1 in person.DivisionSubordinates)
                    {
                        yield return person1;
                    }
                }
            }
        }

        public Person(string name, bool student = false, bool teacher = false)
        {
            Name = name;
            HasTeacher = teacher;
            HasStudent = student;
            Subordinates = new List<Person>();
        }

    }
}
