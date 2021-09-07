using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public static class ForEachExtensions
{
    public static void ForEachWithIndex<T>(this IEnumerable<T> enumerable, System.Action<T, int> handler)
    {
        int idx = 0;
        foreach (T item in enumerable)
            handler(item, idx++);
    }
}


public class FuzzyLogic : MonoBehaviour
{
    public float inputPermintaan, inputPersediaan;
    public List<float> fuzzyNumberBertambah;
    public List<float> fuzzyNumberBerkurang;
    public float permintaanTurun;
    public float permintaanNaik;
    public float persediaanSedikit;
    public float persediaanBanyak;
    public float produksiKurang;
    public float produksiTambah;
    public List<float> defuzzifikasiBertambah;
    public List<float> defuzzifikasiBerkurang;
    public float centroid;
    public TabelRules tabelRules;
    public GameObject fuzzifikasi;
    public GameObject operasiFuzzy;

    private float mNaik, mTurun, mSedikit, mBanyak;

    [System.Serializable]
    public static class KomposisiAturan
    {
        public static float bertambah, berkurang;
    }

    // Start is called before the first frame update
    void Start()
    {
        Permintaan permintaan = new Permintaan(permintaanTurun, permintaanNaik);
        Persediaan persediaan = new Persediaan(persediaanSedikit, persediaanBanyak);
        Produksi produksi = new Produksi(produksiKurang, produksiTambah);
        
        List<Rules> RulesList = new List<Rules>();
        RulesList.Add(new Rules(permintaan.LabelTurun, persediaan.LabelBanyak, produksi.LabelBerkurang));
        RulesList.Add(new Rules(permintaan.LabelTurun, persediaan.LabelSedikit, produksi.LabelBerkurang));
        RulesList.Add(new Rules(permintaan.LabelNaik, persediaan.LabelBanyak, produksi.LabelBertambah));
        RulesList.Add(new Rules(permintaan.LabelNaik, persediaan.LabelSedikit, produksi.LabelBertambah));

        tabelRules.CallRules(RulesList);

        mNaik = permintaan.mNaik(inputPermintaan);
        mTurun = permintaan.mTurun(inputPermintaan);
        mSedikit = persediaan.mSedikit(inputPersediaan);
        mBanyak = persediaan.mBanyak(inputPersediaan);

        Fuzzifikasi(fuzzifikasi);

        Debug.Log("Permintaan Naik " + permintaan.mNaik(inputPermintaan));
        Debug.Log("Permintaan Turun " + permintaan.mTurun(inputPermintaan));

        Debug.Log("Persediaan Sedikit " + persediaan.mSedikit(inputPersediaan));
        Debug.Log("Persediaan Banyak " + persediaan.mBanyak(inputPersediaan));

        OperasiFuzzy(RulesList, operasiFuzzy);
        KomposisiAturan.berkurang = Mathf.Max(fuzzyNumberBerkurang.ToArray());
        KomposisiAturan.bertambah = Mathf.Max(fuzzyNumberBertambah.ToArray());

        Debug.Log("komposisi aturan berkurang " + KomposisiAturan.berkurang);
        Debug.Log("komposisi aturan bertambah " + KomposisiAturan.bertambah);
        int iteration = 5;
        for (int i = 1; i <= iteration ; i++)
        {
            float random1 = Random.Range(produksiKurang, produksiTambah);
            float random2 = Random.Range(produksiKurang, produksiTambah);
            defuzzifikasiBertambah.Add(random1);
            defuzzifikasiBerkurang.Add(random2);
        }
        centroid = ((defuzzifikasiBertambah.Sum() * KomposisiAturan.bertambah) + (defuzzifikasiBerkurang.Sum() * KomposisiAturan.berkurang)) / 
            ( (KomposisiAturan.bertambah * defuzzifikasiBertambah.Count) + (KomposisiAturan.berkurang * defuzzifikasiBerkurang.Count) );
        Debug.LogFormat("Deff Tambah : {0} & {1}", defuzzifikasiBertambah.Sum(), defuzzifikasiBertambah.Count);
        Debug.LogFormat("Deff Kurang : {0} & {1}", defuzzifikasiBerkurang.Sum(), defuzzifikasiBerkurang.Count);

        #region foreach_version
        //foreach (var rules in RulesList)
        //{
        //    if (rules.Rules1.Equals("Turun") && rules.Rules2.Equals("Banyak"))
        //    {
        //        Debug.Log("Min : " + Mathf.Min(mTurun, mBanyak));
        //    }
        //    else if(rules.Rules1.Equals("Turun") && rules.Rules2.Equals("Sedikit"))
        //    {
        //        Debug.Log("Min : " + Mathf.Min(mTurun, mSedikit));
        //    }
        //    else if(rules.Rules1.Equals("Naik") && rules.Rules2.Equals("Banyak"))
        //    {
        //        Debug.Log("Min : " + Mathf.Min(mNaik, mBanyak));
        //    }
        //    else if (rules.Rules1.Equals("Naik") && rules.Rules2.Equals("Sedikit"))
        //    {
        //        Debug.Log("Min : " + Mathf.Min(mNaik, mSedikit));
        //    }
        //}
        #endregion
    }

