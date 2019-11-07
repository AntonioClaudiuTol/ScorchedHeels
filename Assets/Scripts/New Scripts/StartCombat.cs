using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCombat : MonoBehaviour
{
    public void StartCombatAction()
    {
        GameObject.FindWithTag("Player").GetComponent<Character>().combatState = Character.CombatState.Started;
        GameObject.FindWithTag("Enemy").GetComponent<Enemy>().State = EnemyState.Attacking;
    }
}
