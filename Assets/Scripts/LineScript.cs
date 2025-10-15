using UnityEngine;

public class LineScript : MonoBehaviour
{
    private Renderer rend;
    public Color glowColor = Color.green;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rend.material.EnableKeyword("_EMISSION");
            rend.material.SetColor("_EmissionColor", glowColor);
            rend.material.color = glowColor;
        }
    }
}
