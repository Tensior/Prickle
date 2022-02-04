using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCounter : MonoBehaviour 
{
	static public Sprite zeroS, oneS, twoS, threeS, fourS, fiveS, sixS, sevenS, eightS, nineS;
	static public GameObject zeroOb, oneOb, twoOb, threeOb, fourOb, fiveOb;

	public Sprite[] spriteList = new Sprite[] { zeroS, oneS, twoS, threeS, fourS, fiveS, sixS, sevenS, eightS, nineS};
	public GameObject[] objectList = new GameObject[] { zeroOb, oneOb, twoOb, threeOb, fourOb, fiveOb};

	private int sumS;
	public string QuestName;
	public GameObject _portal;
	public AudioSource QuestFinishMusic;
	private bool OncePlayed;

	public void Start()
	{ 
		_portal.SetActive(false);
		QuestFinishMusic = GetComponent<AudioSource>();
		OncePlayed = false;
	}

	private void ResetPoints() 
	{ 
		for (var i = 0; i <= 5; i++)
			objectList[i].gameObject.SetActive(false);
	}

	private void SetNumbs(int numOne)
	{
        ResetPoints();
		var cnt = sumS / 10;
		var sumSS = sumS - numOne;
		objectList[cnt].gameObject.SetActive(true);
		GetComponent<SpriteRenderer>().sprite = spriteList[sumSS];	}

	void Update () 
	{
		sumS = GameManager.Instance.Points;
        ResetPoints();
		objectList[0].gameObject.SetActive(true);

		if (sumS > 0 && sumS < 10) 
            SetNumbs(0);
		else if (sumS >= 10 && sumS < 20)
				SetNumbs(10);
		else if (sumS >= 20 && sumS < 30)
                SetNumbs(20);
		else if (sumS >= 30 && sumS < 40)
                SetNumbs(30);
		else if (sumS >= 40 && sumS < 50)
                SetNumbs(40);
		else if (sumS >= 50 && sumS < 60)
                SetNumbs(50);
		else
			GetComponent<SpriteRenderer>().sprite = spriteList[0];

		GetPortalOn(sumS);
	}

	public void GetPortalOn(int resNum)
	{
		if (resNum == 50)
		{
			QuestController.SetQuestStatus(QuestName, QuestSetting.QuestStatus.Complete);
			_portal.SetActive(true);
			if (QuestFinishMusic != null && OncePlayed == false)
			{
				QuestFinishMusic.Play();
				OncePlayed = true;
			}
		}
		else
			_portal.SetActive(false);
	}
}
