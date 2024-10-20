using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Attacks
{
    private PlayerController _playerController;
    private float _activationTime;
    private float _dashSpeed;

    private void Awake()
    {
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private IEnumerator DashCoroutine()
    {
        yield return new WaitForSeconds(_activationTime);
    }
}
