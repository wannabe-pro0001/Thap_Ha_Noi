using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThapHN
{
    public class Node
    {
        public int data;
        public Node next { get; set; }

        public Node(int x)
        {
            this.data = x;
            this.next = null;
        }
    }
}
