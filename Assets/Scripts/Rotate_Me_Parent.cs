using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Me_Parent : MonoBehaviour
{

    [SerializeField]
    private int rotValue;

    private Quaternion _targetRot;

    private void Start()
    {
        _targetRot = this.transform.rotation;
    }
    void Update()
    {
        
        switch (rotValue)
        {
            case 0:
                _targetRot = Quaternion.AngleAxis(-90, transform.forward); 
                break;

            case 1:
                _targetRot = Quaternion.AngleAxis(90, transform.forward);
                break;
        }


    }
}
