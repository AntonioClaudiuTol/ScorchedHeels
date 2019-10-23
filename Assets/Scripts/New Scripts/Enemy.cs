using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public List<Character> enemies;

    private void Start()
    {
        enemies = new List<Character>();

    }
}
