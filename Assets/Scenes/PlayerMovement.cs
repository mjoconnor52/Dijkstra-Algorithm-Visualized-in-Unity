using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement
{
    int travelSpeed;
    public GameObject player;


    /// <summary>
    /// Constructor for PlayerMovement, makes the movement of the player
    /// </summary>
    public PlayerMovement(GameObject player) 
    {
        this.player = player;
        travelSpeed = 5;
        player.transform.position = new Vector3(current.getXPos(), current.getYPos(), 0);
    }


    // Update is called once per frame
    public void MoveToVertex()
    {
        if (Input.GetMouseButtonDown(0) && next == null)
        {
            //Getting the mouse data
            Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            next = nextVertex(mousePos);
            if(next != null)
            newLocation = new Vector3(next.getXPos(), next.getYPos(), 0);
        }

        if (next != null && player.transform.position.Equals(newLocation))
        {
            //next.printVertex(); 
            current = next;
            next = null;
        }

        if (next != null) {
            player.transform.position = Vector3.MoveTowards(player.transform.position, newLocation,
                                            (/* travelSpeed / */ current.getDistance(next)) * Time.deltaTime); 
            //Add when you want to slow movement
        }

    }


 
}
