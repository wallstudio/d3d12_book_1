using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// DX12Control.xaml の相互作用ロジック
    /// </summary>
    public partial class DX12Control : UserControl
    {
        [DllImport("02_SimpleTriangle.dll")]
        public static extern IntPtr Init(int hwnd);
        [DllImport("02_SimpleTriangle.dll")]
        public static extern void Render(IntPtr app);
        [DllImport("02_SimpleTriangle.dll")]
        public static extern void Terminate(IntPtr app);


        IntPtr m_app;
        public DX12Control()
        {
            //m_app = Init(Handle);
            InitializeComponent();
        }
    }
}
