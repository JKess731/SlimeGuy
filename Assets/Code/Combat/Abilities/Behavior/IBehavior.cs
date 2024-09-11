using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IBehavior
{
    public virtual void StartBehavior() { }
    public virtual void PerformBehavior() { }
    public virtual void EndBehavior() { }
    public virtual void StartBehavior(Vector2 attackPosition, Quaternion rotation) { }
    public virtual void PerformBehavior(Vector2 attackPosition, Quaternion rotation) { }
    public virtual void CancelBehavior(Vector2 attackPosition, Quaternion rotation) { }
}
