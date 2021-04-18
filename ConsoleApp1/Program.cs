using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int width = 700;
            int height = 700;
            var lines = "Lines";
            var rectangle = "Rectangle";
            string lineJson, rectangleJson;
            string baseDir = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("ConsoleApp1"));
            Console.WriteLine(baseDir);

            FileStream lineStream = new FileStream($"{baseDir}Points/{lines}.txt", FileMode.Open);
            using (StreamReader reader = new StreamReader(lineStream))
            { lineJson = reader.ReadLine(); }
            
            FileStream rectangleStream = new FileStream($"{baseDir}Points/{rectangle}.txt", FileMode.Open);
            using (StreamReader reader = new StreamReader(rectangleStream))
            { rectangleJson = reader.ReadLine(); }
            
            var linesList = JsonConvert.DeserializeObject<List<List<NPoint>>>(lineJson);
            var rectangleList = JsonConvert.DeserializeObject<List<List<NPoint>>>(rectangleJson);
            
            using(var image = new Bitmap(width, height))
            {
                using (var graphics = Graphics.FromImage(image))
                {
                    SolidBrush whiteBrush = new SolidBrush(Color.White);
                    Pen myPen = new Pen(Brushes.DeepSkyBlue);
                    Rectangle fillRect = new Rectangle(0, 0, width, height);
                    Region fillRegion = new Region(fillRect);
                    graphics.CompositingQuality = CompositingQuality.HighSpeed;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.FillRegion(whiteBrush, fillRegion);
                    
                    rectangleList.ForEach(delegate (List<NPoint> line)
                    {
                        Pen currentPen = new Pen(Brushes.DeepSkyBlue);
                        graphics.DrawLine(currentPen, line[0].x, line[0].y, line[1].x, line[1].y);
                    });
                    
                    linesList.ForEach(delegate (List<NPoint> line)
                    {
                        Pen currentPen = new Pen(Brushes.Black);
                        var iS = new NIntersection(rectangleList, line);
                        if (iS.CheckIntersec())
                            currentPen = new Pen(Brushes.Red);
                        
                        graphics.DrawLine(currentPen, line[0].x, line[0].y, line[1].x, line[1].y);
                    });
                    
                    image.Save($"{baseDir}Painted {lines}.png", ImageFormat.Png);
                }       
            } 
        }
    }
    
}