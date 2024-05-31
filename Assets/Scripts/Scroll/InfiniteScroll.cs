using UnityEngine;
using UnityEngine.UI;

public class InfiniteScroll : MonoBehaviour
{
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private RectTransform _viewPortTransform;
    [SerializeField] private RectTransform _contentPanelTransform;
    [SerializeField] private HorizontalLayoutGroup _layoutGroup;
    [SerializeField] private RectTransform[] _itemList;

    private void Awake()
    {
        int ItemsToAdd = Mathf.CeilToInt(_viewPortTransform.rect.width / (_itemList[0].rect.width + _layoutGroup.spacing));
        for (int i = 0; i < ItemsToAdd; i++)
        {
            RectTransform RT = Instantiate(_itemList[i % _itemList.Length], _contentPanelTransform);
            RT.SetAsLastSibling();
        }

        for (int i = 0; i < ItemsToAdd; i++)
        {
            int num = _itemList.Length - i - 1;
            while (num < 0)
                num += _itemList.Length;
            RectTransform RT = Instantiate(_itemList[num], _contentPanelTransform);
            RT.SetAsFirstSibling();
        }

        //_contentPanelTransform.localPosition = new Vector3((0 - (_itemList[0].rect.width + _layoutGroup.spacing) * ItemsToAdd), 
        //    _contentPanelTransform.localPosition.y, 
        //    _contentPanelTransform.localPosition.z);
    }

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
