using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private enum MenuPages
    {
        HEAD,
        SHIRT,
        ARMS,
        LEGS,
        SHOES
    }

    [SerializeField]
    private GameObject[] bodyPartCollection;

    private MenuPages currentMenuPage = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayBodyParts(int bodyPartType)
    {
        //
        if(bodyPartType != (int)currentMenuPage)
        {
            bodyPartCollection[(int)currentMenuPage].SetActive(false);
            bodyPartCollection[bodyPartType].SetActive(true);
            currentMenuPage = (MenuPages)bodyPartType;
        }
    }
}
