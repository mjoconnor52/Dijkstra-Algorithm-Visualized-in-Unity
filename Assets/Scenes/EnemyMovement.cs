using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Movement
{

    //Unsure how to remove redudent code from Player Movement, probably want to make a master class: Done
    // Start is called before the first frame update

    public GameObject enemy;

    public EnemyMovement(GameObject enemy, VertexClass playerVertex)
    {
        this.enemy = enemy;

        //Assigns a new current for the enemy, where the player is not at
        while (current == playerVertex)
        {
            int index = Random.Range(0, adjMatrix.Count - 1);
            current = adjMatrix[index];
        }

        enemy.transform.position = new Vector3(current.getXPos(), current.getYPos(), 0);
    }


    // Adapted from Eric Autry and his 301 Algorithms class
    // This takes a more greedy approach to Shortest Path, which can have optimally proven via a greedy proof
    // Function that will find the shortest Path from the current location of enemy to the player 
    public List<VertexClass> dumbFindPath()
    {
        //The path for the vertex
        List<VertexClass> path = new();
        next = current.getNieghbor(current.nieghborDistances()[Random.Range(0, current.nieghborsSize() - 1)]);
        path.Add(next); 
        return path; 
    }

    // Movement the enemy from their current position to each vertex in the path until it reaches the path
    public void MoveToVertex(List<VertexClass> path)
    {
        foreach (VertexClass vertex in path)
        {
            newLocation = new Vector3(vertex.getXPos(), vertex.getYPos(), 0);
            while(!enemy.transform.position.Equals(newLocation))
            {
                enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, newLocation, 
                                            0.0001f * Time.deltaTime);
            }
        }
    }


        public List<VertexClass> pathFind(VertexClass playerVertex){
        List<VertexClass> path = new();
             //int size = listVertices.Length;
        // //An array that keeps tracks of the previous values for the AdjMatrix
        // uint[] preInt = new uint[size];

        // //Our min search heap 
        // Heap heap = new();


        // //basically this and min_dist will always be the same as we will use this to keep track of next vertices
        // double loop_min = Mathf.Infinity;
        // VertexClass next = null;
        
        // //Updating the data structures
        // heap.AddChild(0); //Adds the distance from current to current, which is 0

        // while (!heap.isEmpty())
        // {
        //     double min_dist = heap.Peek();
        //     uint lastInd = (uint) vertex.getIndex();
        //     heap.removeMin();
        //     vertex.setVisited(true);


        //     //getting the list to be returned 
        //     if (vertex.Equals(playerVertex))
        //     {
        //         int prevInd = playerVertex.getIndex();
        //         int currentInd = current.getIndex();

        //         while (prevInd != currentInd)
        //         {
        //             VertexClass prevVert = listVertices[prevInd];
        //             path.Add(prevVert);
        //             prevInd = prevVert.getIndex();
        //         }

        //         path.Add(current);
        //         path.Reverse();

        //         break;
        //     }

        //     //pushing all the nieghbor distances into the heap
        //     foreach (float distance in vertex.nieghborDistances())
        //     {
        //         VertexClass nieghbor = vertex.getNieghbor(distance);
        //         int nieghborPrevInd = nieghbor.getIndex();

        //         if (nieghbor == null || nieghbor.getVisited()){
        //             continue; 
        //         }
                
        //         heap.AddChild(distance + min_dist);
        //         preInt[nieghborPrevInd] = lastInd;

        //         if (loop_min > distance + min_dist)
        //         {
        //             loop_min = distance + min_dist;
        //             next = nieghbor; 
        //         }
        //     }

        //     vertex = next; 
        //     loop_min = Mathf.Infinity;
        // }
        return path; 
    }
}
