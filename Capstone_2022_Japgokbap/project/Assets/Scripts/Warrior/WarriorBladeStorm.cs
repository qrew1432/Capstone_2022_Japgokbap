using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorBladeStorm : WarriorSkill
{
    public IEnumerator DoBladeStorm(){
        if(readySkill){
            readySkill = false;
            playerAnimator.SetTrigger("doBladeStorm");
            yield return new WaitForSeconds(animDelay);

            GameObject instantePrefab= Instantiate(skillPrefab, transform.position, Quaternion.identity);
            instantePrefab.transform.parent = playerTransform;
            yield return new WaitForSeconds(duration);
            
            Destroy(instantePrefab);
        }
    }
}
