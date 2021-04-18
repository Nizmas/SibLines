using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    public class NIntersection
    {
        private List<List<NPoint>> _rectangle { get; set; }
        private List<NPoint> _line { get; set; }

        public NIntersection(List<List<NPoint>> rect, List<NPoint> line)
        {
            this._rectangle = rect;
            this._line = line;
        }

        public bool CheckIntersec()
        {
            var interLine = new HashSet<NPoint>();
            foreach (var r in _rectangle)
            {
                var x1 = r[0].x;
                var y1 = r[0].y;
                var x2 = r[1].x; 
                var y2 = r[1].y;
                var x3 = _line[0].x;
                var y3 = _line[0].y;
                var x4 = _line[1].x; 
                var y4 = _line[1].y;
                var lineR = LineCoef(new List<int> {x1, y1}, new List<int> {x2, y2});
                var lineL = LineCoef(new List<int> {x3, y3}, new List<int> {x4, y4});
                var interSeg = InterSegment(lineR, lineL);
                
                if (interSeg != null)
                {
                    if (x1 > x2) { var xx = x1; x1 = x2; x2 = xx; }
                    if (y1 > y2) { var yy = y1; y1 = y2; y2 = yy; }
                    if (x3 > x4) { var xx = x3; x3 = x4; x4 = xx; }
                    if (y3 > y4) { var yy = y3; y3 = y4; y4 = yy; }
                }
                if (((x1 <= interSeg[0]) && (interSeg[0]  <= x2)) && ((y1 <= interSeg[1]) && (interSeg[1] <= y2))){
                    if (((x3 <= interSeg[0]) && (interSeg[0]  <= x4)) && ((y3 <= interSeg[1]) && (interSeg[1] <= y4))){
                        // console.log("Есть пересечение отрезков");
                        return true;
                    }
                    interLine.Add(new NPoint {x = interSeg[0], y = interSeg[1]});
                }  
            }
            if (interLine.Count == 2) 
                return CheckInterLine(interLine);
            return false;
        }
        private List<int> LineCoef(List<int> p1, List<int> p2) {
            var A = (p1[1] - p2[1]);
            var B = (p2[0] - p1[0]);
            var C = (p1[0]*p2[1] - p2[0]*p1[1]);
            return new List<int>{A, B, -C};
        }
        
        private List<int> InterSegment(List<int> L1, List<int> L2)
        {
            var D = L1[0] * L2[1] - L1[1] * L2[0];
            var Dx = L1[2] * L2[1] - L1[1] * L2[2];
            var Dy = L1[0] * L2[2] - L1[2] * L2[0];
            if (D != 0) {
                var x = Dx / D;
                var y = Dy / D;
                return new List<int> {x, y};
            }
            return null;
        }

        private bool CheckInterLine(HashSet<NPoint> intLine)
        {
            var interLine = intLine.ToList();
            var xL = _line[0].x;  
            var yL = _line[0].y;
            int x1; int y1; int x2; int y2;
            if (interLine[0].x >  interLine[1].x){
                x1 = interLine[1].x;
                x2 = interLine[0].x;
            }
            else {
                x1 = interLine[0].x;
                x2 = interLine[1].x;
            }
            if (interLine[0].y >  interLine[1].y){
                y1 = interLine[1].y;
                y2 = interLine[0].y;
            }
            else {
                y1 = interLine[0].y;
                y2 = interLine[1].y;
            }
            if (((x1 <= xL) && (xL <= x2)) && ((y1 <= yL) && (yL <= y2))) {
                // console.log("Есть вхождение отрезка");
                return true;
            }
            return false;
        }
    }
}