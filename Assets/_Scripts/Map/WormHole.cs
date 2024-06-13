using UnityEngine;

public class WormHole : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            MapLevel.Instance.LevelUp();
            AsyncLoader.Instance.LoadLevel("start");
            SaveManager.Instance.SaveGame();
        }
    }
}
