using UnityEngine;

public class SwitchMaterialChanger : MonoBehaviour
{
    // Assign this in the inspector
    public Material newMaterial;

    // This function changes the material of the GameObject's MeshRenderer
    public void ChangeMaterial()
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