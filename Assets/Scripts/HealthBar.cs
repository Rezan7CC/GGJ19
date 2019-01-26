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
        healthBarImage.fillAmount = Game.Instance.GameModel.GetNormalizedHealth();
    }
}
