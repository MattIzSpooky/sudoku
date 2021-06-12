using System.Collections.Generic;
using System.Linq;
using Domain.Board;
using Domain.Parsers;
using NUnit.Framework;

namespace Tests.ISudokuParser
{
    public class SamuraiSudokuParserTests
    {
        private Field[] _fields;

        private const int AmountOfFields = 5;

        private const string SamuraiSudoku =
            @"800000700003050206700300095000091840000007002000062000000000000609080000002903000
            149000000000091000000060000007120008000000340405008067000000000000007020000050003
            000000000000008000000004000010600005030070080800005010000900000000800000000000000
            900060000030400000000000000390800407065000000200037600000080000000190000000000914
            000402800000080902000000000000610000400800000098750000670008001901060700002000009";

        [SetUp]
        public void Setup()
        {
            _fields = new SamuraiSudokuParser().Parse(SamuraiSudoku);
        }

        [Test]
        public void Parse_WithSamurai_ShouldReturnFiveFields()
        {
            Assert.True(_fields.Length == AmountOfFields);
        }

        [Test]
        public void Parse_WithSamurai_ShouldHaveNineQuadrantsPerField()
        {
            foreach (var field in _fields)
            {
                Assert.AreEqual(9, field.Quadrants.Count);
            }
        }

        [Test]
        public void Parse_WithSamurai_EachQuadrantHasOffsets()
        {
            var firstCellFirstQuadrant = _fields[0].Quadrants[0].Cells[0];
            var firstCellSecondQuadrant = _fields[1].Quadrants[0].Cells[0];

            Assert.AreNotEqual(firstCellFirstQuadrant.Coordinate, firstCellSecondQuadrant.Coordinate);
        }

        [Test]
        public void Parse_WithSamurai_ShouldHaveOverflowingCells()
        {
            const int quadrantFactor = 2;

            var centerQuadrant = 0;
            var centerGrid = _fields.Length / quadrantFactor;
            var leftQuadrant = 8;

            var areSameList = new List<bool>();
            
            for (var i = 0; i < _fields.Length; i++)
            {
                if (i != centerGrid)
                {
                    var firstFieldLastQuadrant = _fields[i].Quadrants[leftQuadrant];
                    var centerFieldLastQuadrant = _fields[centerGrid].Quadrants[centerQuadrant];

                    for (var j = 0; j < firstFieldLastQuadrant.Cells.Count; j++)
                    {
                        var cellFirstField = firstFieldLastQuadrant.Cells[j];
                        var cellCenterField = centerFieldLastQuadrant.Cells[j];
                
                        areSameList.Add(ReferenceEquals(cellFirstField.Value, cellCenterField.Value));
                    }
                }


                leftQuadrant -= quadrantFactor;
                centerQuadrant += quadrantFactor;

                Assert.True(areSameList.All(b => b));
            }
        }
    }
}