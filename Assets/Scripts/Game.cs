using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Game : MonoBehaviour {

    public Text scoreText;
    private int score;
    public GameObject Exit;
    private int bonus = 1;
    private int workersCount, workerBonus = 1;
    [Header("Магазин")]
    public int[] shopCosts;
    public int[] bonusScale;
    public Text[] shopText;
    public GameObject shopPan;
    public Button[] shopBttns;
    public float[] bonusTime;

    private void Start()
    {
        StartCoroutine(BonusPerSec());
    }

    private void Update()
    {
        scoreText.text = "Дерево: " + score;
    }

    public void ShopPanel()
    {
        if (!shopPan.activeSelf)
        {
            shopPan.SetActive(!shopPan.activeSelf);
            Exit.SetActive(shopPan.activeSelf);
        }
        else
        {
            shopPan.SetActive(!shopPan.activeSelf);
            Exit.SetActive(shopPan.activeSelf);
        }
    }

    public void BonusClick(int index)
    {
        if(score >= shopCosts[index])
        {
            bonus += bonusScale[index];
            bonusScale[index] += 1;
            score -= shopCosts[index];
            shopCosts[index] *= 2;
            shopText[index].text = "Купить\n" + shopCosts[index];
        }
        else
        {
            Debug.Log("Нет манет!");
        }
    }

    public void HireWorker(int index)
    {
        if(score >= shopCosts[index])
        {
            workersCount++;
            score -= shopCosts[index];
        }
    }

    public void startBonusTimer(int index)
    {
        
        int cost = 2 * workersCount;
        shopText[1].text = "Купить Баф\n" + cost;
        if (score >= cost)
        {
            StartCoroutine(bonusTimer(bonusTime[index], index));
        }
        score -= cost;
    }

    IEnumerator BonusPerSec()
    {
        while (true)
        {
            score += (workersCount * workerBonus);                   //сколько дают предприятия
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator bonusTimer(float time, int index)
    {
        shopBttns[index].interactable = false;
        if(index == 0 && workersCount > 0) 
        {
            workerBonus *= 2;
            yield return new WaitForSeconds(time);
            workerBonus /= 2;
        }

        shopBttns[index].interactable = true;
    }

    public void OnClick()
    {
        score += bonus; ;
    }
}