using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    void Start()
    {
        
    }


    void Update()
    {

    }

    private void PlayerTakeDamage(float damage)
    {
        GameManager.gameManager.playerHealth.DamageUnit(damage);
        Debug.Log("Player Damage Taken: " + damage + ". Player Health = " + GameManager.gameManager.playerHealth.Health);
    }

    private void PlayerHeal(float heal)
    {
        GameManager.gameManager.playerHealth.HealUnit(heal);
        Debug.Log("Player Healed: " + heal + ". Player Health = " + GameManager.gameManager.playerHealth.Health);
    }
}
