using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public EQManager eqManager;
    public Text hpText;
    public Slider hpBar;

    public Text staminaText;
    public Slider staminaBar;

    public Text expText;
    public Slider expBar;

    public Text strText;

    public Text dexText;
    public Text criticalText;

    public Text vitText;
    public Text conText;
    public Text freePointsText;

    private static bool UIExists;

    // Start is called before the first frame update
    void Start()
    {
        if (!UIExists) {
            UIExists = true;
            DontDestroyOnLoad(transform.gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.maxValue = eqManager.maxHp;
        hpBar.value = eqManager.currentHp;
        hpText.text = "<b>" + eqManager.currentHp + "/" + eqManager.maxHp + "</b>";

        staminaBar.maxValue = eqManager.maxStamina;
        staminaBar.value = eqManager.currentStamina;
        staminaText.text = "<b>" + eqManager.currentStamina + "/" + eqManager.maxStamina + "</b>";

        expBar.maxValue = eqManager.expToLvlUp;
        expBar.value = eqManager.currentExp;
        expText.text = "<b>" + eqManager.currentExp + "/" + eqManager.expToLvlUp + "</b>";

        strText.text = eqManager.str + "";
        dexText.text = eqManager.dex + "";
        criticalText.text = "Critical chance: " + eqManager.critChance + "%";
        vitText.text = eqManager.vit + "";
        conText.text = eqManager.con + "";
        freePointsText.text = "Free points: " + eqManager.freePoints;
    }
}
