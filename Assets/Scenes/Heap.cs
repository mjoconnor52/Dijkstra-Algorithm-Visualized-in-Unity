using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// A min heap using a binary Tree (perhaps update to an d-ary tree). THIS ONLY COMPARES NUMBERS (DOUBLE), Prob can be refactored
/// </summary>
public class Heap{ 
    

        //tree uses 1 based index-ing
        Double[] tree;
        int size = 1; // The total size of the array 
        Int64 capacity = 0; //the number of elements in the array + 1. Used throughout the code as an index for the elements in the array 
        int top = 1; // the "top" of the heap

        /// <summary>
        /// Constructor for the Binary Tree Heap
        /// </summary>
        public Heap() { 
            tree = new Double[1];
            tree[0] = Double.NegativeInfinity;
            capacity++; 
        }

        /// <summary>
        /// Adding a new Child to the tree
        /// </summary>
        /// <param name="item">An Object T</param>
        /// <returns>A boolean asserting the item was added or not</returns>
        public bool AddChild(Double item) {

            bool bubble = true; 

            //If tree is at capacity, we will double the size of the tree
            if (atCapacity())
            {
                size *= 2; 
                Double[] newTree = new Double[size];
                tree.CopyTo(newTree, 0);
                tree = newTree;
                for (Int64 i = capacity; i < size; i++)
                {
                    tree[i] = Double.PositiveInfinity;
                }
            }
            
            //Double min = Peek();

            //Casting the T values as doubles to use them, but will be saved as whatever T they were originally

            Int64 index = capacity;
            
            tree[index] = item;

            //This loop will bubble the newly added element from the bottom of the tree to the top depending on the size
            while (bubble)
            {
                int indexAbove = (int) MathF.Floor(index / 2);
                if (indexAbove > 0)
                {
                    double valmin = tree[indexAbove];

                    if(item < valmin)
                    {
                        swap(index, indexAbove);
                        index = indexAbove; 
                    }
                    else
                    {
                        bubble = false;
                    }
                }
                else {
                    tree[top] = item; 
                    bubble = false; 
                }
            }

            capacity++; 
            return true;         
        }

        /// <summary>
        /// Will decrease the value of a number at an index.  val < tree[index]. If this precondition is not satistifed, it will be rejected
        /// </summary>
        /// <param name="index">The index of the value that will be replaced. Use list indexing</param>
        /// <param name="val">The updated value</param>
        public void decreaseValue(Int64 index, Double item) {

            bool bubble = true;

            //does not decrease Value if the number is amller 
            if (item > tree[index])
            {
                return; 
            }

            tree[index] = item;

            //This loop will bubble the newly added element from the bottom of the tree to the top depending on the size
            while (bubble)
            {
                int indexAbove = (int)MathF.Floor(index / 2);
                if (indexAbove > 0)
                {
                    double valmin = tree[indexAbove];

                    if (item < valmin)
                    {
                        swap(index, indexAbove);
                        index = indexAbove;
                    }
                    else
                    {
                        bubble = false;
                    }
                }
                else
                {
                    tree[top] = item;
                    bubble = false;
                }
            }
        }


        /// <summary>
        /// Removes the minimium value at the top of the heap and updates the heap
        /// </summary>
        public void removeMin()
        {
            //reducing the capacity of the tree since we are removing one element
            capacity--;

            if (isEmpty())
            {
            return; 
            }    

            tree[top] = tree[capacity];
            tree[capacity] = Double.PositiveInfinity;

            //intial values for searching
            double min = Double.PositiveInfinity;
            double val = Double.PositiveInfinity;

            Int64 minIndex = -1; 

            for(Int64 i = 1; i < capacity; i++){
                val = tree[i]; 
                if(val == Double.PositiveInfinity){ break; }
                if(min > val){
                    min = val; 
                    minIndex = i; 
                }
            }

            swap(top, minIndex); 

            //Dumb code, better for sorted trees
            // //This will start at the top of the tree
            // int indexAbove = top; 
            // int index = top + 1; 
            // bool bubble = true;

            // while (bubble)
            // {
            //     Double branch1Diff = tree[index] - tree[indexAbove];
            //     Double branch2Diff = tree[index + 1] - tree[indexAbove];

            //     //Testing based off the Differences between the two branches, if they are all the same, we will just leave it alone
            //     if (branch1Diff <= branch2Diff && !Double.IsInfinity(branch2Diff))
            //     {
            //         swap(index, indexAbove);
            //         indexAbove = index;
            //         index *= 2; 
            //     }
            //     else if (branch1Diff > branch2Diff && !Double.IsInfinity(branch1Diff))
            //     {
            //         swap(index + 1, indexAbove);
            //         indexAbove = index + 1;
            //         index = (index + 1) * 2;
            //     }
            //     else
            //     {
            //         bubble = false;
            //     }
            // }
        }

        /// <summary>
        /// A function that will swap two indeces in the tree
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        public void swap(Int64 index1, Int64 index2)
        {
            Double temp = tree[index1];
            tree[index1] = tree[index2];
            tree[index2] = temp;
        }

        /// <summary>
        /// Returns the Top element of the tree without removing it
        /// </summary>
        /// <returns>Returns T</returns>
        public Double Peek()
        {
            return tree[top];
        }

        /// <summary>
        /// Determines if the tree heap is atCapacity or not
        /// </summary>
        /// <returns>A bool if it is at Capacity or not</returns>
        public bool atCapacity() {
            return (capacity == size); 
        }

        public bool isEmpty() { 
            return (capacity == 1);
        }

        /// <summary>
        /// A Debugging function that will find the top of the heap
        /// </summary>
        public void printHeap()
        {
            int max = 1;
            int count = 0;

            for (int i = 1; i < size; i++)
            {
                
                Console.Write(tree[i] + " ");
                count++;
                if(count == max)
                {
                    Console.Write("\n");
                    max *= 2;
                    count = 0;
                }
            }

            Console.Write("\n");

        }

        //Used to help parse types: https://stackoverflow.com/questions/11474948/constraint-cannot-be-special-class-system-object#11475004
    }

    
    /* 
    internal class Program
    {
        static void Main(string[] args)
        {
            Heap heap = new Heap();

            heap.AddChild(70); 
            heap.AddChild(.2);
            heap.AddChild(100);
            heap.AddChild(-15);
            heap.AddChild(400);
            heap.AddChild(7);
            heap.AddChild(9);
            heap.AddChild(10);
            heap.printHeap();
            heap.removeMin();
            heap.removeMin();
            heap.printHeap();
            heap.decreaseValue(6, -14);
            heap.printHeap();
        }
    }
    */ 

