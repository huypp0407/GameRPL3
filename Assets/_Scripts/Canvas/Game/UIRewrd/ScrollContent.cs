using UnityEngine;

public class ScrollContent : _MonoBehaviour
{
    public float ItemSpacing { get { return itemSpacing; } }

    public float HorizontalMargin { get { return horizontalMargin; } }

    public float VerticalMargin { get { return verticalMargin; } }

    public bool Horizontal { get { return horizontal; } }

    public bool Vertical { get { return vertical; } }

    public float Width { get { return width; } }

    public float Height { get { return height; } }

    public float ChildWidth { get { return childWidth; } }

    public float ChildHeight { get { return childHeight; } }

    private RectTransform rectTransform;

    private RectTransform[] rtChildren;

    private float width, height;

    private float childWidth, childHeight;

    [SerializeField]
    private float itemSpacing;

    [SerializeField]
    private float horizontalMargin, verticalMargin;

    [SerializeField]
    private bool horizontal, vertical;

    protected override void Start()
    {
        this.ResizeContent();
    }

    public void ResizeContent()
    {
        rectTransform = GetComponent<RectTransform>();
        rtChildren = new RectTransform[rectTransform.childCount];

        if (rectTransform.childCount < 1) return;

        for (int i = 0; i < rectTransform.childCount; i++)
        {
            rtChildren[i] = rectTransform.GetChild(i) as RectTransform;
        }

        width = rectTransform.rect.width - (2 * horizontalMargin);
        height = rectTransform.rect.height - (2 * verticalMargin);

        childWidth = rtChildren[0].rect.width;

        childHeight = rtChildren[0].rect.height;

        InitializeContentHorizontal();
    }

    private void InitializeContentHorizontal()
    {
        float originX = 50;
        float posOffset = childWidth * 0.5f;
        for (int i = 0; i < rtChildren.Length; i++)
        {
            Vector2 childPos = rtChildren[i].localPosition;
            childPos.x = originX + posOffset + i * (childWidth + itemSpacing);
            childPos.y = -100;
            rtChildren[i].localPosition = childPos;
        }
    }
}
