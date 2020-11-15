using UnityEngine;

namespace FutureGames.Lab
{
    public class IntLinkedListMono : MonoBehaviour
    {
        IntLinkedList linkedList = null;

        private void Start()
        {
            linkedList = new IntLinkedList(new IntLinkedListNode(1, null, null));
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                linkedList.AddLast(linkedList.current.data + 1);
            }

            if(Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log(linkedList);
            }
        }
    }
}