using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

//Brandon Sheley

public class SlimeTrail : MonoBehaviour
{

    [SerializeField] GameObject trailObject;
    [SerializeField] GameObject Puddles;
    public PlayerMove playerMove;
    
    private List<GameObject> trailObjects = new List<GameObject>();
    private GameObject lastTrail;
    private Vector3 lastPosition = new Vector3 ();
    private bool trailCheck = true;
    private bool dive = false;
    private int diveCheck = 0;
    private int trailCount = 0;

    private FMODUnity.StudioEventEmitter eventEmitterRef;
    private void Awake()
    {
        eventEmitterRef = GetComponent<FMODUnity.StudioEventEmitter>();
    }
    //On start get the player characters movement, and set lastPosition to Puddles position to prevent issues with other code on start.
    void Start()
    {
        lastPosition = Puddles.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        eventEmitterRef.Play();
        //On button press send Puddles to the other end of his slime trail. Can press again to leave slime trail and stop diving.
        if (UnityEngine.Input.GetKeyDown(KeyCode.F) && trailObjects.Count > 0)
        {
            if(dive == false){
                dive = true;
                
                trailCount = trailObjects.Count;
                diveCheck = trailCount;
                Debug.Log(diveCheck);
                StartCoroutine(DiveTrail());
            }
            else{
                dive = false;
                
                trailCount = 0;
                diveCheck = 0;
                StopCoroutine(DiveTrail());
                playerMove.input.Enable();
            }
        }


        //On update tracks if Puddles has moved by checking his current position and last known position. If Puddles has moved instantiates
        //a trail patch behind Puddles and adds it to a list. 
        if (Puddles.transform.position != lastPosition && trailCheck == true)
        {
            
            //If Puddles has moved/is moving stops the code for deleting all trails. If there is no current trail instantiates the first 
            //patch of one. If there is checks the
            StopCoroutine(DeleteAllTrails());
            if (lastTrail != null)
            {
                if (Vector3.Distance(transform.position, lastTrail.transform.position) >= .05)
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

    IEnumerator DiveTrail() {
        while (dive == true){
            playerMove.input.Disable();
            diveCheck--;
            
            Vector3 TrailStart = trailObjects[diveCheck].transform.position;
            
            trailCheck = false;
            Puddles.transform.position = TrailStart;
            lastPosition = Puddles.transform.position;
            
            trailCheck = true;
            if (diveCheck <= 0) 
            {
                dive = false;
                
                trailCount = 0;
                diveCheck = 0;
                StopCoroutine(DiveTrail());
                
            }
            yield return new WaitForSeconds(.03f); // Change trail speed
        }
        playerMove.input.Enable();
    }





    //Deletes all trails when called in the for loop above. 
    IEnumerator DeleteAllTrails()
    {
        GameObject removeTrail = trailObjects[0];
        trailObjects.RemoveAt(0);
        Destroy(removeTrail);

        yield return new WaitForSeconds(.05f);
    }

    //Deletes the trail patch at [0] in the list when called if the list count is greater then 10.
    private void DeleteLastTrail() 
    {
        if (trailObjects.Count > 30) 
        {
            GameObject removeTrail = trailObjects[0];
            trailObjects.RemoveAt(0);
            Destroy(removeTrail);
        }
    }

    
}
