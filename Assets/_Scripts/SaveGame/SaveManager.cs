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
        if (StateGameCtrl.isNewGame)
        {
            inventoryName = "Inventory_Default";
            playerName = "Player_Default";
            StateGameCtrl.isNewGame = false;
        }
        string jsonInventory = SaveSystem.GetString(inventoryName);
        Debug.Log(jsonInventory);
        PlayerCtrl.Instance.Inventory.InventoryFromJson(jsonInventory);

        string jsonPlayer = SaveSystem.GetString(playerName);
        Debug.Log(jsonPlayer);
        PlayerData playerData = this.PlayerFromJson(jsonPlayer);
        if (StateGameCtrl.nextLevel)
        {
            playerData.playerPos = Vector3.zero;
            playerData.score++;
            StateGameCtrl.nextLevel = false;
        }
        Player.Instance.SetData(playerData);

        //string jsonInventory = SaveSystem.GetString("character");
        //Debug.Log(jsonInventory);
        //Character.Instance.InventoryFromJson(jsonInventory);
    }

    public virtual void SaveGame()
    {
        Player.Instance.LoadData();
        string jsonInventory = JsonUtility.ToJson(PlayerCtrl.Instance.Inventory);
        SaveSystem.SetString("Inventory", jsonInventory);
        Debug.Log(jsonInventory);

        string jsonPlayer = JsonUtility.ToJson(Player.Instance);
        SaveSystem.SetString("Player", jsonPlayer);
        Debug.Log(jsonPlayer);

    }

    public virtual PlayerData PlayerFromJson(string jsonString)
    {
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(jsonString);
        return playerData;
    }

    public virtual void Savecharacter()
    {
        string jsonPlayer = JsonUtility.ToJson(Character.Instance);
        SaveSystem.SetString("character", jsonPlayer);
        Debug.Log(jsonPlayer);
    }
}
