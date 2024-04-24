using UnityEngine;
using System.Collections;
public class LeverFunctionController : MonoBehaviour
{
    public ConfigurableJointListener MyLeverListener;
    public bool LockStation = false;
    public int delayTime;
    public bool malfunctionFixed;

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
    //Delay Lock Station
    public void DelayDoLockStation()
    {
        StartCoroutine(ToDelayLockStation(1.0f));
    }

    public void DoLockStation()
    {
        LockStation = false;
    }

    public void DoUnlockStation()
    {
        if (malfunctionFixed == true)
        {
            LockStation = true;
        }
        else
        {
            Debug.Log("Need to fix malfunctions first");
        }
    }

    public void DelayDoUnlockStation()
    {
        StartCoroutine(DelayDoUnlockStationEnum(delayTime));
    }

    IEnumerator DelayDoUnlockStationEnum(float delayInSeconds)
    {
        // Wait for the specified number of seconds
        yield return new WaitForSeconds(delayInSeconds);

        // Perform the action after the delay
        LockStation = true;
    }

    //Malfunction fixed bool update functions

    public void MalfunctionIsFixedToFalse()
    {
        malfunctionFixed = false;
    }

    public void MalfunctionIsFixedToTrue()
    {
        malfunctionFixed = true;
    }

    IEnumerator ToDelayLockStation(float delayInSeconds)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delayInSeconds);

        // Call the function after the delay
        DoLockStation();
    }

}

