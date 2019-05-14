using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatterScript : MonoBehaviour {
    public ParticleSystem ps;
    public Renderer splatterRenderer;
    public SplatScript splatPrefab;
    private ParticleSystem.MainModule main;
    private Color color;
    private bool collide;
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    public void Init (Color color, bool collide) {
        main = ps.main;
        this.collide = collide;
        this.color = color;
        main.startColor = color;
       
        ps.Emit(10);

        ParticleSystemRenderer psrenderer = GetComponent<ParticleSystemRenderer>();
        splatterRenderer.material.SetColor("_Color", color);
        splatterRenderer.material.SetColor("_EmissionColor", color*0.25f);
        psrenderer.material.SetColor("_Color", color);
        psrenderer.material.SetColor("_EmissionColor", color*0.5f);


  //      transform.Rotate(transform.forward, Random.value*360);
        transform.SetParent(GameManager.instance.world);
        StartCoroutine("Fade");
    }


    public void Remove()
    {
        StopCoroutine("Fade");
        SplatterManager.instance.RemoveSplatter(this);
    }

    IEnumerator Fade ()
    {
        float elapsedTime = 0f;
        float duration = 0.5f;
        while (elapsedTime<duration)
        {
            splatterRenderer.transform.localScale = Vector3.one*RSLerp.EaseOutCubic(3f, 0.001f, elapsedTime, duration);
            yield return new WaitForEndOfFrame();
            elapsedTime = Mathf.Min(duration, elapsedTime + Time.deltaTime);
        }

        yield return new WaitForSeconds(4.5f);

        Remove();

    }


    void OnParticleCollision(GameObject other)
    {
        if (!collide) return;
        int numCollisionEvents = ps.GetCollisionEvents(other, collisionEvents);
        int i = 0;

        while (i < numCollisionEvents)
        {
            bool willFit = true;
            float splatSize = Random.Range(0.1f, 0.5f);

            Vector3 pos = collisionEvents[i].intersection;
            Vector3 offset = pos-other.transform.position;
            Vector3 normal = collisionEvents[i].normal;
            Vector3 correction = Vector3.zero;

            if (normal == other.transform.right || normal == -other.transform.right)
            {
                float diff = Mathf.Abs(Vector3.Dot(pos-other.transform.position, other.transform.up));
                float max = (other.transform.localScale.y-splatSize)/2;

                correction = normal * (Vector3.Dot(offset, normal)-other.transform.localScale.x/2);
                if (diff>max) willFit = false;
            } 
            else if (normal == other.transform.up || normal == -other.transform.up)
            {
                float diff = Mathf.Abs(Vector3.Dot(pos-other.transform.position, other.transform.right));
                float max = (other.transform.localScale.x-splatSize)/2;
                correction = normal * (Vector3.Dot(offset, normal)-other.transform.localScale.y/2);
                if (diff>max) willFit = false;
            }
            else if (normal == other.transform.forward || normal == -other.transform.forward)
            {
                float diffY = Mathf.Abs(Vector3.Dot(pos-other.transform.position, other.transform.up));
                float maxY = (other.transform.localScale.y-splatSize)/2;
                if (diffY>maxY) willFit = false;
                float diffX = Mathf.Abs(Vector3.Dot(pos-other.transform.position, other.transform.right));
                float maxX = (other.transform.localScale.x-splatSize)/2;
                correction = normal * (Vector3.Dot(offset, normal)-other.transform.localScale.z/2);
                if (diffX>maxX) willFit = false;
            }
            else
            {
                willFit = true;
            }

            if (willFit)
            {
                SplatScript splat =  Instantiate(splatPrefab);
                splat.Init(other.transform, normal, pos-correction + normal*0.001f, color, splatSize );

            }

            i++;
        }
    }
}
