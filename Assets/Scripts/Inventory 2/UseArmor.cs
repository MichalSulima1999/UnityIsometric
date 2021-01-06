using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseArmor : MonoBehaviour
{
    public Material material;

    private GameObject armor;
    private EQManager eqM;
    private Inventory inv;

    public int armorStat;
    

    // Start is called before the first frame update
    void Start()
    {
        eqM = GameObject.FindGameObjectWithTag("Player").GetComponent<EQManager>();
        inv = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        
    }

    public void Helmet() {
        armor = eqM.helmet;

        if (!eqM.helmetEquiped) {
            armor.SetActive(true);
            armor.GetComponent<SkinnedMeshRenderer>().material = material;
            eqM.SetArmor(armorStat);

            Instantiate(gameObject, eqM.eqHelmet.transform, false);
            eqM.helmetEquiped = true;
            Destroy(gameObject);
        } else {
            armor.SetActive(false);
            eqM.SetArmor(-armorStat);

            DeleteFromEq();
            eqM.helmetEquiped = false;
        }
    }

    public void Chestplate() {
        armor = eqM.chestplate;

        if (!eqM.chestplateEquiped) {
            armor.SetActive(true);
            armor.GetComponent<SkinnedMeshRenderer>().material = material;
            eqM.SetArmor(armorStat);

            Instantiate(gameObject, eqM.eqChestplate.transform, false);
            eqM.chestplateEquiped = true;
            Destroy(gameObject);
        } else {
            armor.SetActive(false);
            eqM.SetArmor(-armorStat);

            DeleteFromEq();
            eqM.chestplateEquiped = false;
        }
    }

    public void Legs() {
        armor = eqM.legs;

        if (!eqM.legsEquiped) {
            armor.SetActive(true);
            armor.GetComponent<SkinnedMeshRenderer>().material = material;
            eqM.SetArmor(armorStat);

            Instantiate(gameObject, eqM.eqLegs.transform, false);
            eqM.legsEquiped = true;
            Destroy(gameObject);
        } else {
            armor.SetActive(false);
            eqM.SetArmor(-armorStat);

            DeleteFromEq();
            eqM.legsEquiped = false;
        }
    }

    private void DeleteFromEq() {
        for (int i = 0; i < inv.slots.Length; i++) {
            if (inv.isFull[i] == false) {
                inv.isFull[i] = true;
                Instantiate(gameObject, inv.slots[i].transform, false);
                Destroy(gameObject);
                break;
            }
        }
    }
}
