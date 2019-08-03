using UnityEngine;

public interface IHealth
{

    int TotalHealthPoints { get; }
    void DealDamage(int damage, Transform damager);
    
}