using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;

public class DatasetDamage : MonoBehaviour
{
    [SerializeField] private InputField variabelPertama, variabelKedua;
    private List<float> variabelList = new List<float>();
    private List<float> value_y = new List<float>() { 0, 1 };
    public GameObject gameObject;

    private void Awake()
    {
        variabelPertama.text = 4500f.ToString();
        variabelKedua.text = 1000f.ToString();
    }
    public void Submit()
    {
        variabelList.Add(float.Parse(variabelKedua.text));
        variabelList.Add(float.Parse(variabelPertama.text));
        List<float> variabelListReverse = variabelList.Reverse<float>().ToList();
        List<float> value_yReverse = value_y.Reverse<float>().ToList();
        float max_x = variabelList.Max(value => value);
        float max_y = value_y.Max(value => value);
        float max_x2 = variabelListReverse.Max(value => value);
        float max_y2 = value_yReverse.Max(value => value);

        gameObject.GetComponent<Window_Graph>().ShowGraph(variabelList, value_y, variabelListReverse, value_yReverse);
    }

}
