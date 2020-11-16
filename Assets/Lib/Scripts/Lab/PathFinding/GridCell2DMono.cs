﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FutureGames.Lab
{
    public class GridCell2DMono : MonoBehaviour
    {
        MeshRenderer myMeshRenderer = null;
        MeshRenderer MyMeshRenderer
        {
            get
            {
                return GetComponent<MeshRenderer>();
            }
        }

        public GridCell2D cell = null;

        private void Update()
        {
            if(cell != null)
                cell.Run();
        }

        private void OnMouseOver()
        {
            cell.SetMouseState(MouseState.Hover);
            cell.SetNeiborsMouseState(MouseState.NeighborOfHover);
        }

        private void OnMouseExit()
        {
            cell.SetMouseState(MouseState.None);
            cell.SetNeiborsMouseState(MouseState.None);
        }

        public void SetColor(Color color)
        {
            MyMeshRenderer.material.color = color;
        }

        public void SetMaterial(Material material)
        {
            MyMeshRenderer.material = material;
        }
    }
}