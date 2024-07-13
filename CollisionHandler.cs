using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource warningSound;
    void Start()
    {
        warningSound = Movement.warningSound;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<CapsuleCollider>() != null)
        {
            Debug.Log("Collision with capsule collider");
            if (warningSound != null && !warningSound.isPlaying)
            {
                Debug.Log("Playing warning sound");
                warningSound.Play();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
