using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BodyPart : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    [SerializeField]
    public Manager.IndividualBodyPart thisBodyPart;

    private Canvas canvas;
    private CanvasGroup canvasGroup;
    public RectTransform rectTransform;
    public Vector3 startPos;
    public bool droppedOnSlot;
    public bool isFacePart;

    // Start is called before the first frame update
    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //void OnDrop()
    //{
    //    switch(thisBodyPart)
    //    {
    //        case Manager.IndividualBodyPart.HEAD:
    //            break;
    //        case Manager.IndividualBodyPart.SHIRT:
    //            break;
    //        case Manager.IndividualBodyPart.ARMS:
    //            break;
    //        case Manager.IndividualBodyPart.LEGS:
    //            break;
    //        case Manager.IndividualBodyPart.SHOES:
    //            break;
    //        default:
    //            break;
    //    }
    //}


    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;

        eventData.pointerDrag.GetComponent<BodyPart>().droppedOnSlot = false;
        Debug.Log("OnBeginDrag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        //eventData.delta is the amount the mouse moved in the previous frame
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        //If not dropped on slot
        if(droppedOnSlot == false)
        {
            //Set position back to start position
            transform.position = startPos;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }

    //public void ResetPosition()
    //{
    //    rectTransform = startPos;
    //}
}
