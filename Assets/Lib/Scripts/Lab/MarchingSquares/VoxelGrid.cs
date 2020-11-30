using System;
using System.Collections.Generic;
using UnityEngine;

namespace FutureGames.Lab
{
    public class VoxelGrid : MonoBehaviour
    {
        public int resolution = 8;
        Voxel[] voxels = new Voxel[0];

        public GameObject voxelPrefab = null;

        float voxelSize = 0.1f;

        Material[] voxelMaterials = new Material[0];

        Mesh mesh = null;
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        public void Init(int resolution, float size)
        {
            this.resolution = resolution;
            voxelSize = size / resolution;

            voxels = new Voxel[resolution * resolution];
            voxelMaterials = new Material[voxels.Length];

            for (int y = 0, i = 0; y < resolution; y++)
            {
                for (int x = 0; x < resolution; x++, i++)
                {
                    CreateVoxel(i, x, y);
                }
            }

            GetComponent<MeshFilter>().mesh = mesh = new Mesh();
            mesh.name = "VoxelGrid Mesh";
            vertices = new List<Vector3>();
            triangles = new List<int>();

            Refresh();
        }

        private void Refresh()
        {
            SetVoxelColors();
            Triangulate();
        }

        private void Triangulate()
        {
            vertices.Clear();
            triangles.Clear();
            mesh.Clear();

            TriangulateCellRows();

            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
        }

        private void TriangulateCellRows()
        {
            int cells = resolution - 1;
            for (int y = 0, i = 0; y < cells; y++, i++)
            {
                for (int x = 0; x < cells; x++, i++)
                {
                    TriangulateCell(
                        voxels[i],
                        voxels[i+1],
                        voxels[i+resolution],
                        voxels[i+resolution+1]);
                }
            }
        }

        private void TriangulateCell(Voxel a, Voxel b, Voxel c, Voxel d)
        {
            int cellType = 0;
            if (a.state)
                cellType |= 1;
            if (b.state)
                cellType |= 2;
            if (c.state)
                cellType |= 4;
            if (d.state)
                cellType |= 8;
        }

        private void SetVoxelColors()
        {
            for (int i = 0; i < voxels.Length; i++)
            {
                voxelMaterials[i].color = voxels[i].state ? Color.black : Color.white;
            }
        }

        private void CreateVoxel(int i, int x, int y)
        {
            GameObject go = Instantiate(voxelPrefab);
            go.transform.parent = transform;
            go.transform.localPosition = new Vector3(x + 0.5f, y + 0.5f, -0.01f) * voxelSize;
            go.transform.localScale = Vector3.one * voxelSize * 0.1f;

            voxelMaterials[i] = go.GetComponent<MeshRenderer>().material;
        }

        public void Apply(VoxelStencile stencile)
        {
            for (int y = stencile.yStart; y <= stencile.yEnd; y++)
            {
                int i = y * resolution + stencile.xStart;
                for (int x = stencile.xStart; x <= stencile.xEnd; x++, i++)
                {
                    voxels[i].state = stencile.Apply(x, y, voxels[i].state);
                }
            }

            Refresh();
        }
    }
}