using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBarImage;
    
    // Start is called before the first frame update
    void Start()
    {
        Game.Instance.GameModel.OnHealthChange += OnHealthChanged;
    }

    private void OnHealthChanged(int health)
    {
        healthBarImage.DOFillAmount(Game.Instance.GameModel.GetNormalizedHealth(), 0.1f);
    }
}
