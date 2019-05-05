using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUpdate : MonoBehaviour
{
    string log = "Log:\n";

    public void pushText(string newtext)
    {
        log = log + newtext;
        GetComponent<Text>().text = log;
    }
}
