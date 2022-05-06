using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static bool playerIsDead;
    public Image crosshair;
    public GameObject deadText;
    public GameObject pauseMenu;
    public bool paused;

    public static int amountOfZombies;
    public static int maxAmountOfZombies;
    public GameObject zombie;
    public static int score;
    public float outTime;
    public TextMeshProUGUI outTimer;
    public bool playerIsOut;


    private void Start()
    {
        amountOfZombies = 0;
        maxAmountOfZombies = 3;
        GameManager.paused = false;
        SpawnZombies();
        score = 0;
        outTime = 5f;
        playerIsOut = false;
        Zombie.ZombieDied += SpawnZombies;                          //acá suscribo al evento
    }
    private void Update()
    {
        if (!playerIsDead)
        {
            if (!GameManager.paused)
            {
                if (!playerIsOut)
                {
                    AliveUI();
                }
                else if (playerIsOut) { OutUI(); }
            }
        }

        if (playerIsDead)
        {
            if (!GameManager.paused) { DeadUI(); }
        }
        if (GameManager.paused)
        {
            PausedUI();
        }

        if (amountOfZombies <= maxAmountOfZombies)
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
            Debug.Log("LevelManager ejecutó el método SpawnZombie, el cual pudo o no ser llamado por evento");
            }
    }
    public void OutOfPlayZone()
    {
        if (!GameManager.paused)
        {
            OutUI();
            outTime -= Time.deltaTime;                          //estaba haciendo esto xd
            playerIsOut = true;
            if (outTime <= 0)
            {
                playerIsDead = true;
                outTime = 0;
            }
            int i = (int)outTime;
            outTimer.text = i.ToString();
        }
    }

    void OutUI()
    {
        crosshair.gameObject.SetActive(false);
        outTimer.gameObject.SetActive(true);
        deadText.SetActive(false);
        pauseMenu.SetActive(false);
    }
    void PausedUI()
    {
        crosshair.gameObject.SetActive(false);
        deadText.SetActive(false);
        pauseMenu.SetActive(true);
        outTimer.gameObject.SetActive(false);
    }
    void DeadUI()
    {
        crosshair.gameObject.SetActive(false);
        deadText.SetActive(true);
        pauseMenu.SetActive(false);
        outTimer.gameObject.SetActive(false);
    }
    public void AliveUI()
    {
        crosshair.gameObject.SetActive(true);
        deadText.SetActive(false);
        pauseMenu.SetActive(false);
        outTimer.gameObject.SetActive(false);
    }
}

    
