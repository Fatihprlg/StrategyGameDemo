/// <summary>
/// Classic binary heap with some tricks.
/// Like heap index is in the nodes himselfs. fastest for Contains checks and clearing the heap.
/// </summary>
public class BinaryHeap
{
    private HeapNode[] heap;

    private struct HeapNode
    {
        public readonly CellModel node;
        public readonly int F;

        public HeapNode(int f, CellModel node)
        {
            this.F = f;
            this.node = node;
        }
    }
    
    public BinaryHeap(int capacity)
    {
        heap = new HeapNode[capacity];
        Count = 0;
    }

    public bool Contains(CellModel node)
    {
        return node.PathOptions.heapIndex != Constants.Numerical.NOT_IN_HEAP;
    }
    
    public void Add(CellModel node)
    {
        if (node.PathOptions.heapIndex != Constants.Numerical.NOT_IN_HEAP)
        {
            return;
        }
        if (Count >= heap.Length)
            Expand();

        HeapNode item = new HeapNode(node.PathOptions.f, node);
        int position = Count;
        Count++;
        heap[position] = item;
        heap[position].node.PathOptions.heapIndex = position;
        MoveUp(position);
    }
    
    /// <summary>if heap was small for pathfinding then expand</summary>
    private void Expand () {
        int newSize = heap.Length + 16;
        var newHeap = new HeapNode[newSize];
        heap.CopyTo(newHeap, 0);
        heap = newHeap;
    }

    public CellModel ExtractMin()
    {
        CellModel minNode = heap[0].node;
        Swap(0, Count - 1);
        Count--;
        MoveDown(0);
        minNode.PathOptions.heapIndex = Constants.Numerical.NOT_IN_HEAP;
        return minNode;
    }

    /// <summary>
    /// Classic binary heap moving least element to the top.
    /// </summary>
    private void MoveUp(int position)
    {
        while ((position > 0) && (heap[Parent(position)].F > heap[position].F))
        {
            int original_parent_pos = Parent(position);
            Swap(position, original_parent_pos);
            position = original_parent_pos;
        }
    }

    /// <summary>
    /// Classic downing an element with big value
    /// </summary>
    private void MoveDown(int position)
    {
        int lChild = LeftChild(position);
        int rChild = RightChild(position);
        int largest = 0;
        if ((lChild < Count) && (heap[lChild].F < heap[position].F))
        {
            largest = lChild;
        }
        else
        {
            largest = position;
        }

        if ((rChild < Count) && (heap[rChild].F < heap[largest].F))
        {
            largest = rChild;
        }

        if (largest != position)
        {
            Swap(position, largest);
            MoveDown(largest);
        }
    }
    
    public int Count { get; private set; }

    /// <summary>
    /// Changes also heap indexes from nodes. 
    /// </summary>
    private void Swap(int position1, int position2)
    {
        (heap[position1].node.PathOptions.heapIndex, heap[position2].node.PathOptions.heapIndex) =
            (heap[position2].node.PathOptions.heapIndex, heap[position1].node.PathOptions.heapIndex);
        
        (heap[position1], heap[position2]) = (heap[position2], heap[position1]);
    }

    private static int Parent(int position)
    {
        return (position - 1) / 2;
    }

    private static int LeftChild(int position)
    {
        return 2 * position + 1;
    }

    private static int RightChild(int position)
    {
        return 2 * position + 2;
    }
    
    public void Clear ()
    {
        for (int i = 0; i < Count; i++) {
            heap[i].node.PathOptions.heapIndex = Constants.Numerical.NOT_IN_HEAP;
        }
        Count = 0;
    }
}