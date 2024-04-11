using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class IntroToGame_DD : MonoBehaviour
{
    public Scene_Manager_DD sceneManagerScript;
    public FadeScreen_DD fadeScreenScript;

    public GameObject cameraScreen;
    public GameObject cameraToMove;
    public GameObject startGameText;

    private Vector3 camEndPos = new Vector3(0, -10f, 2f);


    void Start()
    {
        
    }

    public void SwitchSceneToGame()
    {
        StartCoroutine(BeginGameCoroutine());
    }

    private IEnumerator BeginGameCoroutine()
    {
        //lights flicker on 

        yield return new WaitForSeconds(1);

        //main screen turns on showing outside camera view
        cameraScreen.SetActive(true);
        startGameText.SetActive(false);
        yield return new WaitForSeconds(1);

        //ship takes off, revealing the skyline
        float counter = 0;
        float timeDur = 50f;
        while (counter < timeDur)
        {
            counter += Time.deltaTime;
            cameraToMove.transform.position = Vector3.Lerp(cameraToMove.transform.position, camEndPos, counter / timeDur);
            yield return null;
        }
        cameraToMove.transform.position = camEndPos;
        yield return new WaitForSeconds(1);

        //fade to black
        yield return new WaitForSeconds(1);

        //black screen with white text appears:
        //  "in REDACTED city in 208x, the war has let up enough for aid to be provided..."

        yield return new WaitForSeconds(1);

        //fade out to black (if faded back in)

        yield return new WaitForSeconds(1);

        //switch to game scene 
        //sceneManagerScript.BeginGame();
        //fadeScreenScript.FadeOut();
    }
     
}
