using DefaultNamespace;
using UnityEngine;

public class Station : MonoBehaviour, IResetable
{
    public Segment[] segments;
    private int[] requiredSegmentScores;

    private void Start()
    {
        Game.Instance.GameModel.OnScoreChange += OnScoreChanged;
        OnScoreChanged(Game.Instance.GameModel.GetScore());
        requiredSegmentScores = Game.Instance.GameSettings.segmentScores;
    }

    private void OnScoreChanged(int score)
    {
        int totalSegments = 0;
        for (var i = 0; i < requiredSegmentScores.Length; i++)
        {
            var segmentScore = requiredSegmentScores[i];
            if (score >= segmentScore)
            {
                segments[i].ShowSegment();
                totalSegments++;
            }
        }

        if (Game.Instance.GameSignals.OnHomeSegmentCountChanged != null)
        {
            Game.Instance.GameSignals.OnHomeSegmentCountChanged(totalSegments);
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
