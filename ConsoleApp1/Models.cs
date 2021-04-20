using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    public class NPoint
    {
        public int x { get; set; }
        public int y { get; set; }
    }
    
    public class NSegments
    {
        public List<List<NPoint>>  linesList { get; set; }
        public List<List<NPoint>> rectangleList { get; set; }

        public NSegments(string linesName, string rectangleName)
        {
            string lineJson, rectangleJson;
            FileStream lineStream = new FileStream($"{linesName}.txt", FileMode.Open);
            using (StreamReader reader = new StreamReader(lineStream))
            { lineJson = reader.ReadLine(); }
            
            FileStream rectangleStream = new FileStream($"{rectangleName}.txt", FileMode.Open);
            using (StreamReader reader = new StreamReader(rectangleStream))
            { rectangleJson = reader.ReadLine(); }
            
            linesList = JsonConvert.DeserializeObject<List<List<NPoint>>>(lineJson);
            rectangleList = JsonConvert.DeserializeObject<List<List<NPoint>>>(rectangleJson);
        }
    }
}