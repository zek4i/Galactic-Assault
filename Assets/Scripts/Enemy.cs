using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] int scorePerHit = 15;
    [SerializeField] int hitPoints = 2;
    ScoreBoard scoreBoard;
    GameObject parentGameObject; //used transform earlier where only transform can be used but now everything in gameobj can be used
    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>(); //find it form all of its type of obj
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime"); //parents everything thats getting hit
        AddRigidbody(); //instead od adding to every enemy we add it once in code and forget about it
    }

    private void AddRigidbody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void OnParticleCollision(GameObject other)
    {   
        ProcessHit();
        if(hitPoints < 1)
        {
        KillEnemy();
        }
    }

    void ProcessHit()
    {
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity); //instantiation process 
        vfx.transform.parent = parentGameObject.transform; //(.transform cuz)as the variable type is not transform rn but gameobj
        hitPoints--; //there is one hit
        //doing this to make sure the vfx does no get turned off after destruction
        scoreBoard.IncreaseScore(scorePerHit); //accessing scoreboard class 
    }
    void KillEnemy()
    {
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity); //current pos and no rotation
        fx.transform.parent = parentGameObject.transform;
        Destroy(gameObject);
    }

}
