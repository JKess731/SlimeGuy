using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGizmo : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawRay(transform.position, transform.right* 5);
    }
}
