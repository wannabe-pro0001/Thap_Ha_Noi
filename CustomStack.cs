using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThapHN
{
    public class CustomStack
    {
        Node pTop;
        public int count = 0;

        public CustomStack()
        {
            pTop = null;
        }
        public CustomStack(CustomStack mirror)
        {
            Copy(mirror);
        }

        public bool IsEmpty()
        {
            if (pTop == null)
                return true;
            return false;
        }

        public void Push(int x) 
        {
            Node p = new Node(x);
            p.next = pTop;
            pTop = p;
            this.count++;
        }
        public void Push(Node p)
        {
            p.next = pTop;
            pTop = p;
            this.count++;
        }
        public Node Pop() 
        {
            Node temp = pTop;
            pTop = pTop.next;
            this.count--;
            return temp;
        }
        public int Top => this.pTop.data;
        public void Copy(CustomStack cpStack)
        {
            RemoveAll();
            pTop = cpStack.pTop;
            count = cpStack.count;
        }
        public void RemoveAll()
        {
            while (!IsEmpty())
                Pop();
            count = 0;
            pTop = null;
        }
    }
}
