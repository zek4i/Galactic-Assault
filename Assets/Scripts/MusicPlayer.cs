using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    void Awake() //if any music players already exist (we need only 1 music player to exits)
    {
        int numMusicPlayers = FindObjectsOfType<MusicPlayer>().Length; //storing in a variable as the lenght is an array
        if (numMusicPlayers > 1) //use "objects" not "object" as object returns an array for you to use ".Length"
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

}