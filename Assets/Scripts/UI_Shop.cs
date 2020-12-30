using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Shop : MonoBehaviour
{
    [SerializeField] Canvas canvas = default;
    [SerializeField] GameObject scrollViewItemName;
    [SerializeField] GameObject scrollViewItemDescription;
    [SerializeField] GameObject scrollViewPlayInventory;
    [SerializeField] Button buttonItemNamePrefab = default;
    [SerializeField] Button npcButton = default;
    [SerializeField] Button buyButton = default;
    [SerializeField] Button backButton = default;
    [SerializeField] GameObject content;
    [SerializeField] GameObject playerContent;
    [SerializeField] TMP_Text itemDescription = default;
    [SerializeField] TMP_Text shopText = default;
    [SerializeField] TMP_Text inventoryText = default;
    private Shop _shop;
    public Inventory PlayerInventory { get; set; }
    public Inventory ShopInventory { get; set; }

    private void Start()
    {
        _shop = GetComponent<Shop>();
        ShopInventory = GetComponent<Shop>().ShopInventory;
        PlayerInventory = GetComponent<Shop>().PlayerInventory;
    }
    void DrawButtons()
    {
        scrollViewItemName.SetActive(true);
        GoldUI();

        showShopInventory();

        backButton.onClick.RemoveAllListeners();
        backButton.onClick.AddListener(ClearWindows);
        backButton.onClick.AddListener(BackToMainScrollView);
        backButton.onClick.AddListener(showPlayerInventory);
        backButton.onClick.AddListener(showShopInventory);
    }
    void showPlayerInventory()
    {
        scrollViewPlayInventory.SetActive(true);

        foreach (var item in PlayerInventory.ItemList)
        {
            buttonItemNamePrefab.GetComponentInChildren<TMP_Text>().text = $"{item.Name}\n gold: {item.Price}";
            var spawnedButton = Instantiate(buttonItemNamePrefab, playerContent.transform);
            spawnedButton.gameObject.SetActive(true);
            spawnedButton.onClick.RemoveAllListeners();
            spawnedButton.onClick.AddListener(ClearWindows);
            spawnedButton.onClick.AddListener(() => AddItemToShopInventory(item));
            spawnedButton.onClick.AddListener(showShopInventory);
            spawnedButton.onClick.AddListener(showPlayerInventory);
            spawnedButton.onClick.AddListener(GoldUI);
        }
    }
    void showShopInventory()
    {
        foreach (var item in _shop.SelectedItems)
        {
            buttonItemNamePrefab.GetComponentInChildren<TMP_Text>().text = $"{item.Name}\n gold: {item.Price * _shop.ShopPriceMultipler}";
            var spawnedButton = Instantiate(buttonItemNamePrefab, content.transform);
            spawnedButton.gameObject.SetActive(true);
            spawnedButton.onClick.RemoveAllListeners();
            spawnedButton.onClick.AddListener(() => ShowItemInfo(item));
        }
    }
    void ShowItemInfo(Item item)
    {
        scrollViewItemName.SetActive(false);
        scrollViewItemDescription.SetActive(true);
        itemDescription.text = item.Description;
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(ClearWindows);
        buyButton.onClick.AddListener(() => AddItemToPlayerInventory(item));
        buyButton.onClick.AddListener(BackToMainScrollView);
        buyButton.onClick.AddListener(showPlayerInventory);
        buyButton.onClick.AddListener(showShopInventory);
        buyButton.onClick.AddListener(GoldUI);
    }
    void BackToMainScrollView()
    {
        scrollViewItemName.SetActive(true);
        scrollViewItemDescription.SetActive(false);
    }
    public void EnableUIShop()
    {
        npcButton.gameObject.SetActive(true);
        canvas.gameObject.SetActive(true);
        npcButton.onClick.RemoveAllListeners();
        npcButton.onClick.AddListener(DisableNpcButton);
        npcButton.onClick.AddListener(showPlayerInventory);
        npcButton.onClick.AddListener(DrawButtons);
    }
    public void DisableUIShop()
    {
        scrollViewItemDescription.SetActive(false);
        scrollViewItemName.SetActive(false);
        scrollViewPlayInventory.SetActive(false);
        shopText.gameObject.SetActive(false);
        inventoryText.gameObject.SetActive(false);
        npcButton.gameObject.SetActive(false);
    }
    public void ClearWindows()
    {
        foreach (var item in content.GetComponentsInChildren<Button>())
        {
            Destroy(item.gameObject);
        }
        foreach (var item in playerContent.GetComponentsInChildren<Button>())
        {
            Destroy(item.gameObject);
        }
    }
    void AddItemToPlayerInventory(Item item)
    {
        _shop.CheckTransaction(item, ShopInventory, PlayerInventory, _shop.ShopPriceMultipler);
    }
    void AddItemToShopInventory(Item item)
    {
        _shop.CheckTransaction(item, PlayerInventory, ShopInventory, _shop.PlayerPriceMultipler);
    }
    void DisableNpcButton()
    {
        npcButton.gameObject.SetActive(false);
    }
    void GoldUI()
    {
        shopText.gameObject.SetActive(true);
        inventoryText.gameObject.SetActive(true);
        shopText.text = $"Shop\nGold: {ShopInventory.Gold}";
        inventoryText.text = $"Inventory\nGold: {PlayerInventory.Gold}";
    }

}
