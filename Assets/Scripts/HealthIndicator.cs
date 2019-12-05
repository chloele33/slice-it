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

    private void Start()
    {
        //material = GetComponent<Renderer>().material;
        FindObjectOfType<PlayerHealth>().OnHealthPctChanged += HealthIndicator_OnHealthPctChanged;
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

        while (elapsed < updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            foregroundImage.fillAmount = Mathf.Lerp(preChangePct, pct, elapsed / updateSpeedSeconds);
            yield return null;
        }

        foregroundImage.fillAmount = pct;
    }

    //private void LateUpdate()
    //{
    //    transform.LookAt(Camera.main.transform);
    //    transform.Rotate(0, 180, 0);
    //}
}
