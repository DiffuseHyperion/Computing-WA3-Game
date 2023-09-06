using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenuScript
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject tutorialMenu;
        
        public string sceneName;
        public void ChangeScene()
        {
            SceneManager.LoadScene(sceneName);
        }

        public void ToggleTutorial()
        {
            tutorialMenu.SetActive(!tutorialMenu.activeSelf);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
