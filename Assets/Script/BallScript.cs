using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {
    Vector3 startPos, dragPos;
    public Rigidbody rBody;
    public Collider c;
    public Renderer render;
    public Color color;
    public int fingerID;
    public int bounces;
	// Use this for initialization
    public float radius = 0.5f;
  
    public void Init (Vector3 pos, Color color) {
        this.color = color;
        transform.localScale = Vector3.one*radius*2;
        startPos = pos;
        if (Mathf.Abs(startPos.x)>7-radius) startPos.x = (7-radius)*Mathf.Sign(startPos.x);
        if (Mathf.Abs(startPos.z)>12-radius) startPos.z = (12-radius)*Mathf.Sign(startPos.z);
        transform.position  = startPos + Vector3.up*radius;
        pos.z = -Camera.main.transform.position.y;
        startPos = Camera.main.ScreenToWorldPoint(pos);
        render.material.SetColor("_Color", color);
        render.material.SetColor("_EmissionColor", color*0.1f);
        bounces = 3;
        rBody.isKinematic = true;
	}

    public void TouchBegan (Vector3 pos)
    {
        pos.z = -Camera.main.transform.position.y;
        startPos = Camera.main.ScreenToWorldPoint(pos);
    }

    public void TouchDrag (Vector3 pos)
    {
        pos.z = -Camera.main.transform.position.y;
        dragPos = Camera.main.ScreenToWorldPoint(pos);
    }

    public void TouchEnded ()
    {
        Vector3 force = (dragPos-startPos)*0.1f;
        c.enabled = true;
        rBody.isKinematic = false;
        rBody.AddForce(force);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "wall" || collision.collider.tag == "sphere" )
        {
            if (collision.collider.tag == "sphere") bounces = 0;
            bounces--;
            if (bounces <=0)
            {
                SplatterManager.instance.AddSplatter(transform, collision, color);
                Destroy(gameObject);
            }
            
        }
    }


    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "goal")
        {
            Destroy(gameObject);
        }
    }



}
