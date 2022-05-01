using UnityEngine;
using UnityEngine.UI;
using System;

public class LevelManager : MonoBehaviour
{
    public static bool playerIsDead;
    public Image crosshair;
    public GameObject deadText;
    public GameObject pauseMenu;

    public static int amountOfZombies;
    public static int maxAmountOfZombies;
    public GameObject zombie;
    public static int score;
    //private GameObject[] zombieInstance;


    private void Start()
    {
        amountOfZombies = 0;
        maxAmountOfZombies = 3;
        GameManager.paused = false;
        SpawnZombies();
        score = 0;

        Zombie.zombieDied += SpawnZombies;                          //acá suscribo al evento
    }
    private void Update()
    {
        if (!playerIsDead)
        {
            if (!GameManager.paused) { AliveUI(); }
        }

        if (playerIsDead && !GameManager.paused)
        {
            if (!GameManager.paused) { DeadUI(); }
        }
        if (GameManager.paused)
        {
            PausedUI();
        }
        
        if(amountOfZombies <= maxAmountOfZombies)
        {
            SpawnZombies();
        }
    }

    void SpawnZombies()
    {
        if (amountOfZombies < maxAmountOfZombies)
            {
            Instantiate(zombie, new Vector3(UnityEngine.Random.Range(-20, 20), 1, UnityEngine.Random.Range(-20, 20)), Quaternion.identity);
            Debug.Log(score);
            amountOfZombies++;
            Debug.Log("LevelManager ejecutó el método spawnZombie, el cual pudo o no ser llamado por evento");
            }
    }
    void PausedUI()
    {
        crosshair.gameObject.SetActive(false);
        deadText.SetActive(false);
        pauseMenu.SetActive(true);
    }
    void DeadUI()
    {
        crosshair.gameObject.SetActive(false);
        deadText.SetActive(true);
        pauseMenu.SetActive(false);
    }
    void AliveUI()
    {
        crosshair.gameObject.SetActive(true);
        deadText.SetActive(false);
        pauseMenu.SetActive(false);
    }
}

    
