using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class IntroToGame_DD : MonoBehaviour
{
    public Scene_Manager_DD sceneManagerScript;
    public FadeScreen_DD fadeScreenScript;

    public GameObject cameraScreen;
    private Vector3 camStartPos = new Vector3(0, -150f, 2f);
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
        yield return new WaitForSeconds(1);

        //ship takes off, revealing the skyline
        yield return new WaitForSeconds(1);
        StartCoroutine(CameraRise());

        //fade to black
        yield return new WaitForSeconds(1);

        //black screen with white text appears:
        //  "in REDACTED city in 208x, the war has let up enough for aid to be provided..."

        yield return new WaitForSeconds(1);

        //fade out to black (if faded back in)

        yield return new WaitForSeconds(1);

        //switch to game scene 
        sceneManagerScript.BeginGame();
        fadeScreenScript.FadeOut();
    }

    private IEnumerator CameraRise()
    {
        float counter = 0;
        while (counter < 5)
        {
            counter += Time.deltaTime;
            cameraScreen.transform.position = Vector3.Lerp(camStartPos, camEndPos, counter / 5);
            yield return null;
        }
    }
     
}
