using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderEnemy : MonoBehaviour
{

    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private GameObject enemyNet;


    private PlayerController pController;


    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject bulletPrefab;


    float bulletCooldown = 3.0f;
    float movementSpeed = 1.0f;

    float movementRange = 1.0f; //Radius of the vertical movement given from the startting position
    float bulletClock = 0;

    Vector3 initPosition;

    private GameDirector gameDirector;




    // Start is called before the first frame update
    void Start()
    {
        pController = FindFirstObjectByType<PlayerController>();

        enemyNet.transform.localScale = new Vector3(3, movementRange * 8, 1);
        gameDirector = FindFirstObjectByType<GameDirector>();
        

        initPosition = transform.position;
        
    }

    private void Update()
    {
        handleMovement();
        handleShooting();

        bulletClock += Time.deltaTime;
        
        
    }

    public void setBulletCooldown(float coolddown)
    {
        this.bulletCooldown = coolddown;
        
    }

    public void setMovementSpeed(float movement)
    {
        this.movementSpeed = movement;
    }

    public void setMovementRadius(float radius)
    {
        this.movementRange = radius; 
    }

    private void handleMovement()
    {
        Vector3 pos = this.transform.position;


        pos.y += Time.deltaTime * movementSpeed;

        if (pos.y > initPosition.y + movementRange)
        {
            this.movementSpeed = -movementSpeed;
        }
        if (pos.y < initPosition.y - movementRange)
        {
            this.movementSpeed = -movementSpeed;
        }

        this.transform.position = pos;

    }



    private void handleShooting()
    {
        //Animator transition


        if(bulletClock > bulletCooldown)
        {

            


            Vector3 pos = this.transform.position;
            pos.z = 0;
            pos.y += 1.5f;
            pos.x -= 0.5f;
            

            bulletClock = 0;
            GameObject g = Instantiate(bulletPrefab, pos, Quaternion.identity);
           



        }

        



        


    }



    //Player killed the enemy
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Killed");
        gameDirector.increasePlayerScore(100);
        pController.bumpUp();
        Destroy(this.gameObject);
        

    
    }

    //Player gets killed by the enemy
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Execute player
        Debug.Log("Triggered Collision with the enemy");
        //Log death to gamedirector
        gameDirector.murderPlayer();
        //let it handle the event

    }

}
