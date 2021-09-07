using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tables : MonoBehaviour
{
    private Transform entryContainer, entryTemplate;
    private void Awake()
    {
        entryContainer = transform.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("highscoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);
        float templateHeight = 30f;
        for (int i = 0; i < 10; i++)
        {
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryContainer.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);
            Debug.LogFormat("I : {1}, Pos : {2}", i, entryRectTransform.anchoredPosition);
            entryTransform.gameObject.SetActive(true);

            //int rank = i + 1;
            //string rankstring;
            //switch (rank)

            //{
            //    default:
            //        rankstring = rank + "th"; break;
            //    case 1: rankstring = "1st"; break;
            //    case 2: rankstring = "2nd"; break;
            //    case 3: rankstring = "3rd"; break;
            //}
            //int score = random.range(0, 10000);
            //entrytransform.find("postext").getcomponent<text>().text = rankstring;
            //entrytransform.find("scoretext").getcomponent<text>().text = score.tostring();
            //string name = "aaa";
            //entrytransform.find("nametext").getcomponent<text>().text = name;

        }
    }
}
