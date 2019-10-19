using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftSpear : MonoBehaviour
{
    public void CraftSpearAction()
    {
        if(Resources.Berry > 0 && Resources.Branch > 0)
        {
            Resources.Berry--;
            Resources.Branch--;
            Resources.Rock++;
            Resources.isRockUnlocked = true;
        }
    }
}
