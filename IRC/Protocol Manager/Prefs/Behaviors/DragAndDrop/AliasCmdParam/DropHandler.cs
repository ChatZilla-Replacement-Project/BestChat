using Avalonia.VisualTree;
using System.Linq;
using DynamicData;

namespace BestChat.IRC.ProtocolMgr.Prefs.Behaviors.DragAndDrop.AliasCmdParam;

public class DropHandler : Avalonia.Xaml.Interactions.DragAndDrop.DropHandlerBase
{
	private const string strDropTgtRow = @"DropTgtRow";
	private const string strDropTgt = @"DropTgt";

	public override bool Validate(object? objSender, Avalonia.Input.DragEventArgs args, object? objSrcCtxt, object?
		objTgtCtxt, object? objState)
	{
		if(args.Source is Avalonia.Controls.Control ctrl && objSender is Avalonia.Controls.DataGrid dgSrc)
		{
			if(objSrcCtxt is not Data.Prefs.GlobalAliasesOneAliasOneParam aparamSrcCtxt || dgSrc.GetVisualAt(args
					.GetPosition(dgSrc)) is not Avalonia.Controls.DataGrid
						{
							DataContext: Data.Prefs.GlobalAliasesOneAliasOneParam aparamTgtPutItHere,
						} dgTgt)
				return false;


			if(!dgTgt.Classes.Contains(strDropTgt))
				return false;

			if(aparamSrcCtxt.aliasParent is not Data.Prefs.GlobalAliasesOneAliasEditable ealiasCtxt)
				throw new System.InvalidProgramException(@"Somehow we have a parameter in a grid, but the parameter's parent " +
					@"isn't being edited.");

			int iIndexOfItemBelowMouse = ealiasCtxt.PositionalParameters.IndexOf(aparamTgtPutItHere);

			switch(aparamSrcCtxt.IsRequired)
			{
				// TODO: Check the items near where the item being dragged is.
				case true when iIndexOfItemBelowMouse > 0 && !ealiasCtxt.PositionalParameters.ElementAt(iIndexOfItemBelowMouse -
					1).IsRequired && !aparamTgtPutItHere.IsRequired:

				case false when iIndexOfItemBelowMouse < ealiasCtxt.PositionalParameters.Count - 1 && ealiasCtxt
						.PositionalParameters.ElementAt(iIndexOfItemBelowMouse - 1).IsRequired && aparamTgtPutItHere.IsRequired:
					return false;
			}

			bool bIsValid = RunDropAction(dgTgt, false, ealiasCtxt, aparamSrcCtxt,
				aparamTgtPutItHere);

			if(bIsValid)
			{
				Avalonia.Controls.DataGridRow? rowSrc = FindDataGridRowFromChildView(ctrl);
				if(rowSrc is not null)
					ApplyDraggingStyleToRow(rowSrc);
				ClearDraggingStyleFromAllRows(objSender, exceptThis: rowSrc);
			}

			return bIsValid;
		}

		ClearDraggingStyleFromAllRows(objSender);

		return false;
	}

	public override bool Execute(object? objSender, Avalonia.Input.DragEventArgs args, object? objSrcCtxt, object?
			objTgtCtxt, object? objState)
	{
		ClearDraggingStyleFromAllRows(objSender);

		return args.Source is Avalonia.Controls.Control && objSender is Avalonia.Controls.DataGrid dg && Validate(dg, args,
			objSrcCtxt, objTgtCtxt, true);
	}

	public override void Cancel(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
	{
		base.Cancel(sender, e);
		// this is necessary to clear adorner borders when mouse leaves DataGrid
		// they would remain even after changing screens
		ClearDraggingStyleFromAllRows(sender);
	}

	private static bool RunDropAction(Avalonia.Controls.DataGrid dgTgt, bool bExecute, Data.Prefs
		.GlobalAliasesOneAliasEditable ealiasCtxt, Data.Prefs.GlobalAliasesOneAliasOneParam aparamBeingDragged, Data.Prefs
		.GlobalAliasesOneAliasOneParam aparamAfter)
	{
		if(bExecute)
			return false;

		ealiasCtxt.AddPositionedParameterAfter(aparamBeingDragged, aparamAfter);
		dgTgt.SelectedItem = aparamBeingDragged;

		return true;
	}

	private static Avalonia.Controls.DataGridRow? FindDataGridRowFromChildView(Avalonia.StyledElement seSrcChild)
	{
		int iMaxDepth = 16;
		Avalonia.Controls.DataGridRow? row = null;
		Avalonia.StyledElement? seCur = seSrcChild;
		while(iMaxDepth-- > 0 && row is null)
		{
			// ReSharper disable once MergeCastWithTypeCheck
			if(seCur is Avalonia.Controls.DataGridRow)
				row = (Avalonia.Controls.DataGridRow)seCur;

			seCur = seCur?.Parent;
		}

		return row;
	}

	private static Avalonia.Controls.Primitives.DataGridRowsPresenter? GetRowsPresenter(Avalonia.Visual v)
	{
		foreach(Avalonia.Visual vCur in v.GetVisualChildren())
		{
			if(vCur is Avalonia.Controls.Primitives.DataGridRowsPresenter dgrp)
				return dgrp;

			if(GetRowsPresenter(vCur) is Avalonia.Controls.Primitives.DataGridRowsPresenter dgrp2)
				return dgrp2;
		}

		return null;
	}

	private static void ClearDraggingStyleFromAllRows(object? objSender, Avalonia.Controls.DataGridRow? exceptThis = null)
	{
		if(objSender is Avalonia.Controls.DataGrid dg)
		{
			Avalonia.Controls.Primitives.DataGridRowsPresenter? presenter = GetRowsPresenter(dg);
			if(presenter is null) return;

			foreach(Avalonia.Controls.Control ctrlToRemoveClassesFrom in
					from Avalonia.Controls.Control ctrlToTest in presenter.Children
						where ctrlToTest != exceptThis
						where ctrlToTest.Classes.Contains(strDropTgtRow)
						select ctrlToTest
					)
				ctrlToRemoveClassesFrom.Classes.Remove(strDropTgtRow);
		}
	}

	private static void ApplyDraggingStyleToRow(Avalonia.Controls.DataGridRow row)
		=> row.Classes.Add(strDropTgtRow);
}