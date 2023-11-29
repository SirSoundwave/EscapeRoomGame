using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
	public Text nameText;
	public Text levelText;
	public Slider hpSlider;
	public Text hpText;

    private float fillDuration = 1f;

    public void SetHUD(Unit unit)
	{
		nameText.text = unit.name;
		levelText.text = "Lvl " + unit.level;
		hpSlider.maxValue = 1;
		hpSlider.value = unit.currentHP / unit.CalculateTotalStat(StatType.health);
		hpText.text = GameData.NumberFormatter(unit.currentHP) + "/" + GameData.NumberFormatter(unit.CalculateTotalStat(StatType.health));
	}

	public void SetHP(float currentHP, float maxHP)
	{
        float targetFill = Mathf.Clamp01(currentHP / maxHP);

        StopAllCoroutines();

        StartCoroutine(AnimateHealthBar(targetFill, currentHP, maxHP));
    }

    private IEnumerator AnimateHealthBar(float targetFill, float currentHP, float maxHP)
    {
        float currentFill = hpSlider.value;
        float timer = 0f;

        while (timer < fillDuration)
        {
            timer += Time.deltaTime;

            float newFill = Mathf.Lerp(currentFill, targetFill, timer / fillDuration);

            hpSlider.value = newFill;
            hpText.text = GameData.NumberFormatter(newFill * maxHP) + "/" + GameData.NumberFormatter(maxHP);

            yield return null;
        }

        hpSlider.value = targetFill;
        hpText.text = (currentHP > 0 ? GameData.NumberFormatter(currentHP) : 0) + "/" + GameData.NumberFormatter(maxHP);
    }

    
}
