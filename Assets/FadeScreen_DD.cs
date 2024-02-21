using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreen_DD : MonoBehaviour
{
    [SerializeField] private bool fadeOnStart = true;

    public float fadeDuration = 1.5f;  //time it takes to fade out of scene
    //total time to fade out AND in between scenes is fadeDuration * 2
    public Color fadeColor;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        if (fadeOnStart)
        {
            FadeIn();
        }
    }

    public void FadeIn()
    {
        Fade(1, 0);
    }

    public void FadeOut()
    {

        Fade(0, 1);
    }

    public void Fade(float alphaIn, float alphaOut)
    {
        StartCoroutine(FadeRoutine(alphaIn, alphaOut));
    }

    public IEnumerator FadeRoutine(float alphaIn, float alphaOut)
    {
        float timer = 0;
        while (timer <= fadeDuration)
        {
            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDuration);
            rend.material.SetColor("_BaseColor", newColor);

            timer += Time.deltaTime;
            yield return null;
        }
        Color newColor2 = fadeColor;
        newColor2.a = alphaOut;
        rend.material.SetColor("_BaseColor", newColor2);
    }
}
