using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> gameObjects = new List<GameObject>();
    void Start()
    {
        
    }
    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Utkozes!!!!!!!!!!!!!!!!!!!!!!!!" + collision.gameObject.name);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
