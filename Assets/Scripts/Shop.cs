using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    Inventory _shopInventory;
    Inventory _playerInventory;
    UI_Shop _uiShop;
    [SerializeField] float shopPriceMultipler = 1.2f;
    [SerializeField] float playerPriceMultipler = 1f;

    List<Item> _selectedItems;
    public List<Item> SelectedItems => _selectedItems; 
    public Inventory ShopInventory => _shopInventory;

    public Inventory PlayerInventory => _playerInventory;
    public float ShopPriceMultipler => shopPriceMultipler;
    public float PlayerPriceMultipler => playerPriceMultipler;

    private void Start()
    {
        _shopInventory = GetComponent<Inventory>();  
        _uiShop = GetComponent<UI_Shop>();
        SelectRandomItemsToShop();
    }
    public void CheckTransaction(Item item, Inventory seller, Inventory buyer, float priceMultipler)
    {
        if (buyer.Gold >= item.Price && buyer.MaxWeight - buyer.CurrentWeight >= item.Weight)
        {
            buyer.AddItem(item, priceMultipler);
            Debug.Log($"{buyer.name} have bought {item.name}");
            seller.DeleteItem(item, priceMultipler);
        }
        else
        {
            Debug.Log($"{buyer.name} can't buy {item.name}");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInventory = other.GetComponent<Inventory>();
            _uiShop.PlayerInventory = _playerInventory;
            _uiShop.ShopInventory = _shopInventory;
            _uiShop.EnableUIShop();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _uiShop.PlayerInventory = null;
            _playerInventory = null;
            _uiShop.ShopInventory = null;
            _uiShop.DisableUIShop();
            _uiShop.ClearWindows();
        }
    }
    void SelectRandomItemsToShop()
    {
        _selectedItems = new List<Item>(_shopInventory.ItemList);
        for (int i = 0; i < _selectedItems.Count; i++)
        {
            if(Random.Range(0f, 1f) > _selectedItems[i].Rarity)
            {
                _selectedItems.Remove(_selectedItems[i]);
            }
        }
        CalculateSelectedItemsWeight();
    }
    void CalculateSelectedItemsWeight()
    {
        float temp = 0;
        foreach (var item in _selectedItems)
        {
            temp += item.Weight;
        }
        _shopInventory.CurrentWeight = temp;
    }


}
