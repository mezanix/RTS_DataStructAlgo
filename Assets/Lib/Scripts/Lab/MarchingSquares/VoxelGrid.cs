using System;
using UnityEngine;

namespace FutureGames.Lab
{
    public class VoxelGrid : MonoBehaviour
    {
        public int resolution = 8;
        bool[] voxels = new bool[0];

        public GameObject voxelPrefab = null;

        float voxelSize = 0.1f;

        public void Init(int resolution, float size)
        {
            this.resolution = resolution;
            voxelSize = size / resolution;

            voxels = new bool[resolution * resolution];

            for (int y = 0, i = 0; y < resolution; y++)
            {
                for (int x = 0; x < resolution; x++, i++)
                {
                    CreateVoxel(i, x, y);
                }
            }
        }

        private void CreateVoxel(int i, int x, int y)
        {
            GameObject go = Instantiate(voxelPrefab);
            go.transform.parent = transform;
            go.transform.localPosition = new Vector3(x + 0.5f, y + 0.5f, 0f) * voxelSize;
            go.transform.localScale = Vector3.one * voxelSize * 0.9f;
        }
    }
}