using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class GUNtemp : ScriptableObject
{
    [Header("----- WeaponStats -----")]
    public float shootRate;
    public int shootDamage;
    public int shootDist;
    public int currentAmmo;
    public int weaponClipSize;
    public int maxAmmo;
    public int gameStartAmmo;
    [Range(1, 10)] public float ReloadTime;

    public bool noAmmo;

    public string weaponDifferentiator;

    [Header("----- Recoil Settings -----")]
    public float gunRecoilX;
    public float gunRecoilY;
    public float gunRecoilZ;

    public GameObject theGUN;
    public ParticleSystem hitEffect;
}