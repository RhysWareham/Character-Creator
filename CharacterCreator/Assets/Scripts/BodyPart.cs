using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
    [SerializeField]
    private GameObject bodyPartCollection;
    private ColourPicker colourPicker;
    private SkeletonScript skeleton;

    // Start is called before the first frame update
    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        startPos = transform.position;
        colourPicker = canvas.GetComponentInChildren<ColourPicker>();
        skeleton = canvas.GetComponentInChildren<SkeletonScript>();
        Debug.Log("yolo");
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
        //If parent is a skeleton node
        if (this.GetComponentInParent<BodyPartSlot>())
        {
            //Turn on default image
            this.GetComponentInParent<BodyPartSlot>().SetDefaultImage(true);
        }
            

        SetImageRaycastAvailable(false);

        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;

        eventData.pointerDrag.GetComponent<BodyPart>().droppedOnSlot = false;
        Debug.Log("OnBeginDrag");
    }

    public void OnDrag(PointerEventData eventData)
    {
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
            AttachToMenu(this);

            //Set position back to start position
            transform.position = startPos;

            //Check if no sprite on slot; if so, make default visible
            //if(skeleton.NodeCurrentSprite[(int)thisBodyPart]. != skeleton.NodeCurrentSprite[(int)]
        }

        SetImageRaycastAvailable(true);

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //If on skeleton
        if(transform.parent.gameObject != bodyPartCollection)
        {
            //Set this body part to be the edittable image 
            colourPicker.SetEdittingImage(GetComponent<Image>());
        }
        Debug.Log("OnPointerDown");
    }

    public void AttachToSkeleton(BodyPartSlot slot)
    {
        droppedOnSlot = true;

        //If placeholder default image is there
        if (slot.isDefaultOn)
        {
            //Turn off default image
            slot.SetDefaultImage(false);
        }

        ////If not a child of bodyPartCollection
        //if (gameObject.transform.parent.gameObject != bodyPartCollection)
        //{
        //    AttachToMenu(this);
        //}

        //If there is a bodypart in place already
        if (slot.GetComponentInChildren<BodyPart>())
        {
            //If bodypart is not already on skeleton
            if (slot.GetComponentInChildren<Transform>().gameObject != gameObject)
            {
                //Set other sprite back to menu position
                slot.GetComponentInChildren<BodyPart>().AttachToMenu(slot.GetComponentInChildren<BodyPart>());
            }

        }

        //Snap sprite to slot centre position
        GetComponent<RectTransform>().position = slot.GetComponent<RectTransform>().position;
        gameObject.transform.SetParent(slot.gameObject.transform);
    }

    public void AttachToMenu(BodyPart part)
    {
        part.gameObject.transform.SetParent(part.bodyPartCollection.transform);
        part.GetComponent<RectTransform>().position = part.startPos;
    }

    public void SetImageRaycastAvailable(bool trueFalse)
    {
        //For each skeleton node
        for (int i = 0; i < skeleton.SkeletonNodes.Length; i++)
        {
            //If being turned off
            if(trueFalse == false)
            {
                //If that node is not the same as current body part type
                if (skeleton.SkeletonNodes[i].GetComponent<BodyPartSlot>().thisBodyPartAllowed != thisBodyPart)
                {
                    //Set 
                    skeleton.NodeBodyPart[i].GetComponent<Image>().raycastTarget = false;
                    skeleton.SkeletonNodes[i].GetComponent<Image>().raycastTarget = false;
                }
                else
                {
                    Debug.Log(skeleton.SkeletonNodes[i].gameObject);
                    //Set node to be a raycast target
                    skeleton.SkeletonNodes[i].GetComponent<Image>().raycastTarget = true;
                    skeleton.NodeBodyPart[i].GetComponent<Image>().raycastTarget = false;
                }
                //skeleton.NodeBodyPart[i].GetComponent<Image>().raycastTarget = true;
            }
            //If nodes are being turned on
            else
            {
                skeleton.SkeletonNodes[i].GetComponent<Image>().raycastTarget = true;
                skeleton.NodeBodyPart[i].GetComponent<Image>().raycastTarget = true;
            }
        }
    }
    //public void ResetPosition()
    //{
    //    rectTransform = startPos;
    //}
}
