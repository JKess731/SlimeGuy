using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

//Brandon Sheley

public class slimeTrail : MonoBehaviour
{

    [SerializeField] GameObject trailObject;
    [SerializeField] GameObject Puddles;
    characterMovement movement;

    private List<GameObject> trailObjects = new List<GameObject>();
    private GameObject lastTrail;
    private Vector3 lastPosition = new Vector3 ();


    //On start get the player characters movement.
    void Start()
    {
        movement = GetComponent<characterMovement>();
    }

    // Update is called once per frame
    void Update()
    {

        //On button press send Puddles to the other end of his slime trail.
        if (Input.GetKeyDown(KeyCode.Space) && trailObjects.Count >= 0)
        {
            //Original possible method. Is supposed to find the location of the earliest trailObject in the list, and send Puddles to that 
            //location.
            //Vector3 trailStart = trailObjects[0].transform.position;
            //Puddles.transform.position = trailStart;


            //Other Method. Is supposed to track for the trailObject furthest away from Puddles and send Puddles to that location.

            //float furthestTrail = 0f;
            //GameObject furthestSlimePatch = null;
            //foreach (GameObject trailObject in trailObjects) 
            //{ 
                //float distance = Vector3.Distance(Puddles.transform.position, trailObject.transform.position);
                //if (distance > furthestTrail) 
                //{ 
                    //furthestTrail = distance;
                    //furthestSlimePatch = trailObject;

                //}
            //}
            //Puddles.transform.position = furthestSlimePatch.transform.position;

        }


        //Tracks if Puddles has moved by checking his current position and last known position.
        if (Puddles.transform.position != lastPosition)
        {
            //If Puddles has moved/is moving stops the code for deleting all trails. If there is no current trail instantiates the first 
            //patch of one. If there is checks the
            StopCoroutine(DeleteAllTrails());
            if (lastTrail != null)
            {
                if (Vector3.Distance(transform.position, lastTrail.transform.position) >= .5)
                {
                    lastTrail = Instantiate(trailObject);
                    trailObjects.Add(lastTrail);
                    trailObject.transform.position = transform.position;
                    DeleteLastTrail();
                }
            }
            else
            {
                lastTrail = Instantiate(trailObject);
                trailObjects.Add(lastTrail);
                trailObject.transform.position = transform.position;
            }
            lastPosition = Puddles.transform.position;
        }
        else
        {
            for (int i = 0; i < trailObjects.Count; i++)
            {
                //StartCoroutine(DeleteAllTrails());
            }
        }

        
    }

    IEnumerator DeleteAllTrails()
    {
        GameObject removeTrail = trailObjects[0];
        trailObjects.RemoveAt(0);
        Destroy(removeTrail);

        yield return new WaitForSeconds(0.25f);
    }

    private void DeleteLastTrail() 
    {
        if (trailObjects.Count > 10) 
        {
            GameObject removeTrail = trailObjects[0];
            trailObjects.RemoveAt(0);
            Destroy(removeTrail);
        }
    }

    
}
