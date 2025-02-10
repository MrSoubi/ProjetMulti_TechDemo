using UnityEditor.Experimental.GraphView;

public class CharacterBuilder
{
    private string characterName = "John";
    private string characterClass = "Villager";
    private int maxHealth = 1;
    private int attackPower = 1;

	public Character Build()
	{
		Character warrior = new();

		warrior.characterName = characterName;
		warrior.characterClass = characterClass;
		warrior.maxHealth = maxHealth;
		warrior.attackPower = attackPower;

		return warrior;
	}

	public CharacterBuilder WithName(string name)
    {
        characterName = name;
        return this;
    }

    public CharacterBuilder WithClass(string className)
    {
        characterClass = className;
        return this;
    }

	public CharacterBuilder WithHealth(int health)
	{
		maxHealth = health;
		return this;
	}

	public CharacterBuilder WithAttackPower(int attackPower)
	{
		this.attackPower = attackPower;
		return this;
	}
}
