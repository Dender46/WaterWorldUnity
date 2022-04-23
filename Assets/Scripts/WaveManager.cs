using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    [SerializeField] Vector4 waveA = new Vector4(1, 0, 0.5f, 10);
    [SerializeField] Vector4 waveB = new Vector4(0, 1, 0.25f, 20);
    [SerializeField] Vector4 waveC = new Vector4(1, 1, 0.15f, 10);

    [SerializeField] float time = 0.0f;

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
        rend.sharedMaterial.SetVector("_WaveA", waveA);
        rend.sharedMaterial.SetVector("_WaveB", waveB);
        rend.sharedMaterial.SetVector("_WaveC", waveC);
    }

    void Update()
    {
        time += Time.deltaTime;
    }

    public Vector3 GetWaveHeight(float _x, float _z)
    {
        Vector3 tangent = new Vector3(1, 0, 0);
        Vector3 binormal = new Vector3(0, 0, 1);
        Vector3 p = new Vector3(_x, 0, _z);
        p += GerstnerWave(waveA, p, ref tangent, ref binormal);
        p += GerstnerWave(waveB, p, ref tangent, ref binormal);
        p += GerstnerWave(waveC, p, ref tangent, ref binormal);
        return p;
    }

    Vector3 GerstnerWave(Vector4 wave, Vector3 p, ref Vector3 tangent, ref Vector3 binormal)
    {
        float steepness = wave.z;
        float wavelength = wave.w;
        float k = 2 * Mathf.PI / wavelength;
        float c = Mathf.Sqrt(9.8f / k);
        Vector2 d = Vector3.Normalize(wave);
        float f = k * (Vector3.Dot(d, p) - c * time);
        float a = steepness / k;

        tangent += new Vector3(
            -d.x * d.x * (steepness * Mathf.Sin(f)),
            d.x * (steepness * Mathf.Cos(f)),
            -d.x * d.y * (steepness * Mathf.Sin(f))
        );
        binormal += new Vector3(
            -d.x * d.y * (steepness * Mathf.Sin(f)),
            d.y * (steepness * Mathf.Cos(f)),
            -d.y * d.y * (steepness * Mathf.Sin(f))
        );
        return new Vector3(
            d.x * (a * Mathf.Cos(f)),
            a * Mathf.Sin(f),
            d.y * (a * Mathf.Cos(f))
        );
    }
}
