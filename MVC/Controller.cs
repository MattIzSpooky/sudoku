using System;
using Sudoku.Mvc.Contexts;
using Sudoku.Mvc.Views;

namespace Sudoku.Mvc
{
    public abstract class Controller : IDisposable
    {
        protected readonly MvcContext Root;

        protected Controller(MvcContext root)
        {
            Root = root;
        }

        public abstract void Dispose();
    }

    public abstract class Controller<TView, TInput> : Controller
        where TView : View<TInput>
    {
        public TView View { get; set; }

        protected Controller(MvcContext root, TView view) : base(root)
        {
            View = view;
        }

        /// <summary>
        /// A function that is called when OpenController is called.
        /// Should always set the View on Controller.
        /// </summary>
        public abstract void SetupView();
        public override void Dispose() => View.Dispose();
    }
}