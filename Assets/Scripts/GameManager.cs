using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public AudioSource audioSource;
    //private int amountOfZombies;
    //public Zombie zombieInstance;
    //public GameObject zombie;

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
            Destroy(this);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
        /*if (amountOfZombies >= 2)
        {
            Instantiate(zombie);
            amountOfZombies ++;
        }*/
        
    }
}
