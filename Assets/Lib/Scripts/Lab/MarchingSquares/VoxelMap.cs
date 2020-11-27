﻿using System;
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

        string[] filledTypeNames = new string[] { "Filled", "Empty" };
        int filledTypeIndex = 0;

        string[] radiusNames = new string[] { "0", "1", "2", "3", "4", "5", "6" };
        int radiusIndex = 0;

        private void Awake()
        {
            BoxCollider collider = gameObject.AddComponent<BoxCollider>();
            collider.size = new Vector3(size, size, 1f);

            halfSize = size * 0.5f;
            chunkSize = size / chunkResolution;
            voxelSize = chunkSize / voxelResolution;

            chunks = new VoxelGrid[chunkResolution * chunkResolution];

            for (int y = 0, i = 0; y < chunkResolution; y++)
            {
                for (int x = 0; x < chunkResolution; x++, i++)
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
            int centerX = (int)((point.x + halfSize) / voxelSize);
            int centerY = (int)((point.y + halfSize) / voxelSize);

            int chunkX = centerX / voxelResolution;
            int chunkY = centerY / voxelResolution;

            centerX -= chunkX * voxelResolution;
            centerY -= chunkY * voxelResolution;

            int xStart = (centerX - radiusIndex) / voxelResolution;
            xStart = xStart < 0 ? 0 : xStart;

            int xEnd = (centerX + radiusIndex) / voxelResolution;
            xEnd = xEnd > chunkResolution - 1 ? chunkResolution - 1 : xEnd;

            int yStart = (centerY - radiusIndex) / voxelResolution;
            yStart = yStart < 0 ? 0 : yStart;

            int yEnd = (centerY + radiusIndex) / voxelResolution;
            yEnd = yEnd > chunkResolution - 1 ? chunkResolution - 1 : yEnd;

            VoxelStencile activeStencile = new VoxelStencile();
            activeStencile.Init(filledTypeIndex == 0, radiusIndex, voxelResolution);

            int voxelYOffset = yStart * voxelResolution;
            for (int y = yStart; y < yEnd; y++)
            {
                int i = y * chunkResolution + xStart;
                int voxelXOffset = xStart * voxelResolution;
                for (int x = xStart; x < xEnd; x++, i++)
                {

                    activeStencile.SetCenter(centerX-voxelXOffset, centerY-voxelYOffset);

                    chunks[i].Apply(activeStencile);

                    voxelXOffset += voxelResolution;
                }
                voxelYOffset += voxelResolution;
            }


            //Debug.Log(voxelX + " " + voxelY + " in " + chunkX + " " + chunkY);
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

        private void OnGUI()
        {
            //GUI.Label()
            GUILayout.BeginArea(new Rect(4f, 4f, 150f, 500f));

            GUILayout.Label("Fill type");
            filledTypeIndex = GUILayout.SelectionGrid(
                filledTypeIndex, filledTypeNames, filledTypeNames.Length);

            GUILayout.Label("Radius");
            radiusIndex = GUILayout.SelectionGrid(
                radiusIndex, radiusNames, radiusNames.Length);

            GUILayout.EndArea();
        }
    }
}