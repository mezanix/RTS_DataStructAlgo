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
        float gridSize = 1f;

        Voxel dummy_x = null;
        Voxel dummy_y = null;
        Voxel dummy_t = null;

        Material[] voxelMaterials = new Material[0];

        Mesh mesh = null;
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        public VoxelGrid x_neib = null;
        public VoxelGrid y_neib = null;
        public VoxelGrid xy_neib = null;

        public void Init(int resolution, float size)
        {
            this.resolution = resolution;
            gridSize = size;
            voxelSize = size / resolution;

            voxels = new Voxel[resolution * resolution];
            voxelMaterials = new Material[voxels.Length];

            dummy_t = new Voxel();
            dummy_x = new Voxel();
            dummy_y = new Voxel();

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

            if(x_neib != null)
            {
                dummy_x.Become_X_DummyOf(x_neib.voxels[0], gridSize);
            }

            TriangulateCellRows();
            
            if (y_neib != null)
                TriangulateGapRow();

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
                if(x_neib != null)
                {
                    triangulateGapCell(i);
                }
            }
        }

        private void triangulateGapCell(int i)
        {
            Voxel dummySwap = dummy_t;
            dummySwap.Become_X_DummyOf(x_neib.voxels[i + 1], gridSize);

            dummy_t = dummy_x;
            dummy_x = dummySwap;

            TriangulateCell(voxels[i], dummy_t, voxels[i + resolution], dummy_x);
        }

        void TriangulateGapRow()
        {
            dummy_y.Become_Y_DummyOf(y_neib.voxels[0], gridSize);
            int cells = resolution - 1;
            int offset = cells * resolution;

            for (int x = 0; x < cells; x++)
            {
                Voxel dummySwap = dummy_t;
                dummySwap.Become_Y_DummyOf(y_neib.voxels[x + 1], gridSize);

                dummy_t = dummy_y;
                dummy_y = dummySwap;

                TriangulateCell(voxels[x + offset], voxels[x + offset + 1], dummy_t, dummy_y);
            }

            if(x_neib != null)
            {
                dummy_t.Become_XY_DummyOf(xy_neib.voxels[0], gridSize);

                TriangulateCell(voxels[voxels.Length - 1], dummy_x, dummy_y, dummy_t);
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

            switch(cellType)
            {
                case 0:
                    return;

                case 1:
                    Addtriangle(a.position, a.y_edgePosition, a.x_edgePosition);
                    break;

                case 2:
                    Addtriangle(b.position, a.x_edgePosition, b.y_edgePosition);
                    break;

                case 4:
                    Addtriangle(a.y_edgePosition, c.position, c.x_edgePosition);
                    break;

                case 8:
                    Addtriangle(c.x_edgePosition, d.position, b.y_edgePosition);
                    break;

                case 3:
                    AddQuad(a.position, a.y_edgePosition, b.y_edgePosition, b.position);
                    break;

                case 5:
                    AddQuad(a.position, c.position, c.x_edgePosition, a.x_edgePosition);
                    break;

                case 10:
                    AddQuad(c.x_edgePosition, d.position, b.position, a.x_edgePosition);
                    break;

                case 12:
                    AddQuad(a.y_edgePosition, c.position, d.position, b.y_edgePosition);
                    break;

                case 15:
                    AddQuad(a.position, c.position, d.position, b.position);
                    break;

                case 7:
                    AddPentagon(a.position, c.position, c.x_edgePosition, b.y_edgePosition, b.position);
                    break;

                case 11:
                    AddPentagon(b.position, a.position, a.y_edgePosition, c.x_edgePosition, d.position);
                    break;

                case 13:
                    AddPentagon(c.position, d.position, b.y_edgePosition, a.x_edgePosition, a.position);
                    break;

                case 14:
                    AddPentagon(d.position, b.position, a.x_edgePosition, a.y_edgePosition, c.position);
                    break;

                case 6:
                    Addtriangle(b.position, a.x_edgePosition, b.y_edgePosition);
                    Addtriangle(c.position, c.x_edgePosition, a.y_edgePosition);
                    break;

                case 9:
                    Addtriangle(a.position, a.y_edgePosition, a.x_edgePosition);
                    Addtriangle(d.position, b.y_edgePosition, c.x_edgePosition);
                    break;
            }
        }

        private void AddPentagon(Vector3 v0, Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4)
        {
            int vertexIndex = vertices.Count;
            vertices.Add(v0);
            vertices.Add(v1);
            vertices.Add(v2);
            vertices.Add(v3);
            vertices.Add(v4);

            triangles.Add(vertexIndex);
            triangles.Add(vertexIndex + 1);
            triangles.Add(vertexIndex + 2);

            triangles.Add(vertexIndex);
            triangles.Add(vertexIndex + 2);
            triangles.Add(vertexIndex + 3);

            triangles.Add(vertexIndex);
            triangles.Add(vertexIndex + 3);
            triangles.Add(vertexIndex + 4);
        }

        private void AddQuad(Vector3 v0, Vector3 v1, Vector3 v2, Vector3 v3)
        {
            int vertexIndex = vertices.Count;
            vertices.Add(v0);
            vertices.Add(v1);
            vertices.Add(v2);
            vertices.Add(v3);

            triangles.Add(vertexIndex);
            triangles.Add(vertexIndex + 1);
            triangles.Add(vertexIndex + 2);

            triangles.Add(vertexIndex);
            triangles.Add(vertexIndex + 2);
            triangles.Add(vertexIndex + 3);
        }

        private void Addtriangle(Vector3 v_0, Vector3 v_1, Vector3 v_2)
        {
            int vertexIndex = vertices.Count;
            vertices.Add(v_0);
            vertices.Add(v_1);
            vertices.Add(v_2);

            triangles.Add(vertexIndex);
            triangles.Add(vertexIndex + 1);
            triangles.Add(vertexIndex + 2);
        }

        private void SetVoxelColors()
        {
            Debug.Log(voxelMaterials.Length);
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

            voxels[i] = new Voxel(x, y, voxelSize);
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