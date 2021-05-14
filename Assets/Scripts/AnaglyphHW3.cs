using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AnaglyphHW3 : MonoBehaviour
{
    public RenderTexture leftTexture;
    public RenderTexture rightTexture;
    public RenderTexture stereoTexture;
    public RawImage image;
    private Texture2D leftTexture2D;
    private Texture2D rightTexture2D;
    private Texture2D stereoTexture2D;
    // Start is called before the first frame update
    void Start()
    {
        stereoTexture2D = new Texture2D(leftTexture.width, leftTexture.height);
        leftTexture2D = new Texture2D(leftTexture.width, leftTexture.height);
        rightTexture2D = new Texture2D(rightTexture.width, rightTexture.height);
    }

    // Update is called once per frame
    void Update()
    {

        RenderTexture.active = leftTexture;
        leftTexture2D.ReadPixels(new Rect(0, 0, leftTexture.width, leftTexture.height), 0, 0);


        RenderTexture.active = rightTexture;
        rightTexture2D.ReadPixels(new Rect(0, 0, rightTexture.width, rightTexture.height), 0, 0);

        // iterate through every pixel in stereo texture2d object and 
        // set its pixel to (left texture's red, right texture's blue, right texture's green)
        for (int x = 0; x < leftTexture.width; x++)
        {
            for (int y = 0; y < leftTexture.height; y++)
            {
                Color leftPixel = leftTexture2D.GetPixel(x, y);
                Color rightPixel = rightTexture2D.GetPixel(x, y);


                Color stereoPixel = new Color(rightPixel.r, leftPixel.g, leftPixel.b, Mathf.Max(leftPixel.a, rightPixel.a));
                stereoTexture2D.SetPixel(x, y, stereoPixel);
            }
        }
        // Apply changes to stereo texture2d object (.Apply() method)
        stereoTexture2D.Apply();
        // set raw image texture to the stereo texture2d object
        image.texture = stereoTexture2D;
        // set active render texture to null
        RenderTexture.active = null;

    }
}
