using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IAbility
{
    public string AbilityName { get; }
    public Sprite Icon { get; }
    public AbilityState AbilityState { get; set; }
    public float CooldownTime { get; }

    public virtual void Initialize() { }
    public virtual void StartBehavior(Vector2 attackPosition, Quaternion rotation) { }
    public virtual void PerformBehavior(Vector2 attackPosition, Quaternion rotation) { }
    public virtual void CancelBehavior(Vector2 attackPosition, Quaternion rotation) { }
    public virtual void Upgrade(StatsSO playerStats, StatsEnum stats) { }
}
