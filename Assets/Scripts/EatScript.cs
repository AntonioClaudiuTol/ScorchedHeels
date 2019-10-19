using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatScript : MonoBehaviour
{
    
    public void Eat(int foodType)
    {
        switch(foodType)
        {
            case 0:
                {
                    Resources.Energy += 10;
                    break;
                }
            default:
                {
                    break;
                }
        }
    }
}
