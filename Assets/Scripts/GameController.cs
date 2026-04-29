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

    private void Start()
    {
        gameView = GetComponentInChildren<GameView>();
        gameState = GameStates.GamePlaying;
        maxCollectiblesCount = GameObject.FindGameObjectsWithTag("Pick Up").Length;
    }
    
    private void OnGameWon()
    {
        gameState = GameStates.GameWon;
        // Set the text value of our result text
        gameView.resultText.text = "You Win!";
        //Hide count and timer text
        gameView.countText.gameObject.SetActive(false);
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
        gameView.countText.gameObject.SetActive(false);
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

    public void OnPickUpCollectible(int playerCollectibleCount)
    {
        //Play collect sound
        AudioManager.Instance.PlayCollectSFX();
        //Set ui text counter
        gameView.SetCountText(playerCollectibleCount);
        // Check if our 'count' is equal to or exceeded our maxCollectibles count
        if (playerCollectibleCount >= maxCollectiblesCount) 
        {
            StateUpdate(GameStates.GameWon);
        }
    }

    public void UpdateGameTimer(int timerCount)
    {
        gameView.SetTimerText(timerCount);
    }
}
