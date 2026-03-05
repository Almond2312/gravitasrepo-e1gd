using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuScr : MonoBehaviour
{
    [SerializeField] private string startScene;
    public void StartGame()
    {
        SceneManager.LoadScene(startScene);
    }

    void Update()
    {
        
    }
}