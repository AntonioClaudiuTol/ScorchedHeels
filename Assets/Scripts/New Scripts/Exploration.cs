using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploration : MonoBehaviour
{
    [SerializeField] private RectTransform mapMask;
    private int progress = 0;
    
    public void Explore()
    {
        progress += 3;
        mapMask.sizeDelta = new Vector2(progress * 100, progress * 100);
        if (progress > 10)
        {
            progress = 0;
            WindowManager.isDungeonUnlocked = true;
        }

    }
}
