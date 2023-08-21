using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable
{
    public void Interact();
}

public class TheInteractor : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform InteractorSource;
    public float InteractableRange = 20;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) 
        {
            Ray ray = new Ray(InteractorSource.position, InteractorSource.forward);
            if(Physics.Raycast(ray, out RaycastHit hitInfo, InteractableRange))
            {
                if(hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    interactObj.Interact();
                }
            }
        }
    }
}
