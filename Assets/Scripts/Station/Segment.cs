using UnityEngine;

public class Segment : MonoBehaviour
{
    [SerializeField]
    private GameObject segment;

    public void HideSegment()
    {
        if (IsShown())
        {
            segment.SetActive(false);
        }
    }
    
    public void ShowSegment()
    {
        if (IsShown() == false)
        {
            segment.SetActive(true);
        }
    }

    private bool IsShown()
    {
        return segment.activeSelf;
    }
}