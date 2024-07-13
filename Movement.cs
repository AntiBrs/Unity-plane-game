using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 10.0f;
    public static float MAX_SPEED = 100f;
    public static int color = 0; // blue
    Renderer rend;
    public Material[] materials;
    public MeshCollider meshCollider;
    public static AudioSource warningSound;
    public Collider capsule;
    public bool thirdPerson = false;
    public bool flashlight = true;
    public int MAXHEIGHT = 400;
    public int point = 0;
    // Use this for initialization
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.material = materials[color];
        meshCollider = GetComponent<MeshCollider>();
        capsule = GetComponent<CapsuleCollider>();
        warningSound = GetComponent<AudioSource>();
        Debug.Log("Plane Script Added " + gameObject.name);
        point = 0;

    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with object: " + collision.gameObject.name);

        if (collision.collider.GetComponent<CapsuleCollider>() != null)
        {
            Debug.Log("Collision with capsule collider");
            if (warningSound != null && !warningSound.isPlaying)
            {
                Debug.Log("Playing warning sound");
                warningSound.Play();
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("Terrain"))
            {
                Debug.Log("Collision with terrain");
                PauseMenu.isDead = true;
            }
            if (collision.gameObject.CompareTag("Gate"))
            {
                Debug.Log("Collision with Gate");
                ++point;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 moveCamTo = transform.position - transform.forward * 10.0f + transform.up * 5.0f;
        float bias = 0.80f;
        Camera.main.transform.position = Camera.main.transform.position * bias + moveCamTo * (1.0f - bias);

        if (thirdPerson)
        {
            Camera.main.transform.LookAt(transform.position + transform.forward * 30.0f + transform.up * 5.0f);
            moveCamTo = transform.position - transform.forward * 30.0f + transform.up * 5.0f;
        }
        else
        {
            Camera.main.transform.LookAt(transform.position + transform.forward * 3.0f);
        }

        if (flashlight)
        {
            GetComponentInChildren<Light>().enabled = true;
        }
        else
        {
            GetComponentInChildren<Light>().enabled = false;
        }

        if(point >=8)
        {
            PauseMenu.isWon = true;
            Debug.Log("Nyert");
        }

        transform.position += transform.forward * Time.deltaTime * speed;

        speed -= transform.forward.y * Time.deltaTime * 50.0f;

        if (speed != MAX_SPEED)
        {
            speed = MAX_SPEED;
        }

        transform.Rotate(Input.GetAxis("Vertical"), 0.0f, -Input.GetAxis("Horizontal"));
        Camera.main.transform.Rotate(Input.GetAxis("Vertical"), 0.0f, -Input.GetAxis("Horizontal"));

        float terrainHeightWhereWeAre = Terrain.activeTerrain.SampleHeight(transform.position);

        if (terrainHeightWhereWeAre > transform.position.y)
        {
            transform.position = new Vector3(
              transform.position.x,
              terrainHeightWhereWeAre,
              transform.position.z
            );
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
            thirdPerson = !thirdPerson;
            Debug.Log("Third person view toggled");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            flashlight = !flashlight;
        }
        if(transform.position.y > MAXHEIGHT)
        {
            PauseMenu.isDead = true;
        }
    }
}