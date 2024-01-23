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
    private bool trailCheck = true;


    //On start get the player characters movement, and set lastPosition to Puddles position to prevent issues with other code on start.
    void Start()
    {
        movement = GetComponent<characterMovement>();
        lastPosition = Puddles.transform.position;
    }

    // Update is called once per frame and every second I want to die.
    void Update()
    {

        //On button press send Puddles to the other end of his slime trail.
        if (Input.GetKeyDown(KeyCode.Space) && trailObjects.Count > 0)
        {
            //ORIGINAL POSSIBLE METHOD. IS SUPPOSED TO FIND THE LOCATION OF THE EARLIEST TRAILOBJECT IN THE LIST, AND SEND PUDDLES TO THAT 
            //LOCATION.
            Debug.Log(trailObjects[0]);
            Vector3 TrailStart = trailObjects[0].transform.position;
            trailCheck = false;
            Debug.Log("Move Puddles");
            Puddles.transform.position = TrailStart;
            Debug.Log("Last Pos Update");
            lastPosition = Puddles.transform.position;
            trailCheck = true;


            //OTHER METHOD. IS SUPPOSED TO TRACK FOR THE TRAILOBJECT FURTHEST AWAY FROM PUDDLES AND SEND PUDDLES TO THAT LOCATION.

            //float FurthestTrail = 0F;
            //GameObject FurthestSlimePatch = null;
            //foreach (GameObject trailObject in trailObjects)
            //{
            //    float DISTANCE = Vector3.Distance(Puddles.transform.position, trailObject.transform.position);
            //    if (DISTANCE > FurthestTrail)
            //    {
            //        FurthestTrail = DISTANCE;
            //        FurthestSlimePatch = trailObject;

            //    }
            //}
            //trailCheck = false;
            //Debug.Log("Move Puddles");
            //Puddles.transform.position = FurthestSlimePatch.transform.position;
            //Debug.Log("Last Pos Update");
            //lastPosition = Puddles.transform.position;
            //trailCheck = true;

        }


        //On update tracks if Puddles has moved by checking his current position and last known position. If Puddles has moved instantiates
        //a trail patch behind Puddles and adds it to a list. 
        if (Puddles.transform.position != lastPosition && trailCheck == true)
        {
            Debug.Log("New Trail Check");
            //If Puddles has moved/is moving stops the code for deleting all trails. If there is no current trail instantiates the first 
            //patch of one. If there is checks the
            StopCoroutine(DeleteAllTrails());
            if (lastTrail != null)
            {
                if (Vector3.Distance(transform.position, lastTrail.transform.position) >= 1)
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

    //Deletes all trails when called in the for loop above. 
    IEnumerator DeleteAllTrails()
    {
        GameObject removeTrail = trailObjects[0];
        trailObjects.RemoveAt(0);
        Destroy(removeTrail);

        yield return new WaitForSeconds(0.25f);
    }

    //Deletes the trail patch at [0] in the list when called if the list count is greater then 10.
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
