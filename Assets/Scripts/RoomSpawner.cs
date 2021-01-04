using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour {
    public int openingDirection;
    // 1 need bottom door
    // 2 need top door
    // 3 need left door
    // 4 need right door

    private RoomTemplates templates;
    private int rand;
    private bool spawned = false;

    public float waitTime = 4f;

    // Start is called before the first frame update
    void Awake() {
        Destroy(gameObject, waitTime);
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }

    // Update is called once per frame
    void Spawn() {
        if (!spawned) {
            if (openingDirection == 1) {
                //bottom door
                rand = Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
            } else if (openingDirection == 2) {
                //top door
                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
            } else if (openingDirection == 3) {
                //left door
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
            } else if (openingDirection == 4) {
                // right door
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
            }
            spawned = true;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("SpawnPoint")) {
            if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false) {
                if ((other.GetComponent<RoomSpawner>().openingDirection == 1 && openingDirection == 2) || (other.GetComponent<RoomSpawner>().openingDirection == 2 && openingDirection == 1)) {
                    Instantiate(templates.bottomRooms[1], transform.position, templates.bottomRooms[1].transform.rotation);
                    Destroy(gameObject);
                } else if ((other.GetComponent<RoomSpawner>().openingDirection == 1 && openingDirection == 3) || (other.GetComponent<RoomSpawner>().openingDirection == 3 && openingDirection == 1)) {
                    Instantiate(templates.bottomRooms[3], transform.position, templates.bottomRooms[3].transform.rotation);
                    Destroy(gameObject);
                } else if ((other.GetComponent<RoomSpawner>().openingDirection == 1 && openingDirection == 4 || other.GetComponent<RoomSpawner>().openingDirection == 4 && openingDirection == 1)) {
                    Instantiate(templates.bottomRooms[3], transform.position, templates.bottomRooms[3].transform.rotation);
                    Destroy(gameObject);
                } else if ((other.GetComponent<RoomSpawner>().openingDirection == 2 && openingDirection == 3 || other.GetComponent<RoomSpawner>().openingDirection == 3 && openingDirection == 2)) {
                    Instantiate(templates.topRooms[2], transform.position, templates.topRooms[2].transform.rotation);
                    Destroy(gameObject);
                } else if ((other.GetComponent<RoomSpawner>().openingDirection == 2 && openingDirection == 4 || other.GetComponent<RoomSpawner>().openingDirection == 4 && openingDirection == 2)) {
                    Instantiate(templates.topRooms[3], transform.position, templates.topRooms[3].transform.rotation);
                    Destroy(gameObject);
                } else if ((other.GetComponent<RoomSpawner>().openingDirection == 3 && openingDirection == 4 || other.GetComponent<RoomSpawner>().openingDirection == 4 && openingDirection == 3)) {
                    Instantiate(templates.leftRooms[2], transform.position, templates.leftRooms[2].transform.rotation);
                    Destroy(gameObject);
                } else {
                    // spawn wall blocking openings
                    Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }

                
            }
            spawned = true;
        }
    }
}