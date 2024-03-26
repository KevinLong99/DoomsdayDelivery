using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProgressManager : MonoBehaviour
{
    public int CurrentMedTent = 1;
    public List<TextMeshPro> SupplyRequirementList;
    public List<int> BandageReq, OintmentReq, SyringeReq, InsulinReq;

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
}