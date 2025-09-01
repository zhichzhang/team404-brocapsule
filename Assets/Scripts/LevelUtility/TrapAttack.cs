using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Splines;

public class TrapAttack : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public SpriteRenderer spriteRenderer;
    public SplineAnimate attackAni;
    public SplineAnimate goBackAni;
    public GameObject attackBoxToEnemy;
    public GameObject attackBoxToPlayer;
    public float attackCoolDown;
    public float attackDuration;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Vector2 centerOffset;
    [SerializeField] private Vector2 detectionBoxSize;
    [SerializeField] private bool showGizmo;
    private bool s1;
    private bool s2;
    private Color originalColor;
    private bool myLock;
    private float coolDownTimer;
   
    

    private bool isAttacking = false;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        s1 = false;
        s2 = false;
        myLock = false;
    }

    // Update is called once per frame
    void Update()
    {
        coolDownTimer -= Time.deltaTime;
        Player player = isPlayerInRange();
        if (player != null)
        {
            if (coolDownTimer < 0)
            {
                coolDownTimer = attackCoolDown;
                StartAttack();
            }
            
        }
        //if (Input.GetKeyDown(KeyCode.C))
        //{
            
        //}
    }

    private void LateUpdate()
    {
        
        if (isAttacking)
        {
            if (!attackBoxToEnemy.activeSelf)
            {
                if (!s1)
                {
                    StartCoroutine(ResetAttackBoxToEnemyCoroutine());
                }

            }
            if (!attackBoxToPlayer.activeSelf)
            {
                if (!s2)
                {
                    StartCoroutine(ResetAttackBoxToPlayerCoroutine());
                }

            }
        }
    }

    public void StartAttack()
    {
        if (myLock) return;
        StartCoroutine(nameof(FlashColorAttackCoroutine));
        StartCoroutine(nameof(StopAttackCoroutine));

    }


    IEnumerator ResetAttackBoxToEnemyCoroutine()
    {
        s1 = true;
        yield return new WaitForSeconds(1f);
        if (isAttacking)
            attackBoxToEnemy.SetActive(true);
        s1 = false;
    }

    IEnumerator ResetAttackBoxToPlayerCoroutine()
    {
        s2 = true;
        yield return new WaitForSeconds(1f);
        if (isAttacking)
            attackBoxToPlayer.SetActive(true);
        s2 = false;
    }

    IEnumerator FlashColorAttackCoroutine()
    {
        myLock = true;
        Color originalColor = spriteRenderer.color;
        Color flashColor = Color.red;
        float flashDuration = 0.4f; 
        int numberOfFlashes = 5;

        for (int i = 0; i < numberOfFlashes; i++)
        {
            // Switch between original and flash color
            spriteRenderer.color = spriteRenderer.color == originalColor ? flashColor : originalColor;
            yield return new WaitForSeconds(flashDuration);
        }

        // Ensure the color is reset to original after flashing
        if (!isAttacking){
            attackBoxToEnemy.SetActive(true);
            attackBoxToPlayer.SetActive(true);
            isAttacking = true;
            attackAni.Restart(true);
        }
        
    }

    IEnumerator StopAttackCoroutine()
    {
        //Debug.Log("StopAttackCoroutine");
        yield return new WaitForSeconds(attackDuration);
        if (isAttacking)
        {
            attackBoxToEnemy.SetActive(false);
            attackBoxToPlayer.SetActive(false);
            isAttacking = false;
            goBackAni.Restart(true);
            spriteRenderer.color = originalColor;
        }

        myLock = false;
    }

    public Player isPlayerInRange()
    {
        Vector2 detectBoxCenter = (Vector2)transform.position + centerOffset;
        Vector2 detectBoxC1 = detectBoxCenter + detectionBoxSize / 2;
        Vector2 detectBoxC2 = detectBoxCenter - detectionBoxSize / 2;
        Collider2D[] colliders = Physics2D.OverlapAreaAll(detectBoxC1, detectBoxC2, playerLayer);
        foreach (var hit in colliders)
        {
            Player player = hit.GetComponent<Player>();
            if (player != null)
            {
                return player;
            }
        }
        return null;
    }

    private void OnDrawGizmos()
    {
        if (showGizmo)
        {
            Gizmos.color = Color.red;
            Vector2 boxCenter = (Vector2)transform.position + centerOffset;
            Gizmos.DrawWireCube(boxCenter, detectionBoxSize);
            
        }

    }
}
