using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private float sedikit, banyak;
    private string labelSedikit = "Sedikit";
    private string labelBanyak = "Banyak";

    public Damage(float sedikit, float banyak)
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

    public float mSedikit(float inputDamage)
    {
        float hasil;
        if (inputDamage <= sedikit)
        {
            hasil = 1;
        }
        else if(inputDamage > sedikit && inputDamage < banyak)
        {
            hasil = (this.banyak - inputDamage) / (this.banyak - this.sedikit);
        }
        else if(inputDamage >= banyak)
        {
            hasil = 0;
        }
        else
        {
            hasil = 0;
        }
        return hasil;
    }

    public float mBanyak(float inputDamage)
    {
        float hasil;
        if (inputDamage <= sedikit)
        {
            hasil = 1;
        }
        else if (inputDamage > sedikit && inputDamage < banyak)
        {
            hasil = (inputDamage - this.sedikit) / (this.banyak - this.sedikit);
        }
        else if (inputDamage >= banyak)
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
