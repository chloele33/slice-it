using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 20;

    private int currentHealth;

    public event Action<float> OnHealthPctChanged = delegate { };

    //private void OnEnable()
    //{
    //    currentHealth = maxHealth;
    //}

    //public void ModifyHealth(int amount)
    //{
    //    currentHealth += amount;

    //    float currentHealthPct = (float)currentHealth / (float)maxHealth;
    //    OnHealthPctChanged(currentHealthPct);
    //}

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        ModifyHealth(-10);
    //    }
    //}

    private void Awake()
    {
        currentHealth = maxHealth;
        //GetComponentInChildren<SteamVR_TrackedController>().TriggerClicked += RemoveHealth;
    }

    public void ModifyHealth(int amount)
    {
        currentHealth -= amount;

        float currentHealthPct = (float)currentHealth / (float)maxHealth;
        OnHealthPctChanged(currentHealthPct);
    }

    private void RemoveHealth(object sender, ClickedEventArgs e)
    {
        //currentHealth--;

        ModifyHealth(1);
        //float pct = (float)currentHealth / (float)maxHealth;
        //OnHealthPctChanged(pct);
    }
}
