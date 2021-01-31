using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Bloxor.Game
{
    public class ShapeFactory
    {
        List<Tuple<int, Point[], string>> _shapes = new List<Tuple<int, Point[], string>>();
        private int _totalWeight;


        Point[] RotateShape(Point[] points)
        {
            return points.ToList().Select(p => new Point(p.Y, p.X)).ToArray();
        }
        public ShapeFactory()
        {
            // Single square
            var color = "red";
            var single = new []
            {
                new Point(0, 0)
            };
            AddShape(5, single, color);

            // Two squares
            color = "yellow";
            var duo1 = new []
            {
                new Point(0, 0),
                new Point(0, 1),
            };
            var duo2 = new []
            {
                new Point(0, 0),
                new Point(1, 0),
            };
            // AddShape(2, duo1, color);
            // AddShape(2, duo2, color);

            color = "pink";
            
            var long1 = Enumerable.Range(0,5).ToList().Select(i => new Point(i, 0)).ToArray();
            var long2 = RotateShape(long1);
            
            // AddShape(5, long1, color);
            // AddShape(5, long2, color);


            color = "blue";
            var elle1 = new []
            {
                new Point(0, 0),
                // new Point(1, 0),
                // new Point(2, 0),
                // new Point(2, 1),
                // new Point(2, 2),
            };
            var elle2 = RotateShape(elle1);
            var elle3 = RotateShape(elle2);
            var elle4 = RotateShape(elle3);
            
            AddShape(1, elle1, color);
            // AddShape(1, elle2, color);
            // AddShape(1, elle3, color);
            // AddShape(1, elle4, color);

            _totalWeight = _shapes.Select(x => x.Item1).Sum();
        }

        void AddShape(int weight, Point[] points, string color)
        {
            _shapes.Add(new Tuple<int, Point[], string>(weight, points, color));
        }
        
        public Shape ChooseRandomShape()
        {
            var cumulativeValue = 0;
            var value = new Random().Next() % _totalWeight;
            foreach (var shape in _shapes)
            {
                cumulativeValue += shape.Item1;
                if (value < cumulativeValue)
                {
                    return new Shape(shape.Item2, shape.Item3);
                }
            }

            return null;
        }
    }
}