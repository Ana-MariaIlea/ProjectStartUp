using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public float MaxHealth = 100;
    public float CurrentHealth { get; private set; }

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        CurrentHealth -= damage;

       // Debug.Log(transform.name + " Takes " + damage + " damage");

        if (CurrentHealth < 0)
        {
            handleDeath();
            CurrentHealth = 0f;
        }
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;

        Debug.Log(transform.name + " Takes " + damage + " damage");

        if (CurrentHealth < 0)
        {
            handleDeath();
            CurrentHealth = 0f;
        }
    }

    public void Heal(int healAmount)
    {
        if (CurrentHealth + healAmount <= MaxHealth)
            CurrentHealth += healAmount;
        else if (CurrentHealth + healAmount > MaxHealth)
            CurrentHealth = MaxHealth;
    }

    public void Heal(float healAmount)
    {
        if (CurrentHealth + healAmount <= MaxHealth)
            CurrentHealth += healAmount;
        else if (CurrentHealth + healAmount > MaxHealth)
            CurrentHealth = MaxHealth;
    }

    /// <summary>
    /// This method is ment to be overwritten so that player and enemy can have their own death functions
    /// </summary>
    public virtual void handleDeath()
    {
        Debug.Log(transform.name + " died");
    }
}
