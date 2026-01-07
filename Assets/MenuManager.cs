using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject menuPanel;   // پنل منو
    public GameObject gamePanel;   // پنل بازی

    // وقتی روی دکمه‌ی Play کلیک می‌کنی
    public void PlayGame()
    {
        menuPanel.SetActive(false); // پنل منو رو خاموش کن
        gamePanel.SetActive(true);  // پنل بازی رو روشن کن
    }

    // وقتی روی دکمه‌ی Exit کلیک می‌کنی
    public void ExitGame()
    {
        Debug.Log("خروج از بازی...");
        Application.Quit();

        // اگر توی ادیتور تست می‌کنی (برای خارج شدن از پلی مود):
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
