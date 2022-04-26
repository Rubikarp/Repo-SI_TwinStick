using UnityEngine.Events;
using UnityEngine;

public class QuitApp : MonoBehaviour
{
    public UnityEvent OnQuit;
    public UnityEvent OnWantToQuit;

    [ContextMenu("WantToQuit")]
    public void WantToQuitGame()
    {
        OnWantToQuit?.Invoke();
#if UNITY_EDITOR
        Debug.Log("are U sure ?");
#endif
    }

    [ContextMenu("Quit")]
    public void QuitGame()
    {
        OnQuit?.Invoke();
        Application.Quit();
#if UNITY_EDITOR
        Debug.LogError("Game have been quit");
#endif
    }
}
