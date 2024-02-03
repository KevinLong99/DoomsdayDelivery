using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate_Thing : MonoBehaviour
{
    public GameObject thingToInstantiate;
    public Transform whereToInstantiate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiateBalls()
    {
        //when lever is at max limit, spawn balls above the conveyor belt

        GameObject newBall = Instantiate(thingToInstantiate, whereToInstantiate);
    }
}
