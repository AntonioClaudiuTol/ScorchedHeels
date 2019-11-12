using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeStatTemp : MonoBehaviour
{

    private Character character;

    private void Awake()
    {
        character = GameObject.FindWithTag("Player").GetComponent<Character>();
    }

    public void UpgradeStats()
    {
        character.maxHealth += 10;
        character.Health += 10;
        character.Damage.BaseValue += 1;
        character.Defense.BaseValue += 1;
        character.HPRegen.BaseValue += 1;
    }
}
