using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class GUNtemp : ScriptableObject
{ 

    public float shootRate;
    public int shootDamage;
    public int shootDist;
    public int currentAmmo;
    public int weaponClipSize;
    public int maxAmmo;
    public int ReloadTime;

    public string weaponDifferentiator;

    public GameObject theGUN;
    public GameObject theBullet;
    public ParticleSystem hitEffect;
}