using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Huo : Magic
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
        return (positions[0].First().y > positions[0].Last().y && //第一画从上到下
                positions[1].First().y > positions[1].Last().y && //第二画从上到下
                positions[2].First().y > positions[2].Last().y && //第三画从上到下
                positions[3].First().y > positions[3].Last().y && //第四画从上到下
                positions[0].Last().y > positions[2].Last().y && //一尾在三尾上
                positions[1].Last().y > positions[3].Last().y && //二尾在四尾上
                positions[2].First().x > positions[0].First().x && //二始在一始右边
                positions[2].First().x < positions[1].First().x && //二始在三始左边
                positions[3].First().y < positions[2].First().y && //四始在三始下面
                positions[3].First().y > positions[2].Last().y //四始在三尾上面
            );
    }
}
