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
    [SerializeField] private float _knockBackTime = 0.2f;       //The time the knockback lasts
    [SerializeField] private float _constForce = 0.25f;         //The constant force applied to the player
    [SerializeField] private float _releaseTime = 0.1f;         //The time it takes for the player to stop being knocked back

    [Header("Knockback Curves")]
    [SerializeField] private AnimationCurve _knockbackHitForceCurve;            //The curve for the hit force
    [SerializeField] private AnimationCurve _knockbackStoppingForceCurve;       //The curve for the stopping force

    private Rigidbody2D _rb2D;
    private Coroutine knockBackStart;
    public bool isBeingKnockedBack { get; set;}

    private void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }

    public IEnumerator KnockBackStart(Vector2 hitDir, float hitDirForce,Vector2 constantForceDirection)
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
            elapedTimer += Time.fixedDeltaTime;
            time += Time.fixedDeltaTime;

            //Calculates the hit force
            hitForce = hitDir * hitDirForce * _knockbackHitForceCurve.Evaluate(time);

            //Combine _hitForce and _constantForce
            knockbackForce = hitForce + constantForce;
            
            //Applies the knockback force
            _rb2D.velocity = knockbackForce;

            yield return new WaitForFixedUpdate();
        }
    }

    public IEnumerator KnockBackSlowStop()
    {
        float time = 0;

        Vector2 currentVelocity = _rb2D.velocity;

        while (time < _releaseTime)
        {
            time += Time.fixedDeltaTime;
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
        if (knockBackStart != null)
        {
            StopCoroutine(knockBackStart);
        }

        knockBackStart = StartCoroutine(KnockBackStart(hitDirection, hitForce, constantForceDirection));
    }

    public void StopKnockback()
    {
        if (knockBackStart != null)
        {
            StopCoroutine(knockBackStart);
        }
    }
}
