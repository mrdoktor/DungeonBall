using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droplet : MonoBehaviour {
    public Renderer drop, shine;
    private DropColor dropColor;
    private int delay;
    public float offset;

    void Start()
    {

    }
    public Vector3 position
    {
        get
        {
            return transform.position;
        }
        set
        {
            transform.position = value + offset*transform.right;
        }
    }

    public void SetColor(DropColor dropColor, int delay)
    {
        this.dropColor = dropColor;
        this.delay = delay;
        if (gameObject.activeInHierarchy) StartCoroutine("SetColor_CR");
    }


    IEnumerator SetColor_CR()
    {
        Color c =  Color.white;
//        c.a = (dropColor.muddy)? 0.25f:1f;
                c.a = 0.5f;
        while (delay>0)
        {
            
            yield return new WaitForEndOfFrame();
            delay--;
        }

        drop.material.SetColor("_Color", dropColor.color);
        drop.material.SetColor("_EmissionColor", dropColor.color*0.5f);
        shine.material.SetColor("_Color", c);
    }

}
