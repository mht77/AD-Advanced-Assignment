using UnityEngine;
using UnityEngine.SceneManagement;

namespace Views
{
    public class SceneLoad : MonoBehaviour
    {
        public void LoadScene(string scene)
        {
            SceneManager.LoadScene(scene);
        }
    }
}
