using UnityEngine;

namespace FutureGames.Lab
{
    public class BinaryTreeNodeMono : MonoBehaviour
    {
        private void Start()
        {
            BinaryTreeNode<string> tree = Sample();

            Debug.Log("Breadth First:");
            BinaryTreeNode<string>.BreadthFirstTravel(tree, (string s) => Debug.Log(s));

            Debug.Log("Depth First:");
            BinaryTreeNode<string>.DepthFirstTravel(tree, (string s) => Debug.Log(s));
        }

        BinaryTreeNode<string> Sample()
        {
            BinaryTreeNode<string> r =
                new BinaryTreeNode<string>("A",
                    new BinaryTreeNode<string>("B",
                        new BinaryTreeNode<string>("C"), new BinaryTreeNode<string>("D")),
                    new BinaryTreeNode<string>("E",
                        new BinaryTreeNode<string>("F"), new BinaryTreeNode<string>("G",
                            new BinaryTreeNode<string>("H"), null)));

            return r;
        }
    }
}