using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CarryOnMedKit : MonoBehaviour, IInteractable
{
    [Header("----- MedkitObject -----")]
    [SerializeField] GameObject Medkit;

    [Header("----- HealAmount -----")]
    [Range(1, 50)][SerializeField] int healAmount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        
        gameManager.instance.playerScript.takeHealth(healAmount);
        Medkit.SetActive(false);
        
    }
}
