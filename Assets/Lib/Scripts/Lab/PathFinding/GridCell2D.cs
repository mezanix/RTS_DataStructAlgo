using System;
using System.Collections.Generic;
using UnityEngine;

namespace FutureGames.Lab
{
    //public enum MouseState
    //{
    //    None,
    //    Hover,
    //    NeighborOfHover,
    //}

    public enum WalkState
    {
        Start,
        Target,
        Walkable,
        Wall,
    }

    public class GridCell2D : IComparable
    {
        public static Action<GridCell2D> BecomeTarget = delegate { };

        GridConnected2D grid;
        Vector2Int index = Vector2Int.zero;
        public Vector2Int Index => index;

        List<GridCell2D> neighbors = new List<GridCell2D>();
        public List<GridCell2D> Neighbors => neighbors;

        GridCell2DMono mono = null;


        public static Color startColor = Color.black;
        public static Color defaultColor = Color.cyan;

        public static Color wallColor = Color.red;

        public static Color targetColor = Color.green;
        public static Color frontierColor = Color.blue;

        public static Color pathColor = Color.yellow;
        public static Color visitedColor = Color.white;

        bool fourNeibs = false;

        //MouseState mouseState = MouseState.None;

        WalkState walkState = WalkState.Walkable;

        int cost = 1;
        public int Cost { get => cost; set => cost = value; }

        int distanceToTarget = int.MaxValue;
        public int DistanceToTarget { get => distanceToTarget; set => distanceToTarget = value; }

        int distanceToTargetAndCost = 1;
        public int DistanceToTargetAndCost
        {
            get
            {
                return distanceToTarget + cost;
            }
        }

        public GridCell2D(GridConnected2D grid, Vector2Int index, GridCell2DMono mono, WalkState walkState, int cost = 1)
        {
            this.grid = grid;
            this.index = index;

            this.mono = mono;
            this.mono.cell = this;

            this.cost = cost;

            SetColor(defaultColor);

            this.walkState = walkState;
            switch (walkState)
            {
                case WalkState.Start:
                    mono.SetColor(startColor);
                    break;

                case WalkState.Target:
                    mono.SetColor(targetColor);
                    break;

                case WalkState.Walkable:
                    break;

                case WalkState.Wall:
                    mono.SetColor(wallColor);
                    break;
            }
        }

        float ColorCostFactor()
        {
            float comp = (float)(10 - cost);
            float deno = 10f;

            return comp / deno;
        }

        public void Run()
        {
            //ColorOnMouseState();

            //SetNeighborsMouseState();
        }

        //private void SetNeighborsMouseState()
        //{
        //    switch(mouseState)
        //    {
        //        case MouseState.None:
        //            //SetNeiborsMouseState(MouseState.None);
        //            break;

        //        case MouseState.Hover:
        //            SetNeiborsMouseState(MouseState.NeighborOfHover);
        //            break;

        //        case MouseState.NeighborOfHover:
        //            break;
        //    }    
        //}

        public void SetMeAsTarget()
        {
            BecomeTarget(this);
        }

        public void SetColor(Color color)
        {
            if (walkState == WalkState.Wall || walkState == WalkState.Start || walkState == WalkState.Target)
                return;

            mono.SetColor(color * ColorCostFactor());
        }

        public bool IsWall()
        {
            //return false;
            return walkState == WalkState.Wall;
        }

        //public void SetNeiborsMouseState(MouseState state)
        //{
        //    foreach(GridCell2D t in neighbors)
        //    {
        //        t.SetMouseState(state);
        //    }
        //}

        //public void SetMouseState(MouseState newState)
        //{
        //    mouseState = newState;
        //}

        //private void ColorOnMouseState()
        //{
        //    switch(mouseState)
        //    {
        //        case MouseState.None:
        //            break;

        //        case MouseState.Hover:
        //            SetColor(hoverColor);
        //            break;

        //        case MouseState.NeighborOfHover:
        //            break;
        //    }
        //}

