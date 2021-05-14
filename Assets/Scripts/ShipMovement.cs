using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public Transform ship;
    public Transform player;
    public Transform earth;
    //float distanceLimit = 4f;
    public float speed = 2.5f; // max 5
    private bool keydown = false;
    private float rot = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ShipControl.shipCanGo)
        {
            keydown = true;
        }

        if (keydown && rot < 45)
        {
            rotateToEarth();
        }
        else if (keydown)
        {
            moveToEarth();
        }
    }


    void rotateToEarth()
    {
        ship.Rotate(new Vector3(0, 10 * Time.deltaTime, 0));
        rot += 10f * Time.deltaTime;

    }

    void moveToEarth()
    {
        Debug.Log("Moving ship");
        ship.position = Vector3.MoveTowards(ship.position, earth.position, speed * Time.deltaTime);
    }
}
