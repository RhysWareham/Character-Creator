using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BodyPartSlot : MonoBehaviour, IDropHandler
{

    public Manager.IndividualBodyPart thisBodyPartAllowed;
    [SerializeField]
    private GameObject defaultBodyPartHolder;
    private GameObject thisDefaultHolder;
    public bool isDefaultOn;
    public int slotNumber;
    public SkeletonScript skeleton;

    // Start is called before the first frame update
    void Start()
    {
        //Get skeleton
        skeleton = GetComponentInParent<SkeletonScript>();

        //Go through skeleton node array to find what number is this one
        for(int i = 0; i < skeleton.SkeletonNodes.Length; i++)
        {
            if(skeleton.SkeletonNodes[i].gameObject == this.gameObject)
            {
                slotNumber = i;
                Debug.Log("Woohoo");
            }

        }

        //Create a holder image
        thisDefaultHolder = Instantiate(defaultBodyPartHolder, this.GetComponent<RectTransform>());
        skeleton.NodeBodyPart[slotNumber] = thisDefaultHolder;
        isDefaultOn = true;

        ////If no sprite
        //if (!this.GetComponentInChildren<BodyPart>())
        //{
        //    //Create a holder image
        //    thisDefaultHolder = Instantiate(defaultBodyPartHolder, this.GetComponent<RectTransform>());
        //    slotNumber = skeleton.NodeBodyPart.Count;
        //    if(thisBodyPartAllowed == Manager.IndividualBodyPart.HEAD)
        //    {
        //        Debug.Log("Head");
        //    }
        //    skeleton.NodeBodyPart.Add(thisDefaultHolder);
        //    //Set Current Node Sprite to default
        //    //NodeCurrentSprite[(int)thisBodyPartAllowed] = thisDefaultHolder;
        //    isDefaultOn = true;
               
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Function called on mouse drop
    /// </summary>
    /// <param name="eventData"></param>
    public virtual void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDropSlot");
        //If not null
        if(eventData.pointerDrag != null)
        {
            //If the body part is the same as the bodypart allowed
            if(eventData.pointerDrag.GetComponent<BodyPart>().thisBodyPart == thisBodyPartAllowed)
            {
                Debug.Log("Dropped on correct slot");

                eventData.pointerDrag.GetComponent<BodyPart>().AttachToSkeleton(this);
                //Set this node's sprite
                //NodeCurrentSprite[(int)thisBodyPartAllowed] = eventData.pointerDrag.gameObject;
                skeleton.NodeBodyPart[slotNumber] = eventData.pointerDrag.gameObject;
            }
            //else if(thisBodyPartAllowed == Manager.IndividualBodyPart.TROUSERS && eventData.pointerDrag.GetComponent<BodyPart>().thisBodyPart == Manager.IndividualBodyPart.LEGS)
            //{
            //    eventData.pointerDrag.GetComponent<BodyPart>().droppedOnSlot = true;
            //    //Snap sprite to slot centre position
            //    eventData.pointerDrag.GetComponent<RectTransform>().position = GetComponent<RectTransform>().localPosition;
            //       // new Vector3(0, GetComponent<RectTransform>().localPosition.y, GetComponent<RectTransform>().position.z);
            //}
            
        }


        //Need to unparent them from the menus so they dont disappear when clicking on a separate body part tab


    }

    public void SetDefaultImage(bool turnOn)
    {
        thisDefaultHolder.SetActive(turnOn);
        isDefaultOn = turnOn;
    }

   
}