        public void StoreNeighbors()
        {
            neighbors.Clear();
            if (IsDownLimit() && !IsDownLeftLimit() && !IsDownRightLimit())
            {
                neighbors.Add(grid.Cells[index.x - 1, index.y]);

                if (fourNeibs == false)
                    neighbors.Add(grid.Cells[index.x - 1, index.y + 1]);

                neighbors.Add(grid.Cells[index.x, index.y + 1]);

                if (fourNeibs == false)
                    neighbors.Add(grid.Cells[index.x + 1, index.y + 1]);

                neighbors.Add(grid.Cells[index.x + 1, index.y]);
            }
            else if (IsUpLimit() && !IsUpLeftLimit() && !IsUpRightLimit())
            {
                neighbors.Add(grid.Cells[index.x - 1, index.y]);

                if (fourNeibs == false)
                    neighbors.Add(grid.Cells[index.x - 1, index.y - 1]);
                neighbors.Add(grid.Cells[index.x, index.y - 1]);

                if (fourNeibs == false)
                    neighbors.Add(grid.Cells[index.x + 1, index.y - 1]);

                neighbors.Add(grid.Cells[index.x + 1, index.y]);
            }

            else if (IsLeftLimit() && !IsDownLeftLimit() && !IsUpLeftLimit())
            {
                neighbors.Add(grid.Cells[index.x, index.y - 1]);

                if (fourNeibs == false)
                    neighbors.Add(grid.Cells[index.x + 1, index.y - 1]);

                neighbors.Add(grid.Cells[index.x + 1, index.y]);

                if (fourNeibs == false)
                    neighbors.Add(grid.Cells[index.x + 1, index.y + 1]);

                neighbors.Add(grid.Cells[index.x, index.y + 1]);
            }
            else if (IsRightLimit() && !IsDownRightLimit() && !IsUpRightLimit())
            {
                neighbors.Add(grid.Cells[index.x, index.y - 1]);

                if (fourNeibs == false)
                    neighbors.Add(grid.Cells[index.x - 1, index.y - 1]);

                neighbors.Add(grid.Cells[index.x - 1, index.y]);

                if (fourNeibs == false)
                    neighbors.Add(grid.Cells[index.x - 1, index.y + 1]);

                neighbors.Add(grid.Cells[index.x, index.y + 1]);
            }

            else if (IsDownLeftLimit())
            {
                neighbors.Add(grid.Cells[index.x, index.y + 1]);

                if (fourNeibs == false)
                    neighbors.Add(grid.Cells[index.x + 1, index.y + 1]);

                neighbors.Add(grid.Cells[index.x + 1, index.y]);
            }
            else if (IsUpLeftLimit())
            {
                neighbors.Add(grid.Cells[index.x, index.y - 1]);

                if (fourNeibs == false)
                    neighbors.Add(grid.Cells[index.x + 1, index.y - 1]);

                neighbors.Add(grid.Cells[index.x + 1, index.y]);
            }

            else if (IsDownRightLimit())
            {
                neighbors.Add(grid.Cells[index.x, index.y + 1]);

                if (fourNeibs == false)
                    neighbors.Add(grid.Cells[index.x - 1, index.y + 1]);

                neighbors.Add(grid.Cells[index.x - 1, index.y]);
            }
            else if (IsUpRightLimit())
            {
                neighbors.Add(grid.Cells[index.x, index.y - 1]);

                if (fourNeibs == false)
                    neighbors.Add(grid.Cells[index.x - 1, index.y - 1]);

                neighbors.Add(grid.Cells[index.x - 1, index.y]);
            }

            else
            {
                neighbors.Add(grid.Cells[index.x - 1, index.y]);

                if (fourNeibs == false)
                    neighbors.Add(grid.Cells[index.x - 1, index.y + 1]);

                neighbors.Add(grid.Cells[index.x, index.y + 1]);

                if (fourNeibs == false)
                    neighbors.Add(grid.Cells[index.x + 1, index.y + 1]);

                neighbors.Add(grid.Cells[index.x + 1, index.y]);

                if (fourNeibs == false)
                    neighbors.Add(grid.Cells[index.x - 1, index.y - 1]);

                neighbors.Add(grid.Cells[index.x, index.y - 1]);

                if (fourNeibs == false)
                    neighbors.Add(grid.Cells[index.x + 1, index.y - 1]);
            }
        }

        bool IsDownLimit()
        {
            return index.y == 0;
        }

        bool IsUpLimit()
        {
            return index.y == grid.Height - 1;
        }

        bool IsLeftLimit()
        {
            return index.x == 0;
        }

        bool IsRightLimit()
        {
            return index.x == grid.Width - 1;
        }

        bool IsDownLeftLimit()
        {
            return IsDownLimit() && IsLeftLimit();
        }

        bool IsUpLeftLimit()
        {
            return IsUpLimit() && IsLeftLimit();
        }

        bool IsUpRightLimit()
        {
            return IsUpLimit() && IsRightLimit();
        }

        bool IsDownRightLimit()
        {
            return IsDownLimit() && IsRightLimit();
        }

        int IComparable.CompareTo(object obj)
        {
            GridCell2D other = obj as GridCell2D;
            if (other == null)
                return 0;

            if (cost < other.cost)
                return 1;
            else if (cost > other.cost)
                return -1;
            else
                return 0;
        }
    }

    public class GridCell2DDistanceToTargetComparer : IComparer<GridCell2D>
    {
        public int Compare(GridCell2D x, GridCell2D y)
        {
            if (x.DistanceToTarget < y.DistanceToTarget)
                return 1;
            else if (x.DistanceToTarget > y.DistanceToTarget)
                return -1;
            else
                return 0;
        }
    }

    public class GridCell2DDistanceToTargetAndCostComparer : IComparer<GridCell2D>
    {
        public int Compare(GridCell2D x, GridCell2D y)
        {
            if (x.DistanceToTargetAndCost < y.DistanceToTargetAndCost)
                return 1;
            else if (x.DistanceToTargetAndCost > y.DistanceToTargetAndCost)
                return -1;
            else
                return 0;
        }
    }
}