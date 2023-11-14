using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelTracker : MonoBehaviour
{
    public int currentXP = 0;
    public int xpToNextLevel = 100;
    public RectTransform xpBar; // A reference to a Unity UI Slider component
    public Text xpText; // A reference to display currentXP/xpToNextLevel as text (optional)
    private List<IPlayerAttack> attacks = new List<IPlayerAttack>();
    private float startingWidth = 1;
    private float xpToWidthUnit;
    

    void Start()
    {
        attacks = GameObject.FindGameObjectWithTag("Player").GetComponents<IPlayerAttack>().ToList();
        xpBar = GameObject.FindGameObjectWithTag("XPBar").GetComponent<RectTransform>();
        xpToWidthUnit = startingWidth / xpToNextLevel;
        UpdateXPBar();
    }

    public void GainXP(int amount)
    {
        currentXP += amount;
        if (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }
        UpdateXPBar();
    }

    void LevelUp()
    {
        currentXP -= xpToNextLevel; // remaining XP rolls over to next level
        xpToNextLevel = (int)(xpToNextLevel * 1.5f); // Increase XP needed for next level by 50%. You can adjust this value.
        xpToWidthUnit = startingWidth / xpToNextLevel;
        foreach (var playerAttack in attacks)
        {
            playerAttack.LevelUp();
        }
        // Here, you can add more logic for leveling up, like increasing player stats, etc.
    }

    void UpdateXPBar()
    {
        Vector3[] corners = new Vector3[4];
        xpBar.GetWorldCorners(corners);
        Vector3 cornerTopLeft = corners[0];
        Vector3 cornerTopRight = corners[1];
        float width = (cornerTopLeft - cornerTopRight).magnitude;
        width = (width < 0) ? -width : width;
        xpBar.position = new Vector3(xpBar.position.x + width / 2, xpBar.position.y);
        xpBar.localScale = new Vector3(currentXP * xpToWidthUnit, xpBar.localScale.y);
        if (xpText) xpText.text = $"{currentXP}/{xpToNextLevel}";
    }

}
