using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Upgrade : MonoBehaviour
{
    public enum UpgradeType
    {
        Health,
        Damage,
        FireRate
    }

    public UpgradeType type;

    public int cost;

    public TMP_Text costDisplay;

    public GameObject maxedText;

    private void Update()
    {
        costDisplay.text = cost.ToString("D3");
    }

    public void Buy()
    {
        if(GameManager.instance.grayMatter >= cost && !maxedText.activeInHierarchy && ShopUI.instance.exiting == false)
        {
            GameManager.instance.grayMatter -= cost;

            GameManager.instance.buySound.Play();

            if(type == UpgradeType.Health)
            {
                if (GameManager.instance.emotions[0].maxhp < 200)
                {
                    foreach (var item in GameManager.instance.emotions)
                    {
                        item.maxhp += 10;
                        item.hp = item.maxhp;
                    }
                }

                if (GameManager.instance.emotions[0].maxhp >= 200)
                {
                    maxedText.SetActive(true);
                }
            }
            else if(type == UpgradeType.Damage)
            {
                if(GameManager.instance.tearDamage < 3)
                {
                    GameManager.instance.tearDamage += 0.15f;
                }

                if(GameManager.instance.tearDamage >= 3)
                {
                    maxedText.SetActive(true);
                }
            }
            else if(type == UpgradeType.FireRate)
            {
                if(GameManager.instance.tearFireRate > 0.4f)
                {
                    GameManager.instance.tearFireRate -= 0.15f;
                }

                if(GameManager.instance.tearFireRate <= 0.4f)
                {
                    maxedText.SetActive(true);
                }
            }

            cost += Random.Range(3,6);
        }
        else
        {
            GameManager.instance.declineSound.Play();
        }
    }
}