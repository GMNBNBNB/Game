using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class DiglogueSystem : MonoBehaviour
{
    [SerializeField] Text targetText;
    [SerializeField] Text Name;
    [SerializeField] Image Portrait;

    DialogueContainer currentDialogue;
    int currentDialogueIndex;

    [Range(0f, 1f)]
    [SerializeField] float visibleTextPercent;
    [SerializeField] float timePerLetter = 0.05f;
    float totalTimeToType, currentTime;
    string lineToShow;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PushText();
        }
        TypeOutText();
    }

    private void TypeOutText()
    {
        if(visibleTextPercent >= 1f) { return; }
        currentTime += Time.deltaTime;
        visibleTextPercent = currentTime / totalTimeToType;
        visibleTextPercent = Mathf.Clamp(visibleTextPercent,0,1f);
        UpdateText();
    }

    private void UpdateText()
    {
        int letterCount = (int)(lineToShow.Length * visibleTextPercent);
        targetText.text = lineToShow.Substring(0, letterCount);
    }

    private void PushText()
    {
        if (visibleTextPercent < 1f)
        {
            visibleTextPercent = 1f;
            UpdateText();
            return;
        }

        if(currentDialogueIndex >= currentDialogue.line.Count){
            Conclude();
        }
        else
        {
            CycleLine();         
        }
    }

    private void CycleLine()
    {
        lineToShow = currentDialogue.line[currentDialogueIndex];
        totalTimeToType = lineToShow.Length * timePerLetter;
        currentTime = 0f;
        visibleTextPercent = 0f;
        targetText.text = "";
        currentDialogueIndex++;
    }

    public void Initialize(DialogueContainer dialogueContainer)
    {
        Show(true);
        currentDialogue = dialogueContainer;
        currentDialogueIndex = 0;
        CycleLine();
        UpdatePortrait();
    }

    private void UpdatePortrait()
    {
        Portrait.sprite = currentDialogue.actor.Protrait;
        Name.text = currentDialogue.actor.Name;

    }

    private void Show(bool v)
    {
       gameObject.SetActive(v);
    }

    private void Conclude()
    {
        Debug.Log("end");
        Show(false);
    }
}
