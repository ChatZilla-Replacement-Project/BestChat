namespace BestChat.IRC.ProtocolMgr.Prefs.Behaviors.DragAndDrop.AliasCmdParam;

public class DragBehavior : Avalonia.Xaml.Interactivity.Behavior
{
	public static readonly Avalonia.StyledProperty<object?> ContextProperty = Avalonia.AvaloniaProperty
		.Register<DragBehavior, object?>(nameof(Context));

	public static readonly Avalonia.StyledProperty<Avalonia.Xaml.Interactions.DragAndDrop.IDragHandler?> HandlerProperty =
		Avalonia.AvaloniaProperty.Register<DragBehavior, Avalonia.Xaml.Interactions.DragAndDrop
		.IDragHandler?>(nameof(Handler));

	public static readonly Avalonia.StyledProperty<double> HorizontalDragThresholdProperty = Avalonia.AvaloniaProperty
		.Register<DragBehavior, double>(nameof(HorizontalDragThreshold), 3);

	public static readonly Avalonia.StyledProperty<double> VerticalDragThresholdProperty = Avalonia.AvaloniaProperty
		.Register<DragBehavior, double>(nameof(VerticalDragThreshold), 3);


	private Avalonia.Point ptDragStartLoc;
	private Avalonia.Input.PointerEventArgs? eventThatTriggeredDrag;
	private bool @lock;
	private bool bCaptured;


	public object? Context
	{
		get => GetValue(ContextProperty);

		set => SetValue(ContextProperty, value);
	}

	public Avalonia.Xaml.Interactions.DragAndDrop.IDragHandler? Handler
	{
		get => GetValue(HandlerProperty);

		set => SetValue(HandlerProperty, value);
	}

	public double HorizontalDragThreshold
	{
		get => GetValue(HorizontalDragThresholdProperty);
		set => SetValue(HorizontalDragThresholdProperty, value);
	}

	public double VerticalDragThreshold
	{
		get => GetValue(VerticalDragThresholdProperty);
		set => SetValue(VerticalDragThresholdProperty, value);
	}

	private new Avalonia.Interactivity.Interactive? AssociatedObject
		=> (Avalonia.Interactivity.Interactive?)base.AssociatedObject;


	private void Released()
	{
		eventThatTriggeredDrag = null;
		@lock = false;
	}


	/// <inheritdoc />
	protected override void OnAttachedToVisualTree()
	{
		AssociatedObject?.AddHandler(Avalonia.Input.InputElement.PointerPressedEvent, OnMousePointerDragStart,
			Avalonia.Interactivity.RoutingStrategies.Direct | Avalonia.Interactivity.RoutingStrategies.Tunnel |
			Avalonia.Interactivity.RoutingStrategies.Bubble);
		AssociatedObject?.AddHandler(Avalonia.Input.InputElement.PointerReleasedEvent, OnMousePointerDragReleased,
			Avalonia.Interactivity.RoutingStrategies.Direct | Avalonia.Interactivity.RoutingStrategies.Tunnel |
			Avalonia.Interactivity.RoutingStrategies.Bubble);
		AssociatedObject?.AddHandler(Avalonia.Input.InputElement.PointerMovedEvent, OnMouseMovedDuringDrag, Avalonia
			.Interactivity.RoutingStrategies.Direct | Avalonia.Interactivity.RoutingStrategies.Tunnel | Avalonia.Interactivity
			.RoutingStrategies.Bubble);
		AssociatedObject?.AddHandler(Avalonia.Input.InputElement.PointerCaptureLostEvent, OnMouseCaptureLost,
			Avalonia.Interactivity.RoutingStrategies.Direct | Avalonia.Interactivity.RoutingStrategies.Tunnel |
			Avalonia.Interactivity.RoutingStrategies.Bubble);
	}

	/// <inheritdoc />
	protected override void OnDetachedFromVisualTree()
	{
		AssociatedObject?.RemoveHandler(Avalonia.Input.InputElement.PointerPressedEvent, OnMousePointerDragStart);
		AssociatedObject?.RemoveHandler(Avalonia.Input.InputElement.PointerReleasedEvent, OnMousePointerDragReleased);
		AssociatedObject?.RemoveHandler(Avalonia.Input.InputElement.PointerMovedEvent, OnMouseMovedDuringDrag);
		AssociatedObject?.RemoveHandler(Avalonia.Input.InputElement.PointerCaptureLostEvent, OnMouseCaptureLost);
	}

	private static async System.Threading.Tasks.Task DoDragDrop(Avalonia.Input.PointerEventArgs triggerEvent, object?
		objVal)
	{
		Avalonia.Input.DataObject data = new();
		data.Set(Avalonia.Xaml.Interactions.DragAndDrop.ContextDropBehavior.DataFormat, objVal!);

		await Avalonia.Input.DragDrop.DoDragDrop(triggerEvent, data, Avalonia.Input.DragDropEffects.Link);
	}

	private void OnMousePointerDragStart(object? _, Avalonia.Input.PointerPressedEventArgs args)
	{
		Avalonia.Input.PointerPointProperties properties = args.GetCurrentPoint(AssociatedObject).Properties;

		if(properties.IsLeftButtonPressed)
		{
			if(args.Source is Avalonia.Controls.Control ctrl &&
				(AssociatedObject is Avalonia.IDataContextProvider dobj
					? dobj.DataContext
					: null
				) == ctrl.DataContext)
			{
				ptDragStartLoc = args.GetPosition(null);
				eventThatTriggeredDrag = args;
				@lock = true;
				bCaptured = true;
			}
		}
	}

	private void OnMousePointerDragReleased(object? objSender, Avalonia.Input.PointerReleasedEventArgs args)
	{
		if(bCaptured)
		{
			if(args.InitialPressMouseButton == Avalonia.Input.MouseButton.Left && eventThatTriggeredDrag is not null)
				Released();

			bCaptured = false;
		}
	}

	private void OnMouseMovedDuringDrag(object? objSender, Avalonia.Input.PointerEventArgs args)
	{
		Avalonia.Input.PointerPointProperties properties = args.GetCurrentPoint(AssociatedObject as Avalonia.Visual)
			.Properties;

		if(bCaptured && properties.IsLeftButtonPressed && eventThatTriggeredDrag is not null)
		{
			Avalonia.Point point = args.GetPosition(null);
			Avalonia.Point diff = ptDragStartLoc - point;
			double dblHorzDragThreshold = HorizontalDragThreshold;
			double dblVertDragThreshold = VerticalDragThreshold;

			if(System.Math.Abs(diff.X) > dblHorzDragThreshold || System.Math.Abs(diff.Y) > dblVertDragThreshold)
			{
				if(@lock)
					@lock = false;
				else
					return;

				object? context = Context ?? (AssociatedObject is Avalonia.IDataContextProvider dobj
					? dobj.DataContext
					: null);

				Handler?.BeforeDragDrop(objSender, eventThatTriggeredDrag, context);

				DoDragDrop(eventThatTriggeredDrag, context).Wait();

				Handler?.AfterDragDrop(objSender, eventThatTriggeredDrag, context);

				eventThatTriggeredDrag = null;
			}
		}
	}

	private void OnMouseCaptureLost(object? objSender, Avalonia.Input.PointerCaptureLostEventArgs args)
	{
		Released();

		bCaptured = false;
	}
}