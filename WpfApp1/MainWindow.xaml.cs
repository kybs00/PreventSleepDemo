using System.Runtime.InteropServices;
using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PreventSleepButton_OnClick(object sender, RoutedEventArgs e)
        {
            SystemSleeps.PreventSleep();
        }

        private void AllowSleepButton_OnClick(object sender, RoutedEventArgs e)
        {
            SystemSleeps.AllowSleep();
        }


        private enum SystemInformationClass
        {
            SystemExecutionState = 16
        };

        [DllImport("Powrprof.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern uint CallNtPowerInformation(
            SystemInformationClass information,
            IntPtr inputBuffer,
            uint inputBufferLength,
            IntPtr outputBuffer,
            uint outputBufferLength
        );

        private void GetDetailButton_OnClick(object sender, RoutedEventArgs e)
        {
            var powerState = GetSystemExecutionState();
            MessageBox.Show(powerState.ToString());
        }
        /// <summary>
        /// 获取系统执行状态
        /// 0表示无，可正常睡眠、息屏
        /// 1表示阻止睡眠中，系统无法睡眠
        /// 2表示阻止息屏中，系统无法息屏
        /// 3表示阻止睡眠以及息屏中，系统无法睡眠、息屏
        /// </summary>
        /// <returns>返回系统执行状态，如果查询失败，返回<see cref="EXECUTION_STATE.ES_CONTINUOUS"/></returns>
        public static EXECUTION_STATE GetSystemExecutionState()
        {
            IntPtr ptr = Marshal.AllocHGlobal(sizeof(uint));
            if (CallNtPowerInformation(SystemInformationClass.SystemExecutionState,
                    IntPtr.Zero, 0, ptr, sizeof(uint)) == 0)
            {
                var objectState = Marshal.PtrToStructure(ptr, typeof(uint));
                var stringState = objectState?.ToString();
                if (stringState != "0" && Enum.TryParse(stringState, out EXECUTION_STATE state))
                {
                    return state;
                }
            }
            return EXECUTION_STATE.ES_CONTINUOUS;
        }
    }
}