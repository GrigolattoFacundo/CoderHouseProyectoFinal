using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static bool playerIsDead;
    public Image crosshair;
    public GameObject deadText;
    public GameObject pauseMenu;


    private void Start()
    {
        GameManager.paused = false;
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

    
