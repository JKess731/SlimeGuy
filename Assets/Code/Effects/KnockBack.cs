using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * This script is responsible for the knockback effect.
 * Youtube Video: https://www.youtube.com/watch?v=Jy1yXbKYW68
 * */

public class KnockBack : MonoBehaviour
{
    [Header("Knockback Variables")]
    [SerializeField] private float _knockBackTime = 0.2f;
    [SerializeField] private float _constForce = 0.25f;
    [SerializeField] private float _delayTime = 0.1f;

    [Header("Knockback Curves")]
    [SerializeField] private AnimationCurve _knockbackHitForceCurve;
    [SerializeField] private AnimationCurve _knockbackStoppingForceCurve;

    private Rigidbody2D _rb2D;
    private Coroutine knockbackCor;
    public bool isBeingKnockedBack { get; set;}

    private void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }

    public IEnumerator KnockbackAction(Vector2 hitDir, float hitDirForce,Vector2 constantForceDirection)
    {

        Vector2 hitForce;
        Vector2 constantForce;
        Vector2 knockbackForce;
        float time = 0;

        
        constantForce = constantForceDirection * _constForce;
        
        isBeingKnockedBack = true;

        float elapedTimer = 0;
        while (elapedTimer < _knockBackTime)
        {
            //Iterates the timer
            elapedTimer += Time.deltaTime;
            time = elapedTimer / _knockBackTime;

            //Calculates the hit force
            hitForce = hitDir * hitDirForce * _knockbackHitForceCurve.Evaluate(time);

            //Combine _hitForce and _constantForce
            knockbackForce = hitForce + constantForce;
            
            //Applies the knockback force
            _rb2D.velocity = knockbackForce;

            yield return new WaitForFixedUpdate();
        }
    }

    public IEnumerator StopKnockback()
    {
        float time = 0;

        Vector2 currentVelocity = _rb2D.velocity;

        while (time < _delayTime)
        {
            time += Time.deltaTime;
            _rb2D.velocity = currentVelocity * _knockbackStoppingForceCurve.Evaluate(time);

            yield return new WaitForFixedUpdate();

        }
        isBeingKnockedBack = false;
    }

    /// <summary>
    /// Calls the knockback coroutine
    /// @param hitDirection: The direction the player is hit
    /// @param constantForceDirection: The constant force direction
    /// @param inputDirection: The input direction of the player
    /// </summary>
    /// <param name="hitDirection"></param>
    /// <param name="constantForceDirection"></param>
    /// <param name="inputDirection"></param>
    //Couroutine is a monobehavior method
    public void CallKnockback(Vector2 hitDirection, float hitForce, Vector2 constantForceDirection)
    {
        if (knockbackCor != null)
        {
            StopCoroutine(knockbackCor);
            StartCoroutine(StopKnockback());
        }

        knockbackCor = StartCoroutine(KnockbackAction(hitDirection, hitForce, constantForceDirection));
    }
}
