using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int width = 700;
            int height = 700;

            var segments = new NSegments("Lines", "Rectangle");

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
                    
                    segments.rectangleList.ForEach(delegate (List<NPoint> line)
                    {
                        Pen currentPen = new Pen(Brushes.DeepSkyBlue);
                        graphics.DrawLine(currentPen, line[0].x, line[0].y, line[1].x, line[1].y);
                    });
                    
                    segments.linesList.ForEach(delegate (List<NPoint> line)
                    {
                        Pen currentPen = new Pen(Brushes.Black);
                        var iS = new NIntersection(segments.rectangleList, line);
                        if (iS.CheckIntersec())
                            currentPen = new Pen(Brushes.Red);
                        
                        graphics.DrawLine(currentPen, line[0].x, line[0].y, line[1].x, line[1].y);
                    });
                    
                    image.Save("Painted lines.png", ImageFormat.Png);
                }       
            } 
        }
    }
    
}