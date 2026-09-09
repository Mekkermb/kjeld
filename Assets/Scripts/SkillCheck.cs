using UnityEngine;
using UnityEngine.InputSystem;

public class SkillCheck : MonoBehaviour
{
    [Header("Input Actions")]
    public InputActionReference skillCheckAction_1;
    public InputActionReference skillCheckAction_2;
    public InputActionReference skillCheckAction_3;
    public InputActionReference skillCheckAction_4;

    [Header("Skill Check Display Chars")]
    public string skillCheckChar_1 = "W";
    public string skillCheckChar_2 = "A";
    public string skillCheckChar_3 = "S";
    public string skillCheckChar_4 = "D";

    [Header("Skill Check Settings")]
    public GameObject skillCheckGameObject;
    public float skillCheckTime = 1.0f;
    public float skillCheckSpawnDistance = 2.5f;
    public float circleStartScale = 1.5f;
    public float circleTargetScale = 1.0f;

    [Header("Difficulty")]
    public float spawnInterval = 10.0f;
    public float spawnIntervalVariance = 2.0f;

    [Header("Precision Settings")]
    public float perfectThreshold = 0.05f;
    public float perfectMultiplier = 5.0f;
    public float greatThreshold = 0.1f;
    public float greatMultiplier = 3.0f;
    public float goodThreshold = 0.2f;
    public float goodMultiplier = 2.0f;

    [Header("State")]
    public bool isActive = false;
    public string ActiveSkillCheckChar = "";
    public float skillCheckTimer = 0.0f;
    public float timeToNextSpawn = 0.0f;

    [Header("References")]
    public PlayerController playerController;
    public MouseHandler mouseHandler;
    public TMPro.TMP_Text skillCheckText;

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        mouseHandler = GameObject.FindGameObjectWithTag("GameController").GetComponent<MouseHandler>();


        EnableAction(skillCheckAction_1, skillCheckChar_1);
        EnableAction(skillCheckAction_2, skillCheckChar_2);
        EnableAction(skillCheckAction_3, skillCheckChar_3);
        EnableAction(skillCheckAction_4, skillCheckChar_4);
        CalcNewTime();
    }

    void EnableAction(InputActionReference reference, string character)
    {
        if (reference == null || reference.action == null)
        {
            Debug.LogWarning("Skill check input action reference is not assigned.");
            return;
        }
        reference.action.Enable();
        reference.action.performed += ctx => HitSkillCheck(character);
    }

    public void CalcNewTime()
    {
        timeToNextSpawn = spawnInterval + Random.Range(-spawnIntervalVariance, spawnIntervalVariance);
    }

    private void Update()
    {
        if (timeToNextSpawn <= 0)
        {
            CalcNewTime();
            string charToSpawn = "";
            int randomIndex = Random.Range(0, 4);
            switch (randomIndex) // ugly shouldve been an array but i dont caare anylonger
            {
                case 0:
                    charToSpawn = skillCheckChar_1;
                    break;
                case 1:
                    charToSpawn = skillCheckChar_2;
                    break;
                case 2:
                    charToSpawn = skillCheckChar_3;
                    break;
                case 3:
                    charToSpawn = skillCheckChar_4;
                    break;
            }
            StartSkillCheck(charToSpawn);
        }
        if (isActive) UpdateSkillCheck();
        else timeToNextSpawn -= Time.deltaTime;

        if (isActive && skillCheckTimer >= skillCheckTime)
        {
            MissSkillCheck("Too slow!");
        }
    }

    public void StartSkillCheck(string skillCheckChar)
    {
        ActiveSkillCheckChar = skillCheckChar;
        Debug.Log(skillCheckChar);
        skillCheckText.text = skillCheckChar;
        skillCheckTimer = 0.0f;
        skillCheckGameObject.SetActive(true);
        isActive = true;
        UpdateSkillCheck();
        skillCheckGameObject.transform.position = Random.insideUnitCircle.normalized * skillCheckSpawnDistance;
    }

    public void UpdateSkillCheck()
    {
        skillCheckTimer += Time.deltaTime;

        Transform approachCircle = GetApproachCircle();
        if (approachCircle == null)
        {
            Debug.LogWarning("No child found for skill check game object.");
            return;
        }

        float progress = Mathf.Clamp01(skillCheckTimer / skillCheckTime);
        float scale = Mathf.Lerp(circleStartScale, circleTargetScale, progress);
        approachCircle.localScale = new Vector3(scale, scale, scale);
    }

    Transform GetApproachCircle()
    {
        if (transform.childCount > 0)
        {
            return transform.GetChild(0);
        }
        if (skillCheckGameObject != null && skillCheckGameObject.transform.childCount > 0)
        {
            return skillCheckGameObject.transform.GetChild(0);
        }
        return null;
    }

    
    float GetTimingError()
    {
        Transform approachCircle = GetApproachCircle();
        if (approachCircle == null) return 1.0f;

        float startToTarget = circleStartScale - circleTargetScale;
        if (startToTarget <= 0.0001f) return 0.0f;

        float scale = approachCircle.localScale.x;
        return Mathf.Clamp01(Mathf.Abs(scale - circleTargetScale) / startToTarget);
    }

    public void EndSkillCheck()
    {
        isActive = false;
        skillCheckTimer = 0.0f;
        skillCheckGameObject.SetActive(false);
    }

    void MissSkillCheck(string reason)
    {
        Debug.Log("Skill check missed: " + reason);
        if (playerController != null) playerController.ResetCombo();
        EndSkillCheck();
    }

    public void HitSkillCheck(string hit)
    {
        if (!isActive)
        {
            if (playerController != null) playerController.ResetCombo();
            return;
        }
        if (ActiveSkillCheckChar != hit)
        {
            MissSkillCheck("Wrong key, expected " + ActiveSkillCheckChar);
            return;
        }

        float error = GetTimingError();
        int mult;
        if (error <= perfectThreshold)
        {
            mult = Mathf.RoundToInt(perfectMultiplier);
            Debug.Log("Skill check PERFECT (error " + error.ToString("F2") + ")");
        }
        else if (error <= greatThreshold)
        {
            mult = Mathf.RoundToInt(greatMultiplier);
            Debug.Log("Skill check GREAT (error " + error.ToString("F2") + ")");
        }
        else if (error <= goodThreshold)
        {
            mult = Mathf.RoundToInt(goodMultiplier);
            Debug.Log("Skill check GOOD (error " + error.ToString("F2") + ")");
        }
        else
        {
            MissSkillCheck("Bad timing (error " + error.ToString("F2") + ")");
            return;
        }

        if (playerController != null) playerController.SkillCheckHit(mult);
        EndSkillCheck();
    }
}
