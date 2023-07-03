using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenuScript
{
    public class MainMenu : MonoBehaviour
    {

        public string sceneName;
        public void ChangeScene()
        {
            SceneManager.LoadScene(sceneName);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
