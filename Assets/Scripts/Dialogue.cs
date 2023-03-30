using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this tutorial used as reference
//https://www.youtube.com/watch?v=_nRzoTzeyxU

[System.Serializable]
public class Dialogue
{
    public string name;

    [TextArea(3, 10)]
    public string[] sentences;
}