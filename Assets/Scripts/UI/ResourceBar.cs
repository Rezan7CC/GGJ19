using DefaultNamespace;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ResourceBar : MonoBehaviour, IResetable
{
    public Image resourceImage;
    public Color normalFillColor = new Color(1f, 1f, 1f, 0.73f);
    public Color fullFillColor = new Color(1f, 1f, 1f, 1f);
    
    // Start is called before the first frame update
    void Start()
    {
        Game.Instance.GameModel.OnResourceAmountChange += OnResourceAmountChanged;
    }

    private void OnResourceAmountChanged(int amount)
    {
        float oldFillAmount = resourceImage.fillAmount;
        resourceImage.fillAmount = Game.Instance.GameModel.GetNormalizedResourceAmount();
        resourceImage.color = resourceImage.fillAmount == 1 ? fullFillColor : normalFillColor;
        if (oldFillAmount < 1 && resourceImage.fillAmount == 1)
        {
            resourceImage.transform.DOScale(1.3f, 0.15f).SetLoops(2, LoopType.Yoyo);
        }
    }

    public void Reset()
    {
        resourceImage.fillAmount = 0;
    }
}
