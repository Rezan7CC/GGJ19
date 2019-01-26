using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class ResourceBar : MonoBehaviour, IResetable
{
    public Image resourceImage;
    
    // Start is called before the first frame update
    void Start()
    {
        Game.Instance.GameModel.OnResourceAmountChange += OnResourceAmountChanged;
    }

    private void OnResourceAmountChanged(int amount)
    {
        resourceImage.fillAmount = Game.Instance.GameModel.GetNormalizedResourceAmount();
    }

    public void Reset()
    {
        resourceImage.fillAmount = 0;
    }
}
