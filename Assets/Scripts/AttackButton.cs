using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class AttackButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI attackInfoText; // Change to TextMeshProUGUI
    private IPlayerAttack attack;

    public void Initialize(IPlayerAttack attack)
    {
        this.attack = attack;
        UpdateInfo();
    }

    public void OnButtonClick()
    {
        // Handle what happens when the button is clicked
        attack.LevelUp();
        UpdateInfo();
        Destroy(this);
    }

    private void UpdateInfo()
    {
        attackInfoText.text = $"Damage: {attack.damage}\nDelay: {attack.delay}\nLevel: {attack.level}";
    }
}
