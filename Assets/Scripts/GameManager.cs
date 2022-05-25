using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static bool paused;

    public Sounds[] sounds;

    private void Awake()
    {
        if(GameManager.instance == null)
        {
            GameManager.instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        foreach  (Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        PlaySound("music");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            PlaySound("shot");

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                Pause();
            }
            else if (paused)
            {
                Unpause();
            }
        }
    }

    public void PlaySound(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Missing " + name + "sound file or reference.");
            return;
        }
        s.source.Play();
    }


    public void PlayGame()
    { 
        SceneManager.LoadScene(1);
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void MainMenu()
    {
        Cursor.lockState = CursorLockMode.None;
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
    public void ExitGame()
    {
        Application.Quit();
    }
}