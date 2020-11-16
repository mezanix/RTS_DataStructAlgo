using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FutureGames.Lab
{
    public class Grid2DSearcher
    {
        GridConnected2D grid = null;

        float animationWait = 0.01f;

        // key: cell going to
        // value: cell comming from
        Dictionary<GridCell2D, GridCell2D> cameFrom = new Dictionary<GridCell2D, GridCell2D>();

        public Grid2DSearcher(GridConnected2D grid)
        {
            this.grid = grid;

            //GridCell2D.BecomeTarget += SetTarget;
        }

        //public IEnumerator BreadthFirstTravel(GridCell2D start)
        //{
        //    this.start = start;

        //    Queue<GridCell2D> frontier = new Queue<GridCell2D>();
        //    HashSet<GridCell2D> reached = new HashSet<GridCell2D>();

        //    frontier.Enqueue(start);
        //    reached.Add(start);

        //    while(frontier.Count > 0)
        //    {
        //        GridCell2D current = frontier.Dequeue();
        //        foreach(GridCell2D t in current.Neighbors)
        //        {
        //            if (reached.Contains(t))
        //                continue;

        //            frontier.Enqueue(t);
        //            reached.Add(t);
        //        }

        //        foreach (GridCell2D t in reached)
        //        {
        //            t.SetColor(Color.white);
        //        }

        //        foreach (GridCell2D t in frontier)
        //        {
        //            t.SetColor(GridCell2D.frontierColor);
        //        }

        //        yield return new WaitForSeconds(animationWait);
        //    }
        //}

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
                    value.SetColor(Color.white);
                }

                foreach (GridCell2D t in frontier)
                {
                    t.SetColor(GridCell2D.frontierColor);
                }

                yield return new WaitForSeconds(animationWait);
            }
        }

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
    }
}