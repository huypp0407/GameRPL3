using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDame : MonoBehaviour
{
    public float health;
    public float lerpTime;
    public float maxHealth = 100;
    public float chipSpeed = 50f;
    public Image front;
    public Image back;

    private void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealth();
    }

    void UpdateHealth()
    {
        float fillF = front.fillAmount;
        float fillB = back.fillAmount;
        float hFraction = health / maxHealth;

        if (fillB > hFraction)
        {
             front.fillAmount = hFraction;
             back.color = Color.red;
             lerpTime += Time.deltaTime;
             float percemt = lerpTime / chipSpeed;
             back.fillAmount = Mathf.Lerp(fillB, hFraction, percemt);
        }

        if (fillF < hFraction)
        {
            back.fillAmount = hFraction;
            back.color = Color.green;
            lerpTime += Time.deltaTime;
            float percemt = lerpTime / chipSpeed;
            front.fillAmount = Mathf.Lerp(fillF, hFraction, percemt);
        }

    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTime = 0f;
    }

    public void Heal(float damage)
    {
        health += damage;
        lerpTime = 0f;
    }

    public void SetMaxHp(float hp)
    {
        this.maxHealth = hp;
    }

    public void SetCurrentHp(float hp)
    {
        this.front.fillAmount = hp/maxHealth;
        this.back.fillAmount = hp/maxHealth;
        this.health = hp;
    }
}
