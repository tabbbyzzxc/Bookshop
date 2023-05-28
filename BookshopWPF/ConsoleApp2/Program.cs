using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ConsoleApp2
{
    public class Program
    {



        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var list = new List<Animal>()
            {
                new Dog() { DogProperty = "DOG", Name = "dogg" },
                new Cat() {CatProperty = "CAT", Name= "catt"}
            };
            var setting = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            var json = JsonConvert.SerializeObject(list, setting);
            var deserialized = JsonConvert.DeserializeObject<List<Animal>>(json, setting);
        }

        
    }

    public abstract class Animal
    {
        public string Name { get; set; }
    }

    public class Dog : Animal
    {
        public string DogProperty { get; set; }
    }

    public class Cat : Animal
    {
        public string CatProperty { get; set; }
    }
}