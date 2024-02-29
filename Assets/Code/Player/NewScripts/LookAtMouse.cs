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

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePosition - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    public Quaternion getRotation()
    {
        return transform.rotation;
    }
}
