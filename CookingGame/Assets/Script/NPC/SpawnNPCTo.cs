using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnNPCTo : MonoBehaviour
{
    [SerializeField] private PatientDataScript npcData;
    private int idNPC;
    [SerializeField] private CreateSicknessScript[] sicknessData;
    [SerializeField] private GameObject obj;//โรคเปล่า
    [SerializeField] private GameObject bedObj;
    [SerializeField] private GameObject binObj;

    [SerializeField] private RawImage tutorialImage;
    private RectTransform rectTransformImage;
    [SerializeField] private Sprite[] tutorial;
    [SerializeField] private float speedHind;
    [SerializeField] private float timeToShowTutorial;
    private float timeToShowTutorialMax;
    private bool hindTu;

    private void SpawnOneNPC()
    {
        GameObject[] _ObjArray = { obj, obj, obj };
        int[] _IntArray = { 1, 1, 1 };//ระดับลดเลือด
        float[] _FolatArray = { 20, 20, 20 };//เวลาลดเลือด

        PatientDataScript npcSpawn = Instantiate(npcData, this.gameObject.transform, false);
        npcSpawn.id = idNPC;
        idNPC++;
        npcSpawn.sicknessID = 2;
        npcSpawn.allModelSickness = _ObjArray;

        npcSpawn.sicknessLevel = 1;

        npcSpawn.declineH = _IntArray;
        npcSpawn.tiemDeclineH = _FolatArray;
        npcSpawn.transform.parent = null;
    }
    private void SpawnTwoNPC()
    {
        int[] _IntArray = { 1, 1, 1 };//ระดับลดเลือด
        float[] _FolatArray = { 20, 20, 20 };//เวลาลดเลือด
        for (int i = 0; i < 2; i++)
        {
            PatientDataScript npcSpawn = Instantiate(npcData, this.gameObject.transform, false);
            npcSpawn.id = idNPC;
            idNPC++;
            npcSpawn.sicknessID = sicknessData[i].id;
            npcSpawn.allModelSickness = sicknessData[i].modleSickness;

            npcSpawn.sicknessLevel = 1;
            npcSpawn.declineH = _IntArray;
            npcSpawn.tiemDeclineH = _FolatArray;
            npcSpawn.transform.parent = null;
        }
    }//โรคใหม่
    private void SpawnDeadNPC()
    {
        GameObject[] _ObjArray = { obj, obj, obj };
        int[] _IntArray = { 0, 0, 0 };//ระดับความรุนแรงของโรค
        float[] _FolatArray = { 20, 20, 20 };//เวลาลดเลือด

        PatientDataScript npcSpawn = Instantiate(npcData, this.gameObject.transform, false);
        npcSpawn.id = idNPC;
        idNPC++;
        npcSpawn.sicknessID = 2;
        npcSpawn.allModelSickness = _ObjArray;

        npcSpawn.sicknessLevel = 1;

        npcSpawn.declineH = _IntArray;
        npcSpawn.tiemDeclineH = _FolatArray;
        npcSpawn.heat = 0;
        npcSpawn.transform.parent = null;
    }
    private void SpawnNPCWillDead()
    {
        GameObject[] _ObjArray = { obj, obj, obj };
        int[] _IntArray = { 0, 0, 0 };//ระดับความรุนแรงของโรค
        float[] _FolatArray = { 20, 20, 20 };//เวลาลดเลือด

        PatientDataScript npcSpawn = Instantiate(npcData, this.gameObject.transform, false);
        npcSpawn.id = idNPC;
        idNPC++;
        npcSpawn.sicknessID = 2;
        npcSpawn.allModelSickness = _ObjArray;

        npcSpawn.sicknessLevel = 1;

        npcSpawn.declineH = _IntArray;
        npcSpawn.tiemDeclineH = _FolatArray;
        npcSpawn.heat = 50;
        npcSpawn.transform.parent = null;
    }
    private void Tutorial()
    {
        if (!hindTu)
        {
            timeToShowTutorial = timeToShowTutorial - Time.deltaTime;
        }
        if (timeToShowTutorial <= 0)
        {
            hindTu = true;
            timeToShowTutorial = timeToShowTutorialMax;
        }
        if (rectTransformImage.position.x <= 650 + 1920 && hindTu)
        {
            rectTransformImage.Translate(Vector3.right * Time.deltaTime * speedHind, Space.Self);
            if (rectTransformImage.position.x >= 650 - 5 + 1920)
            {
                rectTransformImage.gameObject.SetActive(false);
            }
        }
        else if (rectTransformImage.position.x >= 1920 && !hindTu)
        {
            rectTransformImage.gameObject.SetActive(true);
            rectTransformImage.Translate(Vector3.left * Time.deltaTime * speedHind, Space.Self);
        }
    }
    private void Start()
    {
        tutorialImage.texture = tutorial[0].texture;
        timeToShowTutorialMax = timeToShowTutorial;
        rectTransformImage = tutorialImage.GetComponent<RectTransform>();
        hindTu = false;
        idNPC = 0;
        bedObj.SetActive(false);
        binObj.SetActive(false);
        SpawnOneNPC();
    }
    private void Update()
    {
        Tutorial();
        if (TextScript.textStart >= 1 && TextScript.textStart <= 5)
        {
            hindTu = false;
            timeToShowTutorial = timeToShowTutorialMax;
            tutorialImage.texture = tutorial[1].texture;
            bedObj.SetActive(true);
        }
        else if (TextScript.textStart == 6)
        {
            SpawnTwoNPC();
            hindTu = false;
            timeToShowTutorial = timeToShowTutorialMax;
            tutorialImage.texture = tutorial[2].texture;
            TextScript.textStart++;
        }
        else if (TextScript.textStart == 11)
        {
            bedObj.SetActive(false);
            binObj.SetActive(true);
            SpawnDeadNPC();
            TextScript.textStart++;
        }
        else if (TextScript.textStart == 13)
        {
            SpawnNPCWillDead();
            TextScript.textStart++;
        }
    }
}
