using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardRollerController : MonoBehaviour
{
    public DiceRoller[] diceList;
    public Vector3[] diceStartPoint;
    // Start is called before the first frame update
    void Start()
    {
        diceStartPoint = new Vector3[diceList.Length];
        int index = 0;
        foreach (DiceRoller dice in diceList)
        {
            diceStartPoint[index] = dice.transform.position;
            index++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetDicePosition();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (DiceRoller dice in diceList)
            {
                dice.RollInPlace();
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            foreach (DiceRoller dice in diceList)
            {
                dice.RollDicePhysics();
            }
        }
    }

    void ResetDicePosition()
    {
        int index = 0;
        foreach (DiceRoller dice in diceList)
        {
            dice.transform.position = diceStartPoint[index];
            dice.DisableDiceGravity();
            index++;
        }
    }
}
