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
    }
}