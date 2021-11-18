using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyPartNode : SkeletonScript
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
}
