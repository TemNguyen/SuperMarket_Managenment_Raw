using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THK
{
    class CSDL_OOP
    {
        public string[] listName = new string[CSDL.Instance.DSSP.Columns.Count];
        public delegate bool Compare(object s1, object s2);
        public static CSDL_OOP Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new CSDL_OOP();
                }
                return _Instance;
            }
            private set
            {

            }
        }
        private static CSDL_OOP _Instance;
        private CSDL_OOP()
        {
            for (int i = 0; i < CSDL.Instance.DSSP.Columns.Count; i++)
            {
                listName[i] = CSDL.Instance.DSSP.Columns[i].ColumnName;
            }
        }
        public List<SP> getAllSP()
        {
            List<SP> temp = new List<SP>();
            foreach (DataRow dr in CSDL.Instance.DSSP.Rows)
            {
                temp.Add(get1SP(dr));
            }
            return temp;
        }
        private SP get1SP(DataRow dr)
        {
            SP s = new SP();
            s.ID_SP = Convert.ToInt32(dr["ID_SP"]);
            s.Ten = dr["Ten"].ToString();
            s.TrangThai = Convert.ToBoolean(dr["TrangThai"]);
            s.NSX = Convert.ToDateTime(dr["NSX"]);
            s.ID_MH = dr["ID_MH"].ToString();
            return s;
        }
        public List<MatHang> getAllMH()
        {
            List<MatHang> temp = new List<MatHang>();
            foreach (DataRow dr in CSDL.Instance.DSMH.Rows)
            {
                temp.Add(get1MH(dr));
            }
            return temp;
        }
        private MatHang get1MH(DataRow dr)
        {
            MatHang s = new MatHang();
            s.ID_MH = dr["ID_MH"].ToString();
            s.TenMH = dr["TenMH"].ToString();
            return s;
        }
        public List<SP> getSPByName(string name)
        {
            List<SP> temp = new List<SP>();
            foreach (SP i in getAllSP())
            {
                if (i.Ten.Contains(name))
                {
                    temp.Add(i);
                }
            }
            return temp;
        }
        public List<SP> getSPByIDName(string ID_MatHang, string nameSP)
        {
            List<SP> temp = new List<SP>();
            if (nameSP == "")
            {
                foreach (DataRow i in CSDL.Instance.DSSP.Rows)
                {
                    if (i["ID_MH"].ToString() == ID_MatHang)
                    {
                        temp.Add(get1SP(i));
                    }
                }
            }
            else
            {
                foreach (DataRow i in CSDL.Instance.DSSP.Rows)
                {
                    if ((i["ID_MH"].ToString() == ID_MatHang) && (i["Ten"].ToString().Contains(nameSP)))
                    {
                        temp.Add(get1SP(i));
                    }
                }
            }
            return temp;
        }
        public int IndexOf(int IDSP)
        {
            for (int i = 0; i < getAllSP().Count; i++)
            {
                if (getAllSP()[i].ID_SP == IDSP)
                {
                    return i;
                }
            }
            return -1;
        }
        public void AddSPToCSDL(SP s)
        {
            object[] o = new object[CSDL.Instance.DSSP.Columns.Count];
            o[0] = s.ID_SP;
            o[1] = s.Ten;
            o[2] = s.TrangThai;
            o[3] = s.NSX;
            o[4] = s.ID_MH;
            CSDL.Instance.DSSP.Rows.Add(o);
        }
        public void EditStudentInCSDL(SP s, int index)
        {
            object[] o = new object[CSDL.Instance.DSSP.Columns.Count];
            o[0] = s.ID_SP;
            o[1] = s.Ten;
            o[2] = s.TrangThai;
            o[3] = s.NSX;
            o[4] = s.ID_MH;
            CSDL.Instance.DSSP.Rows[index].ItemArray = o;
        }
        public void DeleteSPInCSDL(int index)
        {
            CSDL.Instance.DSSP.Rows.Remove(CSDL.Instance.DSSP.Rows[index]);
        }
        public void Sort(List<SP> list, Compare cmp)
        {
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = i + 1; j < list.Count; j++)
                {
                    if (cmp(list[i], list[j]))
                    {
                        SP temp = list[i];
                        list[i] = list[j];
                        list[j] = temp;
                    }
                }
            }
        }
        public bool isExist(string s)
        {
            List<SP> temp = new List<SP>();
            foreach (DataRow dr in CSDL.Instance.DSSP.Rows)
            {
                if (dr["ID_SP"].ToString() == s) return true;
            }
            return false;
        }
    }
}
