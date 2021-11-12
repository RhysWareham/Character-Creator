using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{

    [SerializeField]
    public Manager.IndividualBodyPart thisBodyPart;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrop()
    {
        switch(thisBodyPart)
        {
            case Manager.IndividualBodyPart.HEAD:
                break;
            case Manager.IndividualBodyPart.SHIRT:
                break;
            case Manager.IndividualBodyPart.ARMS:
                break;
            case Manager.IndividualBodyPart.LEGS:
                break;
            case Manager.IndividualBodyPart.SHOES:
                break;
            default:
                break;
        }
    }
}
