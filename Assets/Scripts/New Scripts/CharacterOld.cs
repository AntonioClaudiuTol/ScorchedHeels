using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterOld
{
    public CharacterStat Health;
    public CharacterStat Strength;
    public string Name;
    private float minDamage;
    private float maxDamage;
    public List<Item> loot;

    public CharacterOld()
    {
        Health = new CharacterStat();
        Strength = new CharacterStat();
        Strength.BaseValue = 5;
        minDamage = 0;
        maxDamage = minDamage + Strength.Value;
        loot = GenerateLoot();
    }

    public CharacterOld(string name) : this()
    {
        Name = name;
    }

    public int DoDamage()
    {
        int damage = Random.Range((int)minDamage, (int)maxDamage + 1);
        Debug.Log(this.Name + " did: " + damage + " damage.");
        return damage;
    }

    private List<Item> GenerateLoot()
    {
        List<Item> loot = new List<Item>();
        for (int i = 0; i < 5; i++)
        {
            if(Random.Range(0, 5) > 1)
            {
                loot.Add(new Item());
            }
        }

        return loot;
    }

    public override string ToString() {
        string temp = "";

        for (int i = 0; i < loot.Count; i++)
        {
            temp += string.Concat(temp, string.Concat(loot[i].ToString(), "\n"));
        }
        return temp;
    }

    public void TakeDamage(int value)
    {
        Health.BaseValue -= value;
    }
    public void HealDamage(int value)
    {
        Health.BaseValue += value;
    }
}
