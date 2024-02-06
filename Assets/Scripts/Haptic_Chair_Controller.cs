using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haptic_Chair_Controller : MonoBehaviour
{
    //0.5f is weak, 1 is good.
    //need (slightly) higher numbers when moving along the z axis (force3x and 4x)
    //easier to move chair forwards and backwards than side to side

    //to resemble wind, the chair can react to one of the small forces
    //for station movement, slightly higher force impulse
    //for ship malfunciton, higher force impulse
    //ship flying, highest impulse upon stop (show that the ship is shoddy and old, use audio cues
    //      and VFX (like sparks) when ship comes to a halt to collaborate with the sudden stop of the ship.

    public Actuate.ActuateAgent actuateAgent;

    private Vector3 force1x = new Vector3(0, 0, 1);		//
    private Vector3 force2x = new Vector3(0, 0, -2);	//
    private Vector3 force3x = new Vector3(-2, 0, 0);	//
    private Vector3 force4x = new Vector3(3, 0, 0);		//

    private Vector3 forwardForce = new Vector3(0, 0, 5);

    private Vector3 targetPoint = new Vector3(300, 0, 300);
    private Vector3 resetPoint = new Vector3(350, 0.5f, 0);

    public GameObject environmentSphere;

    private float rotationSpeed = 10.0f;
    private bool doRotateSphere = false;

    private float currentSpeed = 0;
    private float accel = 0.5f;

    private float lerpCounter, lerpDuration;
    void Start()
    {
        actuateAgent.SetMotionSource(this.gameObject);

        //StartCoroutine(TestForces());

        StartCoroutine(FlyForAmountOfSeconds(7));
    }

    void FixedUpdate()
    {

        if (doRotateSphere == true)
        {
            //this is the environment with the buildings. NOT the haptic chair ball
            environmentSphere.transform.Rotate(Vector3.left * (rotationSpeed * Time.deltaTime));

            //make ball move at constant speed/acceleration
            currentSpeed += Mathf.Min(accel * Time.deltaTime, 1);
            Vector3 pos = Vector3.MoveTowards(this.transform.position, targetPoint, rotationSpeed * currentSpeed * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(pos);
            //script credits to DuckType @ https://forum.unity.com/threads/object-moving-and-acceleration.582256/
        }




        if (this.gameObject.transform.position.z > 300)    //this is an arbitrary point that we make for the ship to fly at a certain amount of time
                                                            //and once they hit that length (if no ship error stops them) then they stop abruptly regardless once they get to the med tent
        {
            this.gameObject.transform.position = resetPoint;
            //while flying, if ship encounters an error (at random or predetermined), stop and set position at 0
            //to jerk stop the player and issue an alert that something is broken
        }


    }

    public void SwitchStationLeft()
    {
        //change force1x to proper force vector
        //this.gameObject.GetComponent<Rigidbody>().AddForce(force1x, ForceMode.Impulse);
        StartCoroutine(HapticLeft());
    }

    public void SwitchStationRight()
    {
        StartCoroutine(HapticRight());
    }

    IEnumerator TestForces()
    {
        yield return new WaitForSeconds(5);
        this.gameObject.GetComponent<Rigidbody>().AddForce(force1x, ForceMode.Impulse);
        yield return new WaitForSeconds(5);
        this.gameObject.GetComponent<Rigidbody>().AddForce(force2x, ForceMode.Impulse);
        yield return new WaitForSeconds(5);
        this.gameObject.GetComponent<Rigidbody>().AddForce(force3x, ForceMode.Impulse);
        yield return new WaitForSeconds(5);
        this.gameObject.GetComponent<Rigidbody>().AddForce(force4x, ForceMode.Impulse);
        yield return new WaitForSeconds(5);

    }


    IEnumerator HapticLeft()
    {
        this.gameObject.GetComponent<Rigidbody>().AddForce(force1x, ForceMode.Impulse);
        yield return new WaitForSeconds(3);
    }

    IEnumerator HapticRight()
    {

        this.gameObject.GetComponent<Rigidbody>().AddForce(force1x, ForceMode.Impulse);
        yield return new WaitForSeconds(0.5f);

        //utilize LERP to make the actuate acceleration approach zero instead of violently resetting.

        lerpCounter = 0;
        lerpDuration = 1f;
        Vector3 startingVel = this.gameObject.GetComponent<Rigidbody>().velocity;
        Vector3 startingAngVel = this.gameObject.GetComponent<Rigidbody>().angularVelocity;

        while (lerpCounter < lerpDuration)
        {
            lerpCounter += Time.deltaTime;
            this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.Lerp(startingVel, Vector3.zero, lerpCounter / lerpDuration);
            this.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.Lerp(startingAngVel, Vector3.zero, lerpCounter / lerpDuration);
            yield return null;
        }

        this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;       


    }

    IEnumerator FlyForAmountOfSeconds(float secondsToFly)
    {
        doRotateSphere = true;
        yield return new WaitForSeconds(secondsToFly);
        doRotateSphere = false;

        this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        this.gameObject.transform.position = resetPoint;
    }
}

/* 

//have the ball go to the reset point when:
    1. ship starts flying to next tent
    2. ship ends flying to next tent
    3. ship encounters mid-flight issue and needs to stop
    4. ship continues flying after mid-flight issue is solved

//have the ball lerp down to zero, NOT RESETTING POINT
    1. on station switches

If Zac answers, find a way to reset the chair to its level/starting point
    **do this after each station switch (so the player is level with each station after chair movement






 */
