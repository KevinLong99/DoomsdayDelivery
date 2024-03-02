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

    public Typewriter_UI typewriter_script;

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

    public void SendSupplyRequestToMothership()
    {
        string toMothershipMessage = 
            "INCOMING TRANSMISSION FROM SURVIVORS... \n \n " +
            "Requesting following relief supplies: \n" +
            numViles + " viles, \n" +
            numSyringes + " syringes, \n" +
            numBandages + " bandages, \n" +
            numOintment + " ointment packets.";

        typewriter_script.StartTypewriterView(toMothershipMessage);
    }


    private void OnTriggerEnter(Collider other)
    {
        //need to make a drone tag for drone object
        if (other.gameObject.tag == "Medbox")
        {
            //compare drone supplies that were delivered to the supplies that the tent needs.
            //do math to determine if the supplies and time delivered were enough
            int successVal = 0;

            //get these values from the drone package that arrives
            int receivedViles = 0;
            int receivedSyringes = 0;
            int receivedBandages = 0;
            int receivedOintment = 0;

            if ((receivedViles / numViles) >= 0.7f) successVal++;
            if ((receivedSyringes / numSyringes) >= 0.7f) successVal++;
            if ((receivedBandages / numBandages) >= 0.7f) successVal++;
            if ((receivedOintment / numOintment) >= 0.7f) successVal++;

            if (successVal > 1)
            {
                //success response
                Debug.Log("You delivered enough supplies!");
            } else
            {
                //fail response
                Debug.Log("You failed to deliver enough supplies! \nPeople will die because of your failure!");
            }
        }
    }
}
