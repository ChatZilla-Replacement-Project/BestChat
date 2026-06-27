using Avalonia.VisualTree;
using System.Linq;
using DynamicData;

namespace BestChat.IRC.ProtocolMgr.Prefs.Behaviors.DragAndDrop.AliasCmdParam;

public class DropHandler : Platform.UI.Desktop.DragAndDrop.DropHandler<Data.Prefs.GlobalAliasesOneAliasOneParam>
{
	protected override bool ValidateInternal(in BestChat.IRC.Data.Prefs.GlobalAliasesOneAliasOneParam aparamSrcCtxt, Avalonia.Controls.DataGrid dgTgt, in bool bExecute, in BestChat.IRC.Data.Prefs.GlobalAliasesOneAliasOneParam aparamTgtPutItHere)
	{
			if(aparamSrcCtxt.aliasParent is not Data.Prefs.GlobalAliasesOneAliasEditable ealiasCtxt)
				throw new System.InvalidProgramException(@"Somehow we have a parameter in a grid, but the parameter's parent isn't being edited.");

			int iIndexOfItemBelowMouse = ealiasCtxt.PositionalParameters.IndexOf(aparamTgtPutItHere);

			switch(aparamSrcCtxt.IsRequired)
			{
				// TODO: Check the items near where the item being dragged is.
				case true when iIndexOfItemBelowMouse > 0 && !ealiasCtxt.PositionalParameters.ElementAt(iIndexOfItemBelowMouse - 1).IsRequired && !aparamTgtPutItHere.IsRequired:

				case false when iIndexOfItemBelowMouse < ealiasCtxt.PositionalParameters.Count - 1 && ealiasCtxt.PositionalParameters.ElementAt(iIndexOfItemBelowMouse - 1).IsRequired && aparamTgtPutItHere.IsRequired:
					return false;
			}

		return RunDropAction(dgTgt, false, ealiasCtxt, aparamSrcCtxt, aparamTgtPutItHere);
	}

	private static bool RunDropAction(Avalonia.Controls.DataGrid dgTgt, bool bExecute, Data.Prefs.GlobalAliasesOneAliasEditable ealiasCtxt, Data.Prefs.GlobalAliasesOneAliasOneParam aparamBeingDragged, Data.Prefs.GlobalAliasesOneAliasOneParam aparamAfter)
	{
		if(bExecute)
			return false;

		ealiasCtxt.AddPositionedParameterAfter(aparamBeingDragged, aparamAfter);
		dgTgt.SelectedItem = aparamBeingDragged;

		return true;
	}
}