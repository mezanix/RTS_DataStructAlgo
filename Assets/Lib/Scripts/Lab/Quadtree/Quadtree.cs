using System.Collections.Generic;
using UnityEngine;

namespace FutureGames.Lab.QuadtreeSpace
{
    public class Quadtree
    {
        Rectangle boundary = null;
        List<Point> points = new List<Point>();

        int capacity = 0;
        bool divided = false;

        Quadtree northEast = null;
        Quadtree northWest = null;
        Quadtree southEast = null;
        Quadtree southWest = null;

        List<Point> addedBeforeDivide = new List<Point>();

        public Quadtree(Rectangle boundary, int capacity)
        {
            this.boundary = boundary;
            this.capacity = capacity;
        }

        public bool Insert(Point point)
        {
            if (boundary.Contains(point) == false)
            {
                return false;
            }

            if (points.Count < capacity)
            {
                points.Add(point);
                addedBeforeDivide.Add(point);
                return true;
            }
            else if (points.Count >= capacity)
            {
                if (divided == false)
                {
                    points.Clear();
                    Subdivide();
                }

                if (northEast.Insert(point) == true)
                {
                    return true;
                }
                else if (northWest.Insert(point) == true)
                {
                    return true;
                }
                else if (southEast.Insert(point) == true)
                {
                    return true;
                }
                else if (southWest.Insert(point) == true)
                {
                    return true;
                }
                addedBeforeDivide.Clear();
            }

            // error
            return false;
        }

        private void Subdivide()
        {
            northEast = new Quadtree(boundary.NorthEast, capacity);
            northWest = new Quadtree(boundary.NorthWest, capacity);

            southEast = new Quadtree(boundary.SouthEast, capacity);
            southWest = new Quadtree(boundary.SouthWest, capacity);

            divided = true;
        }

        public void Show(Texture2D texture)
        {
            for (float x = boundary.West; x < boundary.East; x += 1f)
            {
                texture.SetPixel((int)x, (int)boundary.South, Color.red);
                texture.SetPixel((int)x, (int)boundary.North, Color.green);
            }

            for (float y = boundary.South; y < boundary.North; y += 1f)
            {
                texture.SetPixel((int)boundary.West, (int)y, Color.blue);
                texture.SetPixel((int)boundary.East, (int)y, Color.grey);
            }

            if (divided == true)
            {
                northEast.Show(texture);
                northWest.Show(texture);

                southEast.Show(texture);
                southWest.Show(texture);
            }

            for (int i = 0; i < points.Count; i++)
            {
                texture.SetPixel((int)points[i].x, (int)points[i].y, Color.white);
            }
        }
    }
}