using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combat : MonoBehaviour
{
    public Character hero;
    public Character enemy;
    public Text heroText;
    public Text enemyText;
    public Text loot;
    private int heroHP = 10;
    private int enemyHP = 10;
    private WaitForSeconds waitForSeconds = new WaitForSeconds(1f);

    IEnumerator Progress()
    {
        while (true)
        {
            if(enemy.Health.BaseValue <= 0)
            {
                if(!string.IsNullOrEmpty(enemy.loot.ToString()))
                {
                    loot.text = string.Concat(loot.text, enemy.ToString());
                }
                enemy = new Character();
                enemy.Health.BaseValue = 5;
            }
            if(hero.Health.BaseValue <= 0)
            {
                StopCoroutine(Progress());
                StopAllCoroutines();
            }
            hero.Health.BaseValue--;
            enemy.Health.BaseValue -= 2;
            heroText.text = hero.Health.BaseValue.ToString();
            enemyText.text = enemy.Health.BaseValue.ToString();
            yield return waitForSeconds;
        }
    }

    public void StartCombat()
    {
        StartCoroutine(Progress());
    }

    private void Start()
    {
        hero = new Character();
        enemy = new Character();
        hero.Health.BaseValue = (float)heroHP;
        enemy.Health.BaseValue = (float)enemyHP;
        StartCoroutine(Progress());
    }
}
