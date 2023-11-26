using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class ArticyDialogueText : MonoBehaviour
{
    float _timeAtStart;
    Color _baseColor;
    float _waitTime;

    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _text;

    public Color Color
    {
        set
        {
            _title.color = value;
            _text.color = value;
            _baseColor = value;
        }
    }
    public string Text { set => _text.text = value; }
    public string Title { set => _title.text = value; }

    public float WaitTime { set => _waitTime = (value < 5 ? value : 5); }

    void Start()
    {
        _timeAtStart = Time.time;
    }

    private void Update()
    {
        Color fadeColor = _baseColor;

        fadeColor.a = _baseColor.a * (1 - Mathf.Pow(Mathf.Clamp01((Time.time - _timeAtStart) / _waitTime), 2));

        _title.color = fadeColor;
        _text.color = fadeColor;


        if (Time.time - _timeAtStart > _waitTime)
        {
            Destroy(gameObject);
        }
    }
}
