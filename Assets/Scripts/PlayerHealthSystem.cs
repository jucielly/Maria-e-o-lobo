using Cainos.CustomizablePixelCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class PlayerHealthSystem : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;
    private int currentHealth;
    private GameObject player;
    public Text healthText;
    public Animator playerAnimator;
    public AudioSource damage;
    public GameObject gameOverUi;
    public GameObject parede;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = currentHealth + "/" + maxHealth;
    }


   public void Damage(int amount)
    {
        currentHealth -= amount;
        playerAnimator.SetTrigger("InjuredFront");
        damage.Play();


        if (currentHealth <= 0)
        {

            StartCoroutine(Death());
       
        }
    }

    IEnumerator Death()
    {
        float playerx = player.GetComponent<Transform>().position.x;
        float playerY = player.GetComponent<Transform>().position.y;

        Instantiate(parede, new Vector2(playerx, playerY), Quaternion.identity);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PixelCharacterController>().IsDead = true;
        print("morreu");
        yield return new WaitForSeconds(3);
        gameOverUi.SetActive(true);
    }

}
