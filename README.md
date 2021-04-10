# BlazorDesktopWPF Custom Title Bar

.NET 6 preview 3 introduced a new `BlazorWebView` control for WPF apps. Together with upcoming BlazorDesktop, it is likely to be the future of developing desktop applications using a mix of C# and web technology, with cross-platform capabilties. Read more about it [here](https://devblogs.microsoft.com/aspnet/asp-net-core-updates-in-net-6-preview-3/#blazorwebview-controls-for-wpf-windows-forms).

In this project WPF is simply used as a shell for the Blazor app. But, as a shell it is responsible for certain behaviour considered "native" to Windows. The default WPF title bar is the most obvious example; being isolated from the Blazor app means the title bar is typically hard to customise.

This project was an attempt to replicate a standard Windows title bar, but do it using HTML, inside the `BlazorWebView`, making it fully customisable.

This screenshot shows an example HTML title bar, rendered by Blazor:

![TitleBar Image](https://cdn.jam-es.com/img/blazordesktopwpf-customtitlebar/titlebar.png)

Where the code looks roughly like this, together with CSS, ready to customise:
```html
<div class="titlebar">
	<button class="titlebar-btn" @onclick="...">
		<Icon Name="Navigation"/>
	</button>
	<p class="window-title">My Web Application</p>
	<div class="hspace"></div>
	<button class="titlebar-btn" @onclick="...">
		<Icon Name="Settings"/>
	</button>
	<button class="titlebar-btn" @onclick="Minimize">
		<Icon Name="Minimize"/>
	</button>
	<button class="titlebar-btn" @onclick="Maximize">
		<Icon Name="Maximize"/>
		<Icon Name="Restore"/>
	</button>
	<button class="titlebar-cbtn" @onclick="Close">
		<Icon Name="Close"/>
	</button>
</div>
```

**Note:** At time of writing .NET 6 is in early preview. This repo won't be maintained, it might not work with anything after Preview 3.

## Test the Demo

If you are so desperate to try this, that you are willing to run a random `.exe`, go ahead and [download the demo app from the Releases Page](https://github.com/James231/BlazorDesktopWPF-CustomTitleBar/releases). And run the `BlazorWebView-CustomTitleBar.exe`.

It is self-contained, meaning it includes the .NET 6 runtime so nothing else is required.

## From Source

To use the source code make sure you do the following:
- Install [Visual Studio Preview](https://visualstudio.microsoft.com/vs/preview/) (version 16.10 Preview 1 or later)
- Install [.NET 6 SDK Preview 3](https://dotnet.microsoft.com/download/dotnet/6.0)
- Install [WebView2 Runtime](https://developer.microsoft.com/en-us/microsoft-edge/webview2/#download-section)
- Clone the repo and open the `.sln` in VS Preview. It should build and run 🤞


## Known Issues

There are issues with dragging the title bar to move the window around the screen. See section below.

All other title bar behavior works as you would expect including: minimize, maximize, close, resizing, window snapping using keyboard shortcuts, etc. The window (including borders) looks almost identical to a standard WPF application.

But, there are bugs relating to .NET 6 preview 3, which will likely be fixed in later releases:
- The webview runs in a separate process which may appear as an additional window on the taskbar.
- Stopping debugging in VS sometimes only closes the WebView, and not the whole application window.

### Window Dragging

WPF events do not work over WebViews. That means standard `MouseDrag` events on the titlebar cannot be used to drag the window around the screen.

Instead, mouse events on the title bar must come from JavaScript/DOM events from within the WebView itself. While the JS `onmousemove` event can be used, it is called to infrequently to get smooth dragging of the window.

The workaround being used is `onmousedown`, and `onmouseup` in JS on the title bar triggering a C# function to be called 60 times a second via Dispatcher. This function takes the place of the `MouseDrag` event handler, by moving the window (based on the mouse position).

This has several issues, with potential solutions mentioned below:
- Performance impact with the dispatcher calling this method 60 times a second, with window position changes each time. There is room for performance improvements to be made here.
- If the mouse moves quickly, there is a noticeable lag before the window position updates. In this time the mouse may have moved off the titlebar so `onmouseup` doesn't fire  and the window keeps moving after the dragging has stopped. Subscribing to OS level mouse events, rather than events within the window, may solve this problem.
- Some native functionality is lost, most notably window snapping when dragged to an edge of the screen. I'm not aware of how to fix this, as a completely different approach would be needed. Maybe if the WebView2 control is updated allowing certain mouse events to pass through to a real WPF titlebar underneath (currently not possible as `IsHitTestVisible` is not supported by WebView2).

Any ideas on fixing these would be greatly appreciated 😉

The dependency on [ModernWpf](https://github.com/Kinnara/ModernWpf) could also be removed, as it is overkill when the only use is to remove the default WPF title bar while preserving native functionality (like keyboard shortcut based window snapping) ...


## Dependencies

[BlazorWebView](https://devblogs.microsoft.com/aspnet/asp-net-core-updates-in-net-6-preview-3/#blazorwebview-controls-for-wpf-windows-forms) based on [WebView2](https://docs.microsoft.com/en-us/microsoft-edge/webview2/) as part of [.NET 6 Preview](https://dotnet.microsoft.com/download/dotnet/6.0).

[ModernWpf](https://github.com/Kinnara/ModernWpf) - While this is a complete UI framework, it is *only* used to removes the default WPF title bar. There are other methods of achieving the same thing, but ModernWpf was the easiest no-comprimises solution.

[FluentUI System Icons](https://github.com/microsoft/fluentui-system-icons) - Microsofts official FluentUI icons. SVGs used on the titlebar buttons: navigation, settings, minimize, maximize, restore, close

System.Windows.Forms is also used to obtain the mouse position.

## License

This code is released under MIT license. This means you can use this for whatever you want. Modify, distribute, sell, fork, and use this as much as you like. Both for personal and commercial use. I hold no responsibility if anything goes wrong.  
  
If you use this, you don't need to refer to this repo, or give me any kind of credit but it would be appreciated. At least a :star: would be nice.  

It took longer than you think to publish and document this tool for free. Perhaps you could consider buying me lunch?

[![paypal](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=MLD56V6HQWCKU&source=url)

## Contributing

Pull Requests are welcome. But, note that by creating a pull request you are giving me permission to merge your code and release it under the MIT license mentioned above. At no point will you be able to withdraw merged code from the repository, or change the license under which it has been made available.