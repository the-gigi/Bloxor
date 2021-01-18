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
        
        public ShapeFactory()
        {
            // Single square
            var color = "blue";
            var single = new []
            {
                new Point(0, 0)
            };
            AddShape(2, single, color);

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
            AddShape(2, duo1, color);
            AddShape(2, duo2, color);

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