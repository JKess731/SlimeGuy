using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class SwipeMono : AbilityMonoBase
{
    [Header("Swipe Attributes")]
    [SerializeField] private GameObject _swipe;

    [SerializeField] private float _pushRange = 5f;   // The range to push the player forward
    [SerializeField] private float _pushSpeed = 10f;  // Speed of the player's forward push
    [SerializeField] private float _swipeStartingPoint;

    [Header("Prefab Attributes")]
    [SerializeField] private int _swipeDamage;
    [SerializeField] private float _swipeKnockback;
    [SerializeField] private float _activationTime;
    [SerializeField] private float _swipeSpeed;
    [SerializeField] private float _swipeRange;

    private Rigidbody2D _playerRb;  // Rigidbody of the player
    private Vector2 _pushDirection; // Direction to push the player
    private Vector2 _startPos;
    private Vector2 _pushVector;

    public override void Initialize()
    {
        base.Initialize();
        _playerRb = GameObject.FindWithTag("player").GetComponent<Rigidbody2D>();
    }

    public override void StartBehavior(Vector2 attackPosition, Quaternion rotation)
    {
        AbilityState = AbilityState.STARTING;
        
        Quaternion swipeRotation = rotation * Quaternion.Euler(0, 0, _swipeStartingPoint);

        Vector2 vecForAng = rotation * Vector2.right;
        _pushDirection = vecForAng; // Use the direction of the swipe

        _startPos = _playerRb.transform.position;
        _pushVector = _pushDirection * _pushSpeed;

        GameObject newSwipe = Instantiate(_swipe, attackPosition, swipeRotation);
        newSwipe.GetComponent<Swipe>().Initialize(_swipeDamage, _swipeKnockback, _activationTime, _swipeSpeed, _swipeRange);

        // Push the player forward
        StartCoroutine(PushPlayerForward());
        StartCoroutine(Cooldown());
    }

    public override void PerformBehavior(Vector2 attackPosition, Quaternion rotation) { }

    public override void CancelBehavior(Vector2 attackPosition, Quaternion rotation) { }

    public override void Upgrade(StatsSO playerstats, StatsEnum stat)
    {
        switch (stat)
        {
            //TODO: Implement upgrade logic
        }
    }

    public override IEnumerator Cooldown()
    {
        //diag.Stopwatch stopWatch = new diag.Stopwatch();
        //stopWatch.Start();
        yield return new WaitForSeconds(_activationTime);

        //Debug.Log("Cooldown Started: " + _cooldownTime);
        _abilityState = AbilityState.COOLDOWN;

        yield return new WaitForSeconds(_cooldownTime);


        // Get the elapsed time as a TimeSpan value.
        //stopWatch.Stop();
        //TimeSpan ts = stopWatch.Elapsed;

        // Format and display the TimeSpan value.
        //string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
        //ts.Hours, ts.Minutes, ts.Seconds,
        //ts.Milliseconds / 10);
        //Debug.Log("RunTime " + elapsedTime);

        _abilityState = AbilityState.READY;
        //Debug.Log("Cooldown Finished");
    }

    private IEnumerator PushPlayerForward()
    {
        Debug.Log("velocity:" + _playerRb.velocity);
        Debug.Log("push vector:" + _pushVector);
        Debug.Log("startPos:" + _startPos);
        Debug.Log("playerPos:" + _playerRb.transform.position);
        Debug.Log(Vector2.Distance(_startPos, _playerRb.transform.position));

        // Continue pushing the player forward until the range is reached
        while (Vector2.Distance(_startPos, _playerRb.transform.position) < _pushRange)
        {
            Debug.Log("Pushing player forward...");
            // Apply a force in the forward direction
            _playerRb.AddForce(_pushVector * _pushSpeed, ForceMode2D.Force);
            yield return new WaitForFixedUpdate();
        }

        Debug.Log("Player pushed forward");
        // Stop player's movement after the push is complete
        _playerRb.velocity = Vector2.zero;
    }
}
