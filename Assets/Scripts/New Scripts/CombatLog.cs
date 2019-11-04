using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class CombatLog : MonoBehaviour
{
    [SerializeField] public Text combatLog;
    private static StringBuilder sb;
    public delegate void DamageDealt(string damage);

    private void OnEnable()
    {
        Character.OnDamageDealt += ThisReturnsAString;
        Enemy.OnDamageDealt += ThisReturnsAString;
    }

    private void OnDisable()
    {
        Character.OnDamageDealt -= ThisReturnsAString;
        Enemy.OnDamageDealt -= ThisReturnsAString;
    }

    private void Awake()
    {
        sb = new StringBuilder();
        sb.Length = 0;

    }

    public void ThisReturnsAString(string damage)
    {
        sb.AppendLine(damage);
        combatLog.text = sb.ToString();
    }
}
