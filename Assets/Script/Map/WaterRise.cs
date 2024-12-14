using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class WaterRiseWithReset : MonoBehaviour
{
    [SerializeField] private float riseSpeed = 0.5f; 
    [SerializeField] private float maxHeight = 10f; 
    [SerializeField] private Slider waterLevelSlider; 
    [SerializeField] private string playerTag = "Player"; 

    private float startY; 

    void Start()
    {

        startY = transform.position.y;


        if (waterLevelSlider != null)
        {
            waterLevelSlider.minValue = 0f;
            waterLevelSlider.maxValue = 100f;
            waterLevelSlider.value = 0f;
        }
    }

    void Update()
    {

        if (transform.position.y < maxHeight)
        {
            transform.position += new Vector3(0, riseSpeed * Time.deltaTime, 0);


            UpdateSlider();
        }
    }

    void UpdateSlider()
    {
        if (waterLevelSlider != null)
        {

            float progress = Mathf.Clamp01((transform.position.y - startY) / (maxHeight - startY));
            waterLevelSlider.value = progress * 100f; 
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D tetiklendi, çarpýþan obje: " + collision.gameObject.name);
        if (collision.CompareTag(playerTag))
        {
            Debug.Log("Temas var");
            SceneManager.LoadScene("Menu");
        }
    }
}
