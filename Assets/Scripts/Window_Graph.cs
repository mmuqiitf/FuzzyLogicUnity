using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Window_Graph : MonoBehaviour
{
    [SerializeField] private Sprite circleSprite;
    private RectTransform graphContainer;
    private RectTransform labelTemplateX;
    private RectTransform labelTemplateY;
    private RectTransform labelCircle;
    
    private List<int> value_x = new List<int>();
    private List<int> value_y = new List<int>();

    private int max_x;
    private int max_y;

    void Awake()
    {
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
        labelTemplateX = graphContainer.Find("labelTemplateX").GetComponent<RectTransform>();
        labelTemplateY = graphContainer.Find("labelTemplateY").GetComponent<RectTransform>();
        labelCircle = graphContainer.Find("labelCircle").GetComponent<RectTransform>();

        value_x.Add(0);
        value_x.Add(1);
        value_y.Add(1000);
        value_y.Add(5000);

        max_x = 0;
        max_y = 0;
        
        foreach (int x in value_x)
        {
            if (max_x <= x)
            {
                max_x = x;
            }
        }
        
        foreach (int y in value_y)
        {
            if (max_y <= y)
            {
                max_y = y;
            }
        }
        
        ShowGraph(value_x, value_y);
    }
    
    private void CreateCircle(Vector2 anchoredPosition, int x, int y)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        
        RectTransform labelC = Instantiate(labelCircle);
        labelC.SetParent(graphContainer);
        labelC.gameObject.SetActive(true);
        labelC.anchoredPosition = anchoredPosition + new Vector2(0, 20f);
        labelC.GetComponent<TMP_Text>().text = "("+ x + ", " + y + ")";
    }

    private void ShowGraph(List<int> value_x, List<int> value_y)
    {
        float graphHeight = graphContainer.sizeDelta.y;
        float graphWidth = graphContainer.sizeDelta.x;
        float yMaximum = max_y;
        float xMaximum = max_x;
        float xSize = 15f;
        
        for (int i = 0; i < value_x.Count; i++)
        {
            float xPosition = xSize * value_x[i] * xSize;
            float yPosition = (value_y[i] / yMaximum) * graphHeight;
            CreateCircle(new Vector2(xPosition, yPosition), value_x[i], value_y[i]);
            
            RectTransform labelX = Instantiate(labelTemplateX);
            labelX.SetParent(graphContainer);
            labelX.gameObject.SetActive(true);
            labelX.anchoredPosition = new Vector2(xPosition, -30f);
            labelX.GetComponent<TMP_Text>().text = Mathf.RoundToInt(value_x[i]).ToString();
        }

        int separator = 10;
        for (int i = 1; i <= separator; i++)
        {
            float normalizedValue = i * 1f / separator;
            RectTransform labelY = Instantiate(labelTemplateY);
            labelY.SetParent(graphContainer);
            labelY.gameObject.SetActive(true);
            labelY.anchoredPosition = new Vector2(-40f, normalizedValue * graphHeight);
            labelY.GetComponent<TMP_Text>().text = Mathf.RoundToInt(normalizedValue * yMaximum).ToString();
        }
    }
}
