using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsPlayer : MonoBehaviour
{
    public GameObject[] names;
    private int numNames;
    private int nameCounter;

    void Start()
    {
        numNames = names.Length - 1;
        nameCounter = 0;
        StartCoroutine(SwitchNames());
    }

    IEnumerator SwitchNames()
    {
        while (true)
        {
            yield return new WaitForSeconds(4.75f);

            names[nameCounter].SetActive(false);

            nameCounter++;
            if (nameCounter > numNames)
                nameCounter = 0;

            names[nameCounter].SetActive(true);
        }
    }
}
