using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Permintaan : MonoBehaviour
{
    private float turun, naik;
    private string labelTurun = "Turun";
    private string labelNaik = "Naik";

    public Permintaan(float turun, float naik)
    {
        this.turun = turun;
        this.naik = naik;
    }
    public float Turun
    {
        get { return turun; }
        set { turun = value; }
    }
    
    public float Naik
    {
        get { return naik; }
        set { naik = value; }
    }

    public string LabelTurun
    {
        get { return labelTurun; }
    }

    public string LabelNaik
    {
        get { return labelNaik; }
    }

    public float mTurun(float inputPermintaan)
    {
        float hasil = (this.naik - inputPermintaan) / (this.naik - this.turun);
        return hasil;
    }

    public float mNaik(float inputPermintaan)
    {
        float hasil = (inputPermintaan - this.turun) / (this.naik - this.turun);
        return hasil;
    }
}
