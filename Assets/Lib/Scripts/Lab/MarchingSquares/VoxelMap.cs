using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FutureGames.Lab
{
    public class VoxelMap : MonoBehaviour
    {
        public float size = 2f;

        public int voxelResolution = 8;
        public int chunkResolution = 2;

        public VoxelGrid voxelGridPrefab = null;

        VoxelGrid[] chunks = new VoxelGrid[0];
        float chunkSize = 1f;
        float voxelSize = 1f;
        float halfSize = 1f;

        private void Awake()
        {
            BoxCollider collider = gameObject.AddComponent<BoxCollider>();
            collider.size = new Vector3(size, size, 1f);

            halfSize = size * 0.5f;
            chunkSize = size / chunkResolution;
            voxelSize = chunkSize / chunkResolution;

            chunks = new VoxelGrid[chunkResolution * chunkResolution];

            for (int x = 0, i = 0; x < chunkResolution; x++)
            {
                for (int y = 0; y < chunkResolution; y++, i++)
                {
                    CreateChunk(i, x, y);
                }
            }
        }

        private void Update()
        {
            InteractInput();
        }

        private void InteractInput()
        {
            if (Input.GetMouseButtonDown(0) == false)
                return;

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit) == false)
                return;

            EditVoxels(hit.point);
        }

        private void EditVoxels(Vector3 point)
        {
            int voxelX = (int)((point.x + halfSize) / voxelSize);
            int voxelY = (int)((point.y + halfSize) / voxelSize);

            int chunkX = voxelX / voxelResolution;
            int chunkY = voxelY / voxelResolution;

            Debug.Log(voxelX + " " + voxelY + " in " + chunkX + " " + chunkY);
        }

        private void CreateChunk(int i, int x, int y)
        {
            VoxelGrid chunk = Instantiate(voxelGridPrefab);
            chunk.Init(voxelResolution, chunkSize);
            chunk.transform.parent = transform;
            chunk.transform.localPosition = new Vector3(
                x * chunkSize - halfSize,
                y * chunkSize - halfSize,
                0f);
            chunks[i] = chunk;
        }
    }
}