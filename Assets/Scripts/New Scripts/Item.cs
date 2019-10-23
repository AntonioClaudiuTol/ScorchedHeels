using UnityEngine;
public class Item
{
    public string Name;

    public Item()
    {
        Name = "Item no. " + Time.deltaTime;
    }

    public override string ToString() {
        return Name;
    }

}
