using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yrotation : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(-1000,target.eulerAngles.y,0);
    }
}
