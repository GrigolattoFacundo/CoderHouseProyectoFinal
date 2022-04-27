using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSystem : MonoBehaviour
{
    public float damage = 100f;
    public Camera fpsCam;
    public Zombie zom;
    public ParticleSystem muzzleFlash;
    public GameObject fleshImpact;

    void Start()
    {
        GameObject Enemy = GameObject.FindGameObjectWithTag("Zombie");
        //zom = Enemy.GetComponent<Zombie>();
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
        muzzleFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit))
        {
            
            if (hit.collider.name == "Head")
            {
                zom = hit.transform.GetComponentInParent<Zombie>();
                
                if (zom != null)
                {
                    zom.alive = false;
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
