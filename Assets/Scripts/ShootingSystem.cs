using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSystem : MonoBehaviour
{
    public GameManager GameManager;
    public float damage = 100f;
    public Camera fpsCam;
    public Zombie zom;
    public ParticleSystem muzzleFlash;
    public GameObject fleshImpact;


    private void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            Shoot();
        }
    }

    void Shoot()
    {
        if(GameManager.paused)
            return;
        muzzleFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit))
        {
            
            if (hit.collider.name == "Head" || hit.collider.name == "HeadTop_End")
            {
                zom = hit.transform.GetComponentInParent<Zombie>();
                
                if (zom != null)
                {
                    zom.alive = false;

                    if (LevelManager.score % 10 == 0)
                    {
                        LevelManager.maxAmountOfZombies++;
                    }
                }
            }
            if (hit.collider.tag == "Zombie")
            {
                GameObject blood = Instantiate(fleshImpact, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(blood, 1f);
            }
        }
    }
}
