using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string Name;
    public Sprite Icon;

    public Item()
    {
        //Name = "Item no. " + Time.deltaTime;
    }

    public override string ToString() {
        return Name;
    }
}
