using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] Transform mainPlayer;
    //[SerializeField] Transform left, right;

    //[SerializeField] float playerSpeed, sideMovement, sideLerpSpeed;

    //private Vector2 inputDrag;

    //private Vector2 previousMousePosition;

    //private float leftLimitX => left.localPosition.x;

    //private float rightLimitX => right.localPosition.x;

    //private float sideMovementTarget;

    //private Vector2 mousePositionCM
    //{
    //    get
    //    {
    //        Vector2 pixels = Input.mousePosition;
    //        var inches = pixels / Screen.dpi;
    //        var centimeters = inches * 2.54f;

    //        return centimeters;
    //    }
    //}

    //void Update()
    //{
    //    HandleForward();
    //    HandleInput();
    //    HandleSide();
    //}

    //private void HandleForward()
    //{
    //    if (GameManager.Instance.GameStart())
    //    {
    //        transform.position += transform.forward * Time.deltaTime * playerSpeed;
    //    }
    //}

    //private void HandleInput()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        previousMousePosition = mousePositionCM;

    //        GameManager.Instance.OnGameStart();
    //    }

    //    if (Input.GetMouseButton(0))
    //    {
    //        var deltaMouse = mousePositionCM - previousMousePosition;
    //        inputDrag = deltaMouse;
    //        previousMousePosition = mousePositionCM;
    //    }
    //    else
    //    {
    //        inputDrag = Vector2.zero;
    //    }
    //}

    //private void HandleSide()
    //{
    //    sideMovementTarget += inputDrag.x * sideMovement;
    //    sideMovementTarget = Mathf.Clamp(sideMovementTarget, leftLimitX, rightLimitX);

    //    var localPosition = mainPlayer.localPosition;

    //    localPosition.x = Mathf.Lerp(localPosition.x, sideMovementTarget, Time.deltaTime * sideLerpSpeed);

    //    mainPlayer.localPosition = localPosition;
    //}
    public EnemyManager enemyManager;
    public float speedRotaion;
    public int health;

    private void Start()
    {
        health = 5;
        EventDispatcher.Instance.RegisterListener(EventID.OnChangeValueHealth, UpdateHealth);
    }

    private void Update()
    {
        if(enemyManager.GetCurrentEnemy() != null)
            RotatePlayer(enemyManager.GetCurrentEnemy().transform);
    }

    public void UpdateHealth(object obj)
    {
        health += (int)obj;
        if (health > 7)
            health = 7;
        EventDispatcher.Instance.PostEvent(EventID.OnUpdateHealth, $"{health}");
        if (health == 0)
        {
            UIManager.Instance.ShowEndGame();
        }
    }

    public void SetHealthStartGame()
    {
        health = 5;
    }

    public void RotatePlayer(Transform target)
    {
        Vector3 direction = target.position - transform.position;
        direction.y = 0;
        Quaternion toRatation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRatation, speedRotaion * Time.deltaTime);
    }
}
