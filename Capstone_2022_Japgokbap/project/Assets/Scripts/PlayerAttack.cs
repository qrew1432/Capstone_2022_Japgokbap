using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{    
    [SerializeField] private GameObject attackPrefab;
    public float attackDelay;

    private GameObject player;
    private Animator playerAnimator;

    void Start(){
        player = GameObject.FindWithTag("Player");
        playerAnimator = player.GetComponent<Animator>();
    }

    public IEnumerator Attack(float delay){
        //플레이어 오브젝트의 앞쪽 방향에 생성
        GameObject spawnPrefab = Instantiate(attackPrefab, player.transform.position + player.transform.forward, player.transform.rotation);
        spawnPrefab.transform.parent = player.transform;
        //공격 애니메이션 적용
        playerAnimator.SetTrigger("doSlash");

        //공격시간(delay)후 attack 상태 해제
        //이후 생성한 프리팹 제거
        yield return new WaitForSeconds(delay);
        PlayerMovement.lockBehaviour = false;
        Destroy(spawnPrefab);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag=="Monster"){
            //추후 체력 감소로 바꿀 예정
            Debug.Log("tag");
            Destroy(other.gameObject);
        }
    }
}
