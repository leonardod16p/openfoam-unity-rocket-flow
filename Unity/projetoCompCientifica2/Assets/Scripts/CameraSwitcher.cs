using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [Header("Arraste suas câmeras aqui")]
    public GameObject[] cameras; // Lista de câmeras

    private int indiceAtual = 0; // Qual câmera está ativa agora

    void Start()
    {
        // Garante que só a primeira câmera esteja ligada ao começar
        if (cameras.Length > 0)
        {
            AtivarCamera(0);
        }
    }

    void Update()
    {
        // Ao apertar 'C', troca a câmera
        if (Input.GetKeyDown(KeyCode.C))
        {
            ProximaCamera();
        }
    }

    void ProximaCamera()
    {
        // Desliga a atual
        cameras[indiceAtual].SetActive(false);

        // Aumenta o índice (se chegar no fim, volta pro 0)
        indiceAtual++;
        if (indiceAtual >= cameras.Length)
        {
            indiceAtual = 0;
        }

        // Liga a nova
        cameras[indiceAtual].SetActive(true);
    }

    void AtivarCamera(int index)
    {
        // Desativa todas para garantir
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].SetActive(i == index);
        }
        indiceAtual = index;
    }
}