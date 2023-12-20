using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Deduction : MonoBehaviour
{
    [SerializeField] TMP_Text _deductionText;
    [SerializeField] Button _deductionButton;
    [SerializeField] float _writeSpeed = 0.2f;
    private TestimonyItem _testimonyItem;
    private TestimonyInfo _testimonyInfo;

    public void Init(TestimonyItem item, TestimonyInfo info)
    {
        _testimonyItem = item;
        _testimonyInfo = info;
        SetTestimony();
    }

    Coroutine _typingCoroutine;

    public void SetTestimony()
    {
        if (_typingCoroutine != null) { StopCoroutine(_typingCoroutine); }
        _typingCoroutine = StartCoroutine(WriteLine(_testimonyInfo.Description));
    }

    private void OnDisable()
    {
        if (_typingCoroutine != null)
        {
            _deductionText.text = _testimonyInfo.Description;
            StopCoroutine(_typingCoroutine);
        }
    }

    public void UpdateTestimony()
    {
        TestimonyManager.Instance.ShowContradictionScreen(_testimonyItem, false);
        FMOD.Studio.EventInstance audioEvent = FMODUnity.RuntimeManager.CreateInstance("event:/Writing");
        audioEvent.start();
        audioEvent.release();
    }

    public TestimonyType GetTestimonyType()
    {
        return _testimonyInfo.Type;
    }

    public void LockDeduction()
    {
        _deductionButton.interactable = false;
    }

    IEnumerator WriteLine(string line)
    {
        _deductionText.text = "";
        foreach (char c in line.ToCharArray())
        {
            _deductionText.text += c;

            FMOD.Studio.EventInstance audioEvent = FMODUnity.RuntimeManager.CreateInstance("event:/Handwriting");
            audioEvent.start();
            audioEvent.release();

            yield return new WaitForSeconds(_writeSpeed);
        }

        _typingCoroutine = null;
    }
}
