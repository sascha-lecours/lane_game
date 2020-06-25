using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeperScript : MonoBehaviour
{
    /// <summary>
    /// Singleton
    /// </summary>
    public static ScoreKeeperScript Instance;

    public int currentScore = 0;
    public int lostCargoShips = 0;
    private int gameOverAmount = 3; // Game over when lost cargo ships == this number

    public Text score = null;
    public Text cargoShipsLostText = null;

    public void LoseCargoShip()
    {
        lostCargoShips += 1;
        updateCargoShipsLostText();
        if (lostCargoShips >= gameOverAmount) {
            // TODO: Trigger game over 
        }

    }

    public void updateCargoShipsLostText()
    {
        cargoShipsLostText.text = "Convoys Lost: " + lostCargoShips + "/" + gameOverAmount;
    }

    public void AddPoints(int pointsToAdd)
    {
        currentScore += pointsToAdd;
        score.text = currentScore.ToString();
    }

    void Awake()
    {
        // Register the singleton
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of ScoreKeeperScript!");
        }
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        score.text = currentScore.ToString();
        updateCargoShipsLostText();
    }
}
