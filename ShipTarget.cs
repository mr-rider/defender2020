using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTarget : MonoBehaviour
{
    // Спрайт для использования в качестве прицельной сетки.
    public Sprite targetImage;
    // Start is called before the first frame update
    void Start()
    {
        // Зарегистрировать новый индикатор, соответствующий данному объекту,
        // использовать желтый цвет и нестандартный спрайт.
        IndicatorManager.instance.AddIndicator(gameObject, Color.white, targetImage);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
