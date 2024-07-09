using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    // void OnCollisionEnter(Collision other) //the other obj that its colliding with
    // {
    //     Debug.Log(this.name + "--Collided with--" + other.gameObject.name);
    // }
    [SerializeField] float loadDelay = 1f;
    [SerializeField] ParticleSystem crashVFX;
    void OnTriggerEnter(Collider other)
    {
    //   Debug.Log($"{this.name} **triggered with** {other.gameObject.name}"); //string interpollation method
        StartCrashSequence();
    }

    void StartCrashSequence()
    {
        crashVFX.Play();
        GetComponent<MeshRenderer>().enabled = false; //makes the rocket dissapear after collision
        GetComponent<BoxCollider>().enabled = false; //will not collide after colliding twice
        GetComponent<PlayerControls>().enabled = false; //disabling player movement on collision
        Invoke("ReloadLevel", loadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
