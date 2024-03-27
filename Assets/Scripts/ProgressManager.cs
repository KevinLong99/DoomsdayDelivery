using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProgressManager : MonoBehaviour
{
    public int CurrentMedTent = 1;
    public List<TextMeshPro> SupplyRequirementList;
    public List<int> BandageReq, OintmentReq, SyringeReq, InsulinReq;
    public List<GameObject> MedTents;

    public void UpdateMedTent()
    {
        CurrentMedTent++;
    }

    void Update()
    {
        if (CurrentMedTent >= 1 && CurrentMedTent <= 3)
        {
            int index = CurrentMedTent - 1; // Adjust for zero-based index

            if (SupplyRequirementList.Count >= 4)
            {
                // Assuming each requirement list contains enough elements
                SupplyRequirementList[0].text = BandageReq[index].ToString();
                SupplyRequirementList[1].text = OintmentReq[index].ToString();
                SupplyRequirementList[2].text = SyringeReq[index].ToString();
                SupplyRequirementList[3].text = InsulinReq[index].ToString();
            }
        }
    }

    public void CallRequestDialogue()
    {
        // Check if CurrentMedtent is within the valid range
        if (CurrentMedTent > 0 && CurrentMedTent <= MedTents.Count)
        {
            // Arrays and lists are zero-indexed, so subtract 1 from CurrentMedtent
            GameObject selectedTent = MedTents[CurrentMedTent - 1];

            // Get the MedTent script from the selected GameObject
            Medtent medTentScript = selectedTent.GetComponent<Medtent>();
            if (medTentScript != null)
            {
                // Call the method on the MedTent script
                medTentScript.SendSupplyRequestToMothership();
            }
            else
            {
                Debug.LogError("CallRequestDialogue: MedTent script not found on the selected GameObject.");
            }
        }
        else
        {
            Debug.LogError("CallRequestDialogue: CurrentMedtent is out of range.");
        }
    }

}