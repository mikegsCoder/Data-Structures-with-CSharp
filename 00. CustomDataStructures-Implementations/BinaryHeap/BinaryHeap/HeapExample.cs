﻿using System;

public class HeapExample
{
    static void Main()
    {
        Console.WriteLine("Created an empty heap.");
        var heap = new BinaryHeap<int>();
        heap.Insert(5);
        heap.Insert(8);
        heap.Insert(1);
        heap.Insert(3);
        heap.Insert(12);
        heap.Insert(-4);

        Console.WriteLine("Heap elements (max to min):");
        while (heap.Count > 0)
        {
            var max = heap.Pull();
            Console.WriteLine(max);
        }

        Console.WriteLine(new string('-',30));
        int[] arr = new int[] { 5, 8, 12, 4, 9, 35, 73 };
        Heap<int>.Sort(arr);
        Console.WriteLine(string.Join(" ", arr));
    }
}
