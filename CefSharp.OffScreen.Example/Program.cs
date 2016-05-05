// Copyright © 2010-2015 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using CefSharp.OffScreen;
using System.Text;

namespace CefSharp.MinimalExample.OffScreen
{
    public class Program
    {
        private static ChromiumWebBrowser browser;

        public static void Main(string[] args)
        {
            const string testUrl = "http://rongbay.com/TP-HCM/Mua-Ban-nha-dat-c15.html";

            Console.WriteLine("This example application will load {0}, take a screenshot, and save it to your desktop.", testUrl);
            Console.WriteLine("You may see Chromium debugging output, please wait...");
            Console.WriteLine();

            //Perform dependency check to make sure all relevant resources are in our output directory.
            Cef.Initialize(new CefSettings(), shutdownOnProcessExit: true, performDependencyCheck: true);

            // Create the offscreen Chromium browser.
            browser = new ChromiumWebBrowser(testUrl);
            //browser.GetMainFrame().Cop
            // An event that is fired when the first page is finished loading.
            // This returns to us from another thread.
            browser.LoadingStateChanged += BrowserLoadingStateChanged;
            browser.BrowserInitialized += BrowserInited;
            // We have to wait for something, otherwise the process will exit too soon.
            Console.ReadKey();

            // Clean up Chromium objects.  You need to call this in your application otherwise
            // you will get a crash when closing.
            Cef.Shutdown();
        }

        private static void BrowserInited(object sender, IsBrowserInitializedChangedEventArgs e)
        {
            if (e.IsBrowserInitialized)
            {
                StringBuilder js = new StringBuilder();
                js.AppendLine("function getLinks() {");
                js.AppendLine("     var links = $('.link_direct');");
                js.AppendLine("     var arrUrls = [];");
                js.AppendLine("     $(links).each(function() {");
                js.AppendLine("     //arrUrls[] = $(this).attr('href');");
                js.AppendLine("     alert($(this).attr('href'));");
                js.AppendLine("}");
                js.AppendLine("getLinks();");

                var scriptTask = browser.EvaluateScriptAsync(js.ToString());
                scriptTask.ContinueWith(t =>
                {
                    //Give the browser a little time to render
                    Thread.Sleep(500);
                    // Wait for the screenshot to be taken.
                    var task = browser.GetSourceAsync();
                    task.ContinueWith(x =>
                    {
                        // Make a file to save it to (e.g. C:\Users\jan\Desktop\CefSharp screenshot.png)
                        //var screenshotPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "CefSharp screenshot.png");

                        //Console.WriteLine();
                        //Console.WriteLine("Screenshot ready. Saving to {0}", screenshotPath);

                        // Save the Bitmap to the path.
                        // The image type is auto-detected via the ".png" extension.
                        if (!x.IsFaulted)
                        {
                            var link = task.Result;
                            Console.WriteLine();
                            Console.WriteLine("Link:{0}", link);
                            // We no longer need the Bitmap.
                            // Dispose it to avoid keeping the memory alive.  Especially important in 32-bit applications.
                            task.Dispose();
                        }
                        Console.WriteLine("Links loaded...");
                        Console.WriteLine("Image viewer launched.  Press any key to exit.");
                    });
                });
            }
        }

        private static void BrowserLoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            // Check to see if loading is complete - this event is called twice, one when loading starts
            // second time when it's finished
            // (rather than an iframe within the main frame).
            if (!e.IsLoading)
            {
                // Remove the load event handler, because we only want one snapshot of the initial page.
                //browser.LoadingStateChanged -= BrowserLoadingStateChanged;


            }
        }
    }
}