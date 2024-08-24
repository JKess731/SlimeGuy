using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * This script is responsible for the knockback effect.
 * Youtube Video: https://www.youtube.com/watch?v=Jy1yXbKYW68
 * */
public class KnockBack : MonoBehaviour
{
    [SerializeField] private float knockBackTime = 0.2f;
    [SerializeField] private float constForce = 0.25f;

    private Rigidbody2D rb2D;
    private Coroutine knockbackCoroutine;
    public bool isBeingKnockedBack { get; set;}

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    public IEnumerator KnockbackAction(Vector2 hitDir, float hitDirForce,Vector2 constantForceDirection)
    {

        Vector2 _hitForce;
        Vector2 _constantForce;
        Vector2 _knockbackForce;
        Vector2 _combinedForce;

        _hitForce = hitDir * hitDirForce;
        _constantForce = constantForceDirection * constForce;
        
        isBeingKnockedBack = true;

        float _elapedTimer = 0;
        while (_elapedTimer < knockBackTime)
        {
            //Iterates the timer
            _elapedTimer += Time.deltaTime;

            //Combine _hitForce and _constantForce
            _knockbackForce = _hitForce + _constantForce;

            _combinedForce = _knockbackForce;
            
            //Applies the knockback force
            rb2D.velocity = _combinedForce;

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
        if (knockbackCoroutine != null)
        {
            StopCoroutine(knockbackCoroutine);
        }

        knockbackCoroutine = StartCoroutine(KnockbackAction(hitDirection, hitForce, constantForceDirection));
    }
}
