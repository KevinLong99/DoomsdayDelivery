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
    public GameObject cameraScreen2;

    public GameObject cameraToMove;
    public GameObject startGameText;
    public GameObject inTheYearText;

    private Vector3 camEndPos = new Vector3(0, -10f, 2f);

    public Typewriter_UI typeWriteScript;

    private string inTheYearTxt = "In the city of [REDACTED] in the year 2084...\r\n\r\n" +
        "The war has let up enough for aid to be delivered to survivors in desperate need...";

    private float counter, timeDur = 0;

    public Light spotLightThruster;
    public Light spotLight;
    public Light directionalLight;
    public GameObject reflectionProbe;
    public GameObject mainLogo;
    public PlaySounds soundScript;

    void Start()
    {
        //SwitchSceneToGame();
    }

    public void SwitchSceneToGame()
    {
        StartCoroutine(BeginGameCoroutine());
    }

    private IEnumerator BeginGameCoroutine()
    {
        startGameText.SetActive(false);
        yield return new WaitForSeconds(1.0f);

        //lights flicker on 
        
        reflectionProbe.SetActive(true);
        RenderSettings.ambientIntensity = 0.75f;
        RenderSettings.reflectionIntensity = 0.75f;
        yield return new WaitForSeconds(0.05f);

        spotLightThruster.intensity = 0;
        RenderSettings.ambientIntensity = 0.1f;
        RenderSettings.reflectionIntensity = 0.1f;
        yield return new WaitForSeconds(0.05f);

        RenderSettings.ambientIntensity = 0.5f;
        RenderSettings.reflectionIntensity = 0.5f;
        yield return new WaitForSeconds(0.05f);

        spotLight.intensity = 0.7f;
        RenderSettings.ambientIntensity = 0.75f;
        RenderSettings.reflectionIntensity = 0.75f;
        yield return new WaitForSeconds(0.05f);

        RenderSettings.ambientIntensity = 0.5f;
        RenderSettings.reflectionIntensity = 0.5f;
        yield return new WaitForSeconds(0.05f);

        RenderSettings.ambientIntensity = 0.1f;
        RenderSettings.reflectionIntensity = 0.1f;
        yield return new WaitForSeconds(0.05f);

        RenderSettings.ambientIntensity = 0.75f;
        RenderSettings.reflectionIntensity = 0.75f;
        yield return new WaitForSeconds(0.05f);

        directionalLight.intensity = 1.5f;
        RenderSettings.ambientIntensity = 0.1f;
        RenderSettings.reflectionIntensity = 0.1f;
        yield return new WaitForSeconds(0.05f);

        RenderSettings.ambientIntensity = 0.75f;
        RenderSettings.reflectionIntensity = 0.75f;
        yield return new WaitForSeconds(1.5f);

        soundScript.PlayErorr();    //NOT AN ERROR NOISE: replace with ship flying audio

        //main screen turns on showing outside camera view
        cameraScreen.SetActive(true);
        cameraScreen2.SetActive(true);

        yield return new WaitForSeconds(0.75f);

        //ship takes off, revealing the skyline
        counter = 0;
        timeDur = 6;

        Vector3 posStart = cameraToMove.transform.position;
        while (counter < timeDur)
        {
            cameraToMove.transform.position = Vector3.Lerp(posStart, camEndPos, counter/timeDur);
            counter += Time.deltaTime;
            yield return null;
            if (counter >= 3f && !mainLogo.activeInHierarchy)
                mainLogo.SetActive(true);
        }
        cameraToMove.transform.position = camEndPos;
        yield return new WaitForSeconds(1);

        //fade lights out, including the one over the thruster
        counter = 0;
        timeDur = 3;

        while (counter < timeDur)
        {
            RenderSettings.ambientIntensity = Mathf.Lerp(0.75f, 0.25f, counter/timeDur);
            RenderSettings.reflectionIntensity = Mathf.Lerp(0.75f, 0.25f, counter/timeDur);
            spotLight.intensity = Mathf.Lerp(0.7f, 0f, counter/timeDur);
            directionalLight.intensity = Mathf.Lerp(1.5f, 0f, counter / timeDur);

            counter += Time.deltaTime;
            yield return null;
        }

        cameraScreen2.SetActive(false);
        soundScript.PlayRandomBeeping();
        yield return new WaitForSeconds(0.5f);

        //black screen with white text appears:
        inTheYearText.SetActive(true);
        typeWriteScript.StartTypewriterView(inTheYearTxt);
        

        yield return new WaitForSeconds(6);


        //switch to game scene 
        sceneManagerScript.BeginGame();
        fadeScreenScript.FadeOut();
        //startGameText.SetActive(false);
    }
     
}
