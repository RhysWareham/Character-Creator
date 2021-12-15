﻿using System.Collections;
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
    [SerializeField]
    private Vector3 offsetPos;

    private float startPositive;
    [SerializeField]
    private bool canFlip = false;
    [SerializeField]
    private bool isFlipped = false;

    // Start is called before the first frame update
    void Start()
    {
        startPositive = GetComponent<RectTransform>().localScale.x;
        canvas = FindObjectOfType<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        startPos = transform.position;
        colourPicker = canvas.GetComponentInChildren<ColourPicker>();
        skeleton = canvas.GetComponentInChildren<SkeletonScript>();
        Debug.Log("yolo");
        bodyPartCollection = this.GetComponentInParent<BodyCollection>().gameObject;
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

            //If pointing right instead of left
            if (GetComponent<RectTransform>().localScale.x != startPositive)
            {
                //flip sprite
                //GetComponent<RectTransform>().localScale = new Vector3(startPositive, GetComponent<RectTransform>().localScale.y, GetComponent<RectTransform>().localScale.z);


            }
            if(isFlipped)
            {
                Vector3 localScale = new Vector3(-startPositive, GetComponent<RectTransform>().localScale.y, GetComponent<RectTransform>().localScale.z);
                //transform.localScale = localScale*-1;
                //isFlipped = false;
            }
            //if (startPositive != rectTransform.localScale.x)
            //{
            //    GetComponent<RectTransform>().localScale = new Vector3(-GetComponent<RectTransform>().localScale.x, GetComponent<RectTransform>().localScale.y, GetComponent<RectTransform>().localScale.z);

            //}
        }
        //GetComponent<RectTransform>().localScale = new Vector3(startPositive, GetComponent<RectTransform>().localScale.y, GetComponent<RectTransform>().localScale.z);

        //if(startPositive < 0 && GetComponent<RectTransform>().localScale.x > 0)
        //{
            
        //    GetComponent<RectTransform>().localScale = new Vector3(-GetComponent<RectTransform>().localScale.x, GetComponent<RectTransform>().localScale.y, GetComponent<RectTransform>().localScale.z);
            

        //}
        //if(startPositive < 0 && GetComponent<RectTransform>().localScale >)
        //else



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
        else
        {
            //If on skeleton
            if (transform.parent.gameObject != bodyPartCollection)
            {
                //Set this body part to be the edittable image 
                colourPicker.SetEdittingImage(GetComponent<Image>());
            }
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
        //Vector3 positionChange = GetComponent<RectTransform>().localPosition;

        //If sprite is on right of skeleton, but not flipped, flip it
        //If slot is right, and scale is -1
        //if(slot.isRight && GetComponent<RectTransform>().localScale.x < 0)
        //{
        //    //If does start positive and not flipped
        //    if(startPositive > 0 && GetComponent<RectTransform>().localScale.x > 0)
        //    {
        //        //flip
        //        GetComponent<RectTransform>().localScale = new Vector3(slot.transform.localScale.x * GetComponent<RectTransform>().localScale.x, GetComponent<RectTransform>().localScale.y, GetComponent<RectTransform>().localScale.z);

        //    }

        //    //if (this.GetComponent<RectTransform>().position.x > skeleton.transform.position.x
        //    //&& GetComponent<RectTransform>().localScale.x > 0)
        //    //{
        //    //}
        //}
        //else if(!slot.isRight && GetComponent<RectTransform>().localScale.x > 0)
        //{
        //    if()
        //}

        //if (slot.isRight)
        //{
        //    GetComponent<RectTransform>().localScale = new Vector3(-GetComponent<RectTransform>().localScale.x, GetComponent<RectTransform>().localScale.y, GetComponent<RectTransform>().localScale.z);
        //}
        //Set scale to be correct
        //If slot is right, and scale x is equal to startPositive
        //GetComponent<RectTransform>().localScale = new Vector3(startPositive, GetComponent<RectTransform>().localScale.y, GetComponent<RectTransform>().localScale.z);
        //Only need to flip on drop if slot is Right
        if (canFlip && slot.isRight)
        {
            if (!isFlipped)
            {
                GetComponent<RectTransform>().localScale = new Vector3(-GetComponent<RectTransform>().localScale.x, GetComponent<RectTransform>().localScale.y, GetComponent<RectTransform>().localScale.z);
                isFlipped = true;
            }
        }
        else if(canFlip && !slot.isRight)
        {
            if(isFlipped)
            {
                GetComponent<RectTransform>().localScale = new Vector3(-GetComponent<RectTransform>().localScale.x, GetComponent<RectTransform>().localScale.y, GetComponent<RectTransform>().localScale.z);
                isFlipped = false;
            }
        }

        //    //If started facing right
        //    if (startPositive > 0)
        //    {
        //        //But doesn't need to flip right now
        //        if (GetComponent<RectTransform>().localScale.x < startPositive)
        //        {

        //        }
        //        else
        //        {
        //            GetComponent<RectTransform>().localScale = new Vector3(-GetComponent<RectTransform>().localScale.x, GetComponent<RectTransform>().localScale.y, GetComponent<RectTransform>().localScale.z);
        //        }
        //    }
        //    //If didn't start the right way
        //    else
        //    {
        //        //But doesn't need to flip right now
        //        if (GetComponent<RectTransform>().localScale.x < startPositive)
        //        {
        //            GetComponent<RectTransform>().localScale = new Vector3(-GetComponent<RectTransform>().localScale.x, GetComponent<RectTransform>().localScale.y, GetComponent<RectTransform>().localScale.z);

        //        }
        //    }
        //}
        //else if(canFlip && !slot.isRight)
        //{
        //    //If started facing left
        //    if (startPositive > 0)
        //    {
        //        //But doesn't need to flip right now
        //        if (GetComponent<RectTransform>().localScale.x == startPositive)
        //        {

        //        }
        //        else
        //        {
        //            GetComponent<RectTransform>().localScale = new Vector3(-GetComponent<RectTransform>().localScale.x, GetComponent<RectTransform>().localScale.y, GetComponent<RectTransform>().localScale.z);
        //        }
        //    }
        //    //If didn't start the right way
        //    else
        //    {
        //        //But doesn't need to flip right now
        //        if (GetComponent<RectTransform>().localScale.x > startPositive)
        //        {
        //            GetComponent<RectTransform>().localScale = new Vector3(-GetComponent<RectTransform>().localScale.x, GetComponent<RectTransform>().localScale.y, GetComponent<RectTransform>().localScale.z);

        //        }
        //    }
        //}


        //Snap sprite to slot centre position
        gameObject.transform.SetParent(slot.gameObject.transform);
        GetComponent<RectTransform>().localPosition = offsetPos;
    }

    public void AttachToMenu(BodyPart part)
    {
        if(part.startPositive != part.rectTransform.localScale.x)
        {
            //part.GetComponent<RectTransform>().localScale = new Vector3(-part.GetComponent<RectTransform>().localScale.x, part.GetComponent<RectTransform>().localScale.y, part.GetComponent<RectTransform>().localScale.z);

        }
        part.gameObject.transform.SetParent(part.bodyPartCollection.transform);
        part.GetComponent<RectTransform>().position = part.startPos;
        part.GetComponent<RectTransform>().localScale = new Vector3(part.startPositive, part.GetComponent<RectTransform>().localScale.y, part.GetComponent<RectTransform>().localScale.z);
        part.isFlipped = false;
        part.GetComponent<Image>().raycastTarget = true;
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
