using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MeteorCollision : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;
    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }
    private void OnParticleCollision(GameObject other)
    {
        int nullCollision = part.GetCollisionEvents(other, collisionEvents);
        int i = 0;
        while (i < nullCollision)
        {
            GameObject.Destroy(Instantiate((GameObject)AssetDatabase.LoadAssetAtPath("Assets/Explosion.prefab", typeof(GameObject)), collisionEvents[i].intersection, Quaternion.LookRotation(transform.up)), 8f);
            Collider[] HitByMeteor = Physics.OverlapSphere(collisionEvents[i].intersection, 60f);
            foreach(Collider c in HitByMeteor)
            {
                if(c.transform.name == "Plane")
                {
                    PauseMenu.isDead = true;
                }
            }

            ++i;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
