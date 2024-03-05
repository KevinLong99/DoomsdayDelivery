using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone_InsideShip : MonoBehaviour
{

    private Vector3 preDrop;
    private Vector3 attachPoint;
    private Vector3 deployDrop;

    private void Start()
    {
        preDrop = GameObject.Find("PreDrop_(drone)").transform.position;
        attachPoint = GameObject.Find("AttachPoint_(drone)").transform.position;
        deployDrop = GameObject.Find("DeployDrop_(drone)").transform.position;
    }

    public void DropInStation3()
    {
        StartCoroutine(DroneMoveCoroutine(preDrop, attachPoint, 1f));
    }

    public void DroneDeploy()
    {
        StartCoroutine(DroneMoveCoroutine(attachPoint, deployDrop, 1f));
    }

    IEnumerator DroneMoveCoroutine(Vector3 from, Vector3 to, float time)
    {
        float duration = time;

        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            this.transform.position = Vector3.Lerp(from, to, counter / duration);
            yield return null;
        }
    }
}
