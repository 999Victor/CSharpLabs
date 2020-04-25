using System.Collections.Generic;

namespace Lab7
{
    class Comparer: IComparer<RationalNum>
    {
        public int Compare(RationalNum obj1, RationalNum obj2)
        {
            if ((obj1.FirstNum == obj2.FirstNum) && (obj1.SecondNum == obj2.SecondNum))
            {
                return 1;
            }
            else
            {
                return (obj1.FirstNum * obj2.SecondNum > obj2.FirstNum * obj1.SecondNum) ? 
                    (obj1.FirstNum / obj1.SecondNum) : (obj2.FirstNum / obj2.SecondNum);
            }
        }
    }
}
