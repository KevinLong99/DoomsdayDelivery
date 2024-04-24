using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ProgressManager : MonoBehaviour
{
    public int CurrentMedTent = 1;
    public List<TextMeshPro> SupplyRequirementList;
    public List<int> BandageReq, OintmentReq, SyringeReq, InsulinReq;
    public List<GameObject> MedTents;

    public UnityEvent PlayOrderCountUpdateSFX;

    public void UpdateMedTent()
    {
        CurrentMedTent++;
    }

    public UIManager MyUIManager;
    //Score text color stuff
    // Manually assign these in the Unity editor
    public List<TextMeshPro> SupplyText;
    public List<TextMeshPro> RequirementText;

    // Serialized fields to visualize in the Inspector
    [SerializeField]
    private List<int> SupplyIntegers = new List<int>();
    [SerializeField]
    private List<int> RequirementIntegers = new List<int>();


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

        //Score text color stuff
        // Clear the lists to ensure they contain the latest values each frame
        SupplyIntegers.Clear();
        RequirementIntegers.Clear();

        // Ensure both lists have the same count
        if (SupplyText.Count != RequirementText.Count)
        {
            Debug.LogError("SupplyText and RequirementText lists do not have the same number of elements.");
            return;
        }

        // Convert SupplyText and RequirementText to integers and add to respective lists
        for (int i = 0; i < SupplyText.Count; i++)
        {
            if (int.TryParse(SupplyText[i].text, out int supplyInt))
            {
                SupplyIntegers.Add(supplyInt);
            }
            else
            {
                Debug.LogError($"Supply text at index {i} is not a valid integer: {SupplyText[i].text}");
                // Optionally, add a zero or some default value to maintain list integrity
                SupplyIntegers.Add(0);
            }

            if (int.TryParse(RequirementText[i].text, out int requirementInt))
            {
                RequirementIntegers.Add(requirementInt);
            }
            else
            {
                Debug.LogError($"Requirement text at index {i} is not a valid integer: {RequirementText[i].text}");
                // Optionally, add a zero or some default value to maintain list integrity
                RequirementIntegers.Add(0);
            }
        }

        // Compare the integers in the lists and call the placeholder function if conditions are met
        for (int i = 0; i < SupplyIntegers.Count; i++)
        {
            if (SupplyIntegers[i] >= RequirementIntegers[i])
            {
                // Based on the index, call the corresponding function from MyUIManager
                switch (i)
                {
                    case 0:
                        MyUIManager.ChangeScoreColorBandage(); 
                        break;
                    case 1:
                        MyUIManager.ChangeScoreColorOintment(); 
                        break;
                    case 2:
                        MyUIManager.ChangeScoreColorSyringe();
                        break;
                    case 3:
                        MyUIManager.ChangeScoreColorInsulin();
                        break;
                    default:
                        Debug.LogWarning("Index out of range for specific color change functions");
                        break;
                }
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

    public void ToPlayOrderCountUpdateSFX()
    {
        PlayOrderCountUpdateSFX.Invoke();
    }

}