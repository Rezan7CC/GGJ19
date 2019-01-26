using UnityEngine;
using UnityEngine.UI;

public class ResourceProgressBar : MonoBehaviour
{
    public Image ProgressBar;
    private int _maxSegmentScore;

    private void Start()
    {
        Game.Instance.GameModel.OnScoreChange += OnScoreChanged;
        var segmentScores = Game.Instance.GameSettings.segmentScores;
        _maxSegmentScore = segmentScores[segmentScores.Length - 1];
        ProgressBar.fillAmount = 0;
    }

    private void OnScoreChanged(int score)
    {
        ProgressBar.fillAmount = (float) score / _maxSegmentScore;
    }
}
