using UnityEngine;
using System.Collections;
using PA_DronePack;

public class SwitchMaterialChanger : MonoBehaviour
{
    // Assign this in the inspector
    public Material newMaterial;
    public Material blankMaterial;

    //Success/Fail screens
    public Material successMaterial;
    public Material failMaterial;

    public int DelayedTime; //Delayed time in seconds
    // This function changes the material of the GameObject's MeshRenderer
    public bool canTurnOnAgain = false;

    //Initiate Drone control, disable it while the screen is dark
    public PA_DroneAxisInput myDroneInput;

    private void Update()
    {
        /*
        //Disable drone input while the screen is dark
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
         if (meshRenderer.material.shader.name == blankMaterial.shader.name)
            {
                //myDroneInput.enabled = false;
                //DelayedDroneControlOff();
            }
            else
            {
                //myDroneInput.enabled = true;
            //Debug.Log("The GameObject's material is not equal to BlankMaterial.");
        }
        */
 
    }
    public void ChangeMaterial()
    {
        if(canTurnOnAgain == true)
        {
            // Get the MeshRenderer component attached to this GameObject
            MeshRenderer renderer = GetComponent<MeshRenderer>();

            // Check if the MeshRenderer and newMaterial are not null to avoid errors
            if (renderer != null && newMaterial != null)
            {
                // Change the material to the newMaterial
                renderer.material = newMaterial;
            }
            else
            {
                Debug.LogError("Missing component: MeshRenderer or newMaterial not assigned.");
            }
        }
    }

    public void DisableSwitchInput()
    {
            DelayedDroneControlOff();
            //myDroneInput.enabled = false;
    }

    public void EnableSwitchInput()
    {
        if (canTurnOnAgain == true)
        {
            myDroneInput.enabled = true;
        }
    }

    public void ChangeMaterialToBlank()
    {
        // Get the MeshRenderer component attached to this GameObject
        MeshRenderer renderer = GetComponent<MeshRenderer>();

        // Check if the MeshRenderer and newMaterial are not null to avoid errors
        if (renderer != null && blankMaterial != null)
        {
            // Change the material to the newMaterial
            renderer.material = blankMaterial;
        }
        else
        {
            Debug.LogError("Missing component: MeshRenderer or newMaterial not assigned.");
        }
    }

    public void ChangeMaterialToSuccess()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        if (renderer != null && successMaterial != null)
        {
            // Change the material to the newMaterial
            renderer.material = successMaterial;
        }
    }

    public void ChangeMaterialToFail()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        if (renderer != null && failMaterial != null)
        {
            // Change the material to the newMaterial
            renderer.material = failMaterial;
        }
    }

    public void DelayedChangeMaterialToBlank()
    {
        StartCoroutine(DelayedChange());
    }

    // Coroutine to delay the change of material
    private IEnumerator DelayedChange()
    {
        yield return new WaitForSeconds(DelayedTime);

        MeshRenderer renderer = GetComponent<MeshRenderer>();
        if (renderer != null && blankMaterial != null)
        {
            renderer.material = blankMaterial;
        }
        else
        {
            Debug.LogError("Missing component: MeshRenderer or blankMaterial not assigned.");
        }
    }


    //Delayed turn off of drone control input
    private IEnumerator DelayedDroneControlOff()
    {
        yield return new WaitForSeconds(DelayedTime);

        myDroneInput.enabled = false;

    }


        public void CanTurnOnAgain()
    {
        canTurnOnAgain = true;
    }

    public void CanNotTurnOnAgain()
    {
        canTurnOnAgain = false;
    }
}