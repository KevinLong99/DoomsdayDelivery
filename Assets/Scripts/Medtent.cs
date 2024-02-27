using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text.RegularExpressions;
using UnityEngine;

public class Medtent : MonoBehaviour
{
    public int numViles;
    public int numSyringes;
    public int numBandages;
    public int numOintment;

    private int medtentNum;
    private char numChar;

    void Start()
    {
        //for this to work, the name of the medtent must be in format: Medtent# (with # being a single digit number)
        numChar = this.gameObject.name[7];
        medtentNum = (int)char.GetNumericValue(numChar);
        Debug.Log("Medtent " + medtentNum + " activated.");

        DetermineNeededSupplies();
    }

    private void DetermineNeededSupplies()
    {
        //use the medtents number to help calculate the supplies needed. 
        //when doing calculations, floor the number to an int if is a decimal
        numViles = medtentNum * Random.Range(2,5);
        numSyringes = medtentNum * Random.Range(2, 5);
        numBandages = medtentNum * Random.Range(2, 5);
        numOintment = medtentNum * Random.Range(2, 5);


    }


    private void OnTriggerEnter(Collider other)
    {
        //need to make a drone tag for drone object
        if (other.gameObject.tag == "Drone")
        {
            //compare drone supplies that were delivered to the supplies that the tent needs.
            //do math to determine if the supplies and time delivered were enough
            int successVal = 0;

            int receivedViles = 0;
            int receivedSyringes = 0;
            int receivedBandages = 0;
            int receivedOintment = 0;

            if ((receivedViles * .8f) >= numViles) successVal++;
            if ((receivedSyringes * .8f) >= numViles) successVal++;
            if ((receivedBandages * .8f) >= numViles) successVal++;
            if ((receivedOintment * .8f) >= numViles) successVal++;

            if (successVal > 1)
            {
                //success response
            } else
            {
                //fail response
            }
        }
    }
}
