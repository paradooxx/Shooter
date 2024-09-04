using System;
using System.Collections;
using UnityEngine;

public class BarrelRoll : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    [SerializeField] private HealthBar healthBar;
    [SerializeField] private int maxBarrelHealth = 100;
    [SerializeField] private int barrelDamage = 20;
    [SerializeField] private GameObject destroyEffect;
    [SerializeField] private PlayerMultiplier playerMultiplier;
    [SerializeField] private Renderer characterRenderer;
    [SerializeField] private Color hitColor = Color.red;
    [SerializeField] private float blipDuration = 0.1f;
    private Color originalColor;
    private HealthSystem healthSystem;

    private void Start()
    {
        healthSystem = new HealthSystem(maxBarrelHealth);

        healthBar.Setup(healthSystem);
        healthSystem.OnHealthZero += HealthSystem_OnHealthZero;

        if (characterRenderer != null)
        {
            originalColor = characterRenderer.material.color;
        }
        else
        {
            return;
        }
    }

    private void OnDisable()
    {
        healthSystem.OnHealthZero -= HealthSystem_OnHealthZero;
    }

    private void HealthSystem_OnHealthZero(object sender, EventArgs e)
    {
        GameObject effect = Instantiate(destroyEffect, transform);
        SFXManager.Instance.PlaySound(SoundType.BarrelDestroy, transform);
        effect.transform.SetParent(null);
        playerMultiplier.PlayerInstant();
        Destroy(gameObject);
    }

    private void Update()
    {
        transform.Translate(-Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Bullet"))
        {
            var damage = collider.gameObject.GetComponent<Bullet>().bulletDamage;
            healthSystem.Damage(damage * Bullet.bulletDamageModifier);
            healthSystem.HealthZeroEvent();
            StartCoroutine(BlipEffect());
        }
        if(collider.CompareTag("Player"))
        {
            var player = collider.gameObject.GetComponent<PlayerHealth>();
            player.TakeDamage(barrelDamage);
        }
    }

    private IEnumerator BlipEffect()
    {
        characterRenderer.material.color = hitColor;
        yield return new WaitForSeconds(blipDuration);
        characterRenderer.material.color = originalColor;
    }
}
