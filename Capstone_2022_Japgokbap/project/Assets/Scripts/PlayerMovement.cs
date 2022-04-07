using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region private
    private Camera _camera;

    //캐릭터 움직임 관련
    [Header("Move")]
    [SerializeField] 
    private float speed = 10.0f;
    private float moveDirX;
    private float moveDirZ;
    private Vector3 inputDir;
    private Animator playerAnimator;
    
    //캐릭터 회전 관련
    [Header("Rotate")]
    [SerializeField] private float rotateSpeed = 5.0f;

    //캐릭터 공격 관련(attack은 스크립트를 따로 빼려 했으나 원인을 알 수 없는 오류가 발생해 임시로 넣어둠.)
    [Header("Attack")]
    [SerializeField] private GameObject attackPrefab;
    [SerializeField] private float attackDelay = 0.5f;
    #endregion

    //캐릭터 상태 관련
    [Header("State")]
    #region public
    //공격 시 공격을 제외한 움직임을 막기 위해 사용.
    public bool isAttack = false;
    #endregion

    private void Awake() {
        _camera = GetComponentInChildren<Camera>();
    }

    private void Start() {
        playerAnimator = GetComponent<Animator>();
    }

    private void Update() {
        Move();
        AttackToMouse();

    }

    private void Move(){
        if(!isAttack){
        moveDirX = Input.GetAxisRaw("Horizontal");
        moveDirZ = Input.GetAxisRaw("Vertical");

        inputDir = new Vector3(moveDirX,0,moveDirZ).normalized;

        transform.position += inputDir * speed * Time.deltaTime;
        playerAnimator.SetFloat("speed", 1, -0.1f, Time.deltaTime);
        PlayerRotate();
        }
    }

    private void PlayerRotate(){
        if(moveDirX == 0 && moveDirZ == 0)
            return;
        Quaternion newRotation = Quaternion.LookRotation(inputDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotateSpeed * Time.deltaTime);   //자연스러운 회전을 위해 보간함.
    }

    private void AttackToMouse(){     
        if(!isAttack){                             //마우스로 공격하는 함수
            if(Input.GetMouseButtonDown(0)){                            //마우스 좌클릭 시
                isAttack = true;
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);    //화면상의 마우스 좌표를 얻어옴
                
                RaycastHit rayHit;
                if(Physics.Raycast(ray, out rayHit)){
                    Vector3 mouseDir = new Vector3(rayHit.point.x, transform.position.y, rayHit.point.z) - transform.position;  //위에서 얻은 마우스의 좌표와 현재 위치를 빼서 마우스 방향을 얻음.
                    transform.rotation = Quaternion.LookRotation(mouseDir);     //플레이어의 rotation을 마우스 방향으로 돌림
                }
                StartCoroutine(Attack(attackDelay));
            }
        }
    }

    private IEnumerator Attack(float delay){
        //공격 프리팹을 생성하는 함수(attack은 스크립트를 따로 빼려 했으나 코루틴을 사용하는 과정에 원인을 알 수 없는 오류가 발생해 임시로 넣어둠.)
        //플레이어 오브젝트의 앞쪽 방향에 생성
        GameObject spawnPrefab = Instantiate(attackPrefab, transform.position + transform.forward, transform.rotation);
        
        //공격 애니메이션 적용

        //공격시간(delay)후 attack 상태 해제
        //이후 생성한 프리팹 제거
        yield return new WaitForSeconds(delay);
        isAttack = false;
        Destroy(spawnPrefab);
    }
}