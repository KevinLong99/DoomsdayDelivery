using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmActivate_DD : MonoBehaviour
{

    private Game_Progression gameProg_Script;
    private Animator alarmAnimator;

    private void Start()
    {
        gameProg_Script = GameObject.Find("Game_Manager").GetComponent<Game_Progression>();
        alarmAnimator = this.gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (gameProg_Script.somethingIsBroken == true)
        {
            alarmAnimator.Play("Alarm_on");
        } else
        {
            alarmAnimator.Play("Alarm_off");
        }
    }
}
