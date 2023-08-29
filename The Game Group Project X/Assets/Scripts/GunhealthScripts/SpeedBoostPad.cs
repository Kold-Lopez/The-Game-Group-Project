using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostPad : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("----- SpeedPadObjectObject -----")]
    [SerializeField] GameObject SpeedyPad;

    [Header("----- SpeedIncrease -----")]
    [Range(1, 10)][SerializeField] int movementSpeedIncrease;
    [Range(1, 10)][SerializeField] int jumpHeightIncrease;
    [Range(1, 10)][SerializeField] int SprintSpeedIncrease;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(SpeedyMan());
    }

    IEnumerator SpeedyMan()
    {
        gameManager.instance.playerScript.makeManSpeedy(movementSpeedIncrease, SprintSpeedIncrease, jumpHeightIncrease);
        yield return new WaitForSeconds(5);
        gameManager.instance.playerScript.makeManSpeedy(-movementSpeedIncrease, -SprintSpeedIncrease, -jumpHeightIncrease);
    }
}
