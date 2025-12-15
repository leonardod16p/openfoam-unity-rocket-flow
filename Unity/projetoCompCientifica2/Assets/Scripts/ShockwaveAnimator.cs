using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class ShockwaveAnimator : MonoBehaviour
{
    [Header("Arraste os arquivos .obj/.fbx aqui")]
    public GameObject[] arquivosDosFrames; // MUDANÇA: Aceita GameObjects (arquivos)

    public float fps = 30f;
    public int ultimosFramesLoop = 10;

    // A lista real de malhas agora é privada e montada automaticamente
    private Mesh[] frames; 

    private MeshFilter mf;
    private int indice = 0;
    private float tempo = 0f;
    private int frameInicioLoop;

    void Awake()
    {
        mf = GetComponent<MeshFilter>();

        // LÓGICA NOVA: Converte os arquivos arrastados em Meshes
        if (arquivosDosFrames != null)
        {
            frames = new Mesh[arquivosDosFrames.Length];
            for (int i = 0; i < arquivosDosFrames.Length; i++)
            {
                if (arquivosDosFrames[i] != null)
                {
                    // MUDANÇA AQUI: Adicionamos "InChildren"
                    // Isso faz ele procurar a malha dentro do arquivo se não achar fora
                    MeshFilter filtro = arquivosDosFrames[i].GetComponentInChildren<MeshFilter>();
                    
                    if (filtro != null)
                    {
                        frames[i] = filtro.sharedMesh;
                    }
                }
            }
        }
    }

    void OnEnable()
    {
        if (frames == null) return; // Segurança caso não tenha arrastado nada

        indice = 0;
        tempo = 0f;
        frameInicioLoop = Mathf.Max(0, frames.Length - ultimosFramesLoop);
        
        if (frames.Length > 0)
        {
            mf.mesh = frames[0];
        }
    }

    void Update()
    {
        if (frames == null || frames.Length == 0) return;

        tempo += Time.deltaTime;
        
        if (tempo >= 1f / fps)
        {
            tempo = 0f;
            indice++;

            if (indice >= frames.Length)
            {
                indice = frameInicioLoop;
            }

            mf.mesh = frames[indice];
        }
    }
}