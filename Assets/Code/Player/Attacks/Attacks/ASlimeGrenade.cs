using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASlimeGrenade : MonoBehaviour, IAttackTriggerable
{
    [SerializeField] private float activationTime;
    private bool canActivate = true;

    [SerializeField] private GameObject grenadePrefab;
    [SerializeField] private GameObject trigger;
    [SerializeField] private float grenadeSpeed = 4f;
    [SerializeField] private GameObject greandeSpawnPosObj;
    [SerializeField] private float distance = 2.5f;
    [HideInInspector] public bool grenadeLanded = false;
    [HideInInspector] public Vector3 landingPos;

    private GameObject player;
    private Vector3 direction;
    private Vector3 targetPos;
    
    private void Awake()
    {
        player = GameObject.FindWithTag("player");
        trigger.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (canActivate)
        {
            #region Rotation
            Camera cam = Camera.main;
            Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            direction = mousePos - player.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, angleAxis, Time.deltaTime * 50);

            targetPos = new Vector3(direction.normalized.x - distance, direction.normalized.y - distance);
            #endregion

            HandleInput();
        }
    }

    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(OnActivate(activationTime));
        }
    }

    public void HandleTriggers(GameObject enemy)
    {
        
    }

    private GameObject SpawnGrenade()
    {
        GameObject grenadeGameObj = Instantiate(grenadePrefab);

        grenadeGameObj.AddComponent<Grenade>().Init(targetPos, grenadeSpeed, this);
        grenadeGameObj.transform.position = greandeSpawnPosObj.transform.position;

        return grenadeGameObj;
    }
    
    public IEnumerator OnActivate(float activationTime)
    {
        canActivate = false;
        GameObject grenade = SpawnGrenade();

        yield return new WaitUntil(() => grenadeLanded == true);

        trigger.transform.position = landingPos;
        trigger.SetActive(true);
        yield return new WaitForSeconds(activationTime);
        grenadeLanded = false;
        trigger.SetActive(false);
        canActivate = true;

        Debug.Log("grenade landed!");
    }
}
