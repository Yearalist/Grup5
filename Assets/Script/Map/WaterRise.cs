using UnityEngine;
using UnityEngine.UI;

public class WaterRiseWithSlider : MonoBehaviour
{
    [SerializeField] private float riseSpeed = 0.5f; // Suyun y�kselme h�z�
    [SerializeField] private float maxHeight = 10f; // Suyun ula�abilece�i maksimum y�kseklik
    [SerializeField] private Slider waterLevelSlider; // Ba�l� Slider UI eleman�

    private float startY; // Suyun ba�lang�� y�ksekli�i

    void Start()
    {
        // Su ba�lang�� y�ksekli�ini kaydet
        startY = transform.position.y;

        // Slider'� ba�lang�� de�erine ayarla
        if (waterLevelSlider != null)
        {
            waterLevelSlider.minValue = 0f;
            waterLevelSlider.maxValue = 100f;
            waterLevelSlider.value = 0f;
        }
    }

    void Update()
    {
        // Suyu y�kselt
        if (transform.position.y < maxHeight)
        {
            transform.position += new Vector3(0, riseSpeed * Time.deltaTime, 0);

            // Slider'� g�ncelle
            UpdateSlider();
        }
    }

    void UpdateSlider()
    {
        if (waterLevelSlider != null)
        {
            // Suyun mevcut y�ksekli�ini maksimum y�ksekli�e oranla hesapla
            float progress = Mathf.Clamp01((transform.position.y - startY) / (maxHeight - startY));
            waterLevelSlider.value = progress * 100f; // Slider'� 0-100 aras� de�er alacak �ekilde g�ncelle
        }
    }
}
