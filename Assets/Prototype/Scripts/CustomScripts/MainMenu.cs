using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject creditsMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void goToCredits()
    {
        startMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    public void goToStart()
    {
        startMenu.SetActive(true);
        creditsMenu.SetActive(false);
    }
}
