using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stun", menuName = "Status/Stun")]
public class Stun : StatusSO
{
    [SerializeField] float stunPercentage;
    public override void Initialize()
    {
        throw new System.NotImplementedException();
    }
    public override IEnumerator Apply(GameObject target)
    {
        float localTimer = _Timer;
        Vector2 targetOriginalVelocity = target.GetComponent<Rigidbody2D>().velocity;
        Debug.Log("Before stun,target's velocity is: (" + target.GetComponent<Rigidbody2D>().velocity.x + ", " + target.GetComponent<Rigidbody2D>().velocity.y + ").");
        target.GetComponent<Rigidbody2D>().velocity *= new Vector2(stunPercentage, stunPercentage);
        Debug.Log("After stun,target's velocity is: (" + target.GetComponent<Rigidbody2D>().velocity.x + ", " + target.GetComponent<Rigidbody2D>().velocity.y + ").");
        while (localTimer > 0)
        {
            Debug.Log("I am at the start of while stun, and timer = " + localTimer.ToString());
            localTimer -= 1;
            yield return new WaitForSeconds(1);
        }
        target.GetComponent<Rigidbody2D>().velocity = targetOriginalVelocity;
    }
}
