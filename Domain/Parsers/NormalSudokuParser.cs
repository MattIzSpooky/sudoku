using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Board;
using Domain.Parsers.Builders;
using Domain.Solvers;

namespace Domain.Parsers
{
    public class NormalSudokuParser : ISudokuParser
    {
        public virtual Field[] Parse(string content, int offsetX = 0, int offsetY = 0)
        {
            var squareValue = (int) Math.Round(Math.Sqrt(content.Trim().Length));
            var quadrantHeight = (int) Math.Floor(Math.Sqrt(squareValue));
            var quadrantWidth = squareValue / quadrantHeight;

            var boardValues = new BoardValues
            {
                SquareValue = squareValue,
                QuadrantHeight = quadrantHeight,
                QuadrantWidth = quadrantWidth,
                RowQuadrantsCount = squareValue / quadrantWidth,
                OffsetX = offsetX,
                OffsetY = offsetY
            };

            var sudokuComponents = CreateCells(content, boardValues);
            var quadrants = CreateQuadrants(sudokuComponents, boardValues);

            var field = new Field(quadrants)
            {
                SolverStrategy = new BackTrackingSolver()
            };

            return new[] {field};
        }

        private static List<ISudokuComponent> CreateCells(string content, BoardValues boardValues)
        {
            var sudokuComponents = new List<ISudokuComponent>();

            var newContent = BuildContentWithWalls(content, boardValues);
            var rowBuilder = new RowBuilder(boardValues.OffsetX, boardValues.OffsetY);

            var counter = newContent.Length;
            var totalY = boardValues.SquareValue + boardValues.CalculateRows();
            var totalX = newContent[..newContent.IndexOf("-", StringComparison.Ordinal)].Length /
                         boardValues.RowQuadrantsCount;

            for (var y = 0; y < totalY; y++)
            {
                rowBuilder.SetY(y);

                for (var x = 0; x < totalX; x++)
                {
                    var index = newContent.Length - counter;

                    CreateTile(newContent, index, rowBuilder);

                    counter--;
                }

                sudokuComponents.AddRange(rowBuilder.GetResult().Components);
                rowBuilder.Reset();
            }

            return sudokuComponents;
        }

        private static void CreateTile(string newContent, int index, RowBuilder rowBuilder)
        {
            if (newContent[index] == '|')
                rowBuilder.BuildWall();
            else if (newContent[index] == '-')
                rowBuilder.BuildWall(true);
            else
                rowBuilder.BuildCell((int) char.GetNumericValue(newContent[index..].First()));
        }

        private static string BuildContentWithWalls(string content, BoardValues boardValues)
        {
            var cutFactor = boardValues.SquareValue * boardValues.RowQuadrantsCount;

            StringBuilder builder = new();
            for (var i = 1; i <= content.Length; i++)
            {
                builder.Append(content[i - 1]);

                if (i != 1 && i % boardValues.QuadrantWidth == 0 && i % boardValues.SquareValue != 0 &&
                    i % cutFactor != 0)
                    builder.Append('|');

                if (i == 1 || i % cutFactor != 0 || i == content.Length)
                    continue;

                for (var j = 1; j <= boardValues.SquareValue + boardValues.RowQuadrantsCount - 1; j++)
                    builder.Append('-');
            }

            return builder.ToString();
        }

        private static List<QuadrantComposite> CreateQuadrants(IReadOnlyCollection<ISudokuComponent> components,
            BoardValues boardValues)
        {
            var quadrants = new List<QuadrantComposite>();

            boardValues.QuadrantWidth += 1;
            boardValues.QuadrantHeight += 1;

            var quadrantCounter = 0;
            var maxX = boardValues.QuadrantWidth;
            var maxY = boardValues.QuadrantHeight;

            for (var i = 0; i < boardValues.SquareValue; i++)
            {
                var minX = maxX - boardValues.QuadrantWidth;
                var minY = maxY - boardValues.QuadrantHeight;

                var range = new QuadrantRange
                {
                    MinX = minX + boardValues.OffsetX,
                    MaxX = maxX + boardValues.OffsetX - 1,
                    MinY = minY + boardValues.OffsetY,
                    MaxY = maxY + boardValues.OffsetY - 1
                };

                quadrants.Add(range.CreateQuadrant(components));

                quadrantCounter++;

                if (quadrantCounter == boardValues.RowQuadrantsCount)
                {
                    maxX = boardValues.QuadrantWidth;
                    maxY += boardValues.QuadrantHeight;
                    quadrantCounter = 0;
                    continue;
                }

                maxX += boardValues.QuadrantWidth;
            }

            return quadrants;
        }

        private struct BoardValues
        {
            public int SquareValue { get; init; }
            public int QuadrantWidth { get; set; }
            public int QuadrantHeight { get; set; }
            public int RowQuadrantsCount { get; init; }
            public int OffsetX { get; init; }
            public int OffsetY { get; init; }

            public int CalculateRows()
            {
                return (int) Math.Pow(QuadrantWidth * QuadrantHeight, 2) /
                    (QuadrantWidth * QuadrantHeight * RowQuadrantsCount) - 1;
            }
        }

        private class QuadrantRange
        {
            public int MinX { get; init; }
            public int MinY { get; init; }
            public int MaxX { get; init; }
            public int MaxY { get; init; }

            public QuadrantComposite CreateQuadrant(IEnumerable<ISudokuComponent> components)
            {
                var quadrant = new QuadrantComposite();

                foreach (var cell in GetSpecifiedQuadrantCells(components))
                    quadrant.AddComponent(cell);

                return quadrant;
            }

            private IEnumerable<ISudokuComponent> GetSpecifiedQuadrantCells(IEnumerable<ISudokuComponent> components) =>
                components.Where(cell =>
                    cell.Coordinate.X >= MinX &&
                    cell.Coordinate.X <= MaxX &&
                    cell.Coordinate.Y >= MinY &&
                    cell.Coordinate.Y <= MaxY);
        }
    }
}