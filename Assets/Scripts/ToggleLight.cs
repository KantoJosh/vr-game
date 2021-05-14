using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLight : MonoBehaviour
{
    public Light torchLight;
    public Transform torch;
    public Transform player;
    public ParticleSystem sparkles;
    float distanceLimit = 3.5f;
    //private bool pressed = false;
    // Start is called before the first frame update
    private ParticleSystem.MainModule sparklesmain;
    void Start()
    {
        //sparklesmain = sparkles.main;
        //sparklesmain.startColor = torchLight.color;
        //torchLight.color = new Color(Random.Range(0F, 1F), Random.Range(0F, 1F), Random.Range(0F, 1F));

        sparkles.Pause();
        torchLight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        bool withinDistance = Vector3.Distance(player.position, torch.position) < distanceLimit;
        if (withinDistance)
        {
            torchLight.enabled = true;
            //pressed = true;
            sparkles.Play();
            Debug.Log("Turned on");
        }
        else
        {
            Debug.Log("Turned off");
        }
    }
}
