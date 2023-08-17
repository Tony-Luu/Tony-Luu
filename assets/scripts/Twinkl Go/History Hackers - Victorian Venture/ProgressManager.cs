using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressManager : MonoBehaviour
{
    [SerializeField] private bool HasTilePuzzleBeenCompleted;


    public void IsTilePuzzleCompleted(bool IsCompleted)
    {
        HasTilePuzzleBeenCompleted = IsCompleted;
    }

    public bool HasPlayerCompletedTilePuzzle()
    {
        return HasTilePuzzleBeenCompleted;
    }

    void Awake()
    {
        SceneManager.activeSceneChanged += ResetProgress;
    }

    void ResetProgress(Scene LoadedScene, Scene CurrentScene)
    {
        if (CurrentScene.buildIndex == 0 && HasTilePuzzleBeenCompleted)
        {
            HasTilePuzzleBeenCompleted = false;
        }
    }

}
