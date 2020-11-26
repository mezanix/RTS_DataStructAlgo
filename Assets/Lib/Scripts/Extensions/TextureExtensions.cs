using System.IO;
using UnityEngine;

namespace FutureGames.Lib
{
    public static class TextureExtensions
    {
        public static void PngToDisk(this Texture2D t, string path = "")
        {
            if (string.IsNullOrEmpty(path))
            {
#if UNITY_EDITOR
                path = Application.dataPath + "/" + "MyTexture" + ".png";
#else
                path = Application.persistentDataPath + "/" + "MyTexture" + ".png";
#endif
            }

            Debug.Log(path);

            byte[] bytes = t.EncodeToPNG();

            File.WriteAllBytes(path, bytes);
        }

        public static Vector2 PixelPositionToUv(this Texture2D t, int x, int y)
        {
            return new Vector2(
                (float)x / (float)(t.width - 1),
                (float)y / (float)(t.height - 1));
        }

        public static Vector2Int UvToPixelPosition(this Texture2D t, float x, float y)
        {
            return new Vector2Int(
                Mathf.RoundToInt(x * (t.width - 1)),
                Mathf.RoundToInt(y * (t.height - 1)));
        }

        public static Vector3 AverageOfHSV(this Texture2D t, int x, int y, int step)
        {
            Color[] neibs = new Color[8];

            neibs[0] = t.GetPixel(x - step, y - step);
            neibs[1] = t.GetPixel(x - step, y);
            neibs[2] = t.GetPixel(x - step, y + step);
                       
            neibs[3] = t.GetPixel(x, y + 1);
                       
            neibs[4] = t.GetPixel(x + step, y - step);
            neibs[5] = t.GetPixel(x + step, y);
            neibs[6] = t.GetPixel(x + step, y + step);
                       
            neibs[7] = t.GetPixel(x, y - step);

            return new Vector3(
                HueAverage(neibs),
                SaturationAverage(neibs),
                ValueAverage(neibs));
        }

        public static float HueAverage(Color[] colors)
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

        public static float SaturationAverage(Color[] colors)
        {
            float r = 0f;

            for (int i = 0; i < colors.Length; i++)
            {
                float h = 0f;
                float s = 0f;
                float v = 0f;
                Color.RGBToHSV(colors[i], out h, out s, out v);

                r += s;
            }

            return r / colors.Length;
        }

        public static float ValueAverage(Color[] colors)
        {
            float r = 0f;

            for (int i = 0; i < colors.Length; i++)
            {
                float h = 0f;
                float s = 0f;
                float v = 0f;
                Color.RGBToHSV(colors[i], out h, out s, out v);

                r += v;
            }

            return r / colors.Length;
        }



    }
}