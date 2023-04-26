using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopWatch : MonoBehaviour
{
    [SerializeField] private Text stopwatch;
    private float currentTime = 0;
    private float minutes = 0;
    private bool StopwatchLive = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!StopwatchLive) return;
        
        currentTime += Time.deltaTime;
        MinuteConversion();
        UpdateText();

    }

    private void MinuteConversion()
    {
        if (currentTime >= 60)
        {
            minutes++;
            currentTime -= 60;
        }
    }

    private void UpdateText()
    {
        stopwatch.text = $"{minutes}.{currentTime:0.00}";
    }

    private void OnPlayerDied()
    {
        StopwatchLive = false;
    }

    private void OnEnable()
    {
        Player.PlayerDied += OnPlayerDied;
    }

    private void OnDisable()
    {
        Player.PlayerDied -= OnPlayerDied;
    }
}
