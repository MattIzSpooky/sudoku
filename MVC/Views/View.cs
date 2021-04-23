using System.Collections.Generic;

namespace MVC.Views
{
    public abstract class View<T> : IView
    {
        protected int Height { get; }
        protected int Width { get; }
        protected string Title { get; }
        
        protected readonly List<Input<T>> Inputs = new List<Input<T>>();

        protected View(int width, int height, string title)
        {
            Height = height;
            Width = width;
            Title = title;
        }
        /// <summary>
        /// Maps a given input to the current view.
        /// </summary>
        /// <param name="consoleInput"></param>
        public void MapInput(Input<T> consoleInput)
        {
            Inputs.Add(consoleInput);
        }
        public abstract void Dispose();

        public abstract void Draw();
        public abstract void KeyDown();
    }
}