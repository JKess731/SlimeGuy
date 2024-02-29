using UnityEngine;

public class slimeShotgun : MonoBehaviour
{
    public Transform attackPointShotgun;
    public GameObject slimeBlob;
    public int bulletCount = 3;
    public float delay = 0.1f;

    public LookAtMouse1 ring;
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Quaternion left = ring.getRotation() * Quaternion.Euler(0f, 0f, 10f);
        Quaternion right = ring.getRotation() * Quaternion.Euler(0f, 0f, -10f); ;

        Instantiate(slimeBlob, attackPointShotgun.position, left);
        Instantiate(slimeBlob, attackPointShotgun.position, ring.getRotation());
        Instantiate(slimeBlob, attackPointShotgun.position, right);
    }
}
