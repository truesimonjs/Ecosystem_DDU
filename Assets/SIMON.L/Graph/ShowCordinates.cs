using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class ShowCordinates : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector2 Cordinates; 
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(Cordinates);

        GameObject.Find("CordinateText").GetComponent<TextMeshProUGUI>().SetText(("X: " + Cordinates.x+ "\nY: " + Cordinates.y).Replace("\\n", "\n"));

    }

    public void OnPointerExit(PointerEventData eventData)
    {
      
    }

}
