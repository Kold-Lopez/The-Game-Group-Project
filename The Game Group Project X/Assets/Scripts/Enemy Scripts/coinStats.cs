using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class coinStats : ScriptableObject
{

    public int dropRate;
    public int juggAmount;
    public int meleeAmount;
    public int coinCur;
    public int coinMax;
    public int coinMin;

    public GameObject coin;
}
