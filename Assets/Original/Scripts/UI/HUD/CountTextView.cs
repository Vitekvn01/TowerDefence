using TMPro;
using UnityEngine;

public class CountTextView : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI _text;

   public void ChangeCountText(int count)
   {
      _text.text = count.ToString();
   }
}
