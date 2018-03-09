using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
	public int startingHealth = 100;                            // The amount of health the player starts the game with.
	public float healthRegen = 0.5f;
	public float currentHealth;                                   // The current health the player has.
	public Slider healthSlider;                                 // Reference to the UI's health bar.
	public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.

	public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
	public Color flashColour = new Color(1f, 0f, 0f, 0.5f);     // The colour the damageImage is set to, to flash.
	private Rigidbody rb;

	bool isDead;                                                // Whether the player is dead.
	bool damaged;                                               // True when thea player gets damaged.


	void Start ()
	{

		rb = GetComponent<Rigidbody>();
		// Set the initial health of the player.
		currentHealth = startingHealth;
	}


	void Update ()
	{
		
		if (currentHealth >= 0) {
			//>= 5 permite que o jogador não morra sozinho
			if (currentHealth >= 5) {
				currentHealth -= Time.deltaTime * healthRegen;
				this.gameObject.GetComponent<CharacController> ().gm.updateLife (this.currentHealth);
			}
		} else {
			Death ();
		}

		// If the player has just been damaged...
		if(damaged)
		{
			// ... set the colour of the damageImage to the flash colour.
			damageImage.color = flashColour;
		}
		// Otherwise...
		else
		{
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged = false;
	}


	public void TakeDamage (int amount, Vector3 direction)
	{
		// Set the damaged flag so the screen will flash.
		damaged = true;

		//Joga o personagem na direção oposta ao inimigo
		rb.AddForce (direction*amount*10);

		// Reduce the current health by the damage amount.
		currentHealth -= amount;

		// Set the health bar's value to the current health.
		healthSlider.value = currentHealth;


		// If the player has lost all it's health and the death flag hasn't been set yet...
		if(currentHealth <= 0 && !isDead)
		{
			// ... it should die.
			Death ();
		}
	}


	void Death ()
	{
		// Set the death flag so this function won't be called again.
		isDead = true;

		GameManager gm = GetComponent<CharacController> ().gm;

		gm.pauseGame ();
		gm.showGameOver ();

	}       
}
