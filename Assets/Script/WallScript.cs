using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class WallScript : MonoBehaviour 
{

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.SetTextureScale("_MainTex", new Vector2(transform.localScale.x, transform.localScale.y)*0.05f);
    }

}
