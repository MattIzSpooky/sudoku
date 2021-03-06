using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Board;
using Domain.Board.Leaves;
using Domain.Solvers;

namespace Domain.Parsers
{
    public class JigsawSudokuParser : ISudokuParser
    {
        public Field[] Parse(string content, int x, int y)
        {
            var items = content.Split('=').Skip(1).ToArray();
            var squareValue = (int) Math.Round(Math.Sqrt(items.Length));

            var field = new Field(FillQuadrants(squareValue, items))
                {SolverStrategy = new BackTrackingSolver()};

            return new[] {field};
        }

        private static QuadrantComposite[] CreateQuadrants(int squareValue)
        {
            var quadrants = new QuadrantComposite[squareValue];

            for (var i = 0; i < squareValue; i++)
                quadrants[i] = new QuadrantComposite();

            return quadrants;
        }

        private static List<QuadrantComposite> FillQuadrants(int squareValue, IEnumerable<string> items)
        {
            var quadrants = CreateQuadrants(squareValue);

            int x = 0, y = 0;
            foreach (var item in items)
            {
                var rawCell = item.Split('J').ToArray();

                if (!int.TryParse(rawCell[0], out var cellValue) ||
                    !int.TryParse(rawCell[1], out var quadrantIndex))
                    break;

                quadrants[quadrantIndex].AddComponent(new CellLeaf(new Coordinate(x, y), cellValue)
                {
                    IsLocked = cellValue != 0
                });

                if (x == squareValue)
                {
                    x = 0;
                    y++;
                }

                x++;
            }

            return quadrants.ToList();
        }
    }
}