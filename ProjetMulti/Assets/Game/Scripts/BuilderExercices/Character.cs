using UnityEngine;

public class Character
{
	public string characterName;
	public string characterClass;
	public int maxHealth;
	public int attackPower;

	private int currentHealth;

	public void Attack()
	{
		Debug.Log("Attacks with " + attackPower + " hit points.");
	}
}