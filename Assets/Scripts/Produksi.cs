using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Produksi : MonoBehaviour
{
    private float berkurang, bertambah;
    private string labelBerkurang = "Berkurang";
    private string labelBertambah = "Bertambah";

    public Produksi(float berkurang, float bertambah)
    {
        this.berkurang = berkurang;
        this.bertambah = bertambah;
    }

    public float Berkurang
    {
        get { return berkurang; }
        set { berkurang = value; }
    }

    public float Bertambah
    {
        get { return bertambah; }
        set { bertambah = value; }
    }

    public string LabelBerkurang
    {
        get { return labelBerkurang; }
    }

    public string LabelBertambah
    {
        get { return labelBertambah; }
    }
}
