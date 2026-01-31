using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public bool  locked;
    private SpriteRenderer rend;

    [SerializeField]
    private Sprite faceSprite, backSprite;

    private bool coroutineAllowed, facedUp;

    private Card firstInPair, secondInPair;
    private string firstInPairName, secondInPairName;

    public static int pairsFound;

    public static Queue<Card> sequence;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        rend.sprite = backSprite;
        coroutineAllowed = true;
        facedUp = false;
        locked = false;
        sequence = new Queue<Card>();
    }

    private void OnMouseDown()
    {
        if (!locked && coroutineAllowed)
        {
            StartCoroutine(RotateCard());
        }
    }

    private IEnumerator RotateCard()
    {
        coroutineAllowed = false;

        if (!facedUp)
        {
            sequence.Enqueue(this);
            for (float i = 0f; i <= 180f; i += 10f)
            {
                transform.rotation = Quaternion.Euler(0f, i, 0f);
                if (i == 90f)
                {
                    rend.sprite = faceSprite;
                }
                yield return new WaitForSeconds(0.01f);
            }
        }

        else if (facedUp)
        {
            for (float i = 180f; i >= 0f; i -= 10f)
            {
                transform.rotation = Quaternion.Euler(0f, i, 0f);
                if (i == 90f)
                {
                    rend.sprite = backSprite;
                }
                yield return new WaitForSeconds(0.012f);
                sequence.Clear();
            }
        }

        coroutineAllowed = true;

        facedUp = !facedUp;

        if (sequence.Count == 2)
        {
            CheckResults();
        }
    }

    private void CheckResults()
    {
        firstInPair = sequence.Dequeue();
        secondInPair = sequence.Dequeue();

        firstInPairName = firstInPair.faceSprite.name;
        secondInPairName = secondInPair.faceSprite.name;

        if (firstInPairName == secondInPairName)
        {
            firstInPair.locked = true;
            secondInPair.locked = true;
            pairsFound++;
        }
        else
        {
            firstInPair.StartCoroutine(firstInPair.RotateCard());
            secondInPair.StartCoroutine(secondInPair.RotateCard());
        }
    }

    public IEnumerator RotateBack()
    {
        coroutineAllowed = false;
        yield return new WaitForSeconds(1f);
        for (float i = 180f; i >= 0f; i -= 10f)
        {
            transform.rotation = Quaternion.Euler(0f, i, 0f);
            if (i == 90f)
            {
                rend.sprite = backSprite;
            }
            yield return new WaitForSeconds(0.012f);
            sequence.Clear();
        }
        facedUp = false;
        coroutineAllowed = true;
    }
}
