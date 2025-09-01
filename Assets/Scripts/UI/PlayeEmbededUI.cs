using System.Collections;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerEmbeddedUI : MonoBehaviour
{
    [Header("Health Bars")]
    public StatefulSprite b0;
    public StatefulSprite b1;
    public StatefulSprite b2;
    public StatefulSprite b3;
    public StatefulSprite eye;

    [Header("Mana Bars")]
    public StatefulSprite m2_0;
    public StatefulSprite m2_1;
    public StatefulSprite m3_0;
    public StatefulSprite m3_1;
    public StatefulSprite m3_2;
    public StatefulSprite charge;
    public float flashDuration = 0.1f; // time for charge to stay lit (alpha = 1)
    private Coroutine chargeFlashCoroutine;
    [Header ("chargeIcon")]
    public Light2D chargeLight2D;

    public void chargeFlash()
    {
        if (chargeFlashCoroutine != null)
        {
            StopCoroutine(chargeFlashCoroutine);
        }
        chargeFlashCoroutine = StartCoroutine(ChargeFlashCoroutine());
    }

    private IEnumerator ChargeFlashCoroutine()
    {
        float time = 0;
        Color startColor = charge.renderer.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1);

        // Change alpha from 0 to 1
        while (time < flashStartDuration)
        {
            charge.renderer.color = Color.Lerp(startColor, targetColor, time / flashStartDuration);
            chargeLight2D.intensity = Mathf.Lerp(0f, 4f, time / flashStartDuration);
            time += Time.deltaTime;
            yield return null;
        }
        charge.renderer.color = targetColor;

        // Wait for flashDuration
        yield return new WaitForSeconds(flashDuration);

        // Change alpha from 1 to 0
        time = 0;
        startColor = charge.renderer.color;
        targetColor = new Color(startColor.r, startColor.g, startColor.b, 0);
        while (time < flashStartDuration)
        {
            charge.renderer.color = Color.Lerp(startColor, targetColor, time / flashStartDuration);
            chargeLight2D.intensity = Mathf.Lerp(4f, 0f, time / flashStartDuration);
            time += Time.deltaTime;
            yield return null;
        }
        charge.renderer.color = targetColor;
        chargeLight2D.intensity = 0f; // Ensure final intensity is set
    }
    public float flashStartDuration = 0.1f; // time for charge to change alpha from 0 to 1(or from 1 to 0)


    [Header("Stats")]
    public int maxHealth;
    public int health;
    public int maxMana;
    public int mana;

    [Header("Effect")]
    public float effectDuration = 0.05f;

    [Header("Colors")]
    [SerializeField] private Color l3 = Color.magenta;
    [SerializeField] private Color l2 = Color.white;
    [SerializeField] private Color l1 = Color.yellow;
    [SerializeField] private Color l0 = Color.red;

    private bool isBlocking = false;

    public bool animationTrigger = false;

    private Coroutine b0Coroutine;
    private Coroutine b1Coroutine;
    private Coroutine b2Coroutine;
    private Coroutine b3Coroutine;
    private Coroutine eyeCoroutine;

    void Start()
    {
        InitializeSpriteColors();
        if (maxHealth > 13)
        {
            Debug.LogError("Max health cannot be greater than 13");
        }

        health = PlayerInfo.instance.player.Health;
        mana = PlayerInfo.instance.player.Mana;
        SetColorFaceRight();
        syncMana();
    }

    // Update is called once per frame
    void Update()
    {
        // Handling health or other updates
        

        health = PlayerInfo.instance.player.Health;
        mana = PlayerInfo.instance.player.Mana;
        if (PlayerInfo.instance.player.facingDir == 1)
        {
            SetColorFaceRight();
        }
        else
        {
            SetColorFaceLeft();
        }

        syncMana();


    }

    
    public void increaseHealth()
    {
        if (health >= maxHealth)
        {
            return;
        }
        switch (health % 4)
        {
            case 1:
                SpriteIncreaseInstant(b3);
                SpriteIncreaseInstant(eye);
                health++;
                break;
            case 2:
                SpriteIncreaseInstant(b2);
                health++;
                break;
            case 3:
                SpriteIncreaseInstant(b1);
                health++;
                break;
            case 0:
                SpriteIncreaseInstant(b0);
                health++;
                break;
        }
    }

    public void decreaseHealth()
    {
        if (health <= 1)
        {
            // do death logic
            return;
        }
        switch (health % 4)
        {
            case 1:
                SpriteDecreaseInstant(b0);
                health--;
                break;
            case 0:
                SpriteDecreaseInstant(b1);
                health--;
                break;
            case 3:
                SpriteDecreaseInstant(b2);
                health--;
                break;
            case 2:
                SpriteDecreaseInstant(b3);
                SpriteDecreaseInstant(eye);
                health--;
                break;

        }
    }

    public void increaseMana()
    {
        switch (mana)
        {
            case 0:
                toSolid(m2_0);
                mana++;
                break;
            case 1:
                toSolid(m2_1);
                mana++;
                break;
            default:
                break;

        }
    }

    public void decreaseMana()
    {
        switch (mana)
        {
            case 1:
                toTransparent(m2_0);
                mana--;
                break;
            case 2:
                toTransparent(m2_1);
                mana--;
                break;
            default:
                break;
        }
    }

    public void syncMana()
    {
        
        if (isBlocking)
        {
            toTransparent(m2_0);
            toTransparent(m2_1);
        }
        else if (mana == 0)
        {
            toTransparent(m2_0);
            toTransparent(m2_1);
        }
        else if (mana == 1)
        {
            toSolid(m2_0);
            toTransparent(m2_1);
        }
        else if (mana == 2)
        {
            toSolid(m2_0);
            toSolid(m2_1);
        }
    }
    private void SpriteDecrease(StatefulSprite b, float duration)
    {
        if (b == b0)
        {
            if (b0Coroutine != null) StopCoroutine(b0Coroutine);
            b0Coroutine = StartCoroutine(DecreaseColorCoroutine(b, duration));
        }
        else if (b == b1)
        {
            if (b1Coroutine != null) StopCoroutine(b1Coroutine);
            b1Coroutine = StartCoroutine(DecreaseColorCoroutine(b, duration));
        }
        else if (b == b2)
        {
            if (b2Coroutine != null) StopCoroutine(b2Coroutine);
            b2Coroutine = StartCoroutine(DecreaseColorCoroutine(b, duration));
        }
        else if (b == b3)
        {
            if (b3Coroutine != null) StopCoroutine(b3Coroutine);
            b3Coroutine = StartCoroutine(DecreaseColorCoroutine(b, duration));
        }
        else if (b == eye)
        {
            if (eyeCoroutine != null) StopCoroutine(eyeCoroutine);
            eyeCoroutine = StartCoroutine(DecreaseColorCoroutine(b, duration));
        }
    }

    private void SpriteDecreaseInstant(StatefulSprite sprite)
    {
        switch (sprite.state)
        {
            case 3:
                sprite.renderer.color = l2;
                break;
            case 2:
                sprite.renderer.color = l1;
                break;
            case 1:
                sprite.renderer.color = l0;
                break;
            default:
                sprite.renderer.color = l0;
                return;
        }

        sprite.state = sprite.state - 1; // Decrease state
    }

    private void SpriteIncreaseInstant(StatefulSprite sprite)
    {
        switch (sprite.state)
        {
            case 2:
                sprite.renderer.color = l3;
                break;
            case 1:
                sprite.renderer.color = l2;
                break;
            case 0:
                sprite.renderer.color = l1;
                break;
            default:
                sprite.renderer.color = l3;
                return;
        }

        sprite.state = sprite.state + 1; // Decrease state
    }

    private IEnumerator DecreaseColorCoroutine(StatefulSprite sprite, float duration)
    {
        Color targetColor = l0; // Default to l0
        switch (sprite.state)
        {
            case 3:
                sprite.renderer.color = l3; // reset to l3
                targetColor = l2; // Change to l2
                break;
            case 2:
                sprite.renderer.color = l2;
                targetColor = l1; 
                break;
            case 1:
                sprite.renderer.color = l1; 
                targetColor = l0; 
                break;
            default:
                sprite.renderer.color = l0;
                yield break;
        }

        sprite.state = sprite.state - 1; // Decrease state

        Color startColor = sprite.renderer.color;
        float time = 0;
        while (time < duration)
        {
            sprite.renderer.color = Color.Lerp(startColor, targetColor, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        sprite.renderer.color = targetColor; // Ensure final color is set
        
        
    }

    private void InitializeSpriteColors()
    {
        InitializeSpriteColor(b0);
        InitializeSpriteColor(b1);
        InitializeSpriteColor(b2);
        InitializeSpriteColor(b3);
        InitializeSpriteColor(eye);
    }

    private void InitializeSpriteColor(StatefulSprite sprite)
    {
        switch (sprite.state)
        {
            case 0:
                sprite.renderer.color = l0;
                break;
            case 1:
                sprite.renderer.color = l1;
                break;
            case 2:
                sprite.renderer.color = l2;
                break;
            case 3:
                sprite.renderer.color = l3;
                break;
            default:
                throw new System.ArgumentOutOfRangeException("Invalid state value for sprite color initialization");
        }
    }

    private void toTransparent(StatefulSprite s)
    {
        Color currentColor = s.renderer.color;
        s.renderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, 0);
        //chargeLight2D.intensity = 0f;
    }

    private void toSolid(StatefulSprite s)
    {
        Color currentColor = s.renderer.color;
        s.renderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, 0.4f);
        //chargeLight2D.intensity = 4f;
    }

    private void SetColorFaceRight()
    {
        switch (health)
        {
            case 1:
                b0.renderer.color = l0;
                b1.renderer.color = l0;
                b2.renderer.color = l0;
                b3.renderer.color = l0;
                eye.renderer.color = l0;
                break;
            case 2:
                b0.renderer.color = l0;
                b1.renderer.color = l0;
                b2.renderer.color = l0;
                b3.renderer.color = l1;
                eye.renderer.color = l1;
                break;
            case 3:
                b0.renderer.color = l0;
                b1.renderer.color = l0;
                b2.renderer.color = l1;
                b3.renderer.color = l1;
                eye.renderer.color = l1;
                break;
            case 4:
                b0.renderer.color = l0;
                b1.renderer.color = l1;
                b2.renderer.color = l1;
                b3.renderer.color = l1;
                eye.renderer.color = l1;
                break;
            case 5:
                b0.renderer.color = l1;
                b1.renderer.color = l1;
                b2.renderer.color = l1;
                b3.renderer.color = l1;
                eye.renderer.color = l1;
                break;
            case 6:
                b0.renderer.color = l1;
                b1.renderer.color = l1;
                b2.renderer.color = l1;
                b3.renderer.color = l2;
                eye.renderer.color = l2;
                break;
            case 7:
                b0.renderer.color = l1;
                b1.renderer.color = l1;
                b2.renderer.color = l2;
                b3.renderer.color = l2;
                eye.renderer.color = l2;
                break;
            case 8:
                b0.renderer.color = l1;
                b1.renderer.color = l2;
                b2.renderer.color = l2;
                b3.renderer.color = l2;
                eye.renderer.color = l2;
                break;
            case 9:
                b0.renderer.color = l2;
                b1.renderer.color = l2;
                b2.renderer.color = l2;
                b3.renderer.color = l2;
                eye.renderer.color = l2;
                break;
            case 10:
                b0.renderer.color = l2;
                b1.renderer.color = l2;
                b2.renderer.color = l2;
                b3.renderer.color = l3;
                eye.renderer.color = l3;
                break;
            case 11:
                b0.renderer.color = l2;
                b1.renderer.color = l2;
                b2.renderer.color = l3;
                b3.renderer.color = l3;
                eye.renderer.color = l3;
                break;
            case 12:
                b0.renderer.color = l2;
                b1.renderer.color = l3;
                b2.renderer.color = l3;
                b3.renderer.color = l3;
                eye.renderer.color = l3;
                break;
            case 13:
                b0.renderer.color = l3;
                b1.renderer.color = l3;
                b2.renderer.color = l3;
                b3.renderer.color = l3;
                eye.renderer.color = l3;
                break;
            default:
                Debug.LogError("Invalid health value");
                break;
        }
    }

    public void SetColorFaceLeft()
    {
        switch (health)
        {
            case 1:
                b0.renderer.color = l0;
                b1.renderer.color = l0;
                b2.renderer.color = l0;
                b3.renderer.color = l0;
                eye.renderer.color = l0;
                break;
            case 2:
                b0.renderer.color = l0;
                b1.renderer.color = l0;
                b2.renderer.color = l1;
                b3.renderer.color = l0;
                eye.renderer.color = l1;
                break;
            case 3:
                b0.renderer.color = l0;
                b1.renderer.color = l0;
                b2.renderer.color = l1;
                b3.renderer.color = l1;
                eye.renderer.color = l1;
                break;
            case 4:
                b0.renderer.color = l1;
                b1.renderer.color = l0;
                b2.renderer.color = l1;
                b3.renderer.color = l1;
                eye.renderer.color = l1;
                break;
            case 5:
                b0.renderer.color = l1;
                b1.renderer.color = l1;
                b2.renderer.color = l1;
                b3.renderer.color = l1;
                eye.renderer.color = l1;
                break;
            case 6:
                b0.renderer.color = l1;
                b1.renderer.color = l1;
                b2.renderer.color = l2;
                b3.renderer.color = l1;
                eye.renderer.color = l2;
                break;
            case 7:
                b0.renderer.color = l1;
                b1.renderer.color = l1;
                b2.renderer.color = l2;
                b3.renderer.color = l2;
                eye.renderer.color = l2;
                break;
            case 8:
                b0.renderer.color = l2;
                b1.renderer.color = l1;
                b2.renderer.color = l2;
                b3.renderer.color = l2;
                eye.renderer.color = l2;
                break;
            case 9:
                b0.renderer.color = l2;
                b1.renderer.color = l2;
                b2.renderer.color = l2;
                b3.renderer.color = l2;
                eye.renderer.color = l2;
                break;
            case 10:
                b0.renderer.color = l2;
                b1.renderer.color = l2;
                b2.renderer.color = l3;
                b3.renderer.color = l2;
                eye.renderer.color = l3;
                break;
            case 11:
                b0.renderer.color = l2;
                b1.renderer.color = l2;
                b2.renderer.color = l3;
                b3.renderer.color = l3;
                eye.renderer.color = l3;
                break;
            case 12:
                b0.renderer.color = l3;
                b1.renderer.color = l2;
                b2.renderer.color = l3;
                b3.renderer.color = l3;
                eye.renderer.color = l3;
                break;
            case 13:
                b0.renderer.color = l3;
                b1.renderer.color = l3;
                b2.renderer.color = l3;
                b3.renderer.color = l3;
                eye.renderer.color = l3;
                break;
            default:
                Debug.LogError("Invalid health value");
                break;


        }
    }

    public void startBlock()
    {
        isBlocking = true;
        
    }
    public void endBlock()
    {
        isBlocking = false;
    }

    public void SetAnimTrigger()
    {
        animationTrigger = true;
    }

    public void UnSetAnimTrigger()
    {
        animationTrigger = false;
    }

}