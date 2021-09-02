using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persediaan : MonoBehaviour
{
    private float sedikit, banyak;
    private string labelSedikit = "Sedikit";
    private string labelBanyak = "Banyak";

    public Persediaan(float sedikit, float banyak)
    {
        this.sedikit = sedikit;
        this.banyak = banyak;
    }
    public float Sedikit
    {
        get { return sedikit; }
        set { sedikit = value; }
    }

    public float Banyak
    {
        get { return banyak; }
        set { banyak = value; }
    }

    public string LabelSedikit
    {
        get { return labelSedikit; }
    }

    public string LabelBanyak
    {
        get { return labelBanyak; }
    }

    public float mSedikit(float inputPersediaan)
    {
        float hasil;
        if (inputPersediaan <= sedikit)
        {
            hasil = 1;
        }
        else if (inputPersediaan > sedikit && inputPersediaan < banyak)
        {
            hasil = (this.banyak - inputPersediaan) / (this.banyak - this.sedikit);
        }
        else if (inputPersediaan >= banyak)
        {
            hasil = 0;
        }
        else
        {
            hasil = 0;
        }
        return hasil;
        
    }

    public float mBanyak(float inputPersediaan)
    {
        float hasil;
        if (inputPersediaan <= sedikit)
        {
            hasil = 1;
        }
        else if (inputPersediaan > sedikit && inputPersediaan < banyak)
        {
            hasil = (inputPersediaan - this.sedikit) / (this.banyak - this.sedikit);
        }
        else if (inputPersediaan >= banyak)
        {
            hasil = 0;
        }
        else
        {
            hasil = 0;
        }
        return hasil;
    }
}
