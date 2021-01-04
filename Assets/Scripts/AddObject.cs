using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddObject : MonoBehaviour
{
    public bool isChest;

    private Objects obj;

    int rand;

    void Start() {
        obj = FindObjectOfType<Objects>();
        Invoke("Spawn", 0.1f);
    }

    void Spawn() {
        if (isChest) {
            rand = Random.Range(0, obj.chests.Length);
            Instantiate(obj.chests[rand], transform.position, Quaternion.identity);
        } else {
            rand = Random.Range(0, obj.decorative.Length);
            Instantiate(obj.decorative[rand], transform.position, Quaternion.identity);
        }
    }

}
