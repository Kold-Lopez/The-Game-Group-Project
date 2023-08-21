using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostBars : MonoBehaviour, IInteractable
{
    [Header("----- TheBoostyBarObject -----")]
    [SerializeField] GameObject BoostyBars;
    [Header("----- StatChangeMultiplier -----")]
    [Range(1, 10)][SerializeField] float SpeedIncrease;
    [Range(1, 10)][SerializeField] float SprintSpeedIncrease;
    [Range(10, 60)][SerializeField] int effectDuration;

    // Start is called before the first frame update

    public void Interact()
    {
        StartCoroutine(chocolateyBars());
    }

    IEnumerator chocolateyBars()
    {
        BoostyBars.SetActive(false);
       gameManager.instance.playerScript.makeManSpeedy(SpeedIncrease, SprintSpeedIncrease, 0);
       yield return new WaitForSeconds(effectDuration);
        gameManager.instance.playerScript.makeManSpeedy(-SpeedIncrease, -SprintSpeedIncrease, 0);
    }
}
