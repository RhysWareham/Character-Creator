using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColourPicker : MonoBehaviour
{
    private GraphicRaycaster graphicRaycaster;
    EventSystem eventSystem;
    [SerializeField]
    private Canvas canvas;

    public float rayDistance = 100f;
    public Image edittingImage;
    private Texture2D imageMap;
    private RectTransform rectTrans;

    //Get the index for colour
    public Color newColour;
    public Color[] colours;
    public string[] texts;
    
    private int FindIndexFromColour(Color colour)
    {
        for(int i = 0; i < colours.Length; i++)
        {
            if(colours[i] == colour)
            {
                return i;
            }
        }

        return -1;
    }

    // Start is called before the first frame update
    void Start()
    {

        edittingImage = GetComponent<Image>();
        imageMap = edittingImage.material.mainTexture as Texture2D;
        rectTrans = GetComponent<RectTransform>();
        graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
        newColour = edittingImage.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetColour(string colour)
    {
        
        
        //Get the colour
        for(int i = 0; i < texts.Length; i++)
        {
            if(texts[i] == colour)
            {
                newColour = colours[i];
            }
        }

        //If colour clicked is same as current colour
        if(Manager.currentColor == newColour)
        {
            return;
        }
        edittingImage.color = newColour;
        edittingImage.color = new Color(newColour.r, newColour.g, newColour.b, 100f);

        if(edittingImage.GetComponent<BodyPart>().thisBodyPart == Manager.IndividualBodyPart.HAIR)
        {
            if(edittingImage.GetComponent<BackHair>())
            {
                edittingImage.GetComponent<BackHair>().frontHair.GetComponent<Image>().color = new Color(newColour.r, newColour.g, newColour.b, 100f);
            }
        }

        //image.color.a = 100f;
        Manager.currentColor = newColour;



    }

    public void SetEdittingImage(Image newImage)
    {
        if(edittingImage != newImage)
        {
            edittingImage = newImage;
            imageMap = edittingImage.material.mainTexture as Texture2D;
            newColour = edittingImage.color;
            Manager.currentColor = newColour;
        }
    }

    ///// <summary>
    ///// When mouse click is down on this image
    ///// </summary>
    ///// <param name="eventData"></param>
    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    PointerEventData newData = new PointerEventData(eventSystem);
    //    Debug.Log(eventData.position);
    //    newData.position = Input.mousePosition;
    //    Debug.Log(newData.position);

    //    RaycastHit[] hits;
    //    hits = EventSystem.current.RaycastAll(newData.position, this.transform. rayResults)

    //    List<RaycastResult> rayResults = new List<RaycastResult>();
    //    graphicRaycaster.Raycast(newData, rayResults);

    //    EventSystem.current.RaycastAll(newData, rayResults)

    //    foreach(RaycastResult result in rayResults)
    //    {
    //        //Check if hit this image
    //        if(result.gameObject.GetComponent<ColourPicker>())
    //        {
    //            Vector2 localPos;

    //            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTrans, newData.position, Camera.main, out localPos);
    //            Debug.Log(localPos);
    //            Vector2 pixelUV = localPos.
    //            Vector2 pixelUV = result. //.textureCoord;
    //            pixelUV.x *= image.rectTransform.rect.width;
    //            pixelUV.y *= image.rectTransform.rect.height;
    //            Vector2 tiling = image.material.mainTextureScale;
    //            Color colour = imageMap.GetPixel(Mathf.FloorToInt(pixelUV.x * tiling.x), Mathf.FloorToInt(pixelUV.y * tiling.y));
    //        }
    //    }
    //    //RaycastHit hit;
    //    //Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, rayDistance);
        
    //        Vector2 pixelUV = hit.textureCoord;
    //        pixelUV.x *= image.rectTransform.rect.width;
    //        pixelUV.y *= image.rectTransform.rect.height;
    //        Vector2 tiling = image.material.mainTextureScale;
    //        Color colour = imageMap.GetPixel(Mathf.FloorToInt(pixelUV.x * tiling.x), Mathf.FloorToInt(pixelUV.y * tiling.y));

    //        Debug.Log(colour);
    //        //int index = FindIndexFromColour(colour);
    //        //if(index >= 0)
    //        //{
    //        //    Debug.Log(texts[index]);
    //        //}
        
        
    //}

}
