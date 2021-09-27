using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Gdara
{
    class Window
    {
        const int SW_HIDE = 0;              //ウィンドウを非表示にし、他のウィンドウをアクティブにします。
        const int SW_SHOWNORMAL = 1;        //ウィンドウをアクティブにして表示します。ウィンドウが最小化または最大化されていた場合は、その位置とサイズを元に戻します。
        const int SW_SHOWMINIMIZED = 2;     //ウィンドウをアクティブにして、最小化します。
        const int SW_SHOWMAXIMIZED = 3;     //ウィンドウをアクティブにして、最大化します。
        const int SW_MAXIMIZE = 3;          //ウィンドウを最大化します。
        const int SW_SHOWNOACTIVATE = 4;    //ウィンドウを直前の位置とサイズで表示します。
        const int SW_SHOW = 5;              //ウィンドウをアクティブにして、現在の位置とサイズで表示します。
        const int SW_MINIMIZE = 6;          //ウィンドウを最小化し、Z オーダーが次のトップレベルウィンドウをアクティブにします。
        const int SW_SHOWMINNOACTIVE = 7;   //ウィンドウを最小化します。(アクティブにはしない)
        const int SW_SHOWNA = 8;            //ウィンドウを現在のサイズと位置で表示します。(アクティブにはしない)
        const int SW_RESTORE = 9;           //ウィンドウをアクティブにして表示します。最小化または最大化されていたウィンドウは、元の位置とサイズに戻ります。
        const int SW_SHOWDEFAULT = 10;      //アプリケーションを起動したプログラムが 関数に渡した 構造体で指定された SW_ フラグに従って表示状態を設定します。
        const int SW_FORCEMINIMIZE = 11;    //たとえウィンドウを所有するスレッドがハングしていても、ウィンドウを最小化します。このフラグは、ほかのスレッドのウィンドウを最小化する場合にだけ使用してください。

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetWindow(IntPtr hwnd, GetWindowType uCmd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int GetWindowText(IntPtr hWnd, string lpString, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int GetWindowTextLength(IntPtr hwnd);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        public enum GetWindowType : uint
        {
            First = 0,
            Last = 1,
            Next = 2,
            Prev = 3,
            Owner = 4,
            Child = 5,
            EnabledPopup = 6
        }

        public static IntPtr ActivateSecondWindow()
        {
            IntPtr hWnd = GetForegroundWindow();
            int length = GetWindowTextLength(hWnd);
            string title = new string('\0', length + 1);
            int count = GetWindowText(hWnd, title, title.Length);
            Debug.WriteLine(title.Trim('\0'));

            while (true)
            {
                hWnd = GetWindow(hWnd, GetWindowType.Next);
                length = GetWindowTextLength(hWnd);
                title = new string('\0', length + 1);
                count = GetWindowText(hWnd, title, title.Length);

                if (title.Trim('\0') == "MSCTFIME UI" || title.Trim('\0') == "Default IME" || title.Trim('\0') == "")
                {
                    continue;
                }

                Debug.WriteLine(title.Trim('\0'));
                SetForegroundWindow(hWnd);
                break;
            }

            return hWnd;
        }
    }
}
