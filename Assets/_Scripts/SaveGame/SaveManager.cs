using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    [SerializeField] protected static SaveManager instance;
    public static SaveManager Instance => instance;
    private const string SAVE_1 = "save_1";

    private void Awake()
    {
        if (SaveManager.instance != null) return;
        SaveManager.instance = this;
    }

    private void Start()
    {
        this.LoadSaveGame();
    }

    private void OnApplicationQuit()
    {
        this.SaveGame();
        Debug.Log("Quit");

    }

    protected virtual void LoadSaveGame()
    {
        string inventoryName = "Inventory";
        string playerName = "Player";
        string mapLevel = "Maplevel";
        if (StateGameCtrl.isNewGame)
        {
            inventoryName = "Inventory_Default";
            playerName = "Player_Default";
            mapLevel = "Maplevel_Default";
            StateGameCtrl.isNewGame = false;
        }
        string jsonInventory = SaveSystem.GetString(inventoryName);
        Debug.LogError($"HUYPP :: inventoryName :: {inventoryName} :: {jsonInventory}");
        PlayerCtrl.Instance.Inventory.InventoryFromJson(jsonInventory);

        string jsonPlayer = SaveSystem.GetString(playerName);
        Debug.LogError($"HUYPP :: jsonPlayer :: {jsonPlayer}");
        
        //string jsonInventory = SaveSystem.GetString("character");
        //Debug.Log(jsonInventory);
        //Character.Instance.InventoryFromJson(jsonInventory);
    }

    public virtual void SaveGame()
    {
        string jsonInventory = JsonUtility.ToJson(PlayerCtrl.Instance.Inventory);
        Debug.LogError($"HUYPP :: inventoryName :: Inventory :: {jsonInventory}");
        SaveSystem.SetString("Inventory", jsonInventory);
    }

    public virtual void Savecharacter()
    {
        string jsonPlayer = JsonUtility.ToJson(Character.Instance);
        SaveSystem.SetString("character", jsonPlayer);
        Debug.Log(jsonPlayer);
    }
}
