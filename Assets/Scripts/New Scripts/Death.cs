using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public delegate void CharacterDeath();

    public static event CharacterDeath OnDeath;
    
    public void Die()
    {
        if (OnDeath != null)
        {
            OnDeath();
        }
    }
}
