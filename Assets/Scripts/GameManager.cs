using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool playerIsDead;
    public GameObject Crosshair;
    public GameObject deadText;
    //private int amountOfZombies;
    //public GameObject zombie;

    private void Start()
    {
        playerIsDead = false;
        Crosshair.SetActive(true);
        deadText.SetActive(false);
    }

    private void Update()
    {
        /*if (amountOfZombies > 2)
        {
            Instantiate(zombie);
        }*/
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
        if (playerIsDead == true)
        {
            Crosshair.SetActive(false);
            deadText.SetActive(true);
        }
    }

}
