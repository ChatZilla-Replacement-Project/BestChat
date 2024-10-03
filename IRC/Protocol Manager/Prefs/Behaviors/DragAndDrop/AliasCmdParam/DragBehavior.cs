namespace BestChat.IRC.ProtocolMgr.Prefs.Behaviors.DragAndDrop.AliasCmdParam;

public class DropBehavior : Avalonia.Xaml.Interactivity.Behavior
{
	public static readonly Avalonia.StyledProperty<object?> ContextProperty = Avalonia.AvaloniaProperty
		.Register<DropBehavior, object?>(nameof(Context));


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

}