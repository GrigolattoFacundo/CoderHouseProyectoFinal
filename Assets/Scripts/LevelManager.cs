using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static bool playerIsDead;
    public bool playerIsOut;
    public static int amountOfZombies;
    public static int maxAmountOfZombies;
    public GameObject zombie;

    public bool paused;

    public Image crosshair;
    public GameObject deadMenu;
    public GameObject pauseMenu;
    public GameObject timeOutMenu;
    public float startingLimitTime;
    private bool timeOut;
    private bool died;

    public static int score;
    public float outTime;
    private float limitTime;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreOnOutScreen;
    public TextMeshProUGUI scoreOnDeadScreen;
    public TextMeshProUGUI outTimer;
    public TextMeshProUGUI limitTimeText;


    private void Start()
    {
        timeOut = false;
        died = false;
        amountOfZombies = 0;
        maxAmountOfZombies = 3;
        GameManager.paused = false;
        SpawnZombies();
        score = 0;
        outTime = 5f;
        playerIsOut = false;
        limitTime = startingLimitTime;
        RestartLimitTime();
    }
    private void Update()
    {
        limitTime -= Time.deltaTime;
        limitTimeText.text = limitTime.ToString("0");

        scoreText.text = score.ToString();

        if (limitTime <= 0)
        {
            RestartLimitTime();
            timeOut = true;
            Time.timeScale = 0;
        }

        if (timeOut)
        {
            TimeOverUI();
            return;
        }
        if (died)
        {
            DeadUI();
            return;
        }
        if (playerIsDead)
        {
            Time.timeScale = 0;
            died = true;
            return;
        }
        if (GameManager.paused)
        {
            PausedUI();
            return;
        }
        if (playerIsOut)
        {
            OutUI();
            return;
        }
        else{ AliveUI(); }

        
        if (amountOfZombies < maxAmountOfZombies)
        {
            SpawnZombies();
        }
    }

    void SpawnZombies()
    {
        if (amountOfZombies < maxAmountOfZombies)
            {
            Instantiate(zombie, new Vector3(UnityEngine.Random.Range(-20, 20), 1, UnityEngine.Random.Range(-20, 20)), Quaternion.identity);
            amountOfZombies++;
            }
    }
    public void OutOfPlayZone()
    {
        if (!GameManager.paused)
        {
            OutUI();
            outTime -= Time.deltaTime;
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
    
    public void RestartLimitTime()
    {
        limitTime = startingLimitTime;
        timeOut = false;
    }

    void OutUI()
    {
        crosshair.gameObject.SetActive(false);
        outTimer.gameObject.SetActive(true);
        deadMenu.SetActive(false);
        pauseMenu.SetActive(false);
        timeOutMenu.SetActive(false);
    }
    void PausedUI()
    {
        crosshair.gameObject.SetActive(false);
        deadMenu.SetActive(false);
        pauseMenu.SetActive(true);
        outTimer.gameObject.SetActive(false);
    }
    void DeadUI()
    {
        crosshair.gameObject.SetActive(false);
        deadMenu.SetActive(true);
        pauseMenu.SetActive(false);
        outTimer.gameObject.SetActive(false);
        MouseView.canControl = false;
        scoreOnDeadScreen.text = score.ToString();
        died = false;
        playerIsDead = false;
    }
    public void AliveUI()
    {
        crosshair.gameObject.SetActive(true);
        deadMenu.SetActive(false);
        pauseMenu.SetActive(false);
        outTimer.gameObject.SetActive(false);
        MouseView.canControl = true;
        timeOutMenu.SetActive(false);
    }
    public void TimeOverUI()
    {
        crosshair.gameObject.SetActive(false);
        deadMenu.SetActive(false);
        pauseMenu.SetActive(false);
        outTimer.gameObject.SetActive(false);
        timeOutMenu.SetActive(true);
        scoreOnOutScreen.text = score.ToString();
        MouseView.canControl = false;
    }
}

    
