using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControl : MonoBehaviour
{
    public Transform PlayerCamera;
    public float MaxDistance = 5;
    public static bool shipCanGo = false;
    public ParticleSystem anim;
    // Start is called before the first frame update
    void Start()
    {
        anim.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Pressed E");
            Pressed();

        }
    }

    void Pressed()
    {
        RaycastHit shipControlHit;
        if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out shipControlHit, MaxDistance))
        {
            Debug.Log(shipControlHit.transform.tag);
            // if raycast hits, then it checks if it hit an object with the tag Door.
            if (shipControlHit.transform.tag == "ShipControl")
            {
                //This line will get the Animator from  Parent of the door that was hit by the raycast.
                //anim = doorhit.transform.GetComponentInParent<Animator>();

                //This will set the bool the opposite of what it is.
                shipCanGo = true;
                anim.Play();
                Debug.Log("Ship can go!");
            }
        }
    }
}
