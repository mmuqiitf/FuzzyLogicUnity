using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CodeMonkey.Utils;
using System.Linq;
public class Window_Graph : MonoBehaviour
{
    [SerializeField] private Sprite circleSprite;
    private RectTransform graphContainer;
    private RectTransform labelTemplateX;
    private RectTransform labelTemplateY;
    private RectTransform labelCircle;

    void Awake()
    {
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
        labelTemplateX = graphContainer.Find("labelTemplateX").GetComponent<RectTransform>();
        labelTemplateY = graphContainer.Find("labelTemplateY").GetComponent<RectTransform>();
        labelCircle = graphContainer.Find("labelCircle").GetComponent<RectTransform>();

        //value_y.Add(0);
        //value_y.Add(1);
        //value_x.Add(1000);
        //value_x.Add(5000);

        //value_y2.Add(1);
        //value_y2.Add(0);
        //value_x2.Add(5000);
        //value_x2.Add(1000);

        //max_x = value_x.Max(value => value);
        //max_y = value_y.Max(value => value);
        //max_x2 = value_x2.Max(value => value);
        //max_y2 = value_y2.Max(value => value);
        
        //ShowGraph(value_x, value_y, value_x2, value_y2);
    }
    
    public GameObject CreateCircle(Vector2 anchoredPosition, float x, float y)
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

        return gameObject;
    }

    public void ShowGraph(List<float> value_x, List<float> value_y, List<float> value_x2, List<float> value_y2)
    {
        float graphHeight = graphContainer.sizeDelta.y;
        float graphWidth = graphContainer.sizeDelta.x;
        float yMaximum = value_y.Max(value => value);
        float xMaximum = value_x.Max(value => value);
        float ySize = 15f;
        GameObject lastCircleGameObject = null;
        GameObject lastCircleGameObject2 = null;
        for (int i = 0; i < value_x.Count; i++)
        {
            float yPosition = ySize * value_y[i] * ySize;
            float xPosition = (value_x[i] / xMaximum) * graphWidth;

            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition), value_x[i], value_y[i]);
            if (lastCircleGameObject != null)
            {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }
            lastCircleGameObject = circleGameObject;
            
            
            RectTransform labelY = Instantiate(labelTemplateY);
            labelY.SetParent(graphContainer);
            labelY.gameObject.SetActive(true);
            labelY.anchoredPosition = new Vector2(-40f, yPosition);
            labelY.GetComponent<TMP_Text>().text = Mathf.RoundToInt(value_y[i]).ToString();
        }

        for (int i = 0; i < value_x2.Count; i++)
        {
            float yPosition = ySize * value_y2[i] * ySize;
            float xPosition = (value_x[i] / xMaximum) * graphWidth;

            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition), value_x2[i], value_y2[i]);
            if (lastCircleGameObject2 != null)
            {
                CreateDotConnection(lastCircleGameObject2.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }
            lastCircleGameObject2 = circleGameObject;


            RectTransform labelY = Instantiate(labelTemplateY);
            labelY.SetParent(graphContainer);
            labelY.gameObject.SetActive(true);
            labelY.anchoredPosition = new Vector2(-40f, yPosition);
            labelY.GetComponent<TMP_Text>().text = Mathf.RoundToInt(value_y2[i]).ToString();
        }

        int separator = 5;
        for (int i = 1; i <= separator; i++)
        {
            float normalizedValue = i * 1f / separator;
            RectTransform labelX = Instantiate(labelTemplateX);
            labelX.SetParent(graphContainer);
            labelX.gameObject.SetActive(true);
            labelX.anchoredPosition = new Vector2(normalizedValue * graphWidth, -30f);
            labelX.GetComponent<TMP_Text>().text = Mathf.RoundToInt(normalizedValue * xMaximum).ToString();
        }
    }

    public void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
    {
        GameObject gameObject = new GameObject("DotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, .5f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
    }
}
