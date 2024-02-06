using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownRotation_TEST : MonoBehaviour
{
    private float lerpCounter, lerpDuration;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void LerpTest()
    {
        StartCoroutine(LerpCoroutine());
    }

    IEnumerator LerpCoroutine()
    {
        lerpCounter = 0;
        lerpDuration = 0;



        while (lerpCounter < lerpDuration)
        {
            lerpCounter += Time.deltaTime;


            yield return null;
        }

        //snap values with setting to zero
        
    }
}


/*

        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            gameObjectToMove.transform.rotation = Quaternion.Lerp(currentRot, newRot, counter / duration);
            yield return null;
        }
        rotating = false; 


    this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    this.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    this.gameObject.transform.position = resetPoint;

 */