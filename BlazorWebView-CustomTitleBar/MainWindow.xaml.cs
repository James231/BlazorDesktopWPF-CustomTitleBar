// -------------------------------------------------------------------------------------------------
// BlazorDesktopWPF-CustomTitleBar - © Copyright 2021 - Jam-Es.com
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorWebView_CustomTitleBar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddBlazorWebView();
            Resources.Add("services", serviceCollection.BuildServiceProvider());

            InitializeComponent();

            BlazorWebView_CustomTitleBar.MouseMove.Init();
        }
    }
}
