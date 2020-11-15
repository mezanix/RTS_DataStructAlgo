using System;
using System.Collections.Generic;

namespace FutureGames.Lab
{
    public class BinaryTreeNode<T>
    {
        public T data;
        public BinaryTreeNode<T> left = null;
        public BinaryTreeNode<T> right = null;

        public BinaryTreeNode(T data, BinaryTreeNode<T> left = null, BinaryTreeNode<T> right = null)
        {
            this.data = data;
            this.left = left;
            this.right = right;
        }

        public static void DepthFirstTravel<B>(BinaryTreeNode<B> node, Action<B> action)
        {
            if (node == null)
                return;

            action(node.data);

            DepthFirstTravel(node.left, action);
            DepthFirstTravel(node.right, action);
        }

        public static void BreadthFirstTravel<B>(BinaryTreeNode<B> node, Action<B> action)
        {
            Queue<BinaryTreeNode<B>> queue = new Queue<BinaryTreeNode<B>>();

            queue.Enqueue(node);

            while(queue.Count > 0)
            {
                node = queue.Dequeue();

                action(node.data);

                if(node.left != null)
                {
                    queue.Enqueue(node.left);
                }

                if(node.right != null)
                {
                    queue.Enqueue(node.right);
                }
            }
        }
    }
}