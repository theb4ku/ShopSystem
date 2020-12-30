using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] float gold;
    [SerializeField] float maxWeight;
    private float _currentWeight;
    [SerializeField] List<Item> itemList;
    public float Gold => gold;
    public float MaxWeight => maxWeight;
    public float CurrentWeight
    {
        get { return _currentWeight; }
        set { _currentWeight = value; }
    }
    public List<Item> ItemList => itemList;
    void Start()
    {
        CalculateCurrentWeight();
    }
    public void AddItem(Item item, float priceMultipler)
    {
        itemList.Add(item);
        _currentWeight += item.Weight;
        gold -= item.Price * priceMultipler;
    }
    public void DeleteItem(Item item, float priceMultipler)
    {
        ItemList.Remove(item);
        gold += item.Price * priceMultipler;
        _currentWeight -= item.Weight;
    }
    void CalculateCurrentWeight()
    {
        float temp = 0f;
        foreach (var item in itemList)
        {
            temp += item.Weight;
        }
        _currentWeight = temp;
    }

}
