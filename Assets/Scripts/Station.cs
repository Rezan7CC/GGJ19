using DefaultNamespace;
using UnityEngine;

public class Station : MonoBehaviour, IResetable
{
    public Segment[] segments;
    private int[] requiredSegmentScores;

    private void Start()
    {
        GameObject.FindWithTag(Tags.ServiceLocator).GetComponent<ServiceLocator>().GameModel.OnScoreChange += OnScoreChanged;
        requiredSegmentScores = GameObject.FindWithTag(Tags.ServiceLocator).GetComponent<ServiceLocator>().GameSettings.segmentScores;
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
