using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DrpSpearVertical : MonoBehaviour, SpearLifeTimeReset
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public enum SpearState
    {
        InAir,
        OnGround
    }

    public enum SpearType
    {
        Up,
        Down,
    }

    public enum SpearUse
    {
        Player,
        Level
    }
    public SpearType type;
    public SpearState state;
    public SpearUse useType;
    public float moveSpeed;
    public float liveTime;
    public Transform wallCheckPosition;
    public float wallCheckDistance;
    public LayerMask wallLayer;
    private Rigidbody2D rb;
    public GameObject attackBox;
    public Transform boundPosition;
    public TMPro.TextMeshProUGUI climbNote;
    public TMPro.TextMeshProUGUI stopClimbNote;
    public SpriteRenderer tip;
    public SpriteRenderer body;
    public Color normal;
    public Color mount;
    private LookAtPlayerWhenActive magnet;
    public float customLayeredDeadZone = 0.01f;
    private bool isCountingDown = false;
    [Tooltip("Layers for both horizontal and vertial spear, this is for destory check when spear is close to another")]
    public LayerMask spearsLayer;
    private Light2D light2D; 
    public float volumeIntensity = 0.2f;
    void Start()
    {
        state = SpearState.InAir;
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        magnet = GetComponentInChildren<LookAtPlayerWhenActive>();
        light2D = GetComponentInChildren<Light2D>();

        if (useType == SpearUse.Level)
        {
            state = SpearState.OnGround;
            StuckToLevel();
        }
        else
        {
            StartCoroutine(nameof(DestroySpearCoroutine));
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case SpearState.InAir:
                rb.linearVelocity = -transform.up * moveSpeed;
                checkCollision();
                break;
            case SpearState.OnGround:
                if (isCountingDown)
                {
                    return;
                }
                
                if (PlayerInfo.instance.player.currentInteractingSpear == this && PlayerInfo.instance.player.stateMachine.currentState.animBoolName == "LadderMove")
                {
                    if (Mathf.Abs(PlayerInfo.instance.playerPosition.x - transform.position.x) < PlayerInfo.instance.player.ladderCenterDeadZone + customLayeredDeadZone)
                    {
                        if (PlayerInput.instance.Xinput == 0)
                        {
                            SetColor(mount);
                            if (magnet.isActive)
                            {
                                magnet.Hide();
                            }
                        }
                        else
                        {
                            SetColor(normal);
                            if (!magnet.isActive)
                            {
                                magnet.Show();
                            }
                        }
                    }
                    else
                    {
                        SetColor(normal);
                        if (!magnet.isActive)
                        {
                            magnet.Show();
                        }
                    }
                }
                else
                {
                    SetColor(normal);
                    if (magnet.isActive)
                    {
                        magnet.Hide();
                    }
                }
                break;
        }
    }

    private void checkCollision()
    {
        if (state == SpearState.OnGround)
        {
            return;
        }
        bool isWallDetected = Physics2D.Raycast(wallCheckPosition.position, wallCheckPosition.up, wallCheckDistance, wallLayer);
        if (isWallDetected)
        {
            StuckToLevel();
        }
    }

    private void StuckToLevel()
    {
        state = SpearState.OnGround;
        rb.linearVelocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        attackBox.SetActive(false);

        // destroy spear if there is some close by spear and reset the life time of that spear
        Collider2D[] results = new Collider2D[10];
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(spearsLayer);
        filter.useTriggers = true;
        int colliderCount = GetComponent<Collider2D>().Overlap(filter, results);

        for (int i = 0; i < colliderCount; i++)
        {
            Debug.Log(results[i].gameObject.name);
            var spear = results[i].GetComponent<SpearLifeTimeReset>();
            if (spear != null)
            {
                spear.ResetLifeTime();
                Destroy(gameObject);
                return;
            }
        }

        StartCoroutine(nameof(DisableAttacBoxCoroutine));
        // use overlap collider to check for other spears (Class DrpSpearVertical or DraupnirSpear) , both have implemented SpearLifeTimeReset
    }

    //IEnumerator DestroySpearVCoroutine()
    //{
    //    yield return new WaitForSeconds(liveTime);
    //    Destroy(gameObject);
    //}

    IEnumerator DisableAttacBoxCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        attackBox.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(wallCheckPosition.position, wallCheckPosition.position + wallCheckPosition.up * wallCheckDistance);
    }


    public void SetColor(Color color)
    {
        tip.color = color;
        body.color = color;
        if (light2D == null) { return; }
        if (color == normal)
        {
            light2D.volumeIntensity = 0f;
        }
        else
        {
            light2D.volumeIntensity = volumeIntensity;
        }
    }

    IEnumerator DestroySpearCoroutine()
    {
        float flashDuration = 0.2f;
        int numberOfFlashes = 3; 
        yield return new WaitForSeconds(liveTime);
        isCountingDown = true;
        for (int i = 0; i < numberOfFlashes; i++)
        {
            SetColor(normal);
            yield return new WaitForSeconds(flashDuration);
            SetColor(mount);
            yield return new WaitForSeconds(flashDuration);
        }
        Destroy(gameObject);
    }

    public void ResetLifeTime()
    {
        if (useType == SpearUse.Level)
        {
            return;
        }
        else
        {
            StopCoroutine(nameof(DestroySpearCoroutine));
            StartCoroutine(nameof(DestroySpearCoroutine));
        }
    }
}
