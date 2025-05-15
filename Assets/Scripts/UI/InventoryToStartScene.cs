using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryToStartScene : MonoBehaviour
{
    [SerializeField] private string startSceneName = "StartScene";

    public void GoBackToStart()
    {
        SceneManager.LoadScene(startSceneName);
    }
}

