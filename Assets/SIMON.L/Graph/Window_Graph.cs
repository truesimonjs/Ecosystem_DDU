using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Window_Graph : MonoBehaviour
{
    [SerializeField] private Sprite circleSprite;
    private RectTransform graphContainer;
    public RectTransform dotsContainer;
    private RectTransform LabelTemplateX;
    private RectTransform LabelTemplateY;

    private RectTransform DashTemplateX;
    private RectTransform DashTemplateY;




    //public List<Vector2> valueList = new List<Vector2> { new Vector2(1, 5), new Vector2(4, 6) };


    public void TakeList(List<Vector2> valueList)
    {
        

        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
        dotsContainer = transform.transform.Find("graphContainer").Find("DotsContainer").GetComponent<RectTransform>();
        LabelTemplateX = graphContainer.Find("LabelTemplateX").GetComponent<RectTransform>();
        LabelTemplateY = graphContainer.Find("LabelTemplateY").GetComponent<RectTransform>();


       DashTemplateX = graphContainer.Find("DashTemplateX").GetComponent<RectTransform>();
       DashTemplateY = graphContainer.Find("DashTemplateY").GetComponent<RectTransform>();

        foreach (Transform child in dotsContainer.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        ShowGraph(valueList);
    }


    private GameObject CreateCircle(Vector2 anchorPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(dotsContainer, false);
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
            CircleGameobject.AddComponent<ShowCordinates>();
            CircleGameobject.GetComponent<ShowCordinates>().Cordinates = new Vector2(ValueList[i].x, ValueList[i].y);

            if (lastCircleGameObject != null)
            {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, CircleGameobject.GetComponent<RectTransform>().anchoredPosition);             
            }

            lastCircleGameObject = CircleGameobject;


        }

        int CounterForLabelsX = 0;
        while (CounterForLabelsX / xMaximum<=1)
        {
            RectTransform labelX = Instantiate(LabelTemplateX, dotsContainer, false);

            labelX.gameObject.SetActive(true);
            labelX.anchoredPosition = new Vector2((CounterForLabelsX / xMaximum) * graphWidth - 7, -5f);
            labelX.GetComponent<Text>().text = CounterForLabelsX.ToString();
           



            RectTransform DashX = Instantiate(DashTemplateX, dotsContainer, false);

            DashX.gameObject.SetActive(true);
            DashX.anchoredPosition = new Vector2((CounterForLabelsX / xMaximum) * graphWidth, -5f);
           
            CounterForLabelsX++;
        }


        int seperatorCount = 10;
        for (int i = 0; i < seperatorCount; i++)
        {
            RectTransform labelY = Instantiate(LabelTemplateY, dotsContainer, false);

            labelY.gameObject.SetActive(true);
            float normalizedValue = i * 1f / seperatorCount;
            labelY.anchoredPosition = new Vector2(-20, normalizedValue*graphHeight+7);
            labelY.GetComponent<Text>().text = Mathf.RoundToInt(normalizedValue*yMaximum).ToString();


            RectTransform DashY = Instantiate(DashTemplateY, dotsContainer, false);

            DashY.gameObject.SetActive(true);
            DashY.anchoredPosition = new Vector2(-20, normalizedValue * graphHeight );

        }
        
    }




    private void CreateDotConnection (Vector2 dotPositionA, Vector2 dotPositionB)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(dotsContainer, false);
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
