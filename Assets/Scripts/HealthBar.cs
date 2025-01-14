using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Image _barImage;
 
    private float _delay = .2f;
    private float _speed = 1f;
    
    private Coroutine _changeCoroutine;
    
    private void OnEnable()
    {
        _health.Changed += UpdateDisplay;
    }

    private void OnDisable()
    {
        _health.Changed -= UpdateDisplay;
    }

    private void Start()
    {
        _barImage.fillAmount = _health.GetNormalized();
    }

    private void LateUpdate()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
    
    private IEnumerator SetFill()
    {
        yield return new WaitForSeconds(_delay);

        float targetFill = _health.GetNormalized();

        while (!Mathf.Approximately(_barImage.fillAmount, targetFill))
        {
            _barImage.fillAmount = Mathf.MoveTowards(
                _barImage.fillAmount,
                targetFill,
                _speed * Time.deltaTime);
            
            yield return null;
        }
    }
    
    private void UpdateDisplay()
    {
        if (_changeCoroutine != null)
        {
            StopCoroutine(_changeCoroutine);
        }

        _changeCoroutine = StartCoroutine(SetFill());
    }
}