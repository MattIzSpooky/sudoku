using System.Collections.Generic;
using Domain;
using Domain.Board;
using Domain.Board.Leaves;
using Domain.Solvers;

namespace Tests
{
    public static class GameData
    {
        public static Game Game
        {
            get
            {
                var quadrants = new List<QuadrantComposite>();

                var firstQuadrant = new QuadrantComposite();
                firstQuadrant.AddComponent(new CellLeaf(new Coordinate(0, 0), 0));
                firstQuadrant.AddComponent(new CellLeaf(new Coordinate(1, 0), 3) {IsLocked = true});
                firstQuadrant.AddComponent(new WallLeaf(false, new Coordinate(2, 0)));
                firstQuadrant.AddComponent(new CellLeaf(new Coordinate(0, 1), 4) {IsLocked = true});
                firstQuadrant.AddComponent(new CellLeaf(new Coordinate(1, 1), 0));
                firstQuadrant.AddComponent(new WallLeaf(false, new Coordinate(2, 1)));
                firstQuadrant.AddComponent(new WallLeaf(true, new Coordinate(0, 2)));
                firstQuadrant.AddComponent(new WallLeaf(true, new Coordinate(1, 2)));
                firstQuadrant.AddComponent(new WallLeaf(true, new Coordinate(2, 2)));
                quadrants.Add(firstQuadrant);

                var secondQuadrant = new QuadrantComposite();
                secondQuadrant.AddComponent(new CellLeaf(new Coordinate(3, 0), 4) {IsLocked = true});
                secondQuadrant.AddComponent(new CellLeaf(new Coordinate(4, 0), 0));
                secondQuadrant.AddComponent(new CellLeaf(new Coordinate(3, 1), 0));
                secondQuadrant.AddComponent(new CellLeaf(new Coordinate(4, 1), 2) {IsLocked = true});
                secondQuadrant.AddComponent(new WallLeaf(true, new Coordinate(3, 2)));
                secondQuadrant.AddComponent(new WallLeaf(true, new Coordinate(4, 2)));
                quadrants.Add(secondQuadrant);

                var thirdQuadrant = new QuadrantComposite();
                thirdQuadrant.AddComponent(new CellLeaf(new Coordinate(0, 3), 1) {IsLocked = true});
                thirdQuadrant.AddComponent(new CellLeaf(new Coordinate(1, 3), 0));
                thirdQuadrant.AddComponent(new WallLeaf(false, new Coordinate(2, 3)));
                thirdQuadrant.AddComponent(new CellLeaf(new Coordinate(0, 4), 0));
                thirdQuadrant.AddComponent(new CellLeaf(new Coordinate(1, 4), 2) {IsLocked = true});
                thirdQuadrant.AddComponent(new WallLeaf(false, new Coordinate(2, 4)));
                quadrants.Add(thirdQuadrant);

                var fourthQuadrant = new QuadrantComposite();
                fourthQuadrant.AddComponent(new CellLeaf(new Coordinate(3, 3), 0));
                fourthQuadrant.AddComponent(new CellLeaf(new Coordinate(4, 3), 3) {IsLocked = true});
                fourthQuadrant.AddComponent(new CellLeaf(new Coordinate(3, 4), 1) {IsLocked = true});
                fourthQuadrant.AddComponent(new CellLeaf(new Coordinate(4, 4), 0));
                quadrants.Add(fourthQuadrant);


                return new Game(new[] {new Field(quadrants){SolverStrategy = new BackTrackingSolver()}});
            }
        }
    }
}