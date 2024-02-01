using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haptic_Chair_Control : MonoBehaviour
{
    //0.5f is weak, 1 is good.
    //need (slightly) higher numbers when moving along the z axis (force3x and 4x)
    //easier to move chair forwards and backwards than side to side

    //to resemble wind, the chair can react to one of the small forces
    //for station movement, slightly higher force impulse
    //for ship malfunciton, higher force impulse
    //ship flying, highest impulse upon stop (show that the ship is shoddy and old, use audio cues
    //      and VFX (like sparks) when ship comes to a halt to collaborate with the sudden stop of the ship.

    private Vector3 force1x = new Vector3(0, 0, 1);		//
    private Vector3 force2x = new Vector3(0, 0, -2);	//
    private Vector3 force3x = new Vector3(-2, 0, 0);	//
    private Vector3 force4x = new Vector3(3, 0, 0);		//

    private Vector3 forwardForce = new Vector3(0, 0, 0.5f);

    private Vector3 resetPoint = new Vector3 (0, 0, 0);
    void Start()
    {
        //StartCoroutine(JustWait());
    }

    void FixedUpdate()
    {


        if (Input.GetKey(KeyCode.UpArrow))
        {
            //add force to the rigidbody
            this.gameObject.GetComponent<Rigidbody>().AddForce(forwardForce, ForceMode.VelocityChange);
            Debug.Log("FORWARD");
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.gameObject.GetComponent<Rigidbody>().AddForce(force2x, ForceMode.Impulse);
            Debug.Log("BACK");
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.gameObject.GetComponent<Rigidbody>().AddForce(force3x, ForceMode.Impulse);
            Debug.Log("LEFT");
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.gameObject.GetComponent<Rigidbody>().AddForce(force4x, ForceMode.Impulse);
            Debug.Log("RIGHT");
        }


        if (this.gameObject.transform.position.z > 5000)    //this is an arbitrary point that we make for the ship to fly at a certain amount of time
            //and once they hit that length (if no ship error stops them) then they stop abruptly regardless once they get to the med tent
        {
            this.gameObject.transform.position = resetPoint;
            //while flying, if ship encounters an error (at random or predetermined), stop and set position at 0
            //to jerk stop the player and issue an alert that something is broken
        }

        
    }

    IEnumerator JustWait()
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
}
