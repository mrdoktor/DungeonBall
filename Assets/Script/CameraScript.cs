using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[ExecuteInEditMode]
public class CameraScript : MonoBehaviour {
  public float fWidth = 20.0f; // Desired width
  private float aspect;
    public Canvas canvas;

  private void Start() {
    aspect = 0;
    Update();
  }

  void Update() {
    float newAspect = Screen.width * 1.0f / Screen.height;
    newAspect = Mathf.Min(newAspect, 9.0f/16.0f); 
    if (aspect == newAspect) return;
    aspect = newAspect;
//    Debug.Log("aspect:" + aspect);
    float fT = fWidth / aspect;
    fT = fT / (2.0f * Mathf.Tan(0.5f * Camera.main.fieldOfView * Mathf.Deg2Rad));
        Vector3 v3T = Camera.main.transform.localPosition;
    v3T.z = -fT;
//   v3T.y = 2.25f;
        transform.localPosition = v3T;
    //canvas.planeDistance = Mathf.Abs(v3T.z);
  }
}