using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    internal static class SystemSleeps
    {
        /// <summary>
        /// 阻止系统睡眠/息屏
        /// </summary>
        public static EXECUTION_STATE PreventSleep()
        {
            return SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS | EXECUTION_STATE.ES_SYSTEM_REQUIRED | EXECUTION_STATE.ES_DISPLAY_REQUIRED);
        }

        /// <summary>
        /// 允许系统睡眠
        /// </summary>
        public static EXECUTION_STATE AllowSleep()
        {
            return SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);
        /// <summary>
        /// 使应用程序能够通知系统它正在使用中，从而防止系统在应用程序运行时进入睡眠状态或关闭显示器。
        /// 通知应用之后的状态
        /// </summary>
        [FlagsAttribute]
        public enum EXECUTION_STATE : uint
        {
            /// <summary>
            /// 通知系统正在设置的状态应保持有效，直到使用 ES_CONTINUOUS 的下一次调用和清除其他状态标志之一。
            /// </summary>
            ES_CONTINUOUS = 0x80000000,
            /// <summary>
            /// 防止显示器关闭
            /// </summary>
            ES_DISPLAY_REQUIRED = 0x00000002,
            /// <summary>
            /// 防止系统进入睡眠
            /// </summary>
            ES_SYSTEM_REQUIRED = 0x00000001
        }
    }

}
