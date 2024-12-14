using UnityEngine;
using UnityEngine.UI;

public class WaterRiseWithSlider : MonoBehaviour
{
    [SerializeField] private float riseSpeed = 0.5f; // Suyun yükselme hýzý
    [SerializeField] private float maxHeight = 10f; // Suyun ulaþabileceði maksimum yükseklik
    [SerializeField] private Slider waterLevelSlider; // Baðlý Slider UI elemaný

    private float startY; // Suyun baþlangýç yüksekliði

    void Start()
    {
        // Su baþlangýç yüksekliðini kaydet
        startY = transform.position.y;

        // Slider'ý baþlangýç deðerine ayarla
        if (waterLevelSlider != null)
        {
            waterLevelSlider.minValue = 0f;
            waterLevelSlider.maxValue = 100f;
            waterLevelSlider.value = 0f;
        }
    }

    void Update()
    {
        // Suyu yükselt
        if (transform.position.y < maxHeight)
        {
            transform.position += new Vector3(0, riseSpeed * Time.deltaTime, 0);

            // Slider'ý güncelle
            UpdateSlider();
        }
    }

    void UpdateSlider()
    {
        if (waterLevelSlider != null)
        {
            // Suyun mevcut yüksekliðini maksimum yüksekliðe oranla hesapla
            float progress = Mathf.Clamp01((transform.position.y - startY) / (maxHeight - startY));
            waterLevelSlider.value = progress * 100f; // Slider'ý 0-100 arasý deðer alacak þekilde güncelle
        }
    }
}
