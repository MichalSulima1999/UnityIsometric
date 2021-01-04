using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public bool isEnemy;
    public int dmg;
    private int dmgDealt;

    public GameObject dmgText;

    private EQManager eqM;
    private ThirdPersonMovement playerMovement;

    Camera cam;

    private void Start() {
        eqM = GameObject.FindGameObjectWithTag("Player").GetComponent<EQManager>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonMovement>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && isEnemy) {
            other.GetComponent<EQManager>().HpLoss(dmg);
        } else if(other.CompareTag("Enemy") && !isEnemy && playerMovement.attacking) {
            // hurt enemy
            int critRand = Random.Range(1, 101);
            if (critRand <= eqM.critChance)
                dmgDealt = (dmg + eqM.str) * 2;
            else
                dmgDealt = dmg + eqM.str;

            other.GetComponent<EnemyAI>().HpLoss(dmgDealt);
            Vector3 screenPos = cam.WorldToScreenPoint(other.transform.position);
            var textIns = Instantiate(dmgText, screenPos, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            textIns.GetComponent<Text>().text = "" + dmgDealt;
            textIns.GetComponent<Text>().color = Color.red;
        }

        if (gameObject.CompareTag("Bullet"))
            Destroy(gameObject);
    }
}
