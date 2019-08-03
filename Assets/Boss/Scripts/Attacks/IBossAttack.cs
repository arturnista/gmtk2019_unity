using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBossAttack
{
    
    int Stage { get; }
    bool Finished { get; }
    void Attack();

}
