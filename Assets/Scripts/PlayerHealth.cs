using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 20;

    private int currentHealth;

	public bool shieldOn = false;
	public GameObject Health;

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

	public void Update()
	{
		/*if (shieldOn)
		{
			Health.GetComponent<HealthIndicator>().shieldHealth.color = new Color(0, 212, 255);
		}
		else
		{
			Health.GetComponent<HealthIndicator>().shieldHealth.color = new Color(0, 255, 5);
		}*/
	}

	private void Awake()
    {
        currentHealth = maxHealth;
		shieldOn = false;
        //GetComponentInChildren<SteamVR_TrackedController>().TriggerClicked += RemoveHealth;
    }

    public void ModifyHealth(int amount)
    {
		if (!shieldOn)
		{
			currentHealth -= amount;

			float currentHealthPct = (float)currentHealth / (float)maxHealth;
			OnHealthPctChanged(currentHealthPct);
		}
    }

    private void RemoveHealth(object sender, ClickedEventArgs e)
    {
		//currentHealth--;
		
		ModifyHealth(1);
		
        //float pct = (float)currentHealth / (float)maxHealth;
        //OnHealthPctChanged(pct);
    }
}
