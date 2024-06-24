using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp4
{
    internal class TLiquorDB
    {
        public string tliquors_Name {  get; set; }
        public string tliquors_Alc { get; set; }
        public string tliquors_Amt { get; set; }
        public string tliquors_MIngr { get; set; }
        public string tliquors_Co { get; set; }
        public string tliquors_Exp { get; set; }
        public byte[] tliquors_URL { get; set; }
        public string tliquors_Ctg { get; set; }
    }
}