    // Update is called once per frame
    void Fuzzifikasi(GameObject fuzzifikasi)
    {
        Transform permintaanContainer = fuzzifikasi.transform.Find("PermintaanContainer");
        Transform persediaanContainer = fuzzifikasi.transform.Find("PersediaanContainer");
        Transform uTurun = permintaanContainer.Find("uPermintaanTurun").Find("Value");
        Transform uNaik = permintaanContainer.Find("uPermintaanNaik").Find("Value");
        Transform uSedikit = persediaanContainer.Find("uPersediaanSedikit").Find("Value");
        Transform uBanyak = persediaanContainer.Find("uPersediaanBanyak").Find("Value");
        uTurun.GetComponent<TMP_Text>().text = mTurun.ToString();
        uNaik.GetComponent<TMP_Text>().text = mNaik.ToString();
        uSedikit.GetComponent<TMP_Text>().text = mSedikit.ToString();
        uBanyak.GetComponent<TMP_Text>().text = mBanyak.ToString();
    }

    void OperasiFuzzy(List<Rules> RulesList, GameObject operasiFuzzy)
    {
        //RulesList.ForEachWithIndex((rules, index) => {
        //    if (rules.Rules1.Equals("Turun") && rules.Rules2.Equals("Banyak"))
        //    {
        //        Debug.LogFormat("Rules {1} : Min {0} : {2}", Mathf.Min(mTurun, mBanyak), (index + 1), rules.Output);
        //        if (rules.Output.Equals("Bertambah"))
        //        {
        //            fuzzyNumberBertambah.Add(Mathf.Min(mTurun, mBanyak));
        //        }
        //        else if (rules.Output.Equals("Berkurang"))
        //        {
        //            fuzzyNumberBerkurang.Add(Mathf.Min(mTurun, mBanyak));
        //        }
        //    }
        //    else if (rules.Rules1.Equals("Turun") && rules.Rules2.Equals("Sedikit"))
        //    {
        //        Debug.LogFormat("Rules {1} : Min {0} : {2}", Mathf.Min(mTurun, mSedikit), (index + 1), rules.Output);
        //        if (rules.Output.Equals("Bertambah"))
        //        {
        //            fuzzyNumberBertambah.Add(Mathf.Min(mTurun, mSedikit));
        //        }
        //        else if (rules.Output.Equals("Berkurang"))
        //        {
        //            fuzzyNumberBerkurang.Add(Mathf.Min(mTurun, mSedikit));
        //        }
        //    }
        //    else if (rules.Rules1.Equals("Naik") && rules.Rules2.Equals("Banyak"))
        //    {
        //        Debug.LogFormat("Rules {1} : Min {0} : {2}", Mathf.Min(mNaik, mBanyak), (index + 1), rules.Output);
        //        if (rules.Output.Equals("Bertambah"))
        //        {
        //            fuzzyNumberBertambah.Add(Mathf.Min(mNaik, mBanyak));
        //        }
        //        else if (rules.Output.Equals("Berkurang"))
        //        {
        //            fuzzyNumberBerkurang.Add(Mathf.Min(mNaik, mBanyak));
        //        }
        //    }
        //    else if (rules.Rules1.Equals("Naik") && rules.Rules2.Equals("Sedikit"))
        //    {
        //        Debug.LogFormat("Rules {1} : Min {0} : {2}", Mathf.Min(mNaik, mSedikit), (index + 1), rules.Output);
        //        if (rules.Output.Equals("Bertambah"))
        //        {
        //            fuzzyNumberBertambah.Add(Mathf.Min(mNaik, mSedikit));
        //        }
        //        else if (rules.Output.Equals("Berkurang"))
        //        {
        //            fuzzyNumberBerkurang.Add(Mathf.Min(mNaik, mSedikit));
        //        }
        //    }
        //});

        Transform operasiFuzzyContainer = operasiFuzzy.transform.Find("OperasiFuzzyContainer");
        operasiFuzzyContainer.gameObject.SetActive(false);
        float heightSeperator = 30f;
        RulesList.ForEachWithIndex((rules, index) =>
        {
            Transform cloneTransform = Instantiate(operasiFuzzyContainer, operasiFuzzy.transform);
            RectTransform entryRectTransform = cloneTransform.GetComponent<RectTransform>();
            
            
            if (rules.Rules1.Equals("Turun") && rules.Rules2.Equals("Banyak"))
            {
                Debug.LogFormat("Rules {1} : Min {0} : {2}", Mathf.Min(mTurun, mBanyak), (index + 1), rules.Output);
                cloneTransform.Find("OperasiFuzzyText").GetComponent<TMP_Text>().text = "[R" + index + "] " + Mathf.Min(mTurun, mBanyak) +  " " + rules.Output;
                if (rules.Output.Equals("Bertambah"))
                {
                    fuzzyNumberBertambah.Add(Mathf.Min(mTurun, mBanyak));
                }
                else if (rules.Output.Equals("Berkurang"))
                {
                    fuzzyNumberBerkurang.Add(Mathf.Min(mTurun, mBanyak));
                }
            }
            else if (rules.Rules1.Equals("Turun") && rules.Rules2.Equals("Sedikit"))
            {
                Debug.LogFormat("Rules {1} : Min {0} : {2}", Mathf.Min(mTurun, mSedikit), (index + 1), rules.Output);
                cloneTransform.Find("OperasiFuzzyText").GetComponent<TMP_Text>().text = "[R" + index + "] " + Mathf.Min(mTurun, mSedikit) + " " + rules.Output;
                if (rules.Output.Equals("Bertambah"))
                {
                    fuzzyNumberBertambah.Add(Mathf.Min(mTurun, mSedikit));
                }
                else if (rules.Output.Equals("Berkurang"))
                {
                    fuzzyNumberBerkurang.Add(Mathf.Min(mTurun, mSedikit));
                }
            }
            else if (rules.Rules1.Equals("Naik") && rules.Rules2.Equals("Banyak"))
            {
                Debug.LogFormat("Rules {1} : Min {0} : {2}", Mathf.Min(mNaik, mBanyak), (index + 1), rules.Output); 
                cloneTransform.Find("OperasiFuzzyText").GetComponent<TMP_Text>().text = "[R" + index + "] " + Mathf.Min(mNaik, mBanyak) + " " + rules.Output;
                if (rules.Output.Equals("Bertambah"))
                {
                    fuzzyNumberBertambah.Add(Mathf.Min(mNaik, mBanyak));
                }
                else if (rules.Output.Equals("Berkurang"))
                {
                    fuzzyNumberBerkurang.Add(Mathf.Min(mNaik, mBanyak));
                }
            }
            else if (rules.Rules1.Equals("Naik") && rules.Rules2.Equals("Sedikit"))
            {
                Debug.LogFormat("Rules {1} : Min {0} : {2}", Mathf.Min(mNaik, mSedikit), (index + 1), rules.Output);
                cloneTransform.Find("OperasiFuzzyText").GetComponent<TMP_Text>().text = "[R" + index + "] " + Mathf.Min(mNaik, mSedikit) + " " + rules.Output;
                if (rules.Output.Equals("Bertambah"))
                {
                    fuzzyNumberBertambah.Add(Mathf.Min(mNaik, mSedikit));
                }
                else if (rules.Output.Equals("Berkurang"))
                {
                    fuzzyNumberBerkurang.Add(Mathf.Min(mNaik, mSedikit));
                }
            }
            entryRectTransform.anchoredPosition = new Vector2(0, operasiFuzzyContainer.transform.localPosition.y + (-heightSeperator * index));
            cloneTransform.gameObject.SetActive(true);
        });
    }
}
