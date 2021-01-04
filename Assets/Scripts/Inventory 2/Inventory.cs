using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;
    public GameObject uiInventory;

    private ThirdPersonMovement movement;

    private void Start() {
        movement = GetComponent<ThirdPersonMovement>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            if (uiInventory.activeSelf) {
                movement.canMove = true;
                uiInventory.SetActive(false);
            } else {
                movement.canMove = false;
                uiInventory.SetActive(true);
            }
        }
    }
}
