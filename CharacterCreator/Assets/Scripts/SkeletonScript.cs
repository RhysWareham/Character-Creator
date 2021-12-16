using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonScript : MonoBehaviour
{
    //Array to keep track of what sprites are in what slot
    //public GameObject[] NodeCurrentSprite = new GameObject[System.Enum.GetValues(typeof(Manager.IndividualBodyPart)).Length];//Plus arm, leg, shoe)];
    //public List<GameObject> NodeCurrentSpriteList = new List<GameObject>();
    public GameObject[] SkeletonNodes;
    public GameObject[] NodeBodyPart;

    public GameObject frontHairPos;

    private void Awake()
    {
        NodeBodyPart = new GameObject[SkeletonNodes.Length];
        
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
