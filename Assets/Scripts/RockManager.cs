using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockManager : MonoBehaviour
{

    [Tooltip("Rock objesinin parentı")]
    public GameObject Rock;

    [Tooltip("Rock üretim bekleme zamanı")]
    float waitTime = 0.5f;


    public GameManagerS gm;
   
    IEnumerator RockCreator()
    {
        while (gm.GamePlaying)
        {

            yield return new WaitForSecondsRealtime(waitTime);

            Instantiate(
                Rock,
                new Vector3(Random.Range(-40, 40),
                           this.transform.position.y,
                           this.transform.position.z),
                Quaternion.identity,
                this.transform).GetComponent<RockMovement>()
                .speed = Random.Range(40, 70);
        }

    }

    /// <summary>
    /// Üretimi başlatır
    /// </summary>
    public void StartGame()
    {
        StartCoroutine(RockCreator());
    }

}


