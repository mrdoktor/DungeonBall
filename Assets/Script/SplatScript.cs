using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatScript : MonoBehaviour {
    public Renderer renderer1, renderer2;
    public Transform trn;
    public float duration = 5f;
    private Vector3 pos, normal;
    private Color color;
    private float size;

    public void Init (Transform parent, Vector3 normal, Vector3 pos, Color color, float size) {
        this.color = color;
        this.normal = normal;
        this.size = size;
        renderer1.material.color = color;
        if (renderer2!=null) renderer2.material.color = color;
        transform.position = pos;
        transform.localScale = Vector3.one*size;
        transform.up = normal;
        transform.RotateAround(pos, transform.up, Random.value*360);
        transform.SetParent(parent.parent);
   

        renderer1.material.SetColor("_Color", color);
        renderer1.material.SetColor("_EmissionColor", color*0.125f);
        renderer1.material.renderQueue = 2999;
        StartCoroutine("Fade");
        SplatterManager.instance.AddSplat(this);

	}


    public void Remove()
    {
        StopCoroutine("Fade");
        SplatterManager.instance.RemoveSplat(this);
    }

    IEnumerator Fade ()
    {
        float elapsedTime = 0f;

        Vector3 startPos = transform.position;
        if (trn!=null) {
            startPos = trn.position;
        //    size = trn.localScale.x*0.5f;

        }

        Vector3 endPos = startPos + Vector3.down*size*0.5f;
        if (normal.y!=0) endPos = startPos;
        while (elapsedTime<duration)
        {
            color.a = RSLerp.EaseInOutCubic(1,0, elapsedTime, duration);
            Color emissionColor = RSLerp.EaseInOutCubic(color*0.25f,Color.black, elapsedTime, duration);
            if (trn!=null)
            {
                trn.position = RSLerp.EaseOutCubic(startPos,endPos, elapsedTime, duration);
                trn.localScale = Vector3.one*RSLerp.EaseOutCubic(0.75f,1.25f, elapsedTime, duration);
                renderer2.material.SetColor("_Color", color);
                renderer2.material.SetColor("_EmissionColor", emissionColor);
            }

            renderer1.material.SetColor("_Color", color);
            renderer1.material.SetColor("_EmissionColor", emissionColor);
            yield return new WaitForEndOfFrame();
            elapsedTime = Mathf.Min(duration, elapsedTime + Time.deltaTime);
        }

        Remove();

    }
	
}
