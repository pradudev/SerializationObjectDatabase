using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectSerialization
{
    [Serializable]
    public class Person
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public List<Hobby> Hobbies { get; set; }
    }

    [Serializable]
    public class Hobby
    {
        public string Name { get; set; }

        public int Interest { get; set; }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var obj = new Person { Name = "Pradeep Chouhan", Age = 29, Hobbies = new List<Hobby> { new Hobby { Name = "Cricket", Interest = 9 }, new Hobby { Name = "Carrom", Interest = 9 } } };

            Serializers<Person>.SaveObjectToDB(obj);
        }
    }
}