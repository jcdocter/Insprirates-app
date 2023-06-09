using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    public float movementSpeed;

    private GameObject movePoint;
    private Fishing fishing;
    private Vector3 spawnPoint;
    private Animator animator;

    private void Start()
    {
        fishing = FindObjectOfType<Fishing>();
        spawnPoint = new Vector3(Random.Range(-2.75f, 2.75f), Random.Range(-5.3f, 5.3f), 9.0f);
        animator = GetComponent<Animator>();

        GameObject moveToPoint = Instantiate(new GameObject(), spawnPoint, Quaternion.identity);
        movePoint = moveToPoint;

    }

    private void FixedUpdate()
    {
        if (Inventory.GetInstance().amountOfFish <= 0 && fishing.rules.rewardObject)
        {
            animator.enabled = false;
            fishing.rules.SetPicture(true);

            if(fishing.rules.photoCapture.tookPhoto)
            {
                Inventory.GetInstance().amountOfFish++;
                fishing.tutorialClone.SetActive(true);
                FindObjectOfType<Tutorial>().LastLine();
                Destroy(this.gameObject);
            }

            return;
        }

        if (fishing.rules.rewardObject)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Inventory.GetInstance().amountOfFish++;
                animator.enabled = false;
                fishing.rules.CheckOffQuest();
            }
            return;
        }

        MoveToPoint();
    }

    private void MoveToPoint()
    {
        Vector3 fishPosition = transform.position;
        Vector3 targetPosition = movePoint.transform.position;

        transform.position = Vector3.MoveTowards(fishPosition, Vector3.Lerp(fishPosition, targetPosition, 0.1f), movementSpeed);

        Vector3 lookPos = targetPosition - transform.position;
        float rotationZ = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(rotationZ + 180.0f, -90.0f, 90.0f);

        if (Vector3.Distance(fishPosition, targetPosition) < 1.0f)
        {
            spawnPoint = new Vector3(Random.Range(-2.75f, 2.75f), Random.Range(-5.3f, 5.3f), 9.0f);
            movePoint.transform.position = spawnPoint;
        }
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.GetComponent<Fishing>())
        {
            fishing.amountOfFish++;
            Debug.Log(this.gameObject.name +" : "+ fishing.amountOfFish);
            this.gameObject.transform.localScale *= 5.0f;
            this.gameObject.transform.localRotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
            fishing.rules.rewardObject = this.gameObject;
            fishing.rules.ShowReward(FindObjectOfType<ObjectSpawner>().transform.position);

            fishing.RemoveFish();
        }
    }
}
