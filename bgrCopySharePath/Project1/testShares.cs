using System;
using System.Windows.Forms;


namespace bgrCopySharePath
{
    class SharesTest
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            bgrCSP prog = new bgrCSP(args);
        }
    }
}