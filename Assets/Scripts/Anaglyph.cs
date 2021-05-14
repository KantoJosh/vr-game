
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Anaglyph : MonoBehaviour
{

    public Shader fxShader;
    public Camera cam2;
    public float stereoWidth = 1.0f;
    public float eyeWidth = 0.06f;//0.2f; // Used for experimental purposes.

    private Material mat;
    private RenderTexture rt;

    private void Start()
    {
        //cam2.CopyFrom(Camera.main);
        // Adjust camera y-angles based on stereo width.
        transform.localEulerAngles = Vector3.up * stereoWidth;
        cam2.transform.localEulerAngles = Vector3.up * -stereoWidth;

        // Distance between eyes
        transform.localPosition = Vector3.right * eyeWidth;
        cam2.transform.localPosition = Vector3.right * -eyeWidth;
    }

    private void OnEnable()
    {
        // Prevent errors.
        int w = Screen.width, h = Screen.height;
        Debug.Log("Screen size: " + w.ToString() +"x"+h.ToString());
        if (fxShader == null || w == 0 || h == 0)
        {
            enabled = false;
            return;
        }

        // Initialise materials used for blitting.
        mat = new Material(fxShader);
        mat.hideFlags = HideFlags.HideAndDontSave;
        cam2.enabled = false;

        // Initialse render texture.
        rt = new RenderTexture(w, h, 8, RenderTextureFormat.Default);
        cam2.targetTexture = rt;
    }

    private void OnDisable()
    {
        // Clean up resources.
        if (mat != null) { DestroyImmediate(mat); }
        if (rt != null) { rt.Release(); }
        cam2.targetTexture = null;
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (cam2 == null || mat == null || rt == null)
        {
            enabled = false;
            return;
        }

        // Render to render texture
        cam2.Render();

        // Apply second texture to shader. ("_MainTex" is automatically applied by Unity3D)
        mat.SetTexture("_MainTex2", rt);

        // Blit !
        Graphics.Blit(source, destination, mat);

        // Clean up RenderTexture resources. (Not sure if this is required???)
        rt.Release();
    }
}
