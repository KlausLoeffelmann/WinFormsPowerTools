﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#nullable enable

using Windows.Win32.UI.WindowsAndMessaging;

namespace System.Windows.Forms.Documents
{
    /// <summary>
    ///  Basic Properties for VScroll.
    /// </summary>
    public class VDocumentScrollProperties : DocumentScrollProperties
    {
        internal VDocumentScrollProperties(IDocumentControl? container) : base(container)
        {
        }

        private protected override SCROLLBAR_CONSTANTS Orientation => SCROLLBAR_CONSTANTS.SB_VERT;

        private protected override int GetHorizontalDisplayPosition(IDocumentControl parent)
            => -_value;

        private protected override int GetPageSize(IDocumentControl parent)
            => parent.ClientRectangle.Height;

        private protected override int GetVerticalDisplayPosition(IDocumentControl parent)
            => parent.DisplayRectangle.X;
    }
}
