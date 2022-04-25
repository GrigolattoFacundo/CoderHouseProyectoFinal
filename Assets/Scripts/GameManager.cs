using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public AudioSource audioSource;
    //private int amountOfZombies;
    //public Zombie zombieInstance;
    //public GameObject zombie;
    public static bool paused;

    private void Awake()
    {
        if(GameManager.instance == null)
        {
            GameManager.instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Pause()
    {
        paused = true;
        Time.timeScale = 0f;
    }

    public void Unpause()
    {
        paused = false;
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                Pause();
            }
            else if(paused)
            {
                Unpause();
            }
        }
            
        if (Input.GetKeyDown(KeyCode.R))
        {
            Play();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            MainMenu();
        }
        /*if (amountOfZombies >= 2)
        {
            Instantiate(zombie);
            amountOfZombies ++;
        }*/
        
    }
}
