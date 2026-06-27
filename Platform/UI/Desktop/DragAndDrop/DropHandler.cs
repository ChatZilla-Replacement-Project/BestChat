using System.Linq;
using Avalonia.VisualTree;

namespace BestChat.Platform.UI.Desktop.DragAndDrop;

public abstract class DropHandler<ItemType> : Avalonia.Xaml.Interactions.DragAndDrop.DropHandlerBase
	where ItemType : Platform.DataAndExt.Obj<ItemType>
{
	public static class XamlClassNames
	{
		public const string strDropTgtRow = @"DropTgtRow";
		public const string strDropTgt = @"DropTgt";
	}

	public override bool Validate(object? objSender, Avalonia.Input.DragEventArgs args, object? objSrcCtxt, object? objTgtCtxt, object? objState)
	{
		if(args.Source is Avalonia.Controls.Control ctrl && objSender is Avalonia.Controls.DataGrid dgSrc)
		{
			if(objSrcCtxt is not ItemType ctxt || dgSrc.GetVisualAt(args.GetPosition(dgSrc)) is not Avalonia.Controls.DataGrid
				{
					DataContext: ItemType ctxtPutItHere,
				} dgTgt)
				return false;


			if(!dgTgt.Classes.Contains(XamlClassNames.strDropTgt))
				return false;

			bool bIsValid = ValidateInternal(ctxt, dgTgt, false, ctxtPutItHere);

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

	protected abstract bool ValidateInternal(in ItemType ctxt, Avalonia.Controls.DataGrid dgTgt, in bool bExecute, in ItemType ctxtPutItHere);

	public override bool Execute(object? objSender, Avalonia.Input.DragEventArgs args, object? objSrcCtxt, object?
		objTgtCtxt, object? objState)
	{
		ClearDraggingStyleFromAllRows(objSender);

		return args.Source is Avalonia.Controls.Control && objSender is Avalonia.Controls.DataGrid dg && Validate(dg, args, objSrcCtxt, objTgtCtxt, true);
	}

	public override void Cancel(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
	{
		base.Cancel(sender, e);

		// this is necessary to clear adorner borders when mouse leaves DataGrid
		// they would remain even after changing screens
		ClearDraggingStyleFromAllRows(sender);
	}

	protected static Avalonia.Controls.DataGridRow? FindDataGridRowFromChildView(Avalonia.StyledElement seSrcChild)
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

	protected static Avalonia.Controls.Primitives.DataGridRowsPresenter? GetRowsPresenter(Avalonia.Visual v)
	{
		foreach(Avalonia.Visual vCur in v.GetVisualChildren())
		{
			if(vCur is Avalonia.Controls.Primitives.DataGridRowsPresenter dgrp)
				return dgrp;

			if(GetRowsPresenter(vCur) is { } dgrp2)
				return dgrp2;
		}

		return null;
	}

	protected static void ClearDraggingStyleFromAllRows(object? objSender, Avalonia.Controls.DataGridRow? exceptThis = null)
	{
		if(objSender is Avalonia.Controls.DataGrid dg)
		{
			Avalonia.Controls.Primitives.DataGridRowsPresenter? presenter = GetRowsPresenter(dg);
			if(presenter is null) return;

			foreach(Avalonia.Controls.Control ctrlToRemoveClassesFrom in
				from Avalonia.Controls.Control ctrlToTest in presenter.Children
				where ctrlToTest != exceptThis
				where ctrlToTest.Classes.Contains(XamlClassNames.strDropTgtRow)
				select ctrlToTest
			)
				ctrlToRemoveClassesFrom.Classes.Remove(XamlClassNames.strDropTgtRow);
		}
	}

	protected static void ApplyDraggingStyleToRow(Avalonia.Controls.DataGridRow row)
		=> row.Classes.Add(XamlClassNames.strDropTgtRow);
}