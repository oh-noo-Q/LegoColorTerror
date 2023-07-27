using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixColorEnemy : LegoEnemy
{
    List<LegoColor> colorMix;
    int mixComplete;

    private void Awake()
    {
        colorMix = new List<LegoColor>();
    }

    public override void SetColor(LegoColor _color)
    {
        for (int i = 0; i < pieces.Count; i++)
        {
            int ran = Random.Range(0, 5);
            colorMix.Add((LegoColor)ran);
            pieces[i].GetComponent<Renderer>().material = GameManager.Instance.colorDic[(LegoColor)ran];
        }

        //while (mixComplete < pieces.Count)
        //{
        //    int ran = Random.Range(0, 5);
        //    if(mixComplete > 0)
        //    {
        //        if((LegoColor)ran != colorMix[mixComplete - 1])
        //        {
        //            mixComplete++;
        //            colorMix.Add((LegoColor)ran);
        //            pieces[mixComplete].GetComponent<Renderer>().material = GameManager.Instance.colorDic[(LegoColor)ran];
        //        }
        //    }
        //    else
        //    {
        //        mixComplete++;
        //        colorMix.Add((LegoColor)ran);
        //        pieces[mixComplete].GetComponent<Renderer>().material = GameManager.Instance.colorDic[(LegoColor)ran];
        //    }
            
        //}
    }

    public override void AttackEnemy(LegoColor attackColor, Bullet bullet)
    {
        if(attackColor == colorMix[injureHit])
        {
            if (blockDamage > 0)
            {
                blockDamage--;
                numberBlock.ActiveNumber(blockDamage, mainColor);
                return;
            }
            injureHit++;
            pieces[bullet.targetPiece].SetActive(false);
            SoundManager.instance.PlaySingle(SoundType.balloonExplosion);
            GameManager.Instance.effectController
                    .GenExplosion(pieces[bullet.targetPiece].transform,
                    GameManager.Instance.colorDic[mainColor]);
            if (bullet.targetPiece == pieces.Count - 1)
                GameManager.Instance.enemyManager.KillEnemy(this);
        }
        else
        {
            blockDamage++;
            if (blockDamage > 2) AttackPlayer();
            numberBlock.ActiveNumber(blockDamage, mainColor);
        }
    }
}
