using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ediosn Li
//Makes object look at Mouse
public class LookAtMouse : MonoBehaviour
{
    void Update()
    {
        //Vector3 lookDir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        //float lookAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(lookAngle, Vector3.forward);

        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.WorldToScreenPoint(mousePosition);
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x,
                                        mousePosition.y - transform.position.y);

        transform.up = direction;
    }
}
