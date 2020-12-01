using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FutureGames.Lab
{
    public class Grid2DSearcher
    {
        GridConnected2D grid = null;

        float animationWait = 0.05f;

        // key: cell going to
        // value: cell comming from
        Dictionary<GridCell2D, GridCell2D> cameFrom = new Dictionary<GridCell2D, GridCell2D>();

        public Grid2DSearcher(GridConnected2D grid)
        {
            this.grid = grid;

            //GridCell2D.BecomeTarget += SetTarget;
        }

        public IEnumerator BreadthFirstTravel(GridCell2D start)
        {
            //this.start = start;

            Queue<GridCell2D> frontier = new Queue<GridCell2D>();
            HashSet<GridCell2D> reached = new HashSet<GridCell2D>();

            frontier.Enqueue(start);
            reached.Add(start);

            while (frontier.Count > 0)
            {
                GridCell2D current = frontier.Dequeue();
                foreach (GridCell2D t in current.Neighbors)
                {
                    if (reached.Contains(t))
                        continue;

                    frontier.Enqueue(t);
                    reached.Add(t);
                }

                foreach (GridCell2D t in reached)
                {
                    t.SetColor(Color.white);
                }

                foreach (GridCell2D t in frontier)
                {
                    t.SetColor(GridCell2D.frontierColor);
                }

                yield return new WaitForSeconds(animationWait);
            }
        }

        public IEnumerator BreadthFirstTravelPathTrack()
        {
            Queue<GridCell2D> frontier = new Queue<GridCell2D>();
            cameFrom.Clear();

            frontier.Enqueue(grid.StartCell);
            cameFrom.Add(grid.StartCell, null);

            while (frontier.Count > 0)
            {
                GridCell2D current = frontier.Dequeue();
                foreach (GridCell2D t in current.Neighbors)
                {
                    if (t.IsWall())
                        continue;

                    if (cameFrom.ContainsKey(t))
                        continue;

                    frontier.Enqueue(t);
                    cameFrom.Add(t, current);
                }

                if (current == grid.TargetCell)
                    break;

                foreach (GridCell2D t in cameFrom.Keys)
                {
                    GridCell2D value = cameFrom[t];
                    if (value == null)
                        continue;
                    value.SetColor(GridCell2D.visitedColor);
                }

                foreach (GridCell2D t in frontier)
                {
                    t.SetColor(GridCell2D.frontierColor);
                }

                yield return new WaitForSeconds(animationWait);
            }
        }

        public IEnumerator BreadthFirstTravelPathTrackCost()
        {
            List<GridCell2D> frontier = new List<GridCell2D>();
            cameFrom.Clear();

            frontier.Add(grid.StartCell);
            frontier.Sort();

            cameFrom.Add(grid.StartCell, null);

            Dictionary<GridCell2D, int> costSoFar = new Dictionary<GridCell2D, int>();
            costSoFar.Add(grid.StartCell, grid.StartCell.Cost);


            while (frontier.Count > 0)
            {
                GridCell2D current = frontier[frontier.Count - 1];
                current.SetColor(GridCell2D.visitedColor);
                frontier.RemoveAt(frontier.Count - 1);

                foreach (GridCell2D next in current.Neighbors)
                {
                    if (next.IsWall())
                        continue;

                    int newCost = costSoFar[current] + next.Cost;

                    if (costSoFar.ContainsKey(next) == false ||
                        newCost < costSoFar[next])
                    {
                        if (costSoFar.ContainsKey(next) == false)
                        {
                            costSoFar.Add(next, newCost);
                            next.Cost = newCost;
                        }
                        else
                        {
                            costSoFar[next] = newCost;
                            next.Cost = newCost;
                        }

                        if (next == grid.TargetCell)
                        {
                            if (cameFrom.ContainsKey(next) == false)
                                cameFrom.Add(next, current);

                            frontier.Clear();
                            break;
                        }

                        frontier.Add(next);
                        frontier.Sort();

                        if (cameFrom.ContainsKey(next) == false)
                            cameFrom.Add(next, current);
                    }
                }

                //if (current == grid.TargetCell)
                //    break;

                foreach (GridCell2D t in cameFrom.Keys)
                {
                    GridCell2D value = cameFrom[t];
                    if (value == null)
                        continue;
                    value.SetColor(GridCell2D.visitedColor);
                }

                foreach (GridCell2D t in frontier)
                {
                    t.SetColor(GridCell2D.frontierColor);
                }

                yield return new WaitForSeconds(animationWait);
            }
        }

        public IEnumerator BreadthFirstTravelPathTrackGreedyFirst()
        {
            List<GridCell2D> frontier = new List<GridCell2D>();
            cameFrom.Clear();

            frontier.Add(grid.StartCell);
            frontier.Sort(new GridCell2DDistanceToTargetComparer());

            cameFrom.Add(grid.StartCell, null);

            while (frontier.Count > 0)
            {
                GridCell2D current = frontier[frontier.Count - 1];
                current.SetColor(GridCell2D.visitedColor);
                frontier.RemoveAt(frontier.Count - 1);

                foreach (GridCell2D next in current.Neighbors)
                {
                    if (next.IsWall())
                        continue;

                    if (cameFrom.ContainsKey(next))
                        continue;

                    if (next == grid.TargetCell)
                    {
                        if (cameFrom.ContainsKey(next) == false)
                            cameFrom.Add(next, current);

                        frontier.Clear();
                        break;
                    }

                    next.DistanceToTarget = ManhattanDistance(next.Index, grid.TargetCell.Index);
                    frontier.Add(next);
                    frontier.Sort(new GridCell2DDistanceToTargetComparer());

                    if (cameFrom.ContainsKey(next) == false)
                        cameFrom.Add(next, current);
                }

                //if (current == grid.TargetCell)
                //    break;

                foreach (GridCell2D t in cameFrom.Keys)
                {
                    GridCell2D value = cameFrom[t];
                    if (value == null)
                        continue;
                    value.SetColor(GridCell2D.visitedColor);
                }

                foreach (GridCell2D t in frontier)
                {
                    t.SetColor(GridCell2D.frontierColor);
                }

                yield return new WaitForSeconds(animationWait);
            }
        }

        public IEnumerator BreadthFirstTravelPathTrackAstar()
        {
            List<GridCell2D> frontier = new List<GridCell2D>();
            cameFrom.Clear();

            frontier.Add(grid.StartCell);
            frontier.Sort(new GridCell2DDistanceToTargetAndCostComparer());
            cameFrom.Add(grid.StartCell, null);
            Dictionary<GridCell2D, int> costSoFar = new Dictionary<GridCell2D, int>();
            costSoFar.Add(grid.StartCell, grid.StartCell.Cost);

            while (frontier.Count > 0)
            {
                GridCell2D current = frontier[frontier.Count - 1];
                current.SetColor(GridCell2D.visitedColor);
                frontier.RemoveAt(frontier.Count - 1);

                foreach (GridCell2D next in current.Neighbors)
                {
                    if (next.IsWall())
                        continue;

                    int newCost = costSoFar[current] + next.Cost;

                    if (costSoFar.ContainsKey(next) == false ||
                        newCost < costSoFar[next])
                    {
                        if (costSoFar.ContainsKey(next) == false)
                        {
                            costSoFar.Add(next, newCost);
                            next.Cost = newCost;
                        }
                        else
                        {
                            costSoFar[next] = newCost;
                            next.Cost = newCost;
                        }

                        if (next == grid.TargetCell)
                        {
                            if (cameFrom.ContainsKey(next) == false)
                                cameFrom.Add(next, current);

                            frontier.Clear();
                            break;
                        }

                        next.DistanceToTarget = ManhattanDistance(next.Index, grid.TargetCell.Index);
                        frontier.Add(next);
                        frontier.Sort(new GridCell2DDistanceToTargetAndCostComparer());

                        if (cameFrom.ContainsKey(next) == false)
                            cameFrom.Add(next, current);
                    }
                }

                //if (current == grid.TargetCell)
                //    break;

                foreach (GridCell2D t in cameFrom.Keys)
                {
                    GridCell2D value = cameFrom[t];
                    if (value == null)
                        continue;
                    value.SetColor(GridCell2D.visitedColor);
                }

                foreach (GridCell2D t in frontier)
                {
                    t.SetColor(GridCell2D.frontierColor);
                }

                yield return new WaitForSeconds(animationWait);
            }
        }

        //public IEnumerator BreadthFirstTravelPathTrackAstarCumul()
        //{
        //    List<GridCell2D> frontier = new List<GridCell2D>();
        //    cameFrom.Clear();

        //    frontier.Add(grid.StartCell);
        //    frontier.Sort(new GridCell2DDistanceToTargetAndCostComparer());

        //    cameFrom.Add(grid.StartCell, null);

        //    Dictionary<GridCell2D, int> costSoFar = new Dictionary<GridCell2D, int>();
        //    costSoFar.Add(grid.StartCell, grid.StartCell.DistanceToTargetAndCost);


        //    while (frontier.Count > 0)
        //    {
        //        GridCell2D current = frontier[frontier.Count - 1];
        //        current.SetColor(GridCell2D.visitedColor);
        //        frontier.RemoveAt(frontier.Count - 1);

        //        foreach (GridCell2D next in current.Neighbors)
        //        {
        //            if (next.IsWall())
        //                continue;

        //            int newCost = costSoFar[current] + next.Cost + ManhattanDistance(next.Index, grid.TargetCell.Index);

        //            if (costSoFar.ContainsKey(next) == false ||
        //                newCost < costSoFar[next])
        //            {
        //                if (costSoFar.ContainsKey(next) == false)
        //                {
        //                    costSoFar.Add(next, newCost);
        //                    next.DistanceToTargetAndCost = newCost;
        //                }
        //                else
        //                {
        //                    costSoFar[next] = newCost;
        //                    next.DistanceToTargetAndCost = newCost;
        //                }

        //                if (next == grid.TargetCell)
        //                {
        //                    if (cameFrom.ContainsKey(next) == false)
        //                        cameFrom.Add(next, current);

        //                    frontier.Clear();
        //                    break;
        //                }

        //                //next.DistanceToTarget = ManhattanDistance(next.Index, grid.TargetCell.Index);
        //                frontier.Add(next);
        //                frontier.Sort(new GridCell2DDistanceToTargetAndCostComparer());

        //                if (cameFrom.ContainsKey(next) == false)
        //                    cameFrom.Add(next, current);
        //            }
        //        }

        //        //if (current == grid.TargetCell)
        //        //    break;

        //        foreach (GridCell2D t in cameFrom.Keys)
        //        {
        //            GridCell2D value = cameFrom[t];
        //            if (value == null)
        //                continue;
        //            value.SetColor(GridCell2D.visitedColor);
        //        }

        //        foreach (GridCell2D t in frontier)
        //        {
        //            t.SetColor(GridCell2D.frontierColor);
        //        }

        //        yield return new WaitForSeconds(animationWait);
        //    }
        //}

        public void Run()
        {
            DrawPathToTarget();
        }

        private void DrawPathToTarget()
        {
            if (grid.TargetCell == null)
                return;

            GridCell2D current = grid.TargetCell;

            while (current != grid.StartCell)
            {
                if (cameFrom.ContainsKey(current) == false)
                    break;

                current = cameFrom[current];
                current.SetColor(GridCell2D.pathColor);
            }
        }
    
        public static int ManhattanDistance(Vector2Int a, Vector2Int b)
        {
            return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
        }
    }
}