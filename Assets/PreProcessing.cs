using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreProcessing : MonoBehaviour
{

    public Texture2D baseTexture;

    // Update is called once per frame
    void Update()
    {
        //to draw
        if (Camera.main == null) return;

        if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1)) return;
    
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(!Physics.Raycast(ray, out hit)) return;

        if(hit.collider.transform !=transform) return;

        Vector2 pixelXY = hit.textureCoord;
        pixelXY.x *=baseTexture.width;
        pixelXY.y *=baseTexture.height;

        Color colorSet = Input.GetMouseButton(0) ? Color.white : Color.black;
        Debug.Log("ColorSet");

        baseTexture.SetPixel((int)pixelXY.x, (int)pixelXY.y, colorSet);
        baseTexture.Apply();

    }

}
