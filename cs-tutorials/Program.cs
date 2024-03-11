// See https://aka.ms/new-console-template for more information


public class ListNode
{
    public int val;
    public ListNode? next;
    public ListNode(int x)
    {
        val = x;
        next = null;
    }
}

public class Solution
{
    public static ListNode? GetTail(ListNode? head)
    {
        // can return null if its a cycle
        // must have one node with next -> null
        // prevent cycle looping
        if (head == null)
        {
            Console.WriteLine("GetTail head == null");
            // null head, null tail
            return null;
        }
        if (head.next == null)
        {
            Console.WriteLine("GetTail head.next == null");
            // only one linked item head and tail
            // return head;
            return null;
        }

        ListNode? pos = head.next;

        // this is forever loop upon cycle
        while (pos != null && pos.next != null)
        {
            Console.WriteLine($"on {pos.val} pos.next {pos.next.val}");
            // should not be able to cycle 
            // to pos now or it is cycle
            // shouldNotFindMe current item
            ListNode shouldNotFindMe = pos;
            // curInLoop start at current pos
            ListNode curInLoop = pos;

            // see if current pos has next (its not tail)
            while (curInLoop.next != null)
            {
                curInLoop = curInLoop.next;
                Console.WriteLine($"curInLoop.next {curInLoop.val}");
                if (curInLoop == shouldNotFindMe)
                {
                    Console.WriteLine($"cur {curInLoop.val}* shouldNotFindMe {shouldNotFindMe.val}*****found cycle******");
                    return null;
                }

            }
            pos = pos.next;
        }
        // found tail, no cycle
        Console.WriteLine($"returning {pos}");
        return pos;
    }
    public bool HasCycle(ListNode head)
    {
        if (head == null)
        {
            return false;
        }

        // if its 1 item in list
        // the tail will be null here
        if (head != null && head.next == null)
        {
            // no cycle
            return false;
        }

        ListNode? tail = GetTail(head);
        Console.WriteLine($"HasCycle got tail {tail}");
        if (tail == null)
        {
            // could not find tail
            // has cycle
            return true;
        }
        return false;

    }

    public static ListNode? GetPosition(ListNode? head, int position)
    {
        if (head == null || position < 0)
        {
            return null;
        }

        ListNode? itemAtPostion = head; // at zero
        int curPosition = 0;
        while (curPosition != position && itemAtPostion.next != null)
        {
            itemAtPostion = itemAtPostion.next;
        }
        return itemAtPostion;
    }
}

class Program
{
    static ListNode? MakeLinkedList(int[] data)
    {
        ListNode? previous = null;
        ListNode? head = null;
        foreach (var item in data)
        {
            ListNode newNode = new ListNode(item);
            if (previous != null)
            {
                previous.next = newNode;
            }
            else
            {
                // first time thru
                head = newNode;
                previous = newNode;
            }
        }
        return head;
    }

    static void test1()
    {
        Console.WriteLine("starting");
        int[] data = [-21, 10, 17, 8, 4, 26, 5, 35, 33, -7, -16, 27, -12, 6, 29, -12, 5, 9, 20, 14, 14, 2, 13, -24, 21, 23, -21, 5];
        ListNode? head = MakeLinkedList(data);

        bool hasCycle = new Solution().HasCycle(head);
        Console.WriteLine($"PASS {string.Join(", ", data)} has cycle {hasCycle}");

    }
    static void test2()
    {
        int[] data = [3, 2, 0, -4];
        ListNode? head = MakeLinkedList(data);
        if (head != null)
        {
            // create the cycle
            // update the linked list tail to point to index 1 (zero based)
            ListNode? tail = Solution.GetTail(head);
            Console.WriteLine($"got the tail {tail}");
            if (tail != null)
            {
                // update tail next -> position 1 (zero based)
                ListNode? itemAtPostion = Solution.GetPosition(head, 1);
                Console.WriteLine($"GetPosition {itemAtPostion}");
                // now update the tail to point to position 1, creating cycle.
                tail.next = itemAtPostion;
                // we have a cycle
                bool hasCycle2 = new Solution().HasCycle(head);
                if (hasCycle2)
                {
                    Console.WriteLine($"PASS {string.Join(", ", data)} has cycle {hasCycle2}");
                }
                else
                {
                    Console.WriteLine($"FAIL {string.Join(", ", data)} has cycle {hasCycle2}");
                }

            }
            // we have a cycle
        }
    }
    static void test3()
    {
        bool hasCycle = new Solution().HasCycle(null);
        // should not have cycle 
        if (hasCycle)
        {
            Console.WriteLine($"FAIL NULL  has cycle {hasCycle}");
        }
        else
        {
            Console.WriteLine($"PASS NULL  has cycle {hasCycle}");
        }
    }
    static void test4()
    {
        int[] data = [1];
        ListNode? head = MakeLinkedList(data);
        bool hasCycle = new Solution().HasCycle(head);
        // should not have cycle 
        if (hasCycle)
        {
            Console.WriteLine($"test 4 FAIL NULL  has cycle {hasCycle}");
        }
        else
        {
            Console.WriteLine($"test 4 PASS NULL  has cycle {hasCycle}");
        }
    }
    static void Main(string[] args)
    {
        test1();
        test2();
        test3();
        test4();
    }
}





