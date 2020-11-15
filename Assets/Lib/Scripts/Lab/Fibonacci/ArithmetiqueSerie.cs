using UnityEngine;

namespace FutureGames.Lab
{
    public class ArithmetiqueSerie : MonoBehaviour
    {
        [SerializeField]
        int first = 0;

        [SerializeField]
        int rate = 5;

        [SerializeField]
        int max = 100;

        int current = 0;

        private void Start()
        {
            //Iterate(first);

            current = first;
            DoItWithWhile();
        }

        private void Iterate(int first)
        {
            if (first > 100)
                return;

            int second = first + rate;

            Debug.Log(second);

            Iterate(second);
        }

        void DoItWithWhile()
        {
            while(current < max)
            {
                current += rate;
                Debug.Log(current);
            }
        }
    }
}