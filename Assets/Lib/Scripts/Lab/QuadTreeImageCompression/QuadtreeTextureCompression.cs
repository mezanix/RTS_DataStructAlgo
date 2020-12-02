using FutureGames.Lib;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FutureGames.Lab.QuadtreeSpace
{
    public class QuadtreeTextureCompression : IComparable
    {
        public Rectangle boundary = null;
        bool divided = false;

        Texture2D map = null;
        Texture2D sourceImage = null;

        QuadtreeTextureCompression northEast = null;
        QuadtreeTextureCompression northWest = null;
        QuadtreeTextureCompression southEast = null;
        QuadtreeTextureCompression southWest = null;

        float maxCumulatedWhite = 0f;

        const float relativeLimit = 0.01f;

        public QuadtreeTextureCompression(Rectangle boundary, Texture2D map, Texture2D sourceImage)
        {
            this.boundary = boundary;
            this.map = map;
            this.sourceImage = sourceImage;

            maxCumulatedWhite = MaxCumulatedWhite(map);

            //Debug.Log(this);
        }

        /// <summary>
        /// Take colors from the source image and apply them throu the quadtree leafs to the compressed texture
        /// </summary>
        /// <param name="texture"></param>
        public void Colorize(Texture2D texture)
        {
            List<QuadtreeTextureCompression> leafs = new List<QuadtreeTextureCompression>();
            GetLeafs(leafs);

            for (int y = 0; y < texture.height; y++)
            {
                for (int x = 0; x < texture.width; x++)
                {
                    Vector2 uv = texture.PixelPositionToUv(x, y);

                    Vector2Int positionInSource = sourceImage.UvToPixelPosition(uv.x, uv.y);

                    for (int i = 0; i < leafs.Count; i++)
                    {
                        if (leafs[i].boundary.Contains(
                            new Point((float)positionInSource.x, 
                            (float)positionInSource.y)) == false)
                            continue;

                        texture.SetPixel(x, y, leafs[i].GetDominantColorFromSource());
                        break;
                    }
                }
            }
        }

        public override string ToString()
        {
            return "MaxCumul: " + maxCumulatedWhite + " RelativeCumul: " + RelativeCumulatedWhite();
        }

        float RelativeCumulatedWhite()
        {
            return PartialCumulatedWhite(map) / maxCumulatedWhite;
        }
        float PartialCumulatedWhite(Texture2D texture)
        {
            float r = 0f;
            for (float y = boundary.South; y < boundary.North; y += 1f)
            {
                for (float x = boundary.West; x < boundary.East; x += 1f)
                {
                    r += texture.GetPixel((int)x, (int)y).r;
                }
            }
            return r;
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
            northEast = new QuadtreeTextureCompression(boundary.NorthEast, map, sourceImage);
            northWest = new QuadtreeTextureCompression(boundary.NorthWest, map, sourceImage);

            southEast = new QuadtreeTextureCompression(boundary.SouthEast, map, sourceImage);
            southWest = new QuadtreeTextureCompression(boundary.SouthWest, map, sourceImage);

            divided = true;

            northEast.Subdivide();
            northWest.Subdivide();
            southEast.Subdivide();
            southWest.Subdivide();
        }

        float MaxCumulatedWhite(Texture2D texture)
        {
            float r = 0f;
            for (int y = 0; y < texture.height; y++)
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

        public void PaintUsingDominantColor(Texture2D texture)
        {
            List<QuadtreeTextureCompression> leafs = new List<QuadtreeTextureCompression>();
            GetLeafs(leafs);

            foreach (QuadtreeTextureCompression leaf in leafs)
            {
                leaf.Paint(texture, leaf.GetDominantColorFromSource());
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
            else if (divided == false)
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

        public void GetLeafs(List<QuadtreeTextureCompression> leafs)
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

        public void GetLeafsAndColors(List<QuadtreeTextureCompressionAndColor> leafs)
        {
            if (divided == true)
            {
                northEast.GetLeafsAndColors(leafs);
                northWest.GetLeafsAndColors(leafs);
                southWest.GetLeafsAndColors(leafs);
                southEast.GetLeafsAndColors(leafs);
            }
            else if (divided == false)
            {
                leafs.Add(new QuadtreeTextureCompressionAndColor(this, GetDominantColorFromSource()));
            }
        }

        private Color GetDominantColorFromSource()
        {
            //Color[] colors =
            //    sourceImage.GetPixels(
            //        (int)boundary.West, (int)boundary.South, 
            //        (int)boundary.DoubleWidth, (int)boundary.DoubleHeight);

            //Vector3 hsvAverage = new Vector3(
            //    TextureExtensions.HueAverage(colors),
            //    TextureExtensions.SaturationAverage(colors),
            //    TextureExtensions.ValueAverage(colors));

            //return Color.HSVToRGB(hsvAverage.x, hsvAverage.y, hsvAverage.z);

            return sourceImage.GetPixel((int)boundary.centerX, (int)boundary.centerY);
        }

        private Color GetDominantColorFromSource(int x, int y)
        {
            //Debug.Log(" " + x + " " + y);
            //Debug.Log(" " + (float)x + " " + (float)y);
            //Debug.Log(boundary.centerX);
            if (boundary.Contains(new Point((float)x, (float)y)) == false)
                return Color.magenta;

            return GetDominantColorFromSource();
        }

        public int CompareTo(object obj)
        {
            QuadtreeTextureCompression other = obj as QuadtreeTextureCompression;

            if (boundary.DoubleWidth < other.boundary.DoubleWidth)
                return -1;
            else if (boundary.DoubleWidth > other.boundary.DoubleWidth)
                return 1;
            else
                return 0;
        }

        public Vector2 SmallestSize()
        {
            List<QuadtreeTextureCompression> leafs = new List<QuadtreeTextureCompression>();
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

    public class QuadtreeTextureCompressionAndColor
    {
        public QuadtreeTextureCompression quadtree = null;
        public Color color = Color.black;

        public QuadtreeTextureCompressionAndColor(QuadtreeTextureCompression quadtree, Color color)
        {
            this.quadtree = quadtree;
            this.color = color;
        }
    }
}