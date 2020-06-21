using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeHelperScript : MonoBehaviour
{
    public static TimeHelperScript Instance;

    private float slowTimeRemaining = 0f;
    private float slowFraction = 0.4f; // Lowest it gets
    private float timeResetSpeed = 10f; // Controls rate of return to normal speed once slowime elapsed.

    private float roundUpThreshhold = 0.95f;


    void Awake()
    {
        // Register the singleton
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of TimeHelper!");
        }
        Instance = this;
    }

    public void AddSlowTime(float slowTimeToAdd)
    {
        slowTimeRemaining += slowTimeToAdd;
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
    }
}
