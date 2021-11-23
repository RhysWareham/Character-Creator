using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BodyPartSlot : SkeletonScript, IDropHandler
{

    public Manager.IndividualBodyPart thisBodyPartAllowed;


    // Start is called before the first frame update
    void Start()
    {
        if(this.GetComponentInChildren<Image>().sprite == null)
        {
            NodeCurrentSprite[(int)thisBodyPartAllowed] = false;
            Debug.Log("northinf");
        }
        else
        {
            NodeCurrentSprite[(int)thisBodyPartAllowed] = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Function called on mouse drop
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDropSlot");
        //If not null
        if(eventData.pointerDrag != null)
        {
            //If the body part is the same as the bodypart allowed
            if(eventData.pointerDrag.GetComponent<BodyPart>().thisBodyPart == thisBodyPartAllowed)
            {
                eventData.pointerDrag.GetComponent<BodyPart>().droppedOnSlot = true;
                //Snap sprite to slot centre position
                eventData.pointerDrag.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;

            }
            
        }
    }
}
