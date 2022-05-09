using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCollisionHandeler : MonoBehaviour
{
    [SerializeField] HealtControler myHealth;
    [SerializeField] PlayerCollisionHandeler pch;
    [SerializeField] MovementEnemyHandeler meh;
    NavMeshAgent agent;
    GameObject player;
    float dist;

    void Start()
    {
        //Set my local agent and player to the external ones 
        agent = meh.nmagent;
        player = meh.player;
        //Calls the funcition CheckDistance every 1s from the time = 0        
        InvokeRepeating("CheckDistance",0,1);
    }

    // Update is not used :v
    void Update()
    {

    }
    //Checks the distance between the enemy and player and ask if enemy is stopped
    public void CheckDistance()
    {
        dist = Vector3.Distance(transform.position, player.transform.position);
        IsStopped(dist);     
    }
    //check if enemy isn't moving and do something if true
    public void IsStopped(float dis)
    {         
        // check if enemy is moving
        if (dis <= agent.stoppingDistance)
        {
            // Have the player take damage acording to the type of enemy
            switch(gameObject.tag){
                case "E0":
                    pch.TakeDamage(5);
                    return;
                case "E1":
                    pch.TakeDamage(10);
                    return;
                case "E2":
                    pch.TakeDamage(15);
                    return;
                case "E3":
                    pch.TakeDamage(20);
                    return;
                case "E4":
                    pch.TakeDamage(25);
                    return;
                case "E5":
                    pch.TakeDamage(30);
                    return;
                case "E6":
                    pch.TakeDamage(35);
                    return;
                case "E7":
                    pch.TakeDamage(40);
                    return;
                case "E8":
                    pch.TakeDamage(45);
                    return;
            }

        }
    }
//Rest my health when used
    public void TakeDamage(int dmg){
        myHealth.RestHealth(dmg);
    }
//Check if i have collided with something
    void OnCollisionEnter(Collision collision)
    {
        CheckBullet(collision);
    }

// Check what type of bullet has collided with me and what to do with each type
    public void CheckBullet(Collision col)
    {
        //Take Damage when a player bullet hit me
        switch(col.gameObject.tag)
        {
            case "B00":
                TakeDamage(45);
                return;
            case "B01":
                TakeDamage(45);
                return;
            case "B02":
                TakeDamage(45);
                return;
            case "B03":
                TakeDamage(45);
                return;
            case "B04":
                TakeDamage(45);
                return;
            case "B05":
                TakeDamage(45);
                return;
            case "B06":
                TakeDamage(45);
                return;
        }
        //Do something when my bullet hit me
        switch(col.gameObject.tag)
        {
            case "B10":

                return;
            case "B11":
                
                return;
            case "B12":
                
                return;
            case "B13":
                
                return;
            case "B14":
                
                return;
            case "B15":
                
                return;
            case "B16":
                
                return;
        }
    }
}
