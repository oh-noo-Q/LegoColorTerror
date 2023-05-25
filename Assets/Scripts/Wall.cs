using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] Transform root;
    [SerializeField] Transform player;
    [SerializeField] Transform rootBall;
    [SerializeField] Transform stackBall;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CollectedBall"))
        {
            other.transform.parent = null;
            root.position = new Vector3(root.position.x, root.position.y - 1, root.position.z);
            player.position = new Vector3(player.position.x, player.position.y + 1, player.position.z);
            rootBall.position = new Vector3(rootBall.position.x, rootBall.position.y + 1, rootBall.position.z);
            for(int i = 0; i < stackBall.childCount; i++)
            {
                Transform ball = stackBall.GetChild(i);
                ball.position = new Vector3(ball.position.x, ball.position.y + 1, ball.position.z);
            }
        }
        if(other.CompareTag("RootBall"))
        {
            //GameManager.Instance.OnGameLose();
        }
    }
}
