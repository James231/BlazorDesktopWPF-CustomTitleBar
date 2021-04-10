// -------------------------------------------------------------------------------------------------
// BlazorDesktopWPF-CustomTitleBar - © Copyright 2021 - Jam-Es.com
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Threading;

namespace BlazorWebView_CustomTitleBar
{
    public static class MouseMove
    {
        private static bool isMoving = false;

        private static double startMouseX = 0;
        private static double startMouseY = 0;
        private static double startWindLeft = 0;
        private static double startWindTop = 0;

        public static void Init()
        {
            DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(UpdateWindowPos);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 17);
            dispatcherTimer.Start();
        }

        public static void StartMove()
        {
            isMoving = true;
            startMouseX = GetX();
            startMouseY = GetY();
            startWindLeft = Application.Current.MainWindow.Left;
            startWindTop = Application.Current.MainWindow.Top;
        }

        public static void EndMove()
        {
            isMoving = false;
        }

        public static void UpdateWindowPos(object sender, EventArgs e)
        {
            if (isMoving)
            {
                double moveX = GetX() - startMouseX;
                double moveY = GetY() - startMouseY;
                Application.Current.MainWindow.Left = startWindLeft + moveX;
                Application.Current.MainWindow.Top = startWindTop + moveY;
            }
        }

        private static int GetX()
        {
            return System.Windows.Forms.Control.MousePosition.X;
        }

        private static int GetY()
        {
            return System.Windows.Forms.Control.MousePosition.Y;
        }
    }
}
