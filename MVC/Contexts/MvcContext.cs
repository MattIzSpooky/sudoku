using System;
using System.Linq;
using MVC.Views;

namespace MVC.Contexts
{
    /// <summary>
    /// The framework context.
    /// Manages the views and controllers.
    /// </summary>
    public class MvcContext : IDisposable
    {
        private Controller? _controller;
        private IView? _view;
        private bool _running = true;

        public void Run()
        {
            Console.Clear(); // Clear the screen before running.
            _view?.Draw(); 
            
            Update();
        }

        private void Update()
        {
            while (_running)
            {
                _view?.KeyDown();
                _view?.Draw();
            }
        }

        /// <summary>
        /// Create a new controller based on the given inputs.
        /// The new controller will overwrite the current controller in the MVC Context.
        ///
        /// This is used to switch controllers and thus views.
        /// </summary>
        /// <param name="args">Arguments for the controller you are trying to call.</param>
        /// <typeparam name="TController">The controller</typeparam>
        /// <typeparam name="TConsoleView">The type of view associated to the controller.</typeparam>
        /// <typeparam name="TInput">The type of input associated to the view.</typeparam>
        public void OpenController<TController, TConsoleView, TInput>(params object[] args)
            where TController : Controller<TConsoleView, TInput>
            where TConsoleView : View<TInput>
        {
            _controller?.Dispose();

            var ctrArgs = new[] {this}.Concat(args).ToArray();

            var controller = (TController?) Activator.CreateInstance(typeof(TController), ctrArgs);

            if (controller == null) throw new NullReferenceException("Controller cannot be null. Programming mistake?");
            
            controller.SetupView();
            
            _view = controller.View;
            _controller = controller;
        }

        public void Stop() => _running = false;

        public void Dispose() => _controller?.Dispose();
    }
}