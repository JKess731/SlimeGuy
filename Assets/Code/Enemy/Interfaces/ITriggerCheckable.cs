using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public interface ITriggerCheckable
{
    bool isAggroed { get; set; }
    bool isWithinStikingDistance {  get; set; }
    void setAggroStatus(bool isAggroed_);
    void setStrikingDistance(bool isWithinStrikingDistance_);
}
