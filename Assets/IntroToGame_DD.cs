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
    public GameObject inTheYearText;

    private Vector3 camEndPos = new Vector3(0, -10f, 2f);


    void Start()
    {
        SwitchSceneToGame();
    }

    public void SwitchSceneToGame()
    {
        StartCoroutine(BeginGameCoroutine());
    }

    private IEnumerator BeginGameCoroutine()
    {
        //lights flicker on 

        yield return new WaitForSeconds(2);

        //main screen turns on showing outside camera view
        cameraScreen.SetActive(true);
        startGameText.SetActive(false);
        yield return new WaitForSeconds(1);

        //ship takes off, revealing the skyline
        float counter = 0;
        float timeDur = 7;

        Vector3 posStart = cameraToMove.transform.position;
        while (counter < timeDur)
        {
            cameraToMove.transform.position = Vector3.Lerp(posStart, camEndPos, counter/timeDur);
            counter += Time.deltaTime;
            yield return null;
        }
        cameraToMove.transform.position = camEndPos;
        yield return new WaitForSeconds(1);

        //fade lights out, including the one over the thruster
        RenderSettings.ambientIntensity = 0.25f;
        RenderSettings.reflectionIntensity = 0.25f;
        yield return new WaitForSeconds(1);

        //black screen with white text appears:
        inTheYearText.SetActive(true);

        yield return new WaitForSeconds(3);

        //switch to game scene 
        //sceneManagerScript.BeginGame();
        //fadeScreenScript.FadeOut();
    }
     
}
