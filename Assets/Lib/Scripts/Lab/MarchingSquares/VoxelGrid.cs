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

        Material[] voxelMaterials = new Material[0];

        public void Init(int resolution, float size)
        {
            this.resolution = resolution;
            voxelSize = size / resolution;

            voxels = new bool[resolution * resolution];
            voxelMaterials = new Material[voxels.Length];

            for (int y = 0, i = 0; y < resolution; y++)
            {
                for (int x = 0; x < resolution; x++, i++)
                {
                    CreateVoxel(i, x, y);
                }
            }
            SetVoxelColors();
        }

        private void SetVoxelColors()
        {
            for (int i = 0; i < voxels.Length; i++)
            {
                voxelMaterials[i].color = voxels[i] ? Color.black : Color.white;
            }
        }

        private void CreateVoxel(int i, int x, int y)
        {
            GameObject go = Instantiate(voxelPrefab);
            go.transform.parent = transform;
            go.transform.localPosition = new Vector3(x + 0.5f, y + 0.5f, 0f) * voxelSize;
            go.transform.localScale = Vector3.one * voxelSize * 0.9f;

            voxelMaterials[i] = go.GetComponent<MeshRenderer>().material;
        }

        public void Apply(VoxelStencile stencile)
        {
            for (int y = stencile.yStart; y <= stencile.yEnd; y++)
            {
                int i = y * resolution + stencile.xStart;
                for (int x = stencile.xStart; x <= stencile.xEnd; x++, i++)
                {
                    voxels[i] = stencile.Apply(x, y, voxels[i]);
                }
            }

            SetVoxelColors();
        }
    }
}