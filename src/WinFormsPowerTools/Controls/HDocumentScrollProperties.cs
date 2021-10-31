// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#nullable enable

using Windows.Win32.UI.Controls;

namespace System.Windows.Forms.Documents
{
    /// <summary>
    ///  Basic Properties for VScroll.
    /// </summary>
    public class HDocumentScrollProperties : DocumentScrollProperties
    {
        public HDocumentScrollProperties(DocumentControl? container) : base(container)
        {
        }

        private protected override SCROLLBAR_CONSTANTS Orientation => SCROLLBAR_CONSTANTS.SB_HORZ;

        private protected override int GetHorizontalDisplayPosition(DocumentControl parent)
            => -_value;

        private protected override int GetPageSize(DocumentControl parent)
            => parent.ClientRectangle.Width;

        private protected override int GetVerticalDisplayPosition(DocumentControl parent)
            => parent.DisplayRectangle.Y;
    }
}
