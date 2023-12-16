using System.Collections;
using TMPro;
using UnityEngine;

public class Deduction : MonoBehaviour
{
    [SerializeField] TMP_Text _deductionText;
    [SerializeField] float _writeSpeed = 0.2f;
    private TestimonyItem _testimonyItem;
    private TestimonyInfo _testimonyInfo;

    public void Init(TestimonyItem item, TestimonyInfo info)
    {
        _testimonyItem = item;
        _testimonyInfo = info;
        SetTestimony();
    }

    public void SetTestimony()
    {
        StartCoroutine(WriteLine(_testimonyInfo.Description));
    }

    public void UpdateTestimony()
    {
        TestimonyManager.Instance.ShowContradictionScreen(_testimonyItem, false);
    }

    public TestimonyType GetTestimonyType()
    {
        return _testimonyInfo.Type;
    }

    IEnumerator WriteLine(string line)
    {
        _deductionText.text = "";
        foreach (char c in line.ToCharArray())
        {
            _deductionText.text += c;
            yield return new WaitForSeconds(_writeSpeed);
        }
    }
}