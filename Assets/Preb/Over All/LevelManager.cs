using System.Collections;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "LevelManager")]
public class LevelManager : ScriptableObject
{
    [SerializeField] private int MainMenuBuildIndex = 0;
    [SerializeField] private int FirstLevelBuildIndex = 1;

    public delegate void OnLevelFinish();
    public static event OnLevelFinish onLevelFinish;

    internal static void LevelFinishes()
    {
        onLevelFinish?.Invoke();
    }

    public void GoToMainMenu()
    {
        LoadSceneByIndexAsync(MainMenuBuildIndex);
    }

    public void LoadNextLevel()
    {
        LoadSceneByIndexAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadFirstLevel()
    {
        LoadSceneByIndexAsync(FirstLevelBuildIndex);
    }

    public void RestartCurrentLevel()
    {
        LoadSceneByIndexAsync(SceneManager.GetActiveScene().buildIndex);
    }

    private void LoadSceneByIndexAsync(int index)
    {
        LoadingPanel.Instance?.gameObject.SetActive(true);
        CoroutineRunner.Instance.StartCoroutine(LoadSceneAsync(index));
    }

    private IEnumerator LoadSceneAsync(int index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            if (operation.progress >= 0.9f)
            {
                WaitASec();
                // Activate the scene once it's ready
                operation.allowSceneActivation = true;
            }
            yield return null;
        }

        // Deactivate the loading canvas after loading
        LoadingPanel.Instance?.gameObject.SetActive(false);
        // Resume the game state
        GameStatic.SetGamePause(false);
    }
    private async Task WaitASec()
    {
        await Task.Delay(1000); // Waits for 1 second asynchronously
    }
}