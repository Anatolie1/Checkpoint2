using System;
using System.Collections.Generic;
using System.Text;

namespace WCS
{
    public static class FactoryPerson
    {
        public static Person Create(string name, bool hasteacher, bool hasstudent)
        {
            Person person;
            if (hasteacher && hasstudent)
            {
                person = new Teacher(name, hasteacher, hasstudent);
            }
            else if(hasstudent)
            {
                person = new Teacher(name, hasteacher = false, hasstudent);
            }
            else
            {
                person = new Student(name);
            }
            return person;
        }
    }
}
