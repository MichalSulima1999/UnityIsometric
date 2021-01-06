using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EQManager : MonoBehaviour
{
    public bool weaponEquiped = false;
    public bool revolverEquiped = false;
    public bool swordEquiped = false;
    public GameObject eqWeapon;

    public int revolverDmg;

    public bool helmetEquiped = false;
    public bool chestplateEquiped = false;
    public bool legsEquiped = false;
    public GameObject eqHelmet;
    public GameObject eqChestplate;
    public GameObject eqLegs;

    public GameObject helmet;
    public GameObject chestplate;
    public GameObject legs;

    private int armor;

    public int lvl;
    public int currentExp;
    public int expToLvlUp;
    public int[] expToLvlUpArray;

    public int str;

    public int dex;
    public int critChance;

    public int vit;
    public int con;

    public int freePoints;

    public int currentHp;
    public int maxHp;
    public float hpRegenerateTime;
    private float hpTime;

    public int currentStamina;
    public int maxStamina;
    public float staminaRegenerateTime;
    private float staminaTime;

    public int dmg;

    private void Start() {
        maxHp = vit * 10;
        currentHp = maxHp;

        maxStamina = con * 10;
        currentStamina = maxStamina;
    }

    private void Update() {
        staminaTime += Time.deltaTime;
        if(staminaTime >= staminaRegenerateTime && currentStamina < maxStamina) {
            currentStamina++;
            staminaTime = 0f;
        }

        hpTime += Time.deltaTime;
        if (hpTime >= hpRegenerateTime && currentHp < maxHp) {
            currentHp++;
            hpTime = 0f;
        }

        expToLvlUp = expToLvlUpArray[lvl];
        if(currentExp >= expToLvlUp) {
            lvl++;
            freePoints += 5;
        }

        critChance = (int)Mathf.Pow((float)dex, 0.9f);
        maxHp = vit * 10;
        maxStamina = con * 10;
    }

    public void StrPlus() {
        if(freePoints > 0) {
            str++;
            freePoints--;
        }
    }
    
    public void DexPlus() {
        if(freePoints > 0) {
            dex++;
            freePoints--;
        }
    }
    
    public void VitPlus() {
        if(freePoints > 0) {
            vit++;
            freePoints--;
        }
    }
    
    public void ConPlus() {
        if(freePoints > 0) {
            con++;
            freePoints--;
        }
    }

    public void StaminaUse(int amount) {
        currentStamina -= amount;

        if(currentStamina < 0) {
            currentStamina = 0;
        } 
    }

    public void StaminaAdd(int amount) {
        currentStamina += amount;

        if (currentStamina > maxStamina) {
            currentStamina = maxStamina;
        }
    }

    public void HpLoss(int amount) {
        int loseHP = amount - armor;
        if (loseHP <= 0)
            loseHP = 1;

        currentHp -= loseHP;

        if(currentHp <= 0) {
            currentHp = 0;
            gameObject.SetActive(false);
        }
    }

    public void SetArmor(int amount) {
        armor += amount;
    }
}
