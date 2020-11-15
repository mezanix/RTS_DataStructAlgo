using UnityEngine;

namespace FutureGames.Lib
{
    public class TestPerlinNoise : MonoBehaviour
    {
        [SerializeField]
        int width = 512;

        [SerializeField]
        int height = 512;

        [SerializeField]
        float scale = 1f;

        [SerializeField]
        int octaves = 3;

        [SerializeField]
        float persistance = 1.2f;

        [SerializeField]
        float lacunarity = 2f;

        void Start()
        {
            PerlinNoise.GenerateHeightmap(width, height, scale, octaves, persistance, lacunarity).PngToDisk(
                Application.dataPath + "/" + "MyTexture" +
                "O_" + octaves + "__" +
                "P_" + persistance + "__" +
                "L_" + lacunarity + "__" + ".png");
        }
    }
}