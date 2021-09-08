using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jarak : MonoBehaviour
{
    private float dekat, jauh;
    private string labelDekat = "Dekat";
    private string labelJauh = "Jauh";

    public Jarak(float dekat, float jauh)
    {
        this.dekat = dekat;
        this.jauh = jauh;
    }
    public float Dekat
    {
        get { return dekat; }
        set { dekat = value; }
    }

    public float Jauh
    {
        get { return jauh; }
        set { jauh = value; }
    }

    public string LabelDekat
    {
        get { return labelDekat; }
    }

    public string LabelJauh
    {
        get { return labelJauh; }
    }

    public float mDekat(float inputJarak)
    {
        float hasil;
        if (inputJarak <= dekat)
        {
            hasil = 1;
        }
        else if (inputJarak > dekat && inputJarak < jauh)
        {
            hasil = (this.jauh - inputJarak) / (this.jauh - this.dekat);
        }
        else if (inputJarak >= jauh)
        {
            hasil = 0;
        }
        else
        {
            hasil = 0;
        }
        return hasil;
        
    }

    public float mJauh(float inputJarak)
    {
        float hasil;
        if (inputJarak <= dekat)
        {
            hasil = 1;
        }
        else if (inputJarak > dekat && inputJarak < jauh)
        {
            hasil = (inputJarak - this.dekat) / (this.jauh - this.dekat);
        }
        else if (inputJarak >= jauh)
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
