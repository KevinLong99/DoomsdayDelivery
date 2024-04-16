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

    public Typewriter_UI typeWriteScript;

    private string inTheYearTxt = "In the city of [REDACTED] in the year 2084...\r\n\r\n" +
        "The war has let up enough for aid to be delivered to survivors in desperate need...";

    private float counter, timeDur = 0;

    public Light spotLightThruster;

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
        counter = 0;
        timeDur = 3;

        while (counter < timeDur)
        {
            RenderSettings.ambientIntensity = Mathf.Lerp(0.1f, 0.75f, counter / timeDur);
            RenderSettings.reflectionIntensity = Mathf.Lerp(0.1f, 0.75f, counter / timeDur);
            spotLightThruster.intensity = Mathf.Lerp(1, 0, counter/timeDur);
            counter += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(2);

        //main screen turns on showing outside camera view
        cameraScreen.SetActive(true);
        startGameText.SetActive(false);
        yield return new WaitForSeconds(1);

        //ship takes off, revealing the skyline
        counter = 0;
        timeDur = 1;  //FIXME change to 7

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
        counter = 0;
        timeDur = 3;

        while (counter < timeDur)
        {
            RenderSettings.ambientIntensity = Mathf.Lerp(0.75f, 0.25f, counter/timeDur);
            RenderSettings.reflectionIntensity = Mathf.Lerp(0.75f, 0.25f, counter / timeDur);
            
            counter += Time.deltaTime;
            yield return null;
        }
        //RenderSettings.ambientIntensity = 0.25f;
        //RenderSettings.reflectionIntensity = 0.25f;
        //startGameText.SetActive(true);

        //black screen with white text appears:
        inTheYearText.SetActive(true);
        typeWriteScript.StartTypewriterView(inTheYearTxt);

        yield return new WaitForSeconds(3);


        //switch to game scene 
        //sceneManagerScript.BeginGame();
        //fadeScreenScript.FadeOut();
        //startGameText.SetActive(false);
    }
     
}
