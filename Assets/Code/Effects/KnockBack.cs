using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * This script is responsible for the knockback effect.
 * Youtube Video: https://www.youtube.com/watch?v=Jy1yXbKYW68
 * */
public class KnockBack : MonoBehaviour
{
    [SerializeField] private float knockBackTime = 0.5f;
    [SerializeField] private float hitDirectionForce = 10f;
    [SerializeField] private float constForce = 5f;
    [SerializeField] private float inputForce = 7.5f;

    private Rigidbody2D rb2D;
    private Coroutine knockbackCoroutine;

    public bool isBeingKnockedBack { get; private set; }

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    public IEnumerator KnockbackAction(Vector2 hitDirection, Vector2 constantForceDirection, float inputDirection)
    {
        isBeingKnockedBack = true;

        Vector2 _hitForce;
        Vector2 _constantForce;
        Vector2 _knockbackForce;
        Vector2 _combinedForce;

        _hitForce = hitDirection * hitDirectionForce;
        _constantForce = constantForceDirection * constForce;

        float _elapedTimer = 0;
        while (_elapedTimer < knockBackTime)
        {
            //Iterates the timer
            _elapedTimer += Time.deltaTime;

            //Combine _hitForce and _constantForce
            _knockbackForce = _hitForce + _constantForce;

            //Combine _knockbackForce and _inputForce
            if (inputDirection != 0) { 
                _combinedForce = _knockbackForce + new Vector2(inputDirection * inputForce, 0f);
            }
            else
            {
                _combinedForce = _knockbackForce;
            }

            //Applies the knockback force
            rb2D.velocity = _combinedForce;

            yield return new WaitForFixedUpdate();
        }
        isBeingKnockedBack = false;
    }

    public void CallKnockback(Vector2 hitDirection, Vector2 constantForceDirection, float inputDirection)
    {
        knockbackCoroutine = StartCoroutine(KnockbackAction(hitDirection, constantForceDirection, inputDirection));
    }
}
