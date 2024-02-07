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

    private Vector3 force1x = new Vector3(0, 0, 1);	
    private Vector3 force2x = new Vector3(0, 0, -2);
    private Vector3 force3x = new Vector3(-2, 0, 0);
    private Vector3 force4x = new Vector3(3, 0, 0);	

    private Vector3 stationRotateForce = new Vector3(0, 0, -0.25f);

    private Vector3 statRotRight = new Vector3(-0.33f, 0, 0);
    private Vector3 statRotLeft = new Vector3(0.33f, 0, 0);

    private Vector3 forwardForce = new Vector3(0, 0, 5);

    private Vector3 targetPoint = new Vector3(350, 0.5f, 5000);
    private Vector3 resetPoint = new Vector3(350, 0.5f, 0);

    public GameObject environmentSphere;

    private float rotationSpeed = 10.0f;
    private bool doRotateSphere = false;

    private float currentSpeed = 0;
    private float accel = 0.25f;

    private float lerpCounter, lerpDuration;
    private float ballTravelTime = 0;

    private bool tent1IsComplete = false, tent2IsComplete = false, tent3IsComplete = false;
    void Start()
    {
        actuateAgent.SetMotionSource(this.gameObject);

        ballTravelTime = 3f;   
        FlyFunction();
    }

    //fixedUpdate means Time.deltaTime is always constant
    void FixedUpdate()
    {
        //this is the environment with the buildings. NOT the haptic chair ball
        if (doRotateSphere == true)
        {
            environmentSphere.transform.Rotate(Vector3.left * (rotationSpeed * Time.deltaTime));
        }


        if (this.gameObject.transform.position.z > 100)    //this is an arbitrary point that we make for the ship to fly at a certain amount of time
                                                            //and once they hit that length (if no ship error stops them) then they stop abruptly regardless once they get to the med tent
        {
            this.gameObject.transform.position = resetPoint;
            //while flying, if ship encounters an error (at random or predetermined), stop and set position at 0
            //to jerk stop the player and issue an alert that something is broken
        }
    }

    public void SwitchStationLeft()
    {
        StartCoroutine(HapticRotation(statRotLeft));
    }

    public void SwitchStationRight()
    {
        StartCoroutine(HapticRotation(statRotRight));
    }

    public void FlyFunction()
    {
        StartCoroutine(FlyForAmountOfSeconds(ballTravelTime));
    }

    public void HardStopTheShip()
    {
        StartCoroutine(FlyUntilMalfunction(5));
    }

    //---------------------------COROUTINES-----------------------------------\\

    IEnumerator HapticRotation(Vector3 rotDirection)
    {
        //rotDirection tells which direction chair is moving for the stations rotating.
        this.gameObject.GetComponent<Rigidbody>().AddForce(rotDirection, ForceMode.Impulse);
        yield return new WaitForSeconds(0.5f);

        //utilize LERP to make the actuate acceleration approach zero instead of violently resetting.

        lerpCounter = 0;
        lerpDuration = .5f;
        Vector3 startingVel = this.gameObject.GetComponent<Rigidbody>().velocity;
        Vector3 startingAngVel = this.gameObject.GetComponent<Rigidbody>().angularVelocity;
        Quaternion startingRot = this.gameObject.transform.rotation;

        while (lerpCounter < lerpDuration)
        {
            lerpCounter += Time.deltaTime;
            this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.Lerp(startingVel, Vector3.zero, lerpCounter / lerpDuration);
            this.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.Lerp(startingAngVel, Vector3.zero, lerpCounter / lerpDuration);

            this.gameObject.transform.rotation = Quaternion.Lerp(startingRot, Quaternion.identity, lerpCounter / lerpDuration);
            yield return null;
        }

        this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    IEnumerator FlyForAmountOfSeconds(float secondsToFly)
    {
        //secondsToFly is what ballTravelTime is (that's the value that's passed in)
        doRotateSphere = true;

        lerpDuration = 0;
        Quaternion endRot = Quaternion.Euler(180, 0, 0);
        while (lerpDuration < (secondsToFly / 2))
        {
            lerpDuration += Time.deltaTime;
            this.gameObject.transform.rotation = Quaternion.Lerp(Quaternion.identity, endRot, (lerpDuration / (ballTravelTime/2)));
            yield return null;
        }

        lerpDuration = 0;
        while (lerpDuration < (secondsToFly / 2))
        {
            lerpDuration += Time.deltaTime;
            this.gameObject.transform.rotation = Quaternion.Lerp(endRot, Quaternion.identity, (lerpDuration / (ballTravelTime / 2)));
            yield return null;
        }

        doRotateSphere = false;

        lerpCounter = 0;
        lerpDuration = 1f;
        Vector3 startingVel = this.gameObject.GetComponent<Rigidbody>().velocity;
        Vector3 startingAngVel = this.gameObject.GetComponent<Rigidbody>().angularVelocity;
        Quaternion startingRot = this.gameObject.transform.rotation;
        while (lerpCounter < lerpDuration)
        {
            lerpCounter += Time.deltaTime;
            this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.Lerp(startingVel, Vector3.zero, lerpCounter / lerpDuration);
            this.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.Lerp(startingAngVel, Vector3.zero, lerpCounter / lerpDuration);

            this.gameObject.transform.rotation = Quaternion.Lerp(startingRot, Quaternion.identity, lerpCounter / lerpDuration);
            yield return null;
        }

        this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    IEnumerator FlyUntilMalfunction(float secondsToFly)
    {
        doRotateSphere = true;

        lerpDuration = 0;
        Quaternion endRot = Quaternion.Euler(180, 0, 0);
        while (lerpDuration < (secondsToFly))
        {
            lerpDuration += Time.deltaTime;
            this.gameObject.transform.rotation = Quaternion.Lerp(Quaternion.identity, endRot, (lerpDuration / (ballTravelTime / 2)));
            yield return null;
        }

        lerpCounter = 0;
        lerpDuration = 0.5f;
        Vector3 startingVel = this.gameObject.GetComponent<Rigidbody>().velocity;
        Vector3 startingAngVel = this.gameObject.GetComponent<Rigidbody>().angularVelocity;
        Quaternion startingRot = this.gameObject.transform.rotation;
        while (lerpCounter < lerpDuration)
        {
            lerpCounter += Time.deltaTime;
            this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.Lerp(startingVel, Vector3.zero, lerpCounter / lerpDuration);
            this.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.Lerp(startingAngVel, Vector3.zero, lerpCounter / lerpDuration);

            this.gameObject.transform.rotation = Quaternion.Lerp(startingRot, Quaternion.identity, lerpCounter / lerpDuration);
            yield return null;
        }

        this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        this.gameObject.transform.position = resetPoint;    // <--- key factor to a hard stop

        doRotateSphere = false;
    }
}

/* 

//have the ball go to the reset point when:
    1. ship starts flying to next tent
    2. ship ends flying to next tent
    3. ship encounters mid-flight issue and needs to stop
    4. ship continues flying after mid-flight issue is solved

have the ball lerp down to zero, NOT RESETTING POINT
    1. on station switches

If Zac answers, find a way to reset the chair to its level/starting point
    **do this after each station switch (so the player is level with each station after chair movement



 */


