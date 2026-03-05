using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TargetLockUI : MonoBehaviour
{
    public TargetLockSystem lockSystem;
    public TMP_Text lockText;

    void Update()
    {
        lockText.enabled = lockSystem.LockedTarget != null;
    }
    public void PUB_ApplicationRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void PUB_ApplicationQuit()
    {
        Application.Quit();
    }
}

