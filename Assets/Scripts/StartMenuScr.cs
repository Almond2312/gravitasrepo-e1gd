using UnityEngine;
using UnityEngine.SceneManagement;
//just the start menu, nothing else

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