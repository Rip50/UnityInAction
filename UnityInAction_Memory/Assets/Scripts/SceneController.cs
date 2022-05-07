using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private MemoryCard originalCard;
    [SerializeField] private Sprite[] images;
    [SerializeField] private TextMesh scoreLabel;

    private const int GridRows = 2;
    private const int GridColumns = 4;
    private const float OffsetX = 2f;
    private const float OffsetY = 2.5f; 

    private MemoryCard _firstRevealed;
    private MemoryCard _secondRevealed;

    public int Score { get; private set; } 
    public bool CanReveal => _secondRevealed == null; 

    // Start is called before the first frame update
    void Start()
    {
        var initialPosition = originalCard.transform.position;

        var numbers = new[] { 0, 0, 1, 1, 2, 2, 3, 3 }.ShuffleArray();
        var counter = 0;
        for (var y = 0; y < GridRows; y++)
        {
            MemoryCard card;
            for (var x = 0; x < GridColumns; x++)
            {
                if (x == 0 && y == 0)
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard);
                }

                var id = numbers[counter];
                card.SetCard(id, images[id]);

                var posX = x * OffsetX + initialPosition.x;
                var posY = -y * OffsetY + initialPosition.y;
                card.transform.position = new Vector3(posX, posY, initialPosition.z);
                counter++;
            }  
        }
    }

    public void CardRevealed(MemoryCard card)
    {
        if (_firstRevealed == null)
        {
            _firstRevealed = card;
        }
        else
        {
            _secondRevealed = card;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        if (_firstRevealed.Id == _secondRevealed.Id)
        {
            Score++;
            scoreLabel.text = $"Score: {Score}";
        }
        else
        {
            yield return new WaitForSeconds(2f);
            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();
        }
        _firstRevealed = null;
        _secondRevealed = null;
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}

public static class IntExtensions
{
    public static int[] ShuffleArray(this int[] numbers)
    {
        for (var i = 0; i < numbers.Length; i++)
        {
            var secondNumber = Random.Range(0, numbers.Length);
            (numbers[i], numbers[secondNumber]) = (numbers[secondNumber], numbers[i]);
        }

        return numbers;
    }
}
