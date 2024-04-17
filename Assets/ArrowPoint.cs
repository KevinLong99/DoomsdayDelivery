using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPoint : MonoBehaviour
{
    public ProgressManager myProgressManager; // Reference to the ProgressManager GameObject
    public int myCurrentMedTent; // Stores the current Med Tent number
    public List<GameObject> MedTentList; // List of Med Tent GameObjects
    public GameObject target; // Current target GameObject

    void Update()
    {
        // Update myCurrentMedTent to match the value from ProgressManager
        if (myProgressManager != null)
        {
            myCurrentMedTent = myProgressManager.CurrentMedTent;
        }

        // Assign the target based on myCurrentMedTent value
        if (MedTentList != null && MedTentList.Count > 0)
        {
            switch (myCurrentMedTent)
            {
                case 1:
                    target = MedTentList.Count >= 1 ? MedTentList[0] : null;
                    break;
                case 2:
                    target = MedTentList.Count >= 2 ? MedTentList[1] : null;
                    break;
                case 3:
                    target = MedTentList.Count >= 3 ? MedTentList[2] : null;
                    break;
                    // Add additional cases if there are more than three Med Tents
            }
        }

        // Continue with the existing logic to rotate the arrow towards the target
        if (target != null)
        {
            Vector3 targetDirection = target.transform.position - transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, 2 * Mathf.PI, 0.0f);
            Debug.DrawRay(transform.position, newDirection, Color.red);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }
}