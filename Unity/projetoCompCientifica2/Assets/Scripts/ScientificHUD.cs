using UnityEngine;
using TMPro; // Necessário para mexer no texto

public class ScientificHUD : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI textoDisplay;
    
    [Header("Referência ao Foguete")]
    public GameManager gameManager; // Para saber onde estamos
    public float velocidadeFoguete = 1000f; 

    [Header("Dados da Terra (OpenFOAM)")]
    public float terraPressao = 100000; // Pa
    public float terraDensidade = 1.225f; // kg/m3
    public float terraTemp = 300f;    // K
    public float terraSom = 347f;     // m/s (c)

    [Header("Dados de Titã (OpenFOAM)")]
    public float titaPressao = 146700f;  // Pa (Titã é 1.45atm)
    public float titaDensidade = 5.3f;   // kg/m3 (Muito mais denso!)
    public float titaTemp = 94f;         // K (-179°C)
    public float titaSom = 194f;         // m/s (Som é mais lento no frio)

    void Update()
    {
        if (gameManager == null) return;

        string conteudo = "";
        float machAtual = 0f;

        // Verifica o estado atual no GameManager (precisa deixar a variável EstadoAtual pública no GameManager se não estiver)
        // Vou assumir a lógica baseada no objeto ativo se não tiver acesso à variável enum direta
        
        if (gameManager.skyTerra == RenderSettings.skybox) // Estamos na Terra
        {
            machAtual = velocidadeFoguete / terraSom;
            conteudo = MontarTexto("Terra", terraPressao, terraDensidade, terraTemp, terraSom, machAtual);
        }
        else if (gameManager.skyTita == RenderSettings.skybox) // Estamos em Titã
        {
            machAtual = velocidadeFoguete / titaSom;
            conteudo = MontarTexto("Titã", titaPressao, titaDensidade, titaTemp, titaSom, machAtual);
        }
        else // Espaço
        {
            conteudo = "<b>Vácuo</b>\n";
        }

        textoDisplay.text = conteudo;
        
        // Exemplo simples de aumentar velocidade fictícia
        //velocidadeFoguete += Time.deltaTime * 10f; 
    }

    string MontarTexto(string planeta, float P, float rho, float T, float c, float Mach)
    {
        return $"<b>{planeta}</b>\n" +
               $"\n" +
               $"Pressão: {P:0} Pa\n" +
               $"Densidade: {rho:0.00} kg/m³\n" +
               $"Temperatura: {T:0.0} K\n" +
               $"Vel. Som: {c:0.0} m/s\n" +
               $"\n" +
               $"\n" +
               $"\n" +
               $"<b>Dados da Simulação</b>\n" +
               $"Vel. Foguete: {velocidadeFoguete:0.0} m/s\n" +
               $"<b>Mach: {Mach:0.00}</b>";
    }
}