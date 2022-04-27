using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static bool playerIsDead;
    public Image crosshair;
    public GameObject deadText;
    public GameObject pauseMenu;

    public static int amountOfZombies;
    private int maxAmountOfZombies;
    public GameObject zombie;
    //private GameObject[] zombieInstance;

    private void Start()
    {
        amountOfZombies = 0;
        maxAmountOfZombies = 2;
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

        if (amountOfZombies <= maxAmountOfZombies)
        {
            GameObject zombieInstance = Instantiate(zombie, new Vector3 (Random.Range(-20, 20), 1, Random.Range(-20, 20)), Quaternion.identity);
            amountOfZombies++; //instancia bocha de zombies en vez de uno, tengo que corregir esto
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

    
