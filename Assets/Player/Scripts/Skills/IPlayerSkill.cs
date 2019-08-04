using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerSkill
{

    string Name { get; }
    string Description { get; }
    Sprite Icon { get; }
    
    int Stage { get; }
    bool Enabled { get; set; }

}
