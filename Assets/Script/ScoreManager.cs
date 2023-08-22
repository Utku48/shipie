using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI _scoreText;
    public static int money;

    private void Update()
    {
        _scoreText.text = money.ToString();
    }
}
