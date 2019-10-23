using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public CharacterStat Health;
    public List<Item> loot;

    public Character()
    {
        Health = new CharacterStat();
        loot = GenerateLoot();
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
            temp += string.Concat(temp, string.Concat(loot[i].ToString(), "_O_"));
        }
        return temp;
    }

}
