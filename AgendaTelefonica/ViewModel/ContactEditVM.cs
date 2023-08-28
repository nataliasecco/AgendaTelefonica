using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;


namespace AgendaTelefonica.ViewModel;

public class ContactEditVM : ObservableObject 
{
    public Contato contato {get; set; }

    public RelayCommand OK {get; set; }
    public RelayCommand Cancelar {get; set; }

    private void OkCommand ()
    {
        bool comando = true; 
        WeakReferenceMessenger.Default.Send(new CloseWindowMessage(comando));
    } 

    private void CancelarCommand ()
    {
        bool comando = false; 
        WeakReferenceMessenger.Default.Send(new CloseWindowMessage(comando));

        contato = null;
    }

    public ContactEditVM ()
    {
        OK = new RelayCommand(OkCommand);
        Cancelar = new RelayCommand(CancelarCommand);
        contato = new Contato(); 
    }
}

public class CloseWindowMessage : ValueChangedMessage<bool>
{
    public CloseWindowMessage(bool value) : base(value) {}

}
