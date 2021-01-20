﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_Script : MonoBehaviour
{
    public static UIManager_Script staticUIManager;
    public Text titleLabel;
    public Text scoreLabel;
    private int totalScore;

    private void Awake() {
        if(staticUIManager == null){
            staticUIManager = this;
        }
        totalScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager_Script.staticGameManager.gamePaused || !GameManager_Script.staticGameManager.gameStarted){
            titleLabel.enabled = true;
        }else{
            titleLabel.enabled = false;
        }
    }

    public void ScorePoints(int points){
        totalScore += points;
        scoreLabel.text = "Score: "+totalScore;
    }
}
