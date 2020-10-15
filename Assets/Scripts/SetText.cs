using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using redd096;

public class SetText : MonoBehaviour
{
    [SerializeField] float timeBetweenLetters = 0.1f;
    [SerializeField] float waitBeforeResetText = 5;

    TextMeshProUGUI textMeshPro;
    TextMeshProUGUI TextMeshPro { 
        get 
        {
            if (textMeshPro == null)
                textMeshPro = GetComponent<TextMeshProUGUI>();

            return textMeshPro;
        } }

    Coroutine resetText_Coroutine;

    void ResetText()
    {
        //start coroutine
        resetText_Coroutine = StartCoroutine(ResetText_Coroutine());
    }

    IEnumerator ResetText_Coroutine()
    {
        //wait, then reset
        yield return new WaitForSeconds(waitBeforeResetText);

        TextMeshPro.text = "";
    }

    public void Write(string text)
    {
        //stop coroutine reset
        if (resetText_Coroutine != null)
            StopCoroutine(resetText_Coroutine);

        //write letter by letter, not skippable, then call ResetText
        TextMeshPro.WriteLetterByLetter(text, timeBetweenLetters, ResetText, false);
    }
}
