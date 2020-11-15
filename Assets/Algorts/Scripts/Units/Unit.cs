using System;
using UnityEngine;

namespace FutureGames.Algorts
{
    public class Unit : MonoBehaviour
    {
        PerlinToTerrain perlinToTerrain = null;

        [SerializeField]
        float heightShift = 2f;

        [SerializeField]
        GameObject lookAt = null;

        [SerializeField]
        GameObject body = null;

        private void Start()
        {
            perlinToTerrain = FindObjectOfType<PerlinToTerrain>();
        }

        private void Update()
        {
            CorrectHeight();

            CorrectUpDirection();
        }

        private void CorrectUpDirection()
        {
            Vector3 terrainNormal = perlinToTerrain.NormalOnTerrain(transform.position.x, transform.position.z);

            //transform.LookAt(lookAt.transform, terrainNormal);

            //transform.up = terrainNormal;

            transform.Rotate(transform.up, 120f * Time.deltaTime, Space.World);
        }

        private void CorrectHeight()
        {
            transform.position = new Vector3(
                transform.position.x,
                perlinToTerrain.PositionOnTerrain(transform.position.x, transform.position.z).y + heightShift,
                transform.position.z);

            Linked2DDoubleList<GameObject> ll = new Linked2DDoubleList<GameObject>();

            var neibLL = ll.GetNeighbors();

            Linked2DDoubleList<Cell> cellLL = new Linked2DDoubleList<Cell>();

            var neibCell = cellLL.GetNeighbors();
        }


    }

    public class Cell
    {

    }

    public class Linked2DDoubleList<T>
    {
        public T[] GetNeighbors()
        {
            T[] r = new T[0];



            return r;
        }
    }
}