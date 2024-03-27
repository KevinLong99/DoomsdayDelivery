using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text.RegularExpressions;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;

public class Medtent : MonoBehaviour
{
    /*
    public int numInsulin;
    public int numSyringes;
    public int numBandages;
    public int numOintment;
    */

    //Actual ints as the medtent's requirments
    public int BandageTag;
    public int OintmentTag;
    public int SyringeTag;
    public int InsulinTag;

    private char numChar;

    public Rotate_Me_Parent rotateParentScript;
    public Typewriter_UI typewriter_script;

    public int tentNum;
    public float thresholdPercentage;

    void Start()
    {
        //for this to work, the name of the medtent must be in format: Medtent# (with # being a single digit number)
        //numChar = this.gameObject.name[7];
        //tentNum = (int)char.GetNumericValue(numChar);

    }

    private void DetermineNeededSupplies()
    {
        //use the medtents number to help calculate the supplies needed. 
        //when doing calculations, floor the number to an int if is a decimal
        //numInsulin =  Random.Range(2,5);
        //numSyringes = Random.Range(2,5);
        //numBandages = Random.Range(2,5);
        //numOintment = Random.Range(2,5);


    }

    public void SendSupplyRequestToMothership()
    {
        //DetermineNeededSupplies();

        string toMothershipMessage = 
            "INCOMING TRANSMISSION FROM SURVIVORS... \n \n " +
            "Requesting following relief supplies: \n" +
            BandageTag + " bandages, \n" +
            OintmentTag + " oinment, \n" +
            SyringeTag + " syringes, \n" +
            InsulinTag + " insulin packets.";

        typewriter_script.StartTypewriterView(toMothershipMessage);
    }

    /*
    private void OnTriggerEnter(Collider other)
    {


        //need to make a drone tag for drone object
        if (other.gameObject.tag == "Medbox")
        {
            rotateParentScript.medKitisCompleted = false;

            //compare drone supplies that were delivered to the supplies that the tent needs.
            //do math to determine if the supplies and time delivered were enough
            int successVal = 0;

            //get these values from the drone package that arrives
            int receivedViles = 0;
            int receivedSyringes = 0;
            int receivedBandages = 0;
            int receivedOintment = 0;

            
            if ((receivedViles / numInsulin) >= 0.7f) successVal++;
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

            if (other.gameObject.tag == "Drone")
            {
                Debug.Log("Drone Detected");
                
            }
            


}

    }
    */
}
