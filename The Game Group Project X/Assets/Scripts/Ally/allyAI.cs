using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class allyAI : MonoBehaviour
{
    [SerializeField] Renderer model;
    [SerializeField] NavMeshAgent agent;


    [SerializeField] int HP;
    [SerializeField] int speed;
    [SerializeField] int playerfacespeed;

    [SerializeField] float shootrate;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform shootpos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
