namespace FutureGames.Lab
{
    public class IntLinkedList
    {
        IntLinkedListNode first = null;

        public IntLinkedListNode current = null;

        public IntLinkedList(IntLinkedListNode first)
        {
            this.first = first;
            current = this.first;
        }

        public void AddLast(int data)
        {
            while(OnLast() == false)
            {
                Travel();
            }
            current.next =  new IntLinkedListNode(data, null, current);
            Travel();
        }

        public void TravelBack()
        {
            if (current.previous == null)
                return;

            current = current.previous;
        }

        public void Travel()
        {
            if (current.next == null)
                return;

            current = current.next;
        }

        public bool OnFirst()
        {
            return current.previous == null;
        }

        public bool OnLast()
        {
            return current.next == null;
        }

        public void ResetCurrentToFirst()
        {
            current = first;
        }

        //public void SetCurrentToLast()
        //{

        //}

        public override string ToString()
        {
            ResetCurrentToFirst();

            string r = "Current:" + current.data;

            while (OnLast() == false)
            {
                r += " " + current.data.ToString(); 
                Travel();
            }

            r += " " + current.data.ToString();

            r += " Current:" + current.data;
            return r;
        }
    }

    public class IntLinkedListNode
    {
        public int data = 0;
        public IntLinkedListNode next = null;
        public IntLinkedListNode previous = null;

        public IntLinkedListNode(int data, IntLinkedListNode next, IntLinkedListNode previous)
        {
            this.data = data;
            this.next = next;
            this.previous = previous;
        }
    }
}