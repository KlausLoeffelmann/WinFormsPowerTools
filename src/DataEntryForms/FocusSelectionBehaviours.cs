namespace System.Windows.Forms.DataEntryForms
{
    /// <summary>
    /// Defines the behavior of the Cursor when this DataEntry receives focus.	
    /// /// </summary>
    public enum FocusSelectionBehaviours
	{
		/// <summary>
		/// Sets the Cursor at the beginning of the entry text.
		/// </summary>
		/// <remarks></remarks>
		PlaceCarentUpFront,

		/// <summary>
		/// Selects the whole text, and places the Cursor at the end of the Text.
		/// </summary>
		/// <remarks>Dies ist die Standardeinstellung.</remarks>
		PreSelectInput,

		/// <summary>
		/// Places the Cursor at the end of the Text without selecting the text.
		/// </summary>
		/// <remarks></remarks>
		PlaceCaretAtEnd
	}
}
