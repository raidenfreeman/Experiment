using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    Renderer fillRenderer;

    /// <summary>
    /// The global position from which the fill begins (0%)
    /// </summary>
    [SerializeField]
    Transform Floor;

    /// <summary>
    /// The global position at which the fill ends (100%)
    /// </summary>
    [SerializeField]
    Transform Roof;

    [SerializeField]
    float _percentage = 0f;

    /// <summary>
    /// Updates the fill to a percentage
    /// </summary>
    /// <param name="percentage">A value from 0 to 100</param>
    public void UpdatePercentage(float percentage)
    {
        if (percentage > 0)
        {
            _render.enabled = true;
            fillRenderer.enabled = true;
        }
        if (percentage > 100f)
        {
            Debug.LogWarning($"Fill percentage {percentage} is over 100%");
            percentage = 100f;
        }
        if (percentage < 0f)
        {
            Debug.LogWarning($"Fill percentage {percentage} is negative");
            percentage = 0f;
        }

        if (percentage >= 100f && tweenStarted == false)
        {
            tweenStarted = true;
            transform.DORotate(Vector3.forward * 720, 1, RotateMode.FastBeyond360).SetEase(Ease.Linear);
            var initialScale = transform.localScale;
            transform.DOScale(Vector3.zero, 0.5f).SetDelay(0.5f).SetEase(Ease.InQuint).OnComplete(() =>
            {
                _render.enabled = false;
                fillRenderer.enabled = false;
                transform.localScale = initialScale;
                //this.gameObject.SetActive(false);
            });
        }
        _percentage = percentage;
    }

    private void Update()
    {
        var GlobalPositionDifference = Roof.position - Floor.position;
        fillRenderer.material.SetVector("_PlanePosition", Floor.position + GlobalPositionDifference * _percentage * 0.01f);
    }

    bool tweenStarted = false;
    Renderer _render;
    private void Awake()
    {
        _render = this.GetComponent<MeshRenderer>();
    }
    private void OnEnable()
    {
        tweenStarted = false;
        UpdatePercentage(0f);
    }

    float p = 0;
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.L))
        {
            p += Time.fixedDeltaTime * 40;
            UpdatePercentage(p);
        }
    }
}
