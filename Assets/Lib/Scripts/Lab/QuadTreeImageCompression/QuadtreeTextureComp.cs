using UnityEngine;

namespace FutureGames.Lab.QuadtreeSpace
{
    public class QuadtreeTextureComp
    {
        Rectangle boundary = null;
        bool divided = false;

        Texture2D map = null;

        QuadtreeTextureComp northEast = null;
        QuadtreeTextureComp northWest = null;
        QuadtreeTextureComp southEast = null;
        QuadtreeTextureComp southWest = null;

        float maxCumulatedWhite = 0f;

        public QuadtreeTextureComp(Rectangle boundary, Texture2D map)
        {
            this.boundary = boundary;
            this.map = map;

            maxCumulatedWhite = CumulatedWhite(map);
        }

        float RelativeCumulatedWhite()
        {
            return CumulatedWhite(map) / maxCumulatedWhite;
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

        public void Subdivide(Texture2D laplacianMap)
        {
        }

        void DoSubdivide()
        {
            northEast = new QuadtreeTextureComp(boundary.NorthEast, map);
            northWest = new QuadtreeTextureComp(boundary.NorthWest, map);

            southEast = new QuadtreeTextureComp(boundary.SouthEast, map);
            southWest = new QuadtreeTextureComp(boundary.SouthWest, map);

            divided = true;
        }

        float CumulatedWhite(Texture2D texture)
        {
            float r = 0f;
            for (float y = boundary.South; y < boundary.North; y += 1f)
            {
                for (float x = boundary.West; x < boundary.East; x++)
                {
                    r += texture.GetPixel((int)x, (int)y).r;
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
    }
}