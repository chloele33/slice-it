using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthIndicator : MonoBehaviour
{
    //private Material material;
    [SerializeField]
    private Image foregroundImage;

    [SerializeField]
    private float updateSpeedSeconds = 0.5f;

	[SerializeField]
	//private Image redScreen;


    private void Start()
    {
        //material = GetComponent<Renderer>().material;
        FindObjectOfType<PlayerHealth>().OnHealthPctChanged += HealthIndicator_OnHealthPctChanged;

		//redScreen.enabled = false;
    }

    private void HealthIndicator_OnHealthPctChanged(float pct)
    {
        //material.SetFloat("_Cutoff", 1f - pct);
        StartCoroutine(ChangeToPct(pct));
    }

    private IEnumerator ChangeToPct (float pct)
    {
        float preChangePct = foregroundImage.fillAmount;
        float elapsed = 0f;

		//redScreen.color = new Color(255, 0, 0, 90);
		//redScreen.enabled = true;

        while (elapsed < updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            foregroundImage.fillAmount = Mathf.Lerp(preChangePct, pct, elapsed / updateSpeedSeconds);
			float a = Mathf.Lerp(90, 0, elapsed / updateSpeedSeconds);
			//redScreen.color = new Color(255, 0, 0, a);

			yield return null;
        }

        foregroundImage.fillAmount = pct;
		//redScreen.enabled = false;
    }

    //private void LateUpdate()
    //{
    //    transform.LookAt(Camera.main.transform);
    //    transform.Rotate(0, 180, 0);
    //}
}
