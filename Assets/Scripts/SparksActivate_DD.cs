using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparksActivate_DD : MonoBehaviour
{
    private Game_Progression gameProg_Script;
    private ParticleSystem partSys;

    private bool playIsReady = true;

    private void Start()
    {
        gameProg_Script = GameObject.Find("Game_Manager").GetComponent<Game_Progression>();
        partSys = this.gameObject.GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (gameProg_Script.needToFixShip == true)
        {
            if (playIsReady)
            {
                playIsReady = false;
                StartCoroutine(WaitSomeTime());
            }
        }
    }

    private IEnumerator WaitSomeTime()
    {
        partSys.Play();
        yield return new WaitForSeconds(Random.Range(1.5f, 4.0f));
        playIsReady = true;

    }
}
