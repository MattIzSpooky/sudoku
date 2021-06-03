using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Domain.Board;

namespace Sudoku.Domain.Parsers
{
    public class JigsawSudokuParser : ISudokuParser
    {
        public Board.Sudoku[] Parse(string content, int x, int y)
        {
            var items = content.Split('=').Skip(1).ToArray();
            var squareValue = (int) Math.Round(Math.Sqrt(items.Length));
            
            return new Board.Sudoku[] {new(CreateCells(squareValue, items))};
        }
        
        private QuadrantComposite[] CreateQuadrants(int squareValue)
        {
            var quadrants = new QuadrantComposite[squareValue];
            
            for (var i = 0; i < squareValue; i++)
                quadrants[i] = new QuadrantComposite(null);

            return quadrants;
        }

        private List<QuadrantComposite> CreateCells(int squareValue,IEnumerable<string> items)
        {
            var quadrants = CreateQuadrants(squareValue);
            
            int x = 0, y = 0;
            foreach (var item in items)
            {
                var rawCell = item.Split('J').ToArray();

                if(!int.TryParse(rawCell[0], out var cellValue) || 
                   !int.TryParse(rawCell[1], out var quadrantIndex))
                    break;
                
                quadrants[quadrantIndex].AddCell(new CellLeaf(new Coordinate(x, y), cellValue));

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