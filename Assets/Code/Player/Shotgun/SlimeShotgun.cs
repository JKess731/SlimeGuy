using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class SlimeShotgun : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LookAtMouse ring;
    [SerializeField] private BasicShotgun basicShotgunSO;

    //private float timer = 0;

    private FMODUnity.StudioEventEmitter eventEmitterRef;
    private void Awake()
    {
        eventEmitterRef = GetComponent<FMODUnity.StudioEventEmitter>();
    }

    public void Update()
    {
        //if (Input.GetMouseButton(0) && timer < 0)
        //{
        //    eventEmitterRef.Play();
        //    Shoot();
        //    timer = basicShotgunSO.cooldown;
        //}
        //timer -= Time.deltaTime; 
    }

    private void Shoot()  
    {
        basicShotgunSO.Activate(ring.getRotation(), attackPoint.position);
    }

    //Draws the cone spread rays of the bullets in the editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector2 test1 = Quaternion.AngleAxis(basicShotgunSO.spreadAngle, Vector3.forward) * attackPoint.right;
        Gizmos.DrawRay(attackPoint.position, test1 * 5);

        Vector2 test2 = Quaternion.AngleAxis(-basicShotgunSO.spreadAngle, Vector3.forward) * attackPoint.right;
        Gizmos.DrawRay(attackPoint.position, test2 * 5);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(attackPoint.position, attackPoint.right * 5);
    }
}
