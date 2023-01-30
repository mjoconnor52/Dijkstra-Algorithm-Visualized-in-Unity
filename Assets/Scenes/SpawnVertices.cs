using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnVertices : MonoBehaviour
{
    // what the vertex and edge would end up looking like
    [SerializeField]
    public GameObject vertex;

    int numVerts = 10; //Number of Vertices that the adjMatrix will have
    public static List<VertexClass> adjMatrix; //  May want to change to a list or ArrayList

    // Start is called before the first frame update and the start functions
    // This will basically make the map of the game, I'll want to optomize this so that it starts in Gameplay
    void Awake()
    {
        Camera camera = Camera.main;

        // an offset that will create a border so the verteces can spawn in the correct spot (in pixels)
        int offset = 20;

        // creates the length from the 0 point (bottom right to the halfway point) and gets
        // the needed x and y values to make the AdjMatrix
        float x = camera.ScreenToWorldPoint(new Vector3(camera.scaledPixelWidth - offset, 0, 0)).x;
        float y = camera.ScreenToWorldPoint(new Vector3(0, camera.scaledPixelHeight - offset, 0)).y;

        adjMatrix = initializeAdjMatrix(numVerts, x, y);
        connectVertices();

    }

    /// <summary>
    /// Creates an AdjMatrix for the Vertices in the matrix
    /// </summary>
    /// <param name="numVertices">Number of Vertices in the adjMatrix</param>
    /// <param name="borderSizeX">Size of the Border in the X axis</param>
    /// <param name="borderSizeY">Size of the Border in the Y axis</param>
    List<VertexClass> initializeAdjMatrix(int numVertices, float borderSizeX, float borderSizeY)
    {

        List<VertexClass> vertexList = new List<VertexClass>(numVertices);

        //A for loop that will create a new vertix with a random position inside the border and place it inside of a list of vertices
        for (int i = 0; i < numVertices; i++)
        {
            Vector2 newPos = new Vector2(Random.Range(-borderSizeX, borderSizeX), Random.Range(-borderSizeY, borderSizeY));
            VertexClass newVertex = ScriptableObject.CreateInstance<VertexClass>();
            newVertex.Initialize(Instantiate(vertex), newPos, i);
            vertexList.Add(newVertex);
        }

        int minEdges = 1, maxEdges = numVertices - 1;

        //This will then populate each vertex with a random number of edges to then create the AdjMatrix
        // I want to change this so that it clusters sorta nicely and not very ugly like what we see in the modeling
        for (int i = 0; i < numVertices; i++)
        {

            VertexClass current = vertexList[i];
            int numEdges = Random.Range(minEdges, maxEdges) - current.nieghborsSize();

            //Adds the nieghbors to the vertices
            for (int j = 0; j < numEdges; j++)
            {
                //gets a random nieghbor index from Vertex list
                int nieghborIndex = Random.Range(0, numVertices);

                // Removes the possiblity of getting the nieghbor of yourself or someone who was already a nieghbor
                // Untended side effect of creating more edges than desired
                // NEEDS FIX
                while (nieghborIndex == i || current.isNieghbor(vertexList[nieghborIndex]))
                {
                    int full_loop = i; 
                    nieghborIndex = ++nieghborIndex % vertexList.Capacity;

                    if(nieghborIndex == full_loop) { nieghborIndex = -1; break;}

                }

                if(nieghborIndex == -1) { continue; }

                VertexClass newNieghbor = vertexList[nieghborIndex];
                current.addNieghbor(newNieghbor);
            }
        }

        return vertexList;

    }

    public void connectVertices()
    {

        LineRenderer edge = vertex.GetComponent<LineRenderer>();
        List<VertexClass> totalVertexConnections = new List<VertexClass>();
        // edge.startColor = Color.red;
        // edge.endColor = Color.magenta;
        // edge.positionCount = 0;

        Camera camera = Camera.main;

        //Goes through all the vertices and finds the nieghbors in the two AdjMatrix
        foreach (VertexClass vertex in adjMatrix)
        {
            int index = 0; 
            foreach (VertexClass vertex2 in adjMatrix)
            {
                if (vertex.isNieghbor(vertex2))
                {
                    //Connects the two individual points Needs to be cleaned up a little
                    vertex.ConnectVertex(vertex2, edge, index);
                    index += 2;
                }
            }

            //Remove Excess points
        }
    }

    public static List<VertexClass> getAdjMatrix() { 
        return adjMatrix;
    }
}
