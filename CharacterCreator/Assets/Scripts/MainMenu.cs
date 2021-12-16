using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private enum MenuPages
    {
        SHIRT,
        ARMS,
        LEGS,
        TROUSERS,
        SHOES,
        EYES,
        BROWS,
        EARS,
        NOSE,
        MOUTH,
        HEAD,
        HAIR
    }

    [SerializeField]
    private Button[] facialButtons;
    [SerializeField]
    private Button[] otherButtons;

    [SerializeField]
    private GameObject[] bodyPartCollection;

    [SerializeField]
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
        //Show body part menu
        if(bodyPartType != (int)currentMenuPage)
        {
            //If current page is head
            if ((int)currentMenuPage == (int)MenuPages.HEAD)
            {
                //And new page is face
                if(bodyPartType > 4 && bodyPartType < 10)
                {
                    //Don't turn off head menu
                }
                else
                {

                    bodyPartCollection[(int)currentMenuPage].SetActive(false);
                }
            }

            //If current menu is face
            else if((int)currentMenuPage > 4 && (int)currentMenuPage < 10)
            {
                bodyPartCollection[(int)currentMenuPage].SetActive(false);
                //If new menu is not a face
                if (bodyPartType > 4 || bodyPartType < 10)
                {
                    //Set head to not active
                    bodyPartCollection[(int)MenuPages.HEAD].SetActive(true);

                }
            }
            else
            {
                bodyPartCollection[(int)currentMenuPage].SetActive(false);
            }



            ////If current menu is a facial feature or head
            //    if ((int)currentMenuPage > 4 && (int)currentMenuPage < 11)
            //{
            //    if(bodyPartType )
            //    //If button is not a facial feature
            //    if(bodyPartType <= 4 || bodyPartType >= 10)
            //    {
            //        bodyPartCollection[(int)currentMenuPage].SetActive(false);
            //    }
            //    else
            //    {

            //    }

            //}
            //else
            //{
            //    bodyPartCollection[(int)currentMenuPage].SetActive(false);

            //}
            
            bodyPartCollection[bodyPartType].SetActive(true);
            currentMenuPage = (MenuPages)bodyPartType;
            //If head, make other buttons invisible
            if(currentMenuPage == MenuPages.HEAD)
            {
                foreach(Button button in otherButtons)
                {
                    button.gameObject.SetActive(false);
                }
            }
            if(currentMenuPage == MenuPages.HAIR)
            {
                
                foreach (Button button in otherButtons)
                {
                    button.gameObject.SetActive(true);
                }
                bodyPartCollection[(int)MenuPages.HEAD].SetActive(false);
            }
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
