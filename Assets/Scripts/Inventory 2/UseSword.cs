using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseSword : MonoBehaviour
{
    public Vector3 pickPosition;
    public Vector3 pickRotation;
    public GameObject sword;
    private GameObject hand;

    private Inventory inventory;

    private EQManager eqM;
    private ThirdPersonMovement playerMovement;

    public int revolverDmg;

    public void Equip() {
        hand = GameObject.FindGameObjectWithTag("Hand R");
        eqM = GameObject.FindGameObjectWithTag("Player").GetComponent<EQManager>();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

        if (!eqM.weaponEquiped) {
            Instantiate(sword, hand.transform);
            Invoke("ChangeTransform", 0.1f);
            Instantiate(gameObject, eqM.eqWeapon.transform, false);

            eqM.weaponEquiped = true;
            eqM.swordEquiped = true;
            Invoke("DestroyObj", 0.1f);
        } else {
            foreach (Transform child in eqM.eqWeapon.transform) {
                if(GameObject.ReferenceEquals(child.gameObject, gameObject)) {
                    for (int i = 0; i < inventory.slots.Length; i++) {
                        if (inventory.isFull[i] == false) {
                            inventory.isFull[i] = true;
                            Instantiate(gameObject, inventory.slots[i].transform, false);
                            Destroy(gameObject);
                            break;
                        }
                    }
                    foreach (Transform child1 in hand.transform) {
                        GameObject.Destroy(child1.gameObject);
                    }
                    eqM.weaponEquiped = false;
                    eqM.swordEquiped = false;
                }
            }
        }
    }

    public void EquipRevolver() {
        hand = GameObject.FindGameObjectWithTag("Hand R");
        eqM = GameObject.FindGameObjectWithTag("Player").GetComponent<EQManager>();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonMovement>();

        if (!eqM.weaponEquiped) {
            Instantiate(sword, hand.transform);
            Invoke("ChangeTransform", 0.1f);
            Instantiate(gameObject, eqM.eqWeapon.transform, false);
            Invoke("AimFind", 0.1f);
            eqM.revolverDmg = revolverDmg;
            //playerMovement.FindAim();

            eqM.revolverEquiped = true;
            eqM.weaponEquiped = true;
            Invoke("DestroyObj", 0.1f);
        } else {
            foreach (Transform child in eqM.eqWeapon.transform) {
                if (GameObject.ReferenceEquals(child.gameObject, gameObject)) {
                    for (int i = 0; i < inventory.slots.Length; i++) {
                        if (inventory.isFull[i] == false) {
                            inventory.isFull[i] = true;
                            Instantiate(gameObject, inventory.slots[i].transform, false);
                            Destroy(gameObject);
                            break;
                        }
                    }
                    foreach (Transform child1 in hand.transform) {
                        GameObject.Destroy(child1.gameObject);
                    }
                    eqM.revolverEquiped = false;
                    eqM.weaponEquiped = false;
                }
            }
        }
    }

    public void AimFind() {
        playerMovement.FindAim();
    }

    public void ChangeTransform() {
        sword.transform.position = pickPosition;
        sword.transform.rotation = Quaternion.Euler(pickRotation);
    }

    private void DestroyObj() {
        Destroy(gameObject);
    }
}
