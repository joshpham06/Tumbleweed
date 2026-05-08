using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public enum GameStates
    {
        GamePlaying,
        GameWon,
        GameLost
    };
    private GameView gameView;
    private GameStates gameState;
    
    private int maxCollectiblesCount;
    private int maxEnemiesCount;
    private int CollectiblesCount;
    private int KillCount;

    void Awake()
    {
        maxCollectiblesCount = GameObject.FindGameObjectsWithTag("Pick Up").Length;
        maxEnemiesCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }
    
    private void Start()
    {
        gameView = GetComponentInChildren<GameView>();
        gameState = GameStates.GamePlaying;
    }

    void OnEnable()
    {
        Enemy.OnEnemyKilled += HandleEnemyKilled;
    }

    void OnDisable()
    {
        Enemy.OnEnemyKilled -= HandleEnemyKilled;
    }
    
    private void OnGameWon()
    {
        gameState = GameStates.GameWon;
        // Set the text value of our result text
        gameView.resultText.text = "You Win!";
        //Hide count and timer text
        gameView.CollectiblesCountText.gameObject.SetActive(false);
        gameView.EnemiesCountText.gameObject.SetActive(false);
        gameView.timerText.gameObject.SetActive(false);
        //Play win sfx
        AudioManager.Instance.PlayWinSFX();
        //Enable main menu button
        gameView.mainMenuButton.SetActive(true);
    }

    private void OnGameLost()
    {
        gameState = GameStates.GameLost;
        // Set the text value of our result text
        gameView.resultText.text = "You Lose.";
        //Hide count and timer text
        gameView.CollectiblesCountText.gameObject.SetActive(false);
        gameView.EnemiesCountText.gameObject.SetActive(false);
        gameView.timerText.gameObject.SetActive(false);
        //Play lose sfx
        AudioManager.Instance.PlayLoseSFX();
        //Enable main menu button
        gameView.mainMenuButton.SetActive(true);
    }

    public void StateUpdate(GameStates newState)
    {
        //Exit condition- if the game is not in play, we cannot advance to win or lose
        if (gameState != GameStates.GamePlaying)
        {
            return;
        }
        
        switch (newState)
        {
            case GameStates.GamePlaying:
                break;
            case GameStates.GameWon:
                gameState = GameStates.GameWon;
                OnGameWon();
                break;
            case GameStates.GameLost:
                gameState = GameStates.GameLost;
                OnGameLost();
                break;
        }
    }

    public void OnPickUpCollectible()
    {
        //Play collect sound
        AudioManager.Instance.PlayCollectSFX();
        //Set ui text counter
        CollectiblesCount++;
        gameView.SetCollectiblesCountText(CollectiblesCount);
        CheckGameWin();
    }
    
    public void HandleEnemyKilled()
    {
        //Play kill sound
        //AudioManager.Instance.PlayCollectSFX();
        KillCount++;
        gameView.SetEnemiesCountText(KillCount);
        CheckGameWin();
    }

    public void UpdateGameTimer(int timerCount)
    {
        gameView.SetTimerText(timerCount);
    }

    public int GetMaxCollectiblesCount()
    {
        return maxCollectiblesCount;
    }
    
    public int GetMaxEnemiesCount()
    {
        return maxEnemiesCount;
    }

    private void CheckGameWin()
    {
        if (CollectiblesCount >= maxCollectiblesCount && KillCount >= maxEnemiesCount) 
        {
            StateUpdate(GameStates.GameWon);
        }
    }
}
