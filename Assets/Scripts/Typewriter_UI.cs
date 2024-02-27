// Prepared by Nick Hwang (https://www.youtube.com/nickhwang)

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Typewriter_UI : MonoBehaviour
{
    TMP_Text _tmpProText;
    string writer;

    [SerializeField] float delayBeforeStart = 0f;
    [SerializeField] float timeBtwChars = 0.1f;
    [SerializeField] string leadingChar = "";
    [SerializeField] bool leadingCharBeforeDelay = false;

    void Start()
    {
        _tmpProText = GetComponent<TMP_Text>()!;

        /*
        if (_tmpProText != null)
        {
            writer = _tmpProText.text;
            _tmpProText.text = "";

            StartCoroutine("TypeWriterTMP");
        }
        */
    }

    public void StartTypewriterView(string AIOutput_Incoming)
    {
        
        StartCoroutine(TypeWriterTMP(AIOutput_Incoming));
    }
    

    IEnumerator TypeWriterTMP(string AIOutput)
    {
        writer = AIOutput;
        _tmpProText.text = leadingCharBeforeDelay ? leadingChar : "";

        yield return new WaitForSeconds(delayBeforeStart);

        foreach (char c in writer)
        {
            if (_tmpProText.text.Length > 0)
            {
                _tmpProText.text = _tmpProText.text.Substring(0, _tmpProText.text.Length - leadingChar.Length);
            }
            _tmpProText.text += c;
            _tmpProText.text += leadingChar;
            yield return new WaitForSeconds(timeBtwChars);
        }

        if (leadingChar != "")
        {
            _tmpProText.text = _tmpProText.text.Substring(0, _tmpProText.text.Length - leadingChar.Length);
        }
    }
    
}