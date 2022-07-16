using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Unity;

public class DiceRoller : MonoBehaviour
{
    public GameObject spawnPoint;
    public Rigidbody rb;
    public float rollForce;
    public bool isRolling;
    public int topFaceValue;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.MoveRotation(Quaternion.Euler(360 * UnityEngine.Random.value, 0, 360 * UnityEngine.Random.value));
        isRolling = false;
    }

    private void Update()
    {
        if ((rb.velocity.magnitude < 0.01f && rb.angularVelocity.magnitude < 0.01f) && isRolling)
        {
            UpdateTopFaceValue();
            isRolling = false;
            Debug.Log("Dice stoped, value = "+ topFaceValue, this.gameObject);
        }
    }
    [ContextMenu("UpdateFaceValueTest")]
    private void UpdateTopFaceValue()
    {
        Vector3 topFaceVector = Vector3.up;
        Vector3 f2Vector = transform.up.normalized;
        Vector3 f1Vector = transform.forward.normalized;
        Vector3 f5Vector = transform.up.normalized*-1;
        Vector3 f6Vector = transform.forward.normalized*-1f;
        Vector3 f4Vector = transform.right.normalized * 1f;
        Vector3 f3Vector = transform.right.normalized * -1f;
        /*
        Debug.DrawLine(transform.position, transform.position + f2Vector, Color.green, 1000, false);
        Debug.DrawLine(transform.position, transform.position + f5Vector, Color.red, 1000, false);
        Debug.DrawLine(transform.position, transform.position + f1Vector, Color.blue, 1000, false);
        Debug.DrawLine(transform.position, transform.position + f6Vector, Color.cyan, 1000, false);
        Debug.DrawLine(transform.position, transform.position + f4Vector, Color.yellow, 1000, false);
        Debug.DrawLine(transform.position, transform.position + f3Vector, Color.magenta, 1000, false);
        Debug.DrawLine(transform.position, transform.position + f2Vector, Color.green, 1000, false);
        Debug.DrawLine(transform.position, transform.position + topFaceVector, Color.gray, 1000, false);
        */
        float[] faceAnglesList = new float[6];
        faceAnglesList[0] = Vector3.Angle(f1Vector, topFaceVector);
        faceAnglesList[1] = Vector3.Angle(f2Vector, topFaceVector);
        faceAnglesList[2] = Vector3.Angle(f3Vector, topFaceVector);
        faceAnglesList[3] = Vector3.Angle(f4Vector, topFaceVector);
        faceAnglesList[4] = Vector3.Angle(f5Vector, topFaceVector);
        faceAnglesList[5] = Vector3.Angle(f6Vector, topFaceVector);
        topFaceValue = 1 + Array.IndexOf(faceAnglesList, faceAnglesList.Min());
    }
    public void RollInPlace()
    {
        Debug.Log("Rolling In place");
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezePosition;
        float random1 = UnityEngine.Random.value;
        float random2 = UnityEngine.Random.value;
        float inversor = random2 > 0.5f ? 1 : -1;
        Vector3 forceVector = (Vector3.forward + Vector3.up * 0.2f + (Vector3.right * random1) * inversor) * rollForce;
        rb.AddTorque(forceVector+Vector3.forward *5, ForceMode.Impulse);
    }

    public void RollDicePhysics()
    {
        Debug.Log("Rolling to board");
        isRolling = true;
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.None;
        Vector3 diceCenter = transform.position;
        float random1 = UnityEngine.Random.value;
        float random2 = UnityEngine.Random.value;
        float inversor = random2 > 0.5f ? 1 : -1;
        float xOffset = random1 * inversor;
        float yOffset = -0.6f;
        float zOffset = -0.6f;
        Vector3 forceVector = (Vector3.forward + Vector3.up * 0.2f + (Vector3.right * random1)*inversor) * rollForce;
        rb.AddForceAtPosition(forceVector, new Vector3(diceCenter.x + xOffset, diceCenter.y + yOffset, diceCenter.z + zOffset),ForceMode.Impulse);
        rb.useGravity = true;
    }

    public void DisableDiceGravity()
    {
        rb.useGravity = false;
    }
}
