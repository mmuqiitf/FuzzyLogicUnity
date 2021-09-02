using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rules : MonoBehaviour
{
    private string rules1, rules2, output;

    public Rules(string rules1, string rules2, string output)
    {
        this.rules1 = rules1;
        this.rules2 = rules2;
        this.output = output;
    }

    public string Rules1
    {
        get { return rules1; }
        set { rules1 = value; }
    }
    public string Rules2
    {
        get { return rules2; }
        set { rules2 = value; }
    }

    public string Output
    {
        get { return output; }
        set { output = value; }
    }

}
