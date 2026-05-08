using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    public Text CollectiblesCountText;
    public Text EnemiesCountText;
    public Text resultText;
    public Text timerText;
    public GameObject mainMenuButton;
    public GameController GameController;
    
    // Start is called before the first frame update
    private void Start()
    {
        // Set the text property of our Result Text UI to an empty string, making the game over message blank
        resultText.text = "";
        SetCollectiblesCountText(0);
        SetEnemiesCountText(0);
    }

    // Create a standalone function that can update the 'countText' UI and check if the required amount to win has been achieved
    public void SetCollectiblesCountText(int collectiblesCount)
    {
        // Update the text field of our 'countText' variable
        CollectiblesCountText.text = "Collectibles: " + collectiblesCount + "/" + GameController.GetMaxCollectiblesCount();
    }
    
    public void SetEnemiesCountText(int enemiesCount)
    {
        EnemiesCountText.text = "Enemies: " + enemiesCount + "/" + GameController.GetMaxEnemiesCount();;
    }

    public void SetTimerText(int count)
    {
        timerText.text = "Time: " + count;
    }
}
