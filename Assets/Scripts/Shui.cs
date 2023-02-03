using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shui : Magic
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public static bool Recognizer(List<List<Vector2>> positions)
    {
        return (
                positions[0][0].y > positions[0].Last().y && //第一画从上到下
                positions[1][0].y > positions[1].Last().y && //第二画从上到下
                positions[2][0].y > positions[2].Last().y && //第三画从上到下
                positions[3][0].y > positions[3].Last().y && //第四画从上到下

                positions[2][0].x > positions[2].Last().x && //第三画从右到左
                positions[3][0].x < positions[3].Last().x //第四画从左到右
            );
    }
}
