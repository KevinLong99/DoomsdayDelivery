using UnityEngine;

public class Floater : MonoBehaviour
{
    public float floatingSpeed = 0.5f;
    public float floatingRange = 0.5f; // This is the new public float to set the range of the floating
    private float originalY;

    private void Start()
    {
        // Store the starting position
        originalY = transform.position.y;
    }

    private void Update()
    {
        // Calculate the new Y position using the floatingRange for extent of float
        float newY = originalY + Mathf.Sin(Time.time * floatingSpeed) * floatingRange;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    // Call this function to delete the entire GameObject
    public void DeleteGameObject()
    {
        Destroy(gameObject);
    }
}
