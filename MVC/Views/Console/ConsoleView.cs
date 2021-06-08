using System;
using System.Drawing;
using System.Linq;
using System.Text;
using SysConsole = Colorful.Console;

namespace Sudoku.Mvc.Views.Console
{
    /// <summary>
    /// An abstract view implementation targeting the console.
    /// </summary>
    public abstract class ConsoleView : View<ConsoleKey>
    {
        /// <summary>
        /// The buffer contains the elements we want to write on the screen.
        /// </summary>
        protected readonly ColoredChar[][] Buffer;
        
        /// <summary>
        /// Represents the Y-axis where it should start to write a string.
        /// </summary>
        protected int StringCursor;

        protected ConsoleView(int width, int height, string title) : base(width, height, title)
        {
            Buffer = CreateBuffer();

            SysConsole.OutputEncoding = Encoding.UTF8;
            SysConsole.Title = title;
        }

        /// <summary>
        /// Clears the Buffer and resets the StringCursor.
        /// </summary>
        protected void ClearBuffer()
        {
            StringCursor = 0;
            
            for (var y = 0; y < Height; ++y)
            {
                for (var x = 0; x < Width; ++x)
                {
                    Buffer[y][x] = new ColoredChar
                    {
                        Character = ' '
                    };
                }
            }
        }

        protected ColoredChar CreateChar(char character)
        {
            return CreateChar(character, Color.White);
        }

        protected ColoredChar CreateChar(char character, Color color)
        {
            return new ColoredChar {Character = character, Color = color};
        }

        protected void WriteBuffer()
        {
            SysConsole.SetCursorPosition(0, 0);
            SysConsole.CursorVisible = false;

            for (var y = 0; y < Height; ++y)
            {
                for (var x = 0; x < Buffer[y].Length; x++)
                {
                    var coloredChar = Buffer[y][x];

                    if (coloredChar.Color.IsEmpty) SysConsole.Write(coloredChar.Character);
                    else SysConsole.Write(coloredChar.Character, coloredChar.Color);
                }

                SysConsole.WriteLine();
            }
        }

        public override void Draw()
        {
            ClearBuffer();

            FillBuffer();

            WriteBuffer();
        }

        protected abstract void FillBuffer();

        /// <summary>
        /// Writes a string based on the current StringCursor.
        /// </summary>
        /// <param name="text">What you'd like to write.</param>
        /// <param name="color">The output color.</param>
        protected void WriteString(string text, Color color)
        {
            var chars = text.ToCharArray();

            for (var i = 0; i < chars.Length; i++)
            {
                Buffer[StringCursor][i] = CreateChar(chars[i], color);
            }
            
            StringCursor++;
        }

        private ColoredChar[][] CreateBuffer()
        {
            var render = new ColoredChar[Height][];

            for (var y = 0; y < Height; ++y)
                render[y] = new ColoredChar[Width];

            return render;
        }

        public override void KeyDown()
        {
            var key = SysConsole.ReadKey(true).Key;

            foreach (var input in Inputs.Where(input => input.Key == key))
            {
                input.Action();
            }
        }

        public override void Dispose()
        {
            SysConsole.ResetColor();
            SysConsole.Clear();
        }
    }
}