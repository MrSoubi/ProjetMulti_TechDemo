using System.Collections.Generic;
using UnityEngine;

public class CharacterBuilderTester : MonoBehaviour
{
    private Character aragorn;

    private void Start()
    {
        aragorn = new CharacterBuilder()
            .WithName("Aragorn")
            .WithClass("Warrior")
            .WithHealth(87)
            .WithAttackPower(132)
            .Build();

        aragorn.Attack();
    }
}