using FutureGames.Lib;
using System;
using UnityEngine;

namespace FutureGames.Lab.QuadtreeSpace
{
    /// <summary>
    /// Laplacian = deviation between a value and the surrounding average.
    /// </summary>
    public class LaplacianOnTexture
    {
        int step = 1;
        Texture2D texture = null;

        public LaplacianOnTexture(Texture2D texture)
        {
            this.texture = texture;
        }

        public Texture2D LaplacianMapOnHue()
        {
            Texture2D r = new Texture2D(texture.width, texture.height, TextureFormat.RGB24, false);
            r.filterMode = FilterMode.Point;

            for (int y = 0; y < texture.height; y++)
            {
                for (int x = 0; x < texture.width; x++)
                {
                    float laplaceHue = LaplacianOnHue(x, y);
                    r.SetPixel(x, y, new Color(laplaceHue, laplaceHue, laplaceHue, 1f));
                }
            }

            r.Apply();
            return r;
        }

        private float LaplacianOnHue(int x, int y)
        {
            return Mathf.Abs(texture.GetPixel(x, y).GetHsv().x - AverageOfHue(x, y));
        }

        private float AverageOfHue(int x, int y)
        {
            Color[] neibs = new Color[8];

            neibs[0] = texture.GetPixel(x - step, y - step);
            neibs[1] = texture.GetPixel(x - step, y);
            neibs[2] = texture.GetPixel(x - step, y + step);

            neibs[3] = texture.GetPixel(x, y + 1);

            neibs[4] = texture.GetPixel(x + step, y - step);
            neibs[5] = texture.GetPixel(x + step, y);
            neibs[6] = texture.GetPixel(x + step, y + step);

            neibs[7] = texture.GetPixel(x, y - step);

            return HueAverage(neibs);
        }

        float HueAverage(Color[] colors)
        {
            float r = 0f;

            for (int i = 0; i < colors.Length; i++)
            {
                float h = 0f;
                float s = 0f;
                float v = 0f;
                Color.RGBToHSV(colors[i], out h, out s, out v);

                r += h;
            }

            return r / colors.Length;
        }
    }
}