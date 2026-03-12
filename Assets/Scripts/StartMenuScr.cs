using UnityEngine;
using UnityEngine.SceneManagement;
//just the start menu, nothing else

public class StartMenuScr : MonoBehaviour
{
    [SerializeField] private string startScene;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject optionsPanel;

    private void Start()
    {
        creditsPanel.SetActive(false);
        optionsPanel.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(startScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenCredits()
    {
        creditsPanel.SetActive(true);
    }

    public void CloseCredits()
    {
        creditsPanel.SetActive(false);
    }

    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }
}