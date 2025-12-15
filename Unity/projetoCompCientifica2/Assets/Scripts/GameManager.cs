using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("Cenários")]
    public GameObject chaoTerra;
    public GameObject ondaTerra;
    public Material skyTerra;
    
    public Material skyEspaco;
    
    public GameObject chaoTita;
    public GameObject ondaTita;
    public Material skyTita;

    [Header("Efeitos Visuais")]
    public GameObject poeiraEspacial; // <--- NOVO: Arraste a 'PoeiraEspacial' aqui

    private Texture2D texPreta;
    private float fade = 0f;

    void Start()
    {
        texPreta = new Texture2D(1, 1);
        texPreta.SetPixel(0, 0, Color.black);
        texPreta.Apply();

        StartCoroutine(Sequencia());
    }

    IEnumerator Sequencia()
    {
        // ===== 4s TERRA =====
        MostrarTerra();
        yield return new WaitForSeconds(4f);

        // ===== 1s TRANSIÇÃO TERRA → ESPAÇO =====
        yield return Fade(0.5f, true);  // 0.5s escurece
        MostrarEspaco();
        yield return Fade(0.5f, false); // 0.5s clareia

        // ===== 4s ESPAÇO =====
        yield return new WaitForSeconds(4f);

        // ===== 1s TRANSIÇÃO ESPAÇO → TITÃ =====
        yield return Fade(0.5f, true);
        MostrarTita();
        yield return Fade(0.5f, false);

        // ===== 4s TITÃ =====
        yield return new WaitForSeconds(4f);

        // Loop: volta pro início
        StartCoroutine(Sequencia());
    }

    IEnumerator Fade(float duracao, bool escurecer)
    {
        float t = 0;
        float inicio = escurecer ? 0 : 1;
        float fim = escurecer ? 1 : 0;

        while (t < duracao)
        {
            t += Time.deltaTime;
            fade = Mathf.Lerp(inicio, fim, t / duracao);
            yield return null;
        }
        
        fade = fim;
    }

    void MostrarTerra()
    {
        DesligarTudo(); // Já desliga a poeira automaticamente
        if (chaoTerra) chaoTerra.SetActive(true);
        if (ondaTerra) ondaTerra.SetActive(true);
        RenderSettings.skybox = skyTerra;
        RenderSettings.ambientIntensity = 1f;
    }

    void MostrarEspaco()
    {
        DesligarTudo(); // Limpa Terra/Titã
        RenderSettings.skybox = skyEspaco;
        RenderSettings.ambientIntensity = 0.3f;
        
        // <--- NOVO: Liga a poeira apenas aqui
        if (poeiraEspacial) poeiraEspacial.SetActive(true); 
    }

    void MostrarTita()
    {
        DesligarTudo(); // Já desliga a poeira automaticamente
        if (chaoTita) chaoTita.SetActive(true);
        if (ondaTita) ondaTita.SetActive(true);
        RenderSettings.skybox = skyTita;
        RenderSettings.ambientIntensity = 0.9f;
    }

    void DesligarTudo()
    {
        if (chaoTerra) chaoTerra.SetActive(false);
        if (ondaTerra) ondaTerra.SetActive(false);
        if (chaoTita) chaoTita.SetActive(false);
        if (ondaTita) ondaTita.SetActive(false);

        // <--- NOVO: Garante que a poeira apague ao sair do espaço
        if (poeiraEspacial) poeiraEspacial.SetActive(false); 
    }

    void OnGUI()
    {
        if (fade > 0)
        {
            GUI.color = new Color(0, 0, 0, fade);
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texPreta);
        }
    }
}