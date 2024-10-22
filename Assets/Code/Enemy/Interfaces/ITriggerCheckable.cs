using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

// ITriggerCheckable interface for all trigger checkable objects
public interface ITriggerCheckable
{
    bool _isAggroed { get; set; }
    bool _isWithinStikingDistance {  get; set; }
    void setAggroStatus(bool isAggroed);
    void setStrikingDistance(bool isWithinStrikingDistance);
}
