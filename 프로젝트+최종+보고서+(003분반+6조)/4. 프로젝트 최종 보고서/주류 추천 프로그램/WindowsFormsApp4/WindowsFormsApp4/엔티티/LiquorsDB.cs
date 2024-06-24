using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp4
{
    internal class LiquorsDB
    {
        public string liquors_Name { get; set; }
        public string liquors_Alc { get; set; }
        public string liquors_Co { get; set; }
        public byte[] liquors_URL { get; set; }
        public string liquors_Ctg { get; set; }
    }
}
