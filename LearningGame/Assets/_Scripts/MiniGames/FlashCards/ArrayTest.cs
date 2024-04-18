using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArrayTest : MonoBehaviour
{

    private void Awake()
    {
        //Subscribe to nextQuestion event.
        GameManager.instance.nextQuestion += ChangeSet;
    }

    // reference to each asnwer box texts
    [SerializeField] TextMeshProUGUI[] text;

    [Serializable]
    public class tests //change name
    {
        public string[] answerss;
    }

    [SerializeField] public tests[] elAnswers; //change name

    // make an event that gets the set of answer
    // needs to reference the buttons texts components
    public void ChangeSet(int x) //change name later
    {
        Debug.Log("ChangeSet called");
        for (int i = 0; i < text.Length; i++)
        {
            text[i].text = elAnswers[x].answerss[i];
        }
    }

}
