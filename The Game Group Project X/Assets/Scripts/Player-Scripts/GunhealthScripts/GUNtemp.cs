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
    public int gameStartAmmo;
    [Range(1, 10)] public float ReloadTime;

    public string weaponDifferentiator;

    public GameObject theGUN;
    public ParticleSystem hitEffect;
}