using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerSkill
{
    
    int Stage { get; }
    bool Enabled { get; set; }

}
