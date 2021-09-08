using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TabelRules : MonoBehaviour
{
    private Transform RulesContainer;

    public void CallRules(List<Rules> RulesList)
    {
        RulesContainer = transform.Find("RulesContainer");
        RulesContainer.gameObject.SetActive(false);
        float heightSeperator = 30f;
        RulesList.ForEachWithIndex((rules, index) =>
        {
            Transform entryTransform = Instantiate(RulesContainer, transform);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryTransform.Find("RulesText").GetComponent<TMP_Text>().text = "Jika Damage " + rules.Rules1 + " dan Jarak " 
            + rules.Rules2 + " maka Critical = " + rules.Output + " [R" + index + "]" ;
            entryRectTransform.anchoredPosition = new Vector2(0, RulesContainer.transform.localPosition.y + (-heightSeperator * index));
            entryTransform.gameObject.SetActive(true);
        });
    }
}
