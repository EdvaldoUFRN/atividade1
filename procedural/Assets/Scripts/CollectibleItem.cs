using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public string itemName;
    private bool playerInRange = false; // Flag para saber se o jogador está na área de colisão

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que entrou no trigger é o jogador
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true; // Jogador entrou na área de colisão
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Verifica se o jogador saiu da área de colisão
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false; // Jogador saiu da área de colisão
        }
    }

    private void Update()
    {
        // Verifica se o jogador está na área de colisão e pressionou a tecla 'G'
        if (playerInRange && Input.GetKeyDown(KeyCode.G))
        {
            InventorySystem inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySystem>();
            if (inventory != null)
            {
                inventory.AddItem(itemName); // Adiciona o item ao inventário
                Destroy(gameObject); // Destroi o item após a coleta
            }
        }
    }
}
