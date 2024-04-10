using UnityEngine;

public class LeverFunctionController : MonoBehaviour
{
    public ConfigurableJointListener MyLeverListener;
    public bool LockStation = false;

    private void Update()
    {
        // Ensure MyLeverListener is assigned to avoid null reference exceptions
        if (MyLeverListener != null)
        {
            if (LockStation)
            {
                // Enable MyLeverListener if LockStation is true
                MyLeverListener.enabled = true;
            }
            else
            {
                // Disable MyLeverListener if LockStation is false
                MyLeverListener.enabled = false;
            }
        }
        else
        {
            Debug.LogError("MyLeverListener is not assigned in the inspector.");
        }
    }

    public void DoLockStation()
    {
        LockStation = false;
    }

    public void DoUnlockStation()
    {
        LockStation = true;
    }
}

