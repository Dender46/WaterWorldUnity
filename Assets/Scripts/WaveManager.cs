using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    [SerializeField] float amplitude = 1.0f;
    [SerializeField] float wavelength = 2.0f;
    [SerializeField] float speed = 1.0f;
    [SerializeField] float offset = 0.0f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Instance already exists, destroying 'this'!");
            Destroy(this);
        }
    }
    
    void OnValidate()
    {
        MeshRenderer rend = GetComponent<MeshRenderer>();
        rend.sharedMaterial.SetFloat("_Amplitude", amplitude);
        rend.sharedMaterial.SetFloat("_Wavelength", wavelength);
        rend.sharedMaterial.SetFloat("_Speed", speed);
        Debug.Log("asdasd");
    }

    void Update()
    {
        offset += Time.deltaTime * speed;
    }

    public float GetWaveHeight(float _x, float _z)
    {
        return amplitude * Mathf.Sin(_x / wavelength + offset) + amplitude * Mathf.Sin(_z / wavelength + offset);
    }
}
