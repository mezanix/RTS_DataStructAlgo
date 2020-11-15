using UnityEngine;

namespace FutureGames.Lab
{
    public class Fibonacci : MonoBehaviour
    {
        [SerializeField]
        int max = 50;

        [SerializeField]
        int first = 1;

        [SerializeField]
        int second = 1;

        private void Start()
        {
            Debug.Log(int.MinValue);
            Iterate(first, second);
        }

        private void Iterate(int first, int second)
        {
            if (second > max)
                return;

            int third = first + second;

            Debug.Log(third);

            Iterate(second, third);
        }
    }
}