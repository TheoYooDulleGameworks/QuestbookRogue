using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class RollDice : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    [Header("Value")]
    [SerializeField] private DiceType diceType;
    [SerializeField] private int dieValue;
    [SerializeField] private GameObject checkCollider;

    [Header("Components")]
    [SerializeField] private RectTransform diceImage;
    [SerializeField] private RectTransform shadowImage;
    [SerializeField] private RectTransform generatingAnim;
    [SerializeField] private RectTransform vanishingAnim;
    [SerializeField] private RectTransform modifyingAnim;

    [Header("Source Sprites")]
    [SerializeField] private List<Sprite> possibleValues;
    [SerializeField] private List<Sprite> possibleHovers;
    [SerializeField] private List<Sprite> possibleClicks;
    [SerializeField] private List<Sprite> possibleSlotValues;
    [SerializeField] private List<Sprite> possibleSlotClicks;

    [Header("Own Sprites")]
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite defaultHoverSprite;

    [SerializeField] private Sprite valueSprite = null;
    [SerializeField] private Sprite valueHoverSprite = null;
    [SerializeField] private Sprite valueClickSprite = null;
    [SerializeField] public Sprite valueSlotSprite = null;
    [SerializeField] public Sprite valueSlotClickSprite = null;

    [Header("Bool Triggers")]
    [SerializeField] private bool wasRolled = false;
    [SerializeField] private bool isDragging = false;
    [SerializeField] private bool isMouseInputInitialized = true;
    [SerializeField] private bool notYetRolledAndWait = false;
    [SerializeField] private bool onceRolled = false;
    [SerializeField] private bool endDragged = false;

    [Header("Tweening")]
    [SerializeField] private float popUpScale = 1.35f;
    [SerializeField] private float popUpDuration = 0.25f;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    [SerializeField] private Canvas canvas;
    private Animator animator;



    [Header("Skill Check")]
    [SerializeField] private bool isSkillCostChecking;
    [SerializeField] private bool isSkillCastChecking;
    [SerializeField] private bool isClickable;
    [SerializeField] private bool isSelected;
    [SerializeField] private RectTransform deactivateImageRect;
    [SerializeField] private RectTransform selectedImageRect;
    private bool isVanishing = false;




    private void OnEnable()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        canvas = GameObject.FindWithTag("Canvas").GetComponent<Canvas>();
        animator = GetComponent<Animator>();
        animator.enabled = false;

        checkCollider.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        float knockbackForce = 0f;

        if (collision.collider.CompareTag("RollDiceFence"))
        {
            knockbackForce = 600f;
            Debug.Log("Fence!");
            Vector2 direction = (collision.transform.position - transform.position).normalized;
            Vector2 appliedForce = direction * knockbackForce;
            rb.AddForce(appliedForce, ForceMode2D.Impulse);
        }
        else
        {
            knockbackForce = 200f;
            Vector2 direction = (transform.position - collision.transform.position).normalized;
            Vector2 appliedForce = direction * knockbackForce;
            rb.AddForce(appliedForce, ForceMode2D.Impulse);
        }

        if (this.gameObject != null && rb != null && this.gameObject.activeSelf)
        {
            StartCoroutine(ReduceVelocityOverTime(rb));
        }
    }

    private IEnumerator ReduceVelocityOverTime(Rigidbody2D rb)
    {
        float decelerationRate = 0.95f;
        float minimumSpeed = 0.1f;

        while (rb != null && rb.linearVelocity.magnitude > minimumSpeed)
        {
            rb.linearVelocity *= decelerationRate;
            yield return new WaitForFixedUpdate();
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    public void ActivateRollDice()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
    }

    public void DeActivateRollDice()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }

    public void DestroyRollDice()
    {
        Destroy(gameObject);
    }

    public void OnReRollTriggered()
    {
        if (wasRolled)
        {
            wasRolled = false;
        }
        if (!isMouseInputInitialized)
        {
            isMouseInputInitialized = true;
        }
        if (notYetRolledAndWait)
        {
            notYetRolledAndWait = false;
        }
        if (onceRolled)
        {
            onceRolled = false;
        }

        diceImage.GetComponent<Image>().sprite = defaultSprite;
        PopUpAnim();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isSkillCostChecking || isSkillCastChecking)
        {
            if (!isClickable)
            {
                return;
            }
        }

        if (isDragging || notYetRolledAndWait)
        {
            return;
        }

        if (!wasRolled)
        {
            diceImage.GetComponent<Image>().sprite = defaultHoverSprite;
            return;
        }

        diceImage.GetComponent<Image>().sprite = valueHoverSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isSkillCostChecking || isSkillCastChecking)
        {
            if (!isClickable)
            {
                return;
            }
        }

        if (isDragging || notYetRolledAndWait)
        {
            return;
        }

        if (!wasRolled)
        {
            diceImage.GetComponent<Image>().sprite = defaultSprite;
            return;
        }

        diceImage.GetComponent<Image>().sprite = valueSprite;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (isSkillCostChecking || isSkillCastChecking)
            {
                return;
            }

            CursorManager.Instance.OnClickCursor();

            if (!wasRolled)
            {
                diceImage.GetComponent<Image>().sprite = defaultSprite;

                rectTransform.DOKill();
                rectTransform.localScale = new Vector3(1.2f, 1.2f, 1.2f);

                notYetRolledAndWait = true;

                return;
            }

            transform.SetAsLastSibling();
            diceImage.GetComponent<Image>().sprite = valueClickSprite;

            GetComponent<BoxCollider2D>().enabled = false;

            VfxManager.Instance.DiceUpVfx(rectTransform);

            AudioManager.Instance.PlaySfxWithPitch("GrabOn");

            rectTransform.DOKill();
            rectTransform.localScale = new Vector3(popUpScale, popUpScale, popUpScale);
            rectTransform.DOScale(new Vector3(1f, 1f, 1f), popUpDuration);

            if (checkCollider.activeSelf)
            {
                checkCollider.SetActive(false);
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (isSkillCostChecking || isSkillCastChecking)
            {
                return;
            }

            if (!isMouseInputInitialized)
            {
                isMouseInputInitialized = true;
            }

            CursorManager.Instance.OnDefaultCursor();

            if (!wasRolled)
            {
                rectTransform.DOKill();
                rectTransform.DOScale(new Vector3(1f, 1f, 1f), popUpDuration);
                notYetRolledAndWait = false;

                return;
            }

            diceImage.GetComponent<Image>().sprite = valueSprite;


            if (!endDragged)
            {
                GetComponent<BoxCollider2D>().enabled = true;


                rectTransform.DOKill();
                rectTransform.localScale = new Vector3(popUpScale, popUpScale, popUpScale);
                rectTransform.DOScale(new Vector3(1f, 1f, 1f), popUpDuration);

                if (!checkCollider.activeSelf)
                {
                    checkCollider.SetActive(true);
                }

                StartCoroutine(grabDownRoutine());
            }
            else
            {
                endDragged = false;
            }
        }
    }

    private IEnumerator grabDownRoutine()
    {
        yield return new WaitForSeconds(0f);

        if (this.gameObject.activeSelf)
        {
            VfxManager.Instance.DiceDownVfx(rectTransform);

            AudioManager.Instance.PlaySfxWithPitch("GrabDown");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (isSkillCostChecking)
            {
                if (isClickable)
                {
                    transform.SetAsLastSibling();

                    if (!isSelected)
                    {
                        isSelected = true;
                        selectedImageRect.gameObject.SetActive(true);
                        SkillManager.Instance.SkillCostCount(1);
                        return;
                    }
                    else
                    {
                        isSelected = false;
                        selectedImageRect.gameObject.SetActive(false);
                        SkillManager.Instance.SkillCostCount(-1);
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            if (isSkillCastChecking)
            {
                if (isClickable)
                {
                    transform.SetAsLastSibling();

                    if (!isSelected)
                    {
                        isSelected = true;
                        selectedImageRect.gameObject.SetActive(true);
                        SkillManager.Instance.SkillCastCount(1, this);
                        return;
                    }
                    else
                    {
                        isSelected = false;
                        selectedImageRect.gameObject.SetActive(false);
                        SkillManager.Instance.SkillCastCount(-1, this);
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            if (wasRolled)
            {
                return;
            }

            CursorManager.Instance.OnDefaultCursor();
            RollAllDice();
        }

        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            // Test //
            OnReRollTriggered();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && wasRolled && isMouseInputInitialized)
        {
            if (isSkillCostChecking || isSkillCastChecking)
            {
                return;
            }

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out Vector2 localPoint))
            {
                rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
                ClampPositionToBoundary();

                if (!IsPointerInsideBounds(eventData.position))
                {
                    EndDrag();
                }
            }
        }
    }

    private bool IsPointerInsideBounds(Vector2 mousePosition)
    {
        return mousePosition.x >= 0 && mousePosition.x <= 1920 * canvas.scaleFactor &&
               mousePosition.y >= 0 && mousePosition.y <= 1080 * canvas.scaleFactor;
    }

    private void EndDrag()
    {
        endDragged = true;

        isMouseInputInitialized = false;

        CursorManager.Instance.OnDefaultCursor();
        diceImage.GetComponent<Image>().sprite = valueSprite;

        GetComponent<BoxCollider2D>().enabled = true;

        VfxManager.Instance.DiceDownVfx(rectTransform);

        AudioManager.Instance.PlaySfxWithPitch("GrabDown");

        rectTransform.DOKill();
        rectTransform.localScale = new Vector3(popUpScale, popUpScale, popUpScale);
        rectTransform.DOScale(new Vector3(1f, 1f, 1f), popUpDuration);

        isDragging = false;
        if (shadowImage.gameObject.activeSelf == false)
        {
            shadowImage.gameObject.SetActive(true);
        }

        if (!checkCollider.activeSelf)
        {
            checkCollider.SetActive(true);
        }
    }

    private void ClampPositionToBoundary()
    {
        Vector3 minPosition = transform.parent.GetComponent<RectTransform>().rect.min - rectTransform.rect.min;
        Vector3 maxPosition = transform.parent.GetComponent<RectTransform>().rect.max - rectTransform.rect.max;

        Vector2 clampedPosition = rectTransform.anchoredPosition;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minPosition.x, maxPosition.x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minPosition.y, maxPosition.y);

        rectTransform.anchoredPosition = clampedPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (isSkillCostChecking || isSkillCastChecking)
            {
                return;
            }

            if (!wasRolled)
            {
                return;
            }

            isDragging = true;

            shadowImage.gameObject.SetActive(false);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (isSkillCostChecking || isSkillCastChecking)
            {
                return;
            }

            if (!wasRolled)
            {
                return;
            }

            isDragging = false;

            shadowImage.gameObject.SetActive(true);
        }
    }

    private void RollAllDice()
    {
        StartCoroutine(RollAllDiceRoutine());
    }

    private IEnumerator RollAllDiceRoutine()
    {
        yield return StartCoroutine(RollAllDiceSequenceRoutine());

        yield return new WaitForSeconds(0.5f);

        if (GameManager.Instance.currentStagePhase != StagePhase.DiceUsing)
        {
            GameManager.Instance.UpdateStagePhase(StagePhase.DiceUsing);
        }
    }

    private IEnumerator RollAllDiceSequenceRoutine()
    {
        List<RollDice> allDices = new List<RollDice>(transform.parent.GetComponentsInChildren<RollDice>());

        foreach (var dice in allDices)
        {
            if (!dice.onceRolled)
            {
                dice.onceRolled = true;
                AudioManager.Instance.PlaySfx("DiceThrow");
                dice.DiceRollAnim();
                yield return new WaitForSeconds(0.025f);
            }
        }
    }

    public void DiceRollAnim()
    {

        animator.enabled = true;
        animator.Play("DiceIdle", 0, 0f);
        animator.SetTrigger("DiceRoll");
        isMouseInputInitialized = false;
    }

    public void DiceRolledSfx()
    {
        //AudioManager.Instance.PlaySfx("DiceRolled");
    }

    public void DiceRollValue()
    {

        int randomValue = Random.Range(0, 6);

        if (randomValue == 0)
        {
            dieValue = 0;
        }
        else
        {
            dieValue = randomValue + 1;
        }

        valueSprite = possibleValues[randomValue];
        valueHoverSprite = possibleHovers[randomValue];
        valueClickSprite = possibleClicks[randomValue];
        valueSlotSprite = possibleSlotValues[randomValue];
        valueSlotClickSprite = possibleSlotClicks[randomValue];

        animator.enabled = false;
        diceImage.GetComponent<Image>().sprite = valueSprite;


        rectTransform.DOKill();
        rectTransform.localScale = new Vector3(popUpScale, popUpScale, popUpScale);
        rectTransform.DOScale(new Vector3(1f, 1f, 1f), popUpDuration).OnComplete(() =>
        {
            GetComponent<Image>().raycastTarget = true;
            wasRolled = true;
            isMouseInputInitialized = true;
        });
    }



    public bool IsAboveValue(List<DiceType> diceTypes, int conditionValue)
    {
        if (diceTypes.Contains(diceType))
        {
            if (conditionValue <= dieValue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public bool IsSameValue(List<DiceType> diceTypes, List<int> conditionValues)
    {
        if (diceTypes.Contains(diceType))
        {
            if (conditionValues.Contains(dieValue))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public bool IsSameType(List<DiceType> diceTypes)
    {
        if (diceTypes.Contains(diceType))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsPossibleToModify(List<DiceType> diceTypes)
    {
        if (diceTypes.Contains(diceType))
        {
            if (dieValue == 6 || dieValue == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }
    }

    public bool IsFillValue(List<DiceType> diceTypes)
    {
        if (diceTypes.Contains(diceType))
        {
            if (dieValue != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public int DieValue()
    {
        return dieValue;
    }



    public void RaycastOff()
    {
        GetComponent<Image>().raycastTarget = false;
    }

    public void PopUpAnim()
    {
        rectTransform.DOKill();
        rectTransform.localScale = new Vector3(popUpScale, popUpScale, popUpScale);
        rectTransform.DOScale(new Vector3(1f, 1f, 1f), popUpDuration);
    }



    // SKILLS //

    public void SkillCostCheck()
    {
        isSkillCostChecking = true;
        isSkillCastChecking = false;
    }

    public void SkillCastCheck()
    {
        isSkillCostChecking = false;
        isSkillCastChecking = true;
    }

    public void SkillActivate()
    {
        if (isVanishing)
        {
            return;
        }

        isClickable = true;

        if (deactivateImageRect.gameObject.activeSelf)
        {
            deactivateImageRect.gameObject.SetActive(false);
        }
    }

    public void SkillDeActivate()
    {
        if (isVanishing)
        {
            return;
        }

        isClickable = false;

        deactivateImageRect.gameObject.SetActive(true);
    }

    public void SkillFix(int fixedValue)
    {
        dieValue = fixedValue;
        wasRolled = true;
        onceRolled = true;
        isMouseInputInitialized = true;

        valueSprite = possibleValues[fixedValue - 1];
        valueHoverSprite = possibleHovers[fixedValue - 1];
        valueClickSprite = possibleClicks[fixedValue - 1];
        valueSlotSprite = possibleSlotValues[fixedValue - 1];
        valueSlotClickSprite = possibleSlotClicks[fixedValue - 1];

        diceImage.GetComponent<Image>().sprite = valueSprite;
    }

    public void SkillReRoll()
    {
        DiceRollAnim();
    }

    public void SkillModify(int modifyValue)
    {
        GetComponent<Image>().raycastTarget = false;

        dieValue = Mathf.Clamp(dieValue + modifyValue, 0, 6);

        valueSprite = possibleValues[dieValue - 1];
        valueHoverSprite = possibleHovers[dieValue - 1];
        valueClickSprite = possibleClicks[dieValue - 1];
        valueSlotSprite = possibleSlotValues[dieValue - 1];
        valueSlotClickSprite = possibleSlotClicks[dieValue - 1];

        diceImage.GetComponent<Image>().sprite = valueSprite;

        modifyingAnim.gameObject.SetActive(true);
        rectTransform.DOKill();
        rectTransform.localScale = new Vector3(popUpScale, popUpScale, popUpScale);
        rectTransform.DOScale(new Vector3(1f, 1f, 1f), popUpDuration).OnComplete(() =>
        {
            GetComponent<Image>().raycastTarget = true;
        });
    }

    public void SkillGenerate()
    {
        StartCoroutine(GeneratingAnimRoutine());
    }

    private IEnumerator GeneratingAnimRoutine()
    {
        diceImage.gameObject.SetActive(false);
        shadowImage.gameObject.SetActive(false);
        generatingAnim.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.25f);

        diceImage.gameObject.SetActive(true);
        shadowImage.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.6f);

        GetComponent<Image>().raycastTarget = true;
    }

    public void SkillSpend()
    {
        if (isSkillCostChecking && isSelected)
        {
            isVanishing = true;

            SkillManager.Instance.RegisterRefundDice(diceType, dieValue);

            GetComponent<Image>().raycastTarget = false;
            deactivateImageRect.gameObject.SetActive(false);
            selectedImageRect.gameObject.SetActive(false);
            diceImage.gameObject.SetActive(false);
            shadowImage.gameObject.SetActive(false);
            vanishingAnim.gameObject.SetActive(true);
        }
    }

    public void SkillCancel()
    {
        isSkillCostChecking = false;
        isSkillCastChecking = false;

        if (isClickable)
        {
            isClickable = false;
        }
        else
        {
            deactivateImageRect.gameObject.SetActive(false);
        }

        if (isSelected)
        {
            isSelected = false;
            selectedImageRect.gameObject.SetActive(false);
        }
    }
}

public enum DiceType
{
    None,
    Strength,
    Agility,
    Intelligence,
    Willpower,
}

public enum PaymentType
{
    None,
    Gold,
    Provision,
    Xp,
    Lv,
    Hp,
    Dice,
    Skill,
    Relic,
}