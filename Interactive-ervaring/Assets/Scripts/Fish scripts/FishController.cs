using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    public float movementSpeed;
    private GameObject movePoint;
    private Fishing fishing;
    private Vector3 spawnPoint;

    private void Start()
    {
        fishing = FindObjectOfType<Fishing>();
        spawnPoint = new Vector3(Random.Range(-2.75f, 2.75f), Random.Range(-5.3f, 5.3f), 9.0f);

        GameObject moveToPoint = Instantiate(new GameObject(), spawnPoint, Quaternion.identity);

        movePoint = moveToPoint;
    }

    private void FixedUpdate()
    {
        MoveToPoint();
    }

    private void MoveToPoint()
    {
        Vector3 fishPosition = transform.position;
        Vector3 targetPosition = movePoint.transform.position;

        transform.position = Vector3.MoveTowards(fishPosition, Vector3.Lerp(fishPosition, targetPosition, 0.1f), movementSpeed);

        if(Vector3.Distance(fishPosition, targetPosition) < 1.0f)
        {
            spawnPoint = new Vector3(Random.Range(-2.75f, 2.75f), Random.Range(-5.3f, 5.3f), 9.0f);
            movePoint.transform.position = spawnPoint;
        }
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.GetComponent<Fishing>())
        {
            Inventory.GetInstance().amountOfFish++;
            fishing.rules.CheckOffQuest();
        }
    }
}
