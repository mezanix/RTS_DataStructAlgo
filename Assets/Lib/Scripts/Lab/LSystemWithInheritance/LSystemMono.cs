using UnityEngine;

namespace FutureGames.Lab
{
    public class LSystemMono : MonoBehaviour
    {
        [SerializeField]
        string input = "";

        [SerializeField]
        bool isTree = true;

        LSystem system = null;

        [SerializeField]
        GameObject toClone = null;

        private void Awake()
        {
            system = new LSystem(input, isTree, toClone);
        }

        private void Start()
        {
            system.Run();
        }
    }
}