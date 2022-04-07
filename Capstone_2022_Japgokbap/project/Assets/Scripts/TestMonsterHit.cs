using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMonsterHit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "PlayerAttack"){
            Debug.Log("attacked");
        }
    }
}
