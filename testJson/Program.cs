using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
//using System.Text.Json;
using Newtonsoft.Json;
using ProjectCa;

namespace testJson
{
    internal class Program
    {
        static void Main(string[] args)
        {


            try
            {
                string filePath = @"C:/Users/obyis/OneDrive - Atlantic TU/Semester 4/ObjectOrientedDevelopment/ProjectCa/testJson/gym.json";
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("Error: File not found");
                    return;
                }

                string jsonText = File.ReadAllText(filePath);
                GymInfo GymInfoWrapper = JsonConvert.DeserializeObject<GymInfo>(jsonText);

                foreach(var gymTopic in GymInfoWrapper.fitness_info)
                {
                    Console.WriteLine($"ID: {gymTopic.id}");
                    Console.WriteLine($"Topic: {gymTopic.Topic}");
                    Console.WriteLine($"Description: {gymTopic.Description}");
                    Console.WriteLine();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }


        }

    }

    public class GymInfo
    {
        public List<GymTopic> fitness_info{ get; set; }


    }

    public class GymTopic
    {
        public int id { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
    }
}

