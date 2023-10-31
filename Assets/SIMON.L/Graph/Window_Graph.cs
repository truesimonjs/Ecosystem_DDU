using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Window_Graph : MonoBehaviour
{
    [SerializeField] private Sprite circleSprite;
    private RectTransform graphContainer;
    private RectTransform LabelTemplateX;
    private RectTransform LabelTemplateY;


    

    public List<Vector2> valueList = new List<Vector2> { new Vector2(1, 5), new Vector2(4, 6) };


    public void TakeList()
    {
        

        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
        LabelTemplateX = graphContainer.Find("LabelTemplateX").GetComponent<RectTransform>();
        LabelTemplateY = graphContainer.Find("LabelTemplateY").GetComponent<RectTransform>();


        ShowGraph(valueList);
    }


    private GameObject CreateCircle(Vector2 anchorPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchorPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    private void ShowGraph(List<Vector2> ValueList)
    {
        float graphHeight = graphContainer.sizeDelta.y;
        float graphWidth = graphContainer.sizeDelta.x;


        float yMaximum = 10f;
        for (int i = 0; i < ValueList.Count; i++)
        {
            if (yMaximum< ValueList[i].y)
            {
                yMaximum = ValueList[i].y;
            }
        }
        
        float xMaximum = 10f;
        for (int i = 0; i < ValueList.Count; i++)
        {
            if (xMaximum < ValueList[i].x)
            {
                xMaximum = ValueList[i].x;
            }
        }

        GameObject lastCircleGameObject = null;
        for (int i = 0; i < ValueList.Count; i++)
        {
            float xPosition = (ValueList[i].x / xMaximum) * graphWidth;
            float yPosition = (ValueList[i].y / yMaximum) * graphHeight;

            GameObject CircleGameobject = CreateCircle(new Vector2(xPosition, yPosition));

            if (lastCircleGameObject != null)
            {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, CircleGameobject.GetComponent<RectTransform>().anchoredPosition);             
            }

            lastCircleGameObject = CircleGameobject;


        }

        int CounterForLabelsX = 0;
        while (CounterForLabelsX / xMaximum<=1)
        {
            RectTransform labelX = Instantiate(LabelTemplateX, graphContainer, false);

            labelX.gameObject.SetActive(true);
            labelX.anchoredPosition = new Vector2((CounterForLabelsX / xMaximum) * graphWidth, -5f);
            labelX.GetComponent<Text>().text = CounterForLabelsX.ToString();
            CounterForLabelsX++;
        }


        int seperatorCount = 5;
        for (int i = 0; i < seperatorCount; i++)
        {
            RectTransform labelY = Instantiate(LabelTemplateY, graphContainer, false);

            labelY.gameObject.SetActive(true);
            float normalizedValue = i * 1f / seperatorCount;
            labelY.anchoredPosition = new Vector2(-20, normalizedValue*graphHeight+7);
            labelY.GetComponent<Text>().text = Mathf.RoundToInt(normalizedValue*yMaximum).ToString();
            
        }
        //int CounterForLabelsY = 0;

        //while (CounterForLabelsY / yMaximum <= 1)
        //{
        //    RectTransform labelY = Instantiate(LabelTemplateY, graphContainer, false);

        //    labelY.gameObject.SetActive(true);
        //    labelY.anchoredPosition = new Vector2(-20, (CounterForLabelsY / yMaximum) * graphHeight);
        //    labelY.GetComponent<Text>().text = CounterForLabelsY.ToString();
        //    CounterForLabelsY++;
        //}
    }




    private void CreateDotConnection (Vector2 dotPositionA, Vector2 dotPositionB)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, .5f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, Mathf.Atan2(dir.y, dir.x) * 180 / Mathf.PI);


    }

}
