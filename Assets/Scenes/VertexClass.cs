using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This will be the class for each vertix, basically a wrapper for the vertex gameObject 
/// it will contain the vertex object, a list of edges, a list of distances from each edge 
/// and the position of the vertex 
/// </summary>
public class VertexClass : ScriptableObject
{
    GameObject vertexGO; //The vertex gameObject
    private float xPos = 0; 
    private float yPos = 0;
    private Dictionary<VertexClass, float> nieghbors; //A HashTable of VertexClass and their distances
    private bool visited; 
    private int index;

    public void Initialize(GameObject vertex, Vector2 Pos, int index)
    {
        this.vertexGO = vertex;
        this.xPos = Pos.x;
        this.yPos = Pos.y;
        this.vertexGO.transform.position = new Vector2(xPos, yPos); 
        this.visited = false;
        this.index = index; 
        //Redunant code, but it makes the more user readable when creating the hash Table
        // instead of nieghbor.vextex.transform.position.x, nieghbor.xPos is a lot more readable
        nieghbors = new Dictionary<VertexClass, float>();
    }

    //This implementation should change if using a dense vertex map, however this vertex map will be fairly sparse
    public bool addNieghbor(VertexClass nieghbor) {

        float xPosSquared = (nieghbor.xPos - this.xPos) * (nieghbor.xPos - this.xPos); 
        float yPosSquared = (nieghbor.yPos - this.yPos) * (nieghbor.yPos - this.yPos);

        //Finding the distance between the two vertices
        float distance = Mathf.Sqrt(xPosSquared + yPosSquared);

        //Adding each other into the vertex map
        nieghbors.Add(nieghbor, distance);
        nieghbor.nieghbors.Add(this, distance);


        return true;
    }

    /// <summary>
    /// Sees if the vertex is a Nieghbor or not
    /// </summary>
    /// <param name="neighbor">A vertex class</param>
    /// <returns>A boolean whether or not it is a nieghbor</returns>
    public bool isNieghbor(VertexClass neighbor) { 
        return nieghbors.ContainsKey(neighbor);
    
    }

    /// <summary>
    /// A function that will return a nighbor in the Hashtable
    /// </summary>
    /// <param name="distance">a distance float</param>
    /// <returns>the Vertex that is assoicated with its neighbor</returns>
    public VertexClass getNieghbor(float distance)
    {

        foreach (KeyValuePair<VertexClass, float> entry in nieghbors)
        {
            if(Mathf.Approximately(entry.Value, distance))
            {
                return entry.Key;
            }
        }

            return null; 

    }

   /// <summary>
   /// An array of floats that contains the distances for all of the vertices
   /// </summary>
   /// <returns>An array of floats</returns>
    public float[] nieghborDistances() { 
        float [] nieghborDistances = new float[nieghbors.Count];
        int index = 0; 

        foreach(float distance in nieghbors.Values) {
            nieghborDistances[index] = distance; 
            index++; 
        }
        
        return nieghborDistances;
    
    }

    /// <summary>
    /// Will create a line between the two vertices 
    /// Used from https://answers.unity.com/questions/458207/copy-a-component-at-runtime.html
    /// </summary>
    /// <param name="vertex">The other vertex that will create the connection</param>
    /// <param name="lr">A lineRenderer which will be the basis for each ver</param>
    public void ConnectVertex(VertexClass vertex, LineRenderer lr, int index) {  
        
        //Gets the position Vectors 
        Vector3 thisPostion = new Vector3(getXPos(), getYPos(), 0);
        Vector3 vertexPostion = new Vector3(vertex.getXPos(), vertex.getYPos(), 0);
        

        //Adds a Component to the vertexGO and manipultates the line renderer

        LineRenderer line = vertexGO.GetComponent<LineRenderer>();
        if (line == null)
        {
            line = vertexGO.AddComponent<LineRenderer>();
            line.positionCount = 0;
            line.material = lr.material;
            line.startColor = lr.startColor; 
            line.endColor = lr.endColor;  
            line.sortingLayerID = SortingLayer.layers[0].id; 
        }


        //line.positionCount starts with an extra 2 points, I would not like that to happen, the last statement is the temp fix
        line.positionCount += 2;
        //Adds Positions to line count
        line.SetPosition(index++, thisPostion);
        line.SetPosition(index++, vertexPostion);

    }

    /// <summary>
    /// Returns the size of the nieghbors that the vertex has
    /// </summary>
    /// <returns>the size of the nieghbors list</returns>
    public int nieghborsSize()
    {
        return nieghbors.Count;
    }
    /// <summary>
    /// Get xPos
    /// </summary>
    /// <returns>returns xPos</returns>
    public float getXPos()
    {
        return xPos;
    }

    /// <summary>
    /// Get yPos
    /// </summary>
    /// <returns>returns yPos</returns>
    public float getYPos()
    {
        return yPos;
    }

    /// <summary>
    /// Gets the distance from the nieghbor of he vertex, if the vertex is not a nieghbor, returns -1
    /// </summary>
    /// <param name="nieghbor">A vertex that may be the neighbor of this.vertex</param>
    /// <returns>A distance value or a -1 if the value does not exist</returns>
    public float getDistance(VertexClass nieghbor)
    {
       return nieghbors.TryGetValue(nieghbor, out float distance) ? distance : -1f;
    }

    /// <summary>
    /// A setter function for visited
    /// </summary>
    /// <param name="bol">the new value you want to set visited</param>
    public void setVisited(bool bol) {
        visited = bol;     
    }

    /// <summary>
    /// A getter function for visited
    /// </summary>
    /// <returns>the value of visited</returns>
    public bool getVisited() {
        return visited; 
    
    }

    /// <summary>
    /// A getter function for index in the AdjMatrix
    /// </summary>
    /// <returns>the value of visited</returns>
    public int getIndex(){
        return index; 
    }

    
    /// <summary>
    /// A function that will print the vertix and its nieghbors. Used for debugging purposes. 
    /// </summary>
    public void printVertex() {
        Debug.Log("Vertex Name: " + this.GetHashCode());

        Debug.Log("Nieghbors: \n");

        foreach(KeyValuePair<VertexClass, float> entry in nieghbors)
        {
            Debug.Log(entry.Key.GetHashCode() + ":" + entry.Value);
        }

    } 



}
