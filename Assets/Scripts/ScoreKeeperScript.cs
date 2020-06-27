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
    public GameObject gameOverMenuObject = null;
    private bool gameOver = false;

    public Text score = null;
    public Text cargoShipsLostText = null;
    public Image[] cargoShipIcons = null;

    private float iconAlphaFull = 0.8f;
    private float iconAlphaEmpty = 0.2f;

    public void LoseCargoShip()
    {
        lostCargoShips += 1;
        updateCargoShipsLostText();
        AdjustIcons(cargoShipIcons, (gameOverAmount - lostCargoShips));

        if (lostCargoShips >= gameOverAmount)
        {
            
            Invoke("GameOverInitiator", 0.2f);
        }

    }

    private void GameOverInitiator()
    {
        gameOver = true;
        gameOverMenuObject.SetActive(true);
    }

    void AdjustIcons(Image[] iconArray, int currentCount)
    {
        for (var i = 0; i < iconArray.Length; i++)
        {
            if (currentCount > i)
            {
                iconArray[i].color = new Color(1f, 1f, 1f, iconAlphaFull);
            }
            else
            {
                iconArray[i].color = new Color(1f, 1f, 1f, iconAlphaEmpty);
            }
        }
    }

    public void updateCargoShipsLostText()
    {
        cargoShipsLostText.text = "";
        // cargoShipsLostText.text = "Convoys Lost: " + lostCargoShips + "/" + gameOverAmount;
    }

    public void AddPoints(int pointsToAdd)
    {
        if (!gameOver)
        {
            currentScore += pointsToAdd;
            score.text = currentScore.ToString();
        }
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
