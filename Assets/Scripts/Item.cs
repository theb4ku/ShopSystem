using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 1)]
public class Item : ScriptableObject
{
    public string Name => name;
    public float Weight => weight;
    public string Description => description;
    public float Price => price;
    public float Rarity => rarity;
    

    [SerializeField] private new string name;
    [SerializeField] private float weight;
    [SerializeField] private float price;
    [SerializeField] private string description;
    //[SerializeField] private int id;
    [SerializeField] private float rarity;
    
}
