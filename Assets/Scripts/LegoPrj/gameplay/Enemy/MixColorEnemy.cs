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
            GameManager.Instance.EffectLegoExplosion(pieces[bullet.targetPiece].transform, mainColor);
            if (bullet.targetPiece == pieces.Count - 1)
                GameManager.Instance.enemyManager.KillEnemy(this);
        }
        else
        {
            blockDamage++;
            SoundManager.instance.PlaySingle(SoundType.LayerSound, SoundName.BulletWrongLayer);
            EventDispatcher.Instance.PostEvent(EventID.UpdateMeteorStack, -1);
            numberBlock.ActiveNumber(blockDamage, mainColor);
            if (blockDamage > 2) AttackPlayer();
        }
    }
}
