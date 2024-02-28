using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate_Thing : MonoBehaviour
{
    [SerializeField] private GameObject thingToInstantiate;
    [SerializeField] private Transform whereToInstantiate;

    public void InstantiateRespectiveItem()
    {
        //when lever is at max limit (or when button is presesd), spawn item above the conveyor belt

        GameObject newItem = Instantiate(thingToInstantiate, whereToInstantiate);
    }
}
