namespace TAAS.NetMAUI.Presentation;

public partial class ShellTitleView : ContentView
{


    public ShellTitleView()
	{
		InitializeComponent();

        MachineUserName = Environment.MachineName;

    }

    // 1. Define the BindableProperty
    public static readonly BindableProperty ViewTitleProperty =
        BindableProperty.Create(
            nameof( ViewTitle ),          // Property Name
            typeof( string ),             // Property Return Type
            typeof( ShellTitleView ), // Declaring Type
            defaultValue: string.Empty  // Default Value
        );

    // 2. Create the Property Wrapper
    public string ViewTitle {
        get => ( string )GetValue( ViewTitleProperty );
        set => SetValue( ViewTitleProperty, value );
    }

    public static readonly BindableProperty MachineUserNameProperty =
        BindableProperty.Create(
            nameof( MachineUserName ),
            typeof( string ),
            typeof( ShellTitleView ),
            defaultValue: string.Empty );

    public string MachineUserName {
        get => ( string )GetValue( MachineUserNameProperty );
        set => SetValue( MachineUserNameProperty, value );
    }

}