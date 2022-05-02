using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "zombie config", menuName = "scriptableZombie/config")]
public class ZombieScriptableObject : ScriptableObject
{
    public float speed;
    public float timeToEscape;
    public float stoppingDistance;
    public float scale;

}   
