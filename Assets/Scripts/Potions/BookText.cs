using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BookText : MonoBehaviour
{
    private string pink = "Perseverance, a beacon in the night,\nIgniting the soul, with a steadfast light.\nNever-ending struggles, yet standing tall,\nKeen on the fight, embracing the brawl.";
    private string red = "Rising, we face the challenge ahead,\nEndurance tested, where courage is bred.\nDetermined hearts, in battles we tread.";
    private string orange = "Onward we march, our spirits ignite,\nResilient souls in the heat of the fight.\nAgainst the odds, we stand strong and bright,\nNever faltering, like a beacon of light.\nGrit and courage, our armor so tight,\nEnduring the struggle, embracing the night.";
    private string blue = "Bearing the weight of a world unseen,\nLeaning on courage, in shades serene.\nUnyielding spirit, like the ocean deep,\nEnduring trials, where shadows creep.";
    private string yellow = "Yearning for triumph in the face of strife,\nEmbracing the challenge, defining life's knife.\nLuminous spirit, a beacon so bright,\nLeaving behind shadows, conquering the night.\nOvercoming hurdles, with a will to grow,\nWielding courage as a radiant glow.";

    public GameEvent leftPage;
    public GameEvent rightPage;

    public void GenerateText(Component sender, object data) {
        Debug.Log("Generating text");
        List<int> numbers = (List<int>) data;
        string left = "";
        string right = "";

        if (numbers[0] == 1) {
            left = pink;
        } else if (numbers[0] == 2) {
            left = red;
        } else if (numbers[0] == 3) {
            left = orange;
        } else if (numbers[0] == 4) {
            left = blue;
        } else if (numbers[0] == 5) {
            left = yellow;
        }

        left += "\n\n";

        if (numbers[1] == 1) {
            left += pink;
        } else if (numbers[1] == 2) {
            left += red;
        } else if (numbers[1] == 3) {
            left += orange;
        } else if (numbers[1] == 4) {
            left += blue;
        } else if (numbers[1] == 5) {
            left += yellow;
        }

        if (numbers[2] == 1) {
            right = pink;
        } else if (numbers[2] == 2) {
            right = red;
        } else if (numbers[2] == 3) {
            right = orange;
        } else if (numbers[2] == 4) {
            right = blue;
        } else if (numbers[2] == 5) {
            right = yellow;
        }

        leftPage.Raise(this, left);
        rightPage.Raise(this, right);
    }
}
