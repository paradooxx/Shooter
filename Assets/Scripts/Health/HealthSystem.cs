using System;

public class HealthSystem
{
    public event EventHandler OnHealthChanged,
                              OnHealthZero;

    private int health,
                healthMax;

    public HealthSystem(int healthMax)
    {
        this.healthMax = healthMax;
        health = healthMax;
    }

    public int GetHealth()
    {
        return health;
    }

    public float GetHealthPercent()
    {
        return (float)health / healthMax;
    }

    public void Damage(int damageAmount)
    {
        health -= damageAmount;
        if(health < 0) health = 0;
        if(OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
        HealthZeroEvent();
    }

    public void Heal(int healAmount)
    {
        health += healAmount;
        if(health > healthMax) health = healthMax;
        if(OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }

    public bool IsHealthZero()
    {
        if(health == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void HealthZeroEvent()
    {
        if(IsHealthZero())
        {
            if (OnHealthZero != null) OnHealthZero(this, EventArgs.Empty);
        }
    }
}
