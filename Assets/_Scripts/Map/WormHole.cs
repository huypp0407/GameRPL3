using UnityEngine;

public class WormHole : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            transform.position = new Vector3(0, 20, 0);
            UILevel.Instance.Toggle();
            MapLevel.Instance.LevelUp();
            SaveManager.Instance.SaveGame();

        }
    }
}
