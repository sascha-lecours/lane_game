using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeHelperScript : MonoBehaviour
{
    public static TimeHelperScript Instance;

    private float slowTimeRemaining = 0f;
    private float slowFraction = 0.4f; // Lowest it gets
    public float timeResetSpeed = 15f; // Controls rate of return to normal speed once slowime elapsed.

    private float roundUpThreshhold = 0.95f;
    public GameObject pauseMenuObject = null;


    void Awake()
    {
        // Register the singleton
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of TimeHelper!");
        }
        Instance = this;
    }

    void Start()
    {
        ResumeGame(); // Fixes issue if game starts in paused state
    }

    public void AddSlowTime(float slowTimeToAdd)
    {
        slowTimeRemaining += slowTimeToAdd * slowFraction; // Multiplying by slowfractino ensures that you can use reatime numbers
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenuObject.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenuObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        slowTimeRemaining -= Time.deltaTime;
        if (slowTimeRemaining > 0)
        {
            Time.timeScale = slowFraction;
        } else
        {
            slowTimeRemaining = 0;
            Time.timeScale = Mathf.Lerp(Time.timeScale, 1, timeResetSpeed * Time.deltaTime);
            if (Time.timeScale > roundUpThreshhold)
            {
                Time.timeScale = 1;
            }
        }

        if (Input.GetKeyDown("escape"))
        {
            PauseGame();
        }
    }
}
