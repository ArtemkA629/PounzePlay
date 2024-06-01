using UnityEngine;
using UnityEngine.UI;

public class InfiniteScroll : MonoBehaviour
{
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private RectTransform _viewPortTransform;
    [SerializeField] private RectTransform _contentPanelTransform;
    [SerializeField] private HorizontalLayoutGroup _layoutGroup;
    [SerializeField] private RectTransform[] _itemList;

    private void Update()
    {
        if (_contentPanelTransform.localPosition.x > 0)
            UpdateGroup(true);
        if (_contentPanelTransform.localPosition.x < 0 - (_itemList.Length * (_itemList[0].rect.width + _layoutGroup.spacing)))
            UpdateGroup(false);
    }

    private void UpdateGroup(bool isContentRight)
    {
        Canvas.ForceUpdateCanvases();
        var vectorToUpdateGroup = new Vector3(_itemList.Length * (_itemList[0].rect.width + _layoutGroup.spacing), 0, 0);
        if (isContentRight)
            _contentPanelTransform.localPosition -= vectorToUpdateGroup;
        else
            _contentPanelTransform.localPosition += vectorToUpdateGroup;
    }
}
