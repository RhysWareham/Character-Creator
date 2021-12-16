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
    [SerializeField]
    private Vector3 offsetPos;

    private float startPositive;
    [SerializeField]
    private bool canFlip = false;
    [SerializeField]
    private bool isFlipped = false;

    public GameObject replacement;
    private bool hasDuplicate = false;
    public bool isDuplicate = false;


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

            if (GetComponent<BackHair>())
            {
                GetComponent<BackHair>().frontHair.transform.SetParent(this.transform);
                //GetComponentInChildren<Transform>().gameObject.transform.SetParent(skeleton.frontHairPos.transform);
            }
            //if (startPositive != rectTransform.localScale.x)
            //{
            //    GetComponent<RectTransform>().localScale = new Vector3(-GetComponent<RectTransform>().localScale.x, GetComponent<RectTransform>().localScale.y, GetComponent<RectTransform>().localScale.z);

            //}
        }

        //Create a new body part to replace current one if still part of menu
        //Idk why it is starting with scale x of 0
        if(this.GetComponentInParent<BodyCollection>())
        {
            GameObject newOne = this.gameObject;

            replacement = Instantiate(newOne, bodyPartCollection.transform);
            replacement.GetComponent<BodyPart>().isDuplicate = true;
            replacement.GetComponent<BodyPart>().startPositive = startPositive;
            replacement.GetComponent<BodyPart>().canFlip = canFlip;
            //replacement.GetComponent<RectTransform>().transform.localScale = new Vector2(1, 5);
            replacement.GetComponent<BodyPart>().startPos = startPos;
            AttachToMenu(replacement.GetComponent<BodyPart>());
            hasDuplicate = true;
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
            if(hasDuplicate)
            {
                if(replacement.GetComponentInParent<BodyCollection>())
                {
                    Destroy(replacement);
                }
            }
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
                //slot.GetComponentInChildren<BodyPart>().AttachToMenu(slot.GetComponentInChildren<BodyPart>());
            }

            //If slot image is same as new image and is not default
            if(slot.GetComponentInChildren<BodyPart>().GetComponent<Image>().sprite == GetComponent<Image>().sprite && !slot.GetComponentInChildren<Default>())
            {
                //If there can be 2 stored on the character
                if(1==1)
                {
                    //If slot sprite has a duplicate
                    if(slot.GetComponentInChildren<BodyPart>().hasDuplicate)
                    {
                        
                        //And duplicate is not on skeleton
                        if(slot.GetComponentInChildren<BodyPart>().replacement.GetComponentInParent<BodyCollection>())
                        {
                            //And if duplicate is not the new dragged sprite
                            if(slot.GetComponentInChildren<BodyPart>().replacement != gameObject)
                            {
                                //Destroy the replacement
                                //Destroy(slot.GetComponentInChildren<BodyPart>().replacement);
                                //slot.GetComponentInChildren<BodyPart>().hasDuplicate = false;
                                //And then send the slot image to menu.
                                //replacement = slot.GetComponentInChildren<BodyPart>().gameObject;
                                slot.GetComponentInChildren<BodyPart>().AttachToMenu(slot.GetComponentInChildren<BodyPart>()); ///////////////latest line
                                
                                //Make sure that when it gets attached to menu, it becomes slot's duplicate
                            
                            }
                            //If the duplicate is the new sprite that is being dragged on
                            else
                            {
                                //And delete current dragging sprite's duplicate
                                if(hasDuplicate)
                                {
                                    Destroy(replacement);
                                }
                                
                                replacement = slot.GetComponentInChildren<BodyPart>().gameObject;
                                //Send slot image back to menu?
                                slot.GetComponentInChildren<BodyPart>().AttachToMenu(slot.GetComponentInChildren<BodyPart>());
                                

                                //Attach dragging sprite to skeleton
                                //Gets done in standard part of function
                            }
                        }
                        //If duplicate is on skeleton
                        else
                        {
                            if(hasDuplicate)
                            {
                                if(replacement.GetComponentInParent<BodyCollection>())
                                {
                                    Destroy(replacement);
                                }
                                replacement = slot.GetComponentInChildren<BodyPart>().gameObject;
                            }
                            //Send slot image back to menu?
                            replacement = slot.GetComponentInChildren<BodyPart>().gameObject;
                            slot.GetComponentInChildren<BodyPart>().AttachToMenu(slot.GetComponentInChildren<BodyPart>());
                        }
                    }
                }
                else
                {
                    //Check if duplicate lives
                    //If duplicate of slot image is in menu, delete the duplicate and return the slot image to menu.
                }


            }
            else
            {
                //if(GetComponentInParent<BodyPartSlot>())
                //{

                //}
                slot.GetComponentInChildren<BodyPart>().AttachToMenu(slot.GetComponentInChildren<BodyPart>());
            }
        }
        if (thisBodyPart == Manager.IndividualBodyPart.HAIR)
        {
            if (!slot.GetComponentInChildren<BodyPart>())
            {
                if (skeleton.frontHairPos.GetComponentInChildren<BodyPart>())
                {
                    //If bodypart is not already on skeleton
                    if (slot.GetComponentInChildren<Transform>().gameObject != gameObject)
                    {
                        //Move front hair
                        skeleton.frontHairPos.GetComponentInChildren<BodyPart>().AttachToMenu(skeleton.frontHairPos.GetComponentInChildren<BodyPart>());
                    }
                }
            }
        }

        //Only need to flip on drop if slot is Right
        if (canFlip && slot.isRight)
        {
            if (!isFlipped)
            {
                GetComponent<RectTransform>().localScale = new Vector3(-GetComponent<RectTransform>().localScale.x, GetComponent<RectTransform>().localScale.y, GetComponent<RectTransform>().localScale.z);
                isFlipped = true;
            }
        }
        else if (canFlip && !slot.isRight)
        {
            if (isFlipped)
            {
                GetComponent<RectTransform>().localScale = new Vector3(-GetComponent<RectTransform>().localScale.x, GetComponent<RectTransform>().localScale.y, GetComponent<RectTransform>().localScale.z);
                isFlipped = false;
            }
        }
        

        //Vector3 positionChange = GetComponent<RectTransform>().localPosition;

                //Set scale to be correct
        //If slot is right, and scale x is equal to startPositive
        //GetComponent<RectTransform>().localScale = new Vector3(startPositive, GetComponent<RectTransform>().localScale.y, GetComponent<RectTransform>().localScale.z);
        

        //Snap sprite to slot centre position
        gameObject.transform.SetParent(slot.gameObject.transform);
        GetComponent<RectTransform>().localPosition = offsetPos;

        if(thisBodyPart == Manager.IndividualBodyPart.HAIR)
        {
            if(GetComponent<BackHair>())
            {
                GetComponent<BackHair>().frontHair.transform.SetParent(skeleton.frontHairPos.transform);
            
            }
            else
            {
                gameObject.transform.SetParent(skeleton.frontHairPos.transform);
            }

        }
        
    }

    public void AttachToMenu(BodyPart part)
    {
        part.GetComponent<Image>().raycastTarget = true;
        
        if(part.thisBodyPart == Manager.IndividualBodyPart.HAIR)
        {
            if (part.GetComponent<BackHair>())
            {
                part.GetComponent<BackHair>().frontHair.transform.SetParent(part.transform);
                //GetComponentInChildren<Transform>().gameObject.transform.SetParent(skeleton.frontHairPos.transform);
            }
        }

        part.gameObject.transform.SetParent(part.bodyPartCollection.transform);
        part.GetComponent<RectTransform>().position = part.startPos;
        part.GetComponent<RectTransform>().localScale = new Vector3(part.startPositive, part.GetComponent<RectTransform>().localScale.y, part.GetComponent<RectTransform>().localScale.z);
        part.isFlipped = false;

        //If 
        //if(part.hasDuplicate)
        //{
        //    //If original's duplicate is in menu
        //    if(part.replacement.GetComponentInParent<BodyCollection>())
        //    {
        //        //Destroy the menu replacement
        //        Destroy(part.replacement);
        //        part.hasDuplicate = false;
        //    }
        //}
    }

    public void SetImageRaycastAvailable(bool trueFalse)
    {
        //For each skeleton node
        for (int i = 0; i < skeleton.SkeletonNodes.Length; i++)
        {
            if (skeleton.SkeletonNodes[i] != null)
            {


                //If being turned off
                if (trueFalse == false)
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
    }
    //public void ResetPosition()
    //{
    //    rectTransform = startPos;
    //}
}
