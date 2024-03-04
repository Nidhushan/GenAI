using UnityEngine;
using UnityEngine.SceneManagement;

public class reloadGame : MonoBehaviour
{
    public void restartLevel()
    {
        SceneManager.LoadScene("Level1");
    }
}
