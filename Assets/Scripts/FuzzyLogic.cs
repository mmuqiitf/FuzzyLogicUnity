using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuzzyLogic : MonoBehaviour
{
    public float inputPermintaan, inputPersediaan;

    // Start is called before the first frame update
    void Start()
    {
        Permintaan permintaan = new Permintaan(1000, 5000);
        Persediaan persediaan = new Persediaan(100, 600);
        Produksi produksi = new Produksi(2000, 7000);
        
        List<Rules> RulesList = new List<Rules>();
        RulesList.Add(new Rules(permintaan.LabelTurun, persediaan.LabelBanyak, produksi.LabelBerkurang));


        Debug.Log("Permintaan Naik " + permintaan.mNaik(inputPermintaan));
        Debug.Log("Permintaan Turun " + permintaan.mTurun(inputPermintaan));

        Debug.Log("Persediaan Sedikit " + persediaan.mSedikit(inputPersediaan));
        Debug.Log("Persediaan Banyak " + persediaan.mBanyak(inputPersediaan));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
