using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatterManager : MonoBehaviour {
    public static SplatterManager instance;
    public SplatterScript splatterPrefab;
    public SplatScript splatPrefab;
    public List<SplatterScript> splatterList = new List<SplatterScript>();
    public List<SplatScript> splatList = new List<SplatScript>();

	// Use this for initialization
	void Awake () {
        if (instance==null) instance = this;
	}

    public void Reset()
    {
        splatterList.Clear();
        splatList.Clear();
    }







	// Update is called once per frame
    public void AddSplatter (Transform trn, Collision collision, Color color) 
    {

        SplatterScript splatter =  Instantiate(splatterPrefab);
        splatter.transform.position = trn.position;
        splatter.Init(color, true);
        bool willFit = true;
        float splatSize = 2f;
        Vector3 pos = collision.contacts[0].point;
        Vector3 normal = collision.contacts[0].normal;
        if (normal == collision.collider.transform.right || normal == -collision.collider.transform.right)
        {
            float diff = Mathf.Abs(Vector3.Dot(pos-collision.collider.transform.position,collision.collider.transform.up));
            float max = (collision.collider.transform.localScale.y-splatSize)/2;
            if (diff>max) willFit = false;
        } else if (normal == collision.collider.transform.up || normal == -collision.collider.transform.up)
        {
            float diff = Mathf.Abs(Vector3.Dot(pos-collision.collider.transform.position,collision.collider.transform.right));
            float max = (collision.collider.transform.localScale.x-splatSize)/2;
            if (diff>max) willFit = false;
        }
        else
        {
            willFit = false;
        }

        if (willFit)
        {
            SplatScript splat =  Instantiate(splatPrefab);
            splat.Init(collision.transform, normal, pos+normal*0.001f, color, 2f );
        }

        splatterList.Add(splatter);
        while (splatterList.Count>100) 
        {
            splatterList[0].Remove();
        }
	}

    public void RemoveSplatter (SplatterScript splatter) 
    {
        splatterList.Remove(splatter);
        if (splatter!=null) Destroy(splatter.gameObject);
    }


    public void AddSplat (SplatScript splat) 
    {
        splatList.Add(splat);
        while (splatList.Count>100) 
        {
            splatList[0].Remove();
        }
    }


    public void RemoveSplat (SplatScript splat) 
    {
        splatList.Remove(splat);
        if (splat!=null)    Destroy(splat.gameObject);
    }


}
