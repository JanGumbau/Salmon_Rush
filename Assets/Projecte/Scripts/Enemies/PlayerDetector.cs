using System;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    public BearAttack enemyAttack;
    public string playerTag = "Player";

    private void Reset()
    {
        var col = GetComponent<Collider2D>();
        col.isTrigger = true;
        if (GetComponent<Rigidbody2D>() == null)
        {
            var rb = gameObject.AddComponent<Rigidbody2D>();
            rb.isKinematic = true;
            rb.gravityScale = 0f;
        }

        if (enemyAttack == null)
            enemyAttack = GetComponentInParent<BearAttack>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag)) return;
        if (enemyAttack != null)
            enemyAttack.TriggerAttack();
    }

    }
