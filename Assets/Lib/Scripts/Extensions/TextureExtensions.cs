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
    }
}