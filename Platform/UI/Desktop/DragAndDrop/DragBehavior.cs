namespace BestChat.Platform.UI.Desktop.DragAndDrop;

public abstract class DragBehavior<ItemType>
	: Avalonia.Xaml.Interactivity.Behavior
	where ItemType : Platform.DataAndExt.Obj<ItemType>
{
	public static readonly Avalonia.StyledProperty<ItemType?> CtxtProperty = Avalonia.AvaloniaProperty.Register<DragBehavior<ItemType>, ItemType?>(nameof(Ctxt));

	public static readonly Avalonia.StyledProperty<Avalonia.Xaml.Interactions.DragAndDrop.IDragHandler?> HandlerProperty = Avalonia.AvaloniaProperty.Register<DragBehavior<ItemType>, Avalonia.Xaml.Interactions.DragAndDrop.IDragHandler?>(nameof(Handler));

	public static readonly Avalonia.StyledProperty<double> HorzDragThreshProperty = Avalonia.AvaloniaProperty.Register<DragBehavior<ItemType>, double>(nameof(HorzDragThresh), 3);

	public static readonly Avalonia.StyledProperty<double> VertDragThreshProperty = Avalonia.AvaloniaProperty.Register<DragBehavior<ItemType>, double>(nameof(VertDragThresh), 3);


	private Avalonia.Point ptDragStartLoc;
	private Avalonia.Input.PointerPressedEventArgs? evtargsTrigger;
	private bool @lock;
	private bool bCaptured;
	private static Avalonia.Input.DataFormat<ItemType>? fmt;


	public object? Ctxt
	{
		get
			=> GetValue(CtxtProperty);

		set
			=> SetValue(CtxtProperty, value);
	}

	public Avalonia.Xaml.Interactions.DragAndDrop.IDragHandler? Handler
	{
		get
			=> GetValue(HandlerProperty);

		set
			=> SetValue(HandlerProperty, value);
	}

	public double HorzDragThresh
	{
		get
			=> GetValue(HorzDragThreshProperty);

		set
			=> SetValue(HorzDragThreshProperty, value);
	}

	public double VertDragThresh
	{
		get
			=> GetValue(VertDragThreshProperty);

		set
			=> SetValue(VertDragThreshProperty, value);
	}

	private new Avalonia.Interactivity.Interactive? AssociatedObject
		=> (Avalonia.Interactivity.Interactive?)base.AssociatedObject;

	protected abstract string TypeName
	{
		get;
	}


	private void Released()
	{
		evtargsTrigger = null;
		@lock = false;
	}


	/// <inheritdoc />
	protected override void OnAttachedToVisualTree()
	{
		AssociatedObject?.AddHandler(Avalonia.Input.InputElement.PointerPressedEvent, OnMousePointerDragStart, Avalonia.Interactivity.RoutingStrategies.Direct | Avalonia.Interactivity.RoutingStrategies.Tunnel | Avalonia.Interactivity.RoutingStrategies.Bubble);
		AssociatedObject?.AddHandler(Avalonia.Input.InputElement.PointerReleasedEvent, OnMousePointerDragReleased, Avalonia.Interactivity.RoutingStrategies.Direct | Avalonia.Interactivity.RoutingStrategies.Tunnel | Avalonia.Interactivity.RoutingStrategies.Bubble);
		AssociatedObject?.AddHandler(Avalonia.Input.InputElement.PointerMovedEvent, OnMouseMovedDuringDrag, Avalonia.Interactivity.RoutingStrategies.Direct | Avalonia.Interactivity.RoutingStrategies.Tunnel | Avalonia.Interactivity.RoutingStrategies.Bubble);
		AssociatedObject?.AddHandler(Avalonia.Input.InputElement.PointerCaptureLostEvent, OnMouseCaptureLost, Avalonia.Interactivity.RoutingStrategies.Direct | Avalonia.Interactivity.RoutingStrategies.Tunnel | Avalonia.Interactivity.RoutingStrategies.Bubble);
	}

	/// <inheritdoc />
	protected override void OnDetachedFromVisualTree()
	{
		AssociatedObject?.RemoveHandler(Avalonia.Input.InputElement.PointerPressedEvent, OnMousePointerDragStart);
		AssociatedObject?.RemoveHandler(Avalonia.Input.InputElement.PointerReleasedEvent, OnMousePointerDragReleased);
		AssociatedObject?.RemoveHandler(Avalonia.Input.InputElement.PointerMovedEvent, OnMouseMovedDuringDrag);
		AssociatedObject?.RemoveHandler(Avalonia.Input.InputElement.PointerCaptureLostEvent, OnMouseCaptureLost);
	}

	private static async System.Threading.Tasks.Task DoDragDrop(Avalonia.Input.PointerPressedEventArgs evtargsTrigger, ItemType? val, string strTypeName)
	{
		Avalonia.Input.DataTransfer data = new();
		fmt ??= Avalonia.Input.DataFormat.CreateInProcessFormat<ItemType>(strTypeName);

		data.Add(Avalonia.Input.DataTransferItem.Create(fmt, val!));

		await Avalonia.Input.DragDrop.DoDragDropAsync(evtargsTrigger, data, Avalonia.Input.DragDropEffects.Link);
	}

	private void OnMousePointerDragStart(object? _, Avalonia.Input.PointerPressedEventArgs args)
	{
		Avalonia.Input.PointerPointProperties properties = args.GetCurrentPoint(AssociatedObject).Properties;

		if(!properties.IsLeftButtonPressed)
			return;

		if(args.Source is not Avalonia.Controls.Control ctrl ||
			(AssociatedObject is Avalonia.IDataContextProvider dobj
				? dobj.DataContext
				: null
			) != ctrl.DataContext)
			return;

		ptDragStartLoc = args.GetPosition(null);
		evtargsTrigger = args;
		@lock = true;
		bCaptured = true;
	}

	private void OnMousePointerDragReleased(object? objSender, Avalonia.Input.PointerReleasedEventArgs args)
	{
		if(!bCaptured)
			return;

		if(args.InitialPressMouseButton == Avalonia.Input.MouseButton.Left && evtargsTrigger is not null)
			Released();

		evtargsTrigger = null;

		bCaptured = false;
	}

	private void OnMouseMovedDuringDrag(object? objSender, Avalonia.Input.PointerEventArgs args)
	{
		Avalonia.Input.PointerPointProperties properties = args.GetCurrentPoint(AssociatedObject).Properties;

		if(!bCaptured || !properties.IsLeftButtonPressed || evtargsTrigger is null)
			return;

		Avalonia.Point point = args.GetPosition(null);
		Avalonia.Point diff = ptDragStartLoc - point;
		double dblHorzDragThreshold = HorzDragThresh;
		double dblVertDragThreshold = VertDragThresh;

		if(!(System.Math.Abs(diff.X) > dblHorzDragThreshold) && !(System.Math.Abs(diff.Y) > dblVertDragThreshold))
			return;

		if(@lock)
			@lock = false;
		else
			return;

		ItemType? ctxt = Ctxt is ItemType
			? AssociatedObject is Avalonia.IDataContextProvider dobj
				? dobj.DataContext is ItemType data
					? data
					: null
				: null
			: null;

		Handler?.BeforeDragDrop(objSender, evtargsTrigger, ctxt);

		DoDragDrop(evtargsTrigger, ctxt, TypeName).Wait();

		Handler?.AfterDragDrop(objSender, evtargsTrigger, ctxt);

		evtargsTrigger = null;
	}

	private void OnMouseCaptureLost(object? objSender, Avalonia.Input.PointerCaptureLostEventArgs args)
	{
		Released();

		bCaptured = false;
	}
}