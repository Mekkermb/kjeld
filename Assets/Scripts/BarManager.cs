using UnityEngine;
using UnityEngine.UI;

public class BarManager : MonoBehaviour
{
    public Image UltBar;
    public Image HpBar;

    private float BaseHpScale;
    private float BaseUltScale;
    void Start()
    {
        BaseHpScale = HpBar.rectTransform.localScale.x;
        BaseUltScale = UltBar.rectTransform.localScale.x;
    }

    public void UpdateHpBar(float currentHp, float maxHp)
    {
        float hpPercentage = currentHp / maxHp;
        HpBar.rectTransform.localScale = new Vector3(BaseHpScale * hpPercentage, HpBar.rectTransform.localScale.y, HpBar.rectTransform.localScale.z);
    }
    public void UpdateUltBar(float currentUlt, float maxUlt)
    {
        float ultPercentage = currentUlt / maxUlt;
        ultPercentage = Mathf.Clamp01(ultPercentage); 
        UltBar.rectTransform.localScale = new Vector3(BaseUltScale * ultPercentage, UltBar.rectTransform.localScale.y, UltBar.rectTransform.localScale.z);
    }
}
