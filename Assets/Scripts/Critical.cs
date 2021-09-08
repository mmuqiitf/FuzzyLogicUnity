using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Critical : MonoBehaviour
{
    private float tidak, iya;
    private string labelTidak = "Tidak";
    private string labelIya = "Iya";

    public Critical(float tidak, float iya)
    {
        this.tidak = tidak;
        this.iya = iya;
    }

    public float Tidak
    {
        get { return tidak; }
        set { tidak = value; }
    }

    public float Iya
    {
        get { return iya; }
        set { iya = value; }
    }

    public string LabelTidak
    {
        get { return labelTidak; }
    }

    public string LabelIya
    {
        get { return labelIya; }
    }
}
