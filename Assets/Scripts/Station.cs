using DefaultNamespace;
using UnityEngine;

public class Station : MonoBehaviour, IResetable
{
    public Segment[] segments;
    private int[] requiredSegmentScores;

    private void Start()
    {
        Game.Instance.GameModel.OnScoreChange += OnScoreChanged;
        requiredSegmentScores = Game.Instance.GameSettings.segmentScores;
    }

    private void OnScoreChanged(int score)
    {
        Debug.Log("score: " + score);
        for (var i = 0; i < requiredSegmentScores.Length; i++)
        {
            var segmentScore = requiredSegmentScores[i];
            if (score >= segmentScore)
            {
                segments[i].ShowSegment();
            }
        }
    }

    public void Reset()
    {
        HideSegments();
    }

    private void HideSegments()
    {
        foreach (var segment in segments)
        {
            segment.HideSegment();
        }
    }
}
