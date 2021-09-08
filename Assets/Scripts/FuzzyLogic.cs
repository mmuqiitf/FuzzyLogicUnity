using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;

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
    //public float inputDamage, inputJarak;
    [SerializeField] private InputField inputDamage, inputJarak;
    public List<float> fuzzyNumberIya;
    public List<float> fuzzyNumberTidak;
    public float damageSedikit;
    public float damageBanyak;
    public float jarakDekat;
    public float jarakJauh;
    public float criticalTidak;
    public float criticalIya;
    public float hasil;
    public TabelRules tabelRules;
    public GameObject fuzzifikasi;
    public GameObject operasiFuzzy;
    public GameObject komposisiAturan;
    public GameObject hasilDefuzzifikasi;
    private Transform MenuSetting;

    private float mBanyak, mSedikit, mDekat, mJauh;

    [System.Serializable]
    public static class KomposisiAturan
    {
        public static float iya, tidak;
    }

    void CallFuzzy()
    {
        Transform inputDataDamage = MenuSetting.Find("InputDataDamage");
        Transform inputDataJarak = MenuSetting.Find("InputDataJarak");
        Transform inputDataCritical = MenuSetting.Find("InputDataCritical");
        damageSedikit = float.Parse(inputDataDamage.Find("DamageSedikit").GetComponent<InputField>().text);
        damageBanyak = float.Parse(inputDataDamage.Find("DamageBanyak").GetComponent<InputField>().text);
        jarakDekat = float.Parse(inputDataJarak.Find("JarakDekat").GetComponent<InputField>().text);
        jarakJauh = float.Parse(inputDataJarak.Find("JarakJauh").GetComponent<InputField>().text);
        criticalTidak = float.Parse(inputDataCritical.Find("CriticalTidak").GetComponent<InputField>().text);
        criticalIya = float.Parse(inputDataCritical.Find("CriticalIya").GetComponent<InputField>().text);

        Damage damage = new Damage(damageSedikit, damageBanyak);
        Jarak jarak = new Jarak(jarakDekat, jarakJauh);
        Critical critical = new Critical(criticalTidak, criticalIya);

        List<Rules> RulesList = new List<Rules>();
        RulesList.Add(new Rules(damage.LabelSedikit, jarak.LabelJauh, critical.LabelTidak));
        RulesList.Add(new Rules(damage.LabelSedikit, jarak.LabelDekat, critical.LabelIya));
        RulesList.Add(new Rules(damage.LabelBanyak, jarak.LabelJauh, critical.LabelTidak));
        RulesList.Add(new Rules(damage.LabelBanyak, jarak.LabelDekat, critical.LabelIya));

        tabelRules.CallRules(RulesList);

        mBanyak = damage.mBanyak(float.Parse(inputDamage.text));
        mSedikit = damage.mSedikit(float.Parse(inputDamage.text));
        mDekat = jarak.mDekat(float.Parse(inputJarak.text));
        mJauh = jarak.mJauh(float.Parse(inputJarak.text));
        Debug.Log("mSedikit  " + mSedikit);
        Debug.Log("mJauh  " + mJauh);

        Fuzzifikasi(fuzzifikasi);

        Debug.Log("Damage Banyak " + damage.mBanyak(float.Parse(inputDamage.text)));
        Debug.Log("Damage Sedikit " + damage.mSedikit(float.Parse(inputDamage.text)));

        Debug.Log("Jarak Dekat " + jarak.mDekat(float.Parse(inputJarak.text)));
        Debug.Log("Jarak Jauh " + jarak.mJauh(float.Parse(inputJarak.text)));

        OperasiFuzzy(RulesList, operasiFuzzy);
        KomposisiAturan.tidak = Mathf.Max(fuzzyNumberTidak.ToArray());
        KomposisiAturan.iya = Mathf.Max(fuzzyNumberIya.ToArray());
        SetKomposisiAturan(komposisiAturan);

        Debug.Log("komposisi aturan tidak " + KomposisiAturan.tidak);
        Debug.Log("komposisi aturan iya " + KomposisiAturan.iya);
        
        Defuzzifikasi(hasilDefuzzifikasi);
        #region comment
        //hasil = ((defuzzifikasiIya.Sum() * KomposisiAturan.iya) + (defuzzifikasiTidak.Sum() * KomposisiAturan.tidak)) /
        //    ((KomposisiAturan.iya * defuzzifikasiIya.Count) + (KomposisiAturan.tidak * defuzzifikasiTidak.Count));
        //Debug.LogFormat("Deff Tambah : {0} & {1}", defuzzifikasiIya.Sum(), defuzzifikasiIya.Count);
        //Debug.LogFormat("Deff Kurang : {0} & {1}", defuzzifikasiTidak.Sum(), defuzzifikasiTidak.Count);
        #endregion
    }

    // Start is called before the first frame update
    void Start()
    {
        //inputDamage.text = 4500f.ToString();
        //inputJarak.text = 100f.ToString();
        MenuSetting = GameObject.Find("Canvas").transform.Find("MenuSetting");

        //CallFuzzy();

        #region foreach_version
        //foreach (var rules in RulesList)
        //{
        //    if (rules.Rules1.Equals("Turun") && rules.Rules2.Equals("Banyak"))
        //    {
        //        Debug.Log("Min : " + Mathf.Min(mSedikit, mJauh));
        //    }
        //    else if(rules.Rules1.Equals("Turun") && rules.Rules2.Equals("Sedikit"))
        //    {
        //        Debug.Log("Min : " + Mathf.Min(mSedikit, mDekat));
        //    }
        //    else if(rules.Rules1.Equals("Naik") && rules.Rules2.Equals("Banyak"))
        //    {
        //        Debug.Log("Min : " + Mathf.Min(mBanyak, mJauh));
        //    }
        //    else if (rules.Rules1.Equals("Naik") && rules.Rules2.Equals("Sedikit"))
        //    {
        //        Debug.Log("Min : " + Mathf.Min(mBanyak, mDekat));
        //    }
        //}
        #endregion
    }

    public void SubmitPrediksi()
    {
        CallFuzzy();
    }

    // Update is called once per frame
    void Fuzzifikasi(GameObject fuzzifikasi)
    {
        Transform damageContainer = fuzzifikasi.transform.Find("DamageContainer");
        Transform jarakContainer = fuzzifikasi.transform.Find("JarakContainer");
        Transform uSedikit = damageContainer.Find("uDamageSedikit").Find("Value");
        Transform uBanyak = damageContainer.Find("uDamageBanyak").Find("Value");
        Transform uDekat = jarakContainer.Find("uJarakDekat").Find("Value");
        Transform uJauh = jarakContainer.Find("uJarakJauh").Find("Value");
        uSedikit.GetComponent<TMP_Text>().text = mSedikit.ToString();
        uBanyak.GetComponent<TMP_Text>().text = mBanyak.ToString();
        uDekat.GetComponent<TMP_Text>().text = mDekat.ToString();
        uJauh.GetComponent<TMP_Text>().text = mJauh.ToString();
    }

    void OperasiFuzzy(List<Rules> RulesList, GameObject operasiFuzzy)
    {
        GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag("CLONE");
        foreach (GameObject go in objectsToDestroy)
        {
            Destroy(go);
        }
        fuzzyNumberIya.Clear();
        fuzzyNumberTidak.Clear();
        #region operasi_fuzzy_log
        //RulesList.ForEachWithIndex((rules, index) => {
        //    if (rules.Rules1.Equals("Turun") && rules.Rules2.Equals("Banyak"))
        //    {
        //        Debug.LogFormat("Rules {1} : Min {0} : {2}", Mathf.Min(mSedikit, mJauh), (index + 1), rules.Output);
        //        if (rules.Output.Equals("Iya"))
        //        {
        //            fuzzyNumberIya.Add(Mathf.Min(mSedikit, mJauh));
        //        }
        //        else if (rules.Output.Equals("Tidak"))
        //        {
        //            fuzzyNumberTidak.Add(Mathf.Min(mSedikit, mJauh));
        //        }
        //    }
        //    else if (rules.Rules1.Equals("Turun") && rules.Rules2.Equals("Sedikit"))
        //    {
        //        Debug.LogFormat("Rules {1} : Min {0} : {2}", Mathf.Min(mSedikit, mDekat), (index + 1), rules.Output);
        //        if (rules.Output.Equals("Iya"))
        //        {
        //            fuzzyNumberIya.Add(Mathf.Min(mSedikit, mDekat));
        //        }
        //        else if (rules.Output.Equals("Tidak"))
        //        {
        //            fuzzyNumberTidak.Add(Mathf.Min(mSedikit, mDekat));
        //        }
        //    }
        //    else if (rules.Rules1.Equals("Naik") && rules.Rules2.Equals("Banyak"))
        //    {
        //        Debug.LogFormat("Rules {1} : Min {0} : {2}", Mathf.Min(mBanyak, mJauh), (index + 1), rules.Output);
        //        if (rules.Output.Equals("Iya"))
        //        {
        //            fuzzyNumberIya.Add(Mathf.Min(mBanyak, mJauh));
        //        }
        //        else if (rules.Output.Equals("Tidak"))
        //        {
        //            fuzzyNumberTidak.Add(Mathf.Min(mBanyak, mJauh));
        //        }
        //    }
        //    else if (rules.Rules1.Equals("Naik") && rules.Rules2.Equals("Sedikit"))
        //    {
        //        Debug.LogFormat("Rules {1} : Min {0} : {2}", Mathf.Min(mBanyak, mDekat), (index + 1), rules.Output);
        //        if (rules.Output.Equals("Iya"))
        //        {
        //            fuzzyNumberIya.Add(Mathf.Min(mBanyak, mDekat));
        //        }
        //        else if (rules.Output.Equals("Tidak"))
        //        {
        //            fuzzyNumberTidak.Add(Mathf.Min(mBanyak, mDekat));
        //        }
        //    }
        //});
        #endregion
        Transform operasiFuzzyContainer = operasiFuzzy.transform.Find("OperasiFuzzyContainer");
        operasiFuzzyContainer.gameObject.SetActive(false);
        float heightSeperator = 30f;
        RulesList.ForEachWithIndex((rules, index) =>
        {
            Transform cloneTransform = Instantiate(operasiFuzzyContainer, operasiFuzzy.transform);
            cloneTransform.tag = "CLONE";
            Debug.Log("TAGGING " + cloneTransform.tag);
            RectTransform entryRectTransform = cloneTransform.GetComponent<RectTransform>();
            if (rules.Rules1.Equals("Sedikit") && rules.Rules2.Equals("Jauh"))
            {
                Debug.LogFormat("Rules {1} : Min {0} : {2} - {3} - {4}", Mathf.Min(mSedikit, mJauh), (index + 1), rules.Output, mSedikit, mJauh);
                cloneTransform.Find("OperasiFuzzyText").GetComponent<TMP_Text>().text = "[R" + (index + 1) + "] " + Mathf.Min(mSedikit, mJauh) +  " " + rules.Output;
                if (rules.Output.Equals("Iya"))
                {
                    fuzzyNumberIya.Add(Mathf.Min(mSedikit, mJauh));
                }
                else if (rules.Output.Equals("Tidak"))
                {
                    fuzzyNumberTidak.Add(Mathf.Min(mSedikit, mJauh));
                }
            }
            else if (rules.Rules1.Equals("Sedikit") && rules.Rules2.Equals("Dekat"))
            {
                Debug.LogFormat("Rules {1} : Min {0} : {2}", Mathf.Min(mSedikit, mDekat), (index + 1), rules.Output);
                cloneTransform.Find("OperasiFuzzyText").GetComponent<TMP_Text>().text = "[R" + (index + 1) + "] " + Mathf.Min(mSedikit, mDekat) + " " + rules.Output;
                if (rules.Output.Equals("Iya"))
                {
                    fuzzyNumberIya.Add(Mathf.Min(mSedikit, mDekat));
                }
                else if (rules.Output.Equals("Tidak"))
                {
                    fuzzyNumberTidak.Add(Mathf.Min(mSedikit, mDekat));
                }
            }
            else if (rules.Rules1.Equals("Banyak") && rules.Rules2.Equals("Jauh"))
            {
                Debug.LogFormat("Rules {1} : Min {0} : {2}", Mathf.Min(mBanyak, mJauh), (index + 1), rules.Output); 
                cloneTransform.Find("OperasiFuzzyText").GetComponent<TMP_Text>().text = "[R" + (index + 1) + "] " + Mathf.Min(mBanyak, mJauh) + " " + rules.Output;
                if (rules.Output.Equals("Iya"))
                {
                    fuzzyNumberIya.Add(Mathf.Min(mBanyak, mJauh));
                }
                else if (rules.Output.Equals("Tidak"))
                {
                    fuzzyNumberTidak.Add(Mathf.Min(mBanyak, mJauh));
                }
            }
            else if (rules.Rules1.Equals("Banyak") && rules.Rules2.Equals("Dekat"))
            {
                Debug.LogFormat("Rules {1} : Min {0} : {2}", Mathf.Min(mBanyak, mDekat), (index + 1), rules.Output);
                cloneTransform.Find("OperasiFuzzyText").GetComponent<TMP_Text>().text = "[R" + (index + 1) + "] " + Mathf.Min(mBanyak, mDekat) + " " + rules.Output;
                if (rules.Output.Equals("Iya"))
                {
                    fuzzyNumberIya.Add(Mathf.Min(mBanyak, mDekat));
                }
                else if (rules.Output.Equals("Tidak"))
                {
                    fuzzyNumberTidak.Add(Mathf.Min(mBanyak, mDekat));
                }
            }
            entryRectTransform.anchoredPosition = new Vector2(0, operasiFuzzyContainer.transform.localPosition.y + (-heightSeperator * index));
            cloneTransform.gameObject.SetActive(true);
        });
    }

    void SetKomposisiAturan(GameObject komposisiAturan)
    {
        komposisiAturan.transform.Find("Iya").GetComponent<TMP_Text>().text = string.Format("Iya = {0}", KomposisiAturan.iya);
        komposisiAturan.transform.Find("Tidak").GetComponent<TMP_Text>().text = string.Format("Tidak = {0}", KomposisiAturan.tidak);
    }

    void Defuzzifikasi(GameObject hasilDefuzzifikasi)
    {
        #region comment
        //float hasil = ((defuzzifikasiIya.Sum() * KomposisiAturan.iya) + (defuzzifikasiTidak.Sum() * KomposisiAturan.tidak)) /
        //    ((KomposisiAturan.iya * defuzzifikasiIya.Count) + (KomposisiAturan.tidak * defuzzifikasiTidak.Count));
        //defuzzifikasiDefuzzifikasi.transform.Find("Value").GetComponent<TMP_Text>().text = hasil.ToString();
        #endregion
        if (KomposisiAturan.iya > KomposisiAturan.tidak)
        {
            hasilDefuzzifikasi.transform.Find("Value").GetComponent<TMP_Text>().text = string.Format($"{criticalIya + float.Parse(inputDamage.text)} (Critical : {criticalIya} - Iya)");
        }
        else
        {
            hasilDefuzzifikasi.transform.Find("Value").GetComponent<TMP_Text>().text = string.Format($"{criticalTidak + float.Parse(inputDamage.text)} (Critical : {criticalTidak} - Tidak)");
        }
        
    }
}
