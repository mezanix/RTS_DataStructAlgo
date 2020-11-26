using FutureGames.Lib;
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

        public Texture2D LaplacianMapOnHSV()
        {
            Texture2D r = new Texture2D(texture.width, texture.height, TextureFormat.RGB24, false);
            r.filterMode = FilterMode.Point;

            for (int y = 0; y < texture.height; y++)
            {
                for (int x = 0; x < texture.width; x++)
                {
                    float laplaceHue = LaplacianOnHSV(x, y);
                    r.SetPixel(x, y, new Color(laplaceHue, laplaceHue, laplaceHue, 1f));
                }
            }

            r.Apply();
            return r;
        }

        private float LaplacianOnHSV(int x, int y)
        {
            return Vector3.Distance(texture.GetPixel(x, y).GetHsv(), texture.AverageOfHSV(x, y, step));
        }
}
}