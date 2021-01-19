using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Blazor.Extensions.Canvas.Canvas2D;


namespace Bloxor.Glazor
{
    public class GameEngine : IGameEngine
    {
        ICanvas _canvas;
        Point _mousePosition;
        bool _mouseButtonDown;
        int _screenWidth;
        int _screenHeight;
        SortedList<Tuple<int, int>, IGameObject> _objects = new SortedList<Tuple<int, int>, IGameObject>();
        IGameObject _clickedObject;
        readonly GameTime _gameTime = new GameTime();
        readonly List<IGameEngineEvents> _subscribers = new List<IGameEngineEvents>();
        
        public async ValueTask InitAsync(Canvas2DContext canvas)
        {
            _canvas = new Canvas(canvas);
        }

        public void AddObject(IGameObject o)
        {
            var key = new Tuple<int, int>(o.ZIndex, o.GetHashCode()); 
            _objects.Add(key, o);
        }

        public void RemoveObject(IGameObject o)
        {
            var key = new Tuple<int, int>(o.ZIndex, o.GetHashCode());
            _objects.Remove(key);
        }

        public IGameObject FindObjectAt(Point p)
        {
            return _objects.Reverse().First((pair) => pair.Value.Bounds.Contains(p)).Value;
        }

        public void Subscribe(IGameEngineEvents sink)
        {
            _subscribers.Add(sink);
        }

        public void Unsubscribe(IGameEngineEvents sink)
        {
            _subscribers.Remove(sink);
        }
        public void OnMouseMove(int x, int y)
        {
            _mousePosition.X = x;
            _mousePosition.Y = y;
        }
        
        public void OnMouseDown()
        {
            _mouseButtonDown = true;
            _clickedObject = FindObjectAt(_mousePosition);
            _subscribers.ForEach(s => s.OnMouseUp(_clickedObject));
        }        

        public void OnMouseUp()
        {
            _mouseButtonDown = false;
            _clickedObject = null;
            var obj = FindObjectAt(_mousePosition);
            _subscribers.ForEach(s => s.OnMouseUp(obj));
        }        

        public void Update(int screenWidth, int screenHeight, float timeStamp)
        {
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
            _gameTime.TotalTime = timeStamp;

            foreach (var obj in _objects.Values)
            {
                obj.Update(screenWidth, screenHeight, timeStamp);
            }
        }
        public async ValueTask Render()
        {
            await _canvas.DrawRectangle(0, 0, _screenWidth, _screenHeight, fillColor: "azure");
            var font = "24px verdana";
            var text = $"x: {_mousePosition.X}, y: {_mousePosition.Y} pressed: {_mouseButtonDown}";
            await _canvas.DrawText(10, 30, text, font,"green");
            
            foreach (var obj in _objects.Values)
            {
                await obj.Render(_canvas);
            }
        }
    }
}