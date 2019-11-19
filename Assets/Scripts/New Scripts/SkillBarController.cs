using System;
using UnityEngine;
using UnityEngine.UI;

public class SkillBarController : MonoBehaviour
{
    [SerializeField] private Image imageCooldown;
    [SerializeField] private Text textCooldown;
    public float cooldown = 3;
    public bool isCooldown;
    private float originalCooldown = 3;
    private float cooldownTextValue;

    private void Awake()
    {
        textCooldown.text = cooldown.ToString("F0");
        cooldownTextValue = originalCooldown;
    }
//TODO: Leaga asta la jucator si la atacul lui.
    private void Update()
    {
        cooldownTextValue -= Time.deltaTime;
        if (isCooldown)
        {
            imageCooldown.fillAmount -= Time.deltaTime / cooldown;
            textCooldown.text = cooldownTextValue.ToString("F0");
            if (imageCooldown.fillAmount <= 0)
            {
                imageCooldown.fillAmount = 1;
                cooldownTextValue = originalCooldown;
                cooldown = originalCooldown;
            }
        }
    }
}
