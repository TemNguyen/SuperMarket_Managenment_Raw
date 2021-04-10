using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THK
{
    class SP
    {
        public int ID_SP { get; set; }
        public string Ten { get; set; }
        public bool TrangThai { get; set; }
        public DateTime NSX { get; set; }
        public string ID_MH { get; set; }

        public static bool CompareIDSP(object s1, object s2)
        {
            if (((SP)s1).ID_SP >= ((SP)s2).ID_SP) return true;
            else return false;
        }

        public static bool CompareTen(object s1, object s2)
        {
            if (string.Compare(((SP)s1).Ten, ((SP)s2).Ten) > 0) return true;
            else return false;
        }

        public static bool CompareTT(object s1, object s2)
        {
            if (((SP)s1).TrangThai != ((SP)s2).TrangThai) return true;
            else return false;
        }

        public static bool CompareNSX(object s1, object s2)
        {
            if (string.Compare(((SP)s1).NSX.ToString(), ((SP)s2).NSX.ToString()) > 0) return true;
            else return false;
        }

        public static bool CompareIDMH(object s1, object s2)
        {
            if (string.Compare(((SP)s1).ID_MH, ((SP)s2).ID_MH) > 0) return true;
            else return false;
        }
    }
}
