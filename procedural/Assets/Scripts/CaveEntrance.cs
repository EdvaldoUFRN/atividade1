using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CaveEntrance : MonoBehaviour
{
    public string caveSceneName; // Nome da cena da caverna
    public string mainSceneName; // Nome da cena principal
    public TextMeshProUGUI textoUI;
    private Transform player; // Referência ao jogador, agora atribuída dinamicamente

    private void Start()
    {
        textoUI.enabled = false;

        // Encontra o jogador automaticamente pela tag
        player = GameObject.FindWithTag("Player").transform;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            textoUI.enabled = !textoUI.enabled; // Ativa ou desativa o texto
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            // Verifica se o jogador está na cena da caverna
            if (SceneManager.GetActiveScene().name == caveSceneName)
            {
                // Se estiver, volta para a cena principal
                ReturnToMainScene();
            }
            else
            {
                // Se não estiver, entra na caverna
                EnterCave();
            }
        }
    }

    private void EnterCave()
    {
        // Salva a posição do jogador na cena principal antes de ir para a caverna
        if (SceneManager.GetActiveScene().name == mainSceneName)
        {
            PlayerPrefs.SetFloat("PlayerPosX_Main", player.position.x);
            PlayerPrefs.SetFloat("PlayerPosY_Main", player.position.y);
            PlayerPrefs.SetFloat("PlayerPosZ_Main", player.position.z);
        }

        // Salva o nome da cena atual para voltar depois
        PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);

        Debug.Log("Entrando na caverna...");
        SceneManager.LoadScene(caveSceneName);
    }

    private void ReturnToMainScene()
    {
        // Carrega a cena principal
        Debug.Log("Voltando para a cena principal...");
        SceneManager.LoadScene(mainSceneName);

        // Após carregar a cena, restauramos a posição do jogador
        Invoke("RestorePlayerPosition", 1f); // Atraso de 1 segundo para garantir que a cena foi carregada
    }

    private void RestorePlayerPosition()
    {
        // Verifica se o jogador está na cena principal e restaura a posição
        if (SceneManager.GetActiveScene().name == mainSceneName)
        {
            // Recupera a posição salva na cena principal
            float posX = PlayerPrefs.GetFloat("PlayerPosX_Main", 0f);
            float posY = PlayerPrefs.GetFloat("PlayerPosY_Main", 0f);
            float posZ = PlayerPrefs.GetFloat("PlayerPosZ_Main", 0f);

            player.position = new Vector3(posX, posY, posZ);

            // Limpa as preferências após restaurar a posição
            PlayerPrefs.DeleteKey("PlayerPosX_Main");
            PlayerPrefs.DeleteKey("PlayerPosY_Main");
            PlayerPrefs.DeleteKey("PlayerPosZ_Main");
        }
    }
}
