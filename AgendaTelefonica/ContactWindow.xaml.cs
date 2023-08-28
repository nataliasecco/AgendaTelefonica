using System.Windows;
using AgendaTelefonica.ViewModel;
using CommunityToolkit.Mvvm.Messaging;


namespace AgendaTelefonica;

public partial class ContactWindow: Window
{
    public ContactWindow()
    {
        InitializeComponent();

        WeakReferenceMessenger.Default.Register<CloseWindowMessage>(this, (r, m) => 
        {
            this.Hide();

        });

        DataContext = new ContactEditVM();  
    }
}
