using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    PlayerMovement playerMove;
    EnemyMovement enemyMove;
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        playerMove = new PlayerMovement(player);
        enemyMove = new EnemyMovement(enemy, playerMove.getCurrent());
        
    }

    // Update is called once per frame
    void Update()
    {
        playerMove.MoveToVertex();
        //StartCoroutine(MoveEnemy());  
        if(player.transform.position == enemy.transform.position){
            Debug.Log("Game Over"); 
        }

    }

// I do not want to do a coruotine but I want to use the job system


    // //Coruotine used from Unnamed mobile game project
    // public IEnumerator MoveEnemy(){
    //     List <VertexClass> path = enemyMove.dumbFindPath();
    //     enemyMove.MoveToVertex(path); 
    //     yield return new WaitForSeconds(Random.Range(2, 3)); 
    //     //StartCoroutine(MoveEnemy());
    // }

    // public IEnumerator MovePlayer(){
    //     playerMove.MoveToVertex(); 
    //     yield return null; 
    //     //StartCoroutine(MoveEnemy());
    // }

}
