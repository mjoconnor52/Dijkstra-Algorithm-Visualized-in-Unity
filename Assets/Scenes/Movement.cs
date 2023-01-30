using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement
{

    protected VertexClass current;
    protected VertexClass next;
    protected Camera cam;
    protected Vector3 newLocation;
    float threshold;
    protected List<VertexClass> adjMatrix; 

    /// <summary>
    /// Constructor for Movement class, Base class for both PlayerMovement and Enemymovement
    /// </summary>
    public Movement()
    {
        //Initalizing Variables 
        newLocation = Vector3.zero;
        threshold = 1f;

        // Gets the camera to base movement off of
        cam = Camera.main;
        // Takes the adjMatrix from the SpawnVertices and starts the player at a random vertex

        adjMatrix = SpawnVertices.getAdjMatrix();
        int index = Random.Range(0, adjMatrix.Count - 1);
        current = adjMatrix[index];
    }

    /// <summary>
    /// Detemines if the position is a valid Position with a vertex or not
    /// </summary>
    /// <param name="possiblePos"> A postion vector</param>
    /// <returns>A vertex that exists or null if their is no position</returns>
    public VertexClass nextVertex(Vector3 possiblePos)
    {
        foreach (VertexClass vertex in adjMatrix)
        {
            //Does a vertex basically exist at that position 
            if (approxWithThres(vertex.getXPos(), possiblePos.x, threshold) && approxWithThres(vertex.getYPos(), possiblePos.y, threshold))
            {
                //Is that vertex a neighbor
                if (current.isNieghbor(vertex))
                {
                    return vertex;
                }
            }
        }
        return null;
    }

    /// <summary>
    /// Gives an approaximate error based off the 1s digit
    /// Based off https://answers.unity.com/questions/756538/mathfapproximately-with-a-threshold.html 
    /// </summary>
    /// <param name="val1">a float value</param>
    /// <param name="val2"> a float value</param>
    /// <param name="threshold"> the amount of error allowed</param>
    /// <returns>A bool if they are close or not</returns>
    bool approxWithThres(float val1, float val2, float threshold)
    {

        return ((val1 - val2) < 0 ? (val2 - val1) : (val1 - val2)) <= threshold;
    }

    /// <summary>
    /// A getter for current vertex
    /// </summary>
    /// <returns>Returns the current vertex</returns>
    public VertexClass getCurrent() { return current; } 
}
