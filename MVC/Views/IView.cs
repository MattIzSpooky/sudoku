using System;

namespace MVC.Views
{
    public interface IView : IDisposable
    {
        /// <summary>
        /// Draw the view to the screen.
        /// </summary>
        public void Draw();
        /// <summary>
        /// Listen for key inputs and handle based on the input.
        /// </summary>
        public void KeyDown();
    }
}