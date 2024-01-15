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


    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<characterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && lastTrail != null)
        {
            Debug.Log("Diving");
            Puddles.transform.position = trailObjects[0].transform.position;
        }


        if (Puddles.transform.position != lastPosition)
        {
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
