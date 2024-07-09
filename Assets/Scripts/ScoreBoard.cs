using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreBoard : MonoBehaviour
{
    // Start is called before the first frame update
    int score;
    TMP_Text ScoreText;
    void Start()
    {
        ScoreText = GetComponent<TMP_Text>();
        ScoreText.text = "score"; //only accepts string
    }
    public void IncreaseScore(int amountToIncrease) //public class
    {     
            score += amountToIncrease; //score + amountToIncrease
            ScoreText.text = score.ToString();
    }
}
