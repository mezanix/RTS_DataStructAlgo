using FutureGames.Lib;
using UnityEngine;

namespace FutureGames.Algorts
{
    public class PerlinToTerrain : MonoBehaviour
    {
        Terrain terrain = null;
        Terrain Terrain
        {
            get
            {
                if (terrain == null)
                    terrain = GetComponent<Terrain>();
                return terrain;
            }
        }


        [SerializeField]
        int width = 512;

        [SerializeField]
        int height = 512;

        [SerializeField]
        float scale = 400f;

        [SerializeField]
        int octaves = 3;

        [SerializeField]
        float persistance = 1.2f;

        [SerializeField]
        float lacunarity = 2f;

        //TerrainData terrainData = null;

        float[,] heightmap = new float[0, 0];

        private void Start()
        {
            heightmap = PerlinNoise.Generate(width, height, scale, octaves, persistance, lacunarity);

            Terrain.terrainData.SetHeights(0, 0, heightmap);

            Debug.Log(LocalToWorldFlat(IndicesToLocalFlat(512, 512)));
        }
        
        private void Update()
        {
        }

        public Vector3 PositionOnTerrain(float x, float z)
        {
            Vector2Int indices = WorldToIndiciesFlat(new Vector2(x, z));

            return PositionOnTerrain(indices.x, indices.y);
        }
        public Vector3 NormalOnTerrain(int x, int y)
        {
            Vector2 normalizedFlatPosition = IndiciesToNormalizedFlat(x, y);
            return Terrain.terrainData.GetInterpolatedNormal(normalizedFlatPosition.x, normalizedFlatPosition.y);
        }

        public Vector3 NormalOnTerrain(float x, float z)
        {
            Vector2Int indices = WorldToIndiciesFlat(new Vector2(x, z));

            return NormalOnTerrain(indices.x, indices.y);
        }

        Vector2Int WorldToIndiciesFlat(Vector2 position)
        {
            Vector2 local = WorldToLocalFlat(position);
            return LocalToIndicesFlat(local);
        }

        public Vector3 PositionOnTerrain(int x, int y)
        {
            Vector2 worldFlatPosition = LocalToWorldFlat(IndicesToLocalFlat(x, y));

            return new Vector3(
                worldFlatPosition.x, Terrain.terrainData.GetHeight(x, y), worldFlatPosition.y);
        }



        Vector2 IndiciesToNormalizedFlat(int x, int y)
        {
            return new Vector2(
                (float)x / (float)Terrain.terrainData.heightmapResolution,
                (float)y / (float)Terrain.terrainData.heightmapResolution);
        }

        Vector2Int NormalizedToIndiciesFlat(Vector2 position)
        {
            return new Vector2Int(
                Mathf.RoundToInt(position.x * Terrain.terrainData.heightmapResolution),
                Mathf.RoundToInt(position.y * Terrain.terrainData.heightmapResolution));
        }


        Vector2 IndicesToLocalFlat(int x, int y)
        {
            float terrainWidth = Terrain.terrainData.size.x;
            float terrainLength = Terrain.terrainData.size.z;

            int heightmapWidth = Terrain.terrainData.heightmapResolution;
            int heightmapLength = Terrain.terrainData.heightmapResolution;

            float ratioOnX = terrainWidth / heightmapWidth;
            float ratioOnY = terrainLength / heightmapLength;

            return new Vector2(
                x * ratioOnX,
                y * ratioOnY);
        }

        Vector2Int LocalToIndicesFlat(Vector2 position)
        {
            Vector2 normalizedFlatPosition = LocalToNormalizedFlat(position);

            return NormalizedToIndiciesFlat(normalizedFlatPosition);
        }


        Vector2 LocalToWorldFlat(Vector2 position)
        {
            return new Vector2(
                transform.position.x + position.x,
                transform.position.z + position.y);
        }
    
        Vector2 WorldToLocalFlat(Vector2 position)
        {
            return new Vector2(
                position.x - transform.position.x,
                position.y - transform.position.z);
        }
    
    
        Vector2 LocalToNormalizedFlat(Vector2 position)
        {
            return new Vector2(
                position.x / Terrain.terrainData.size.x,
                position.y / Terrain.terrainData.size.z);
        }
    }
}