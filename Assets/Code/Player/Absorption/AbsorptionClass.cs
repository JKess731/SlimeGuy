using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Edison Li
//Code Credit to BMo
//https://www.youtube.com/watch?v=VWaiU7W5HdE

public class AbsorptionClass : MonoBehaviour
{
    //Two states of absorption
    public bool keyAbsorption;
    public bool dashAbsorb;

    public Collider2D absorbDash;
    public Collider2D absorbClick;

    private FMODUnity.StudioEventEmitter eventEmitterRef;
    private void Awake()
    {
        eventEmitterRef = GetComponent<FMODUnity.StudioEventEmitter>();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            eventEmitterRef.Play();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            eventEmitterRef.Play();
        }

    }

    private IEnumerator Click()
    {
        absorbClick.enabled = true;
        yield return new WaitForSeconds(0.5f);
        absorbClick.enabled = false;
    }
}
