using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenuController : MonoBehaviour
{
    [SerializeField] private GameObject thingsToHide;
    [SerializeField] private GameObject deathText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        Player.PlayerDied += OnPlayerDied;
    }

    private void OnDisable()
    {
        Player.PlayerDied -= OnPlayerDied;
    }

    private void OnPlayerDied()
    {
        thingsToHide.SetActive(false);
        deathText.SetActive(true);
        Time.timeScale = 0;
    }
}
