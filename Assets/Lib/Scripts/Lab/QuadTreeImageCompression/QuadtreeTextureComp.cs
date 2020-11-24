using System.Collections.Generic;
using UnityEngine;
using System;

namespace FutureGames.Lab.QuadtreeSpace
{
    public class QuadtreeTextureComp : IComparable
    {
        public Rectangle boundary = null;
        bool divided = false;

        Texture2D map = null;

        QuadtreeTextureComp northEast = null;
        QuadtreeTextureComp northWest = null;
        QuadtreeTextureComp southEast = null;
        QuadtreeTextureComp southWest = null;

        float maxCumulatedWhite = 0f;

        const float relativeLimit = 0.1f;

        public QuadtreeTextureComp(Rectangle boundary, Texture2D map)
        {
            this.boundary = boundary;
            this.map = map;

            maxCumulatedWhite = MaxCumulatedWhite(map);

            //Debug.Log(this);
        }

        public override string ToString()
        {
            return "MaxCumul: " + maxCumulatedWhite + " RelativeCumul: " + RelativeCumulatedWhite();
        }

        float RelativeCumulatedWhite()
        {
            return PartialCumulatedWhite(map) / maxCumulatedWhite;
        }

        public void Show(Texture2D texture)
        {
            Color color = Color.cyan;

            for (float x = boundary.West; x < boundary.East; x += 1f)
            {
                texture.SetPixel((int)x, (int)boundary.South, color);
                texture.SetPixel((int)x, (int)boundary.North, color);
            }

            for (float y = boundary.South; y < boundary.North; y += 1f)
            {
                texture.SetPixel((int)boundary.West, (int)y, color);
                texture.SetPixel((int)boundary.East, (int)y, color);
            }

            if (divided == true)
            {
                northEast.Show(texture);
                northWest.Show(texture);

                southEast.Show(texture);
                southWest.Show(texture);
            }
        }

        public void Subdivide()
        {
            if (divided == true)
                return;

            if (RelativeCumulatedWhite() < relativeLimit)
                return;

            DoSubdivide();
        }

        void DoSubdivide()
        {
            northEast = new QuadtreeTextureComp(boundary.NorthEast, map);
            northWest = new QuadtreeTextureComp(boundary.NorthWest, map);

            southEast = new QuadtreeTextureComp(boundary.SouthEast, map);
            southWest = new QuadtreeTextureComp(boundary.SouthWest, map);

            divided = true;

            northEast.Subdivide();
            northWest.Subdivide();
            southEast.Subdivide();
            southWest.Subdivide();
        }

        float PartialCumulatedWhite(Texture2D texture)
        {
            float r = 0f;
            for (float y = boundary.South; y < boundary.North; y += 1f)
            {
                for (float x = boundary.West; x < boundary.East; x+= 1f)
                {
                    r += texture.GetPixel((int)x, (int)y).r;
                }
            }
            return r;
        }
        float MaxCumulatedWhite(Texture2D texture)
        {
            float r = 0f;
            for (int y = 0; y < texture.height; y ++)
            {
                for (int x = 0; x < texture.width; x++)
                {
                    r += texture.GetPixel(x, y).r;
                }
            }
            return r;
        }

        void Paint(Texture2D texture, Color color)
        {
            for (float y = boundary.South; y < boundary.North; y += 1f)
            {
                for (float x = boundary.West; x < boundary.East; x++)
                {
                    texture.SetPixel((int)x, (int)y, color);
                }
            }
        }
    
        public void LeafsCount(ref int count)
        {
            if (divided == true)
            {
                northEast.LeafsCount(ref count);
                northWest.LeafsCount(ref count);
                southWest.LeafsCount(ref count);
                southEast.LeafsCount(ref count);
            }
            else if(divided == false)
            {
                count += 1;
            }
        }

        public void GetLeafsBoundaries(List<Rectangle> rectangles)
        {
            if (divided == true)
            {
                northEast.GetLeafsBoundaries(rectangles);
                northWest.GetLeafsBoundaries(rectangles);
                southWest.GetLeafsBoundaries(rectangles);
                southEast.GetLeafsBoundaries(rectangles);
            }
            else if (divided == false)
            {
                rectangles.Add(boundary);
            }
        }

        public void GetLeafs(List<QuadtreeTextureComp> leafs)
        {
            if (divided == true)
            {
                northEast.GetLeafs(leafs);
                northWest.GetLeafs(leafs);
                southWest.GetLeafs(leafs);
                southEast.GetLeafs(leafs);
            }
            else if (divided == false)
            {
                leafs.Add(this);
            }
        }

        public int CompareTo(object obj)
        {
            QuadtreeTextureComp other = obj as QuadtreeTextureComp;

            if (boundary.DoubleWidth < other.boundary.DoubleWidth)
                return -1;
            else if (boundary.DoubleWidth > other.boundary.DoubleWidth)
                return 1;
            else
                return 0;
        }
    
        public Vector2 SmallestSize()
        {
            List<QuadtreeTextureComp> leafs = new List<QuadtreeTextureComp>();
            GetLeafs(leafs);
            Debug.Log("leafs: " + leafs.Count);

            leafs.Sort();
            return new Vector2(leafs[0].boundary.DoubleWidth, leafs[0].boundary.DoubleHeight);
        }

        public Vector2Int CompressedTextureSize()
        {
            Vector2 smallestSize = SmallestSize();

            int width = map.width / (int)smallestSize.x;
            return new Vector2Int(width, (int)(((float)map.height / (float)map.width) * width));
        }
    }
}