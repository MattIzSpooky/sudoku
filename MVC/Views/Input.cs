using System;

namespace Sudoku.Mvc.Views
{
    /// <summary>
    /// An object that contains a map between an input type (char, int, string, etc.) to a function somewhere.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Input<T>
    {
        public T Key { get; }
        public Action Action { get; }

        public Input(T key, Action action)
        {
            Key = key;
            Action = action;
        }
    }
}