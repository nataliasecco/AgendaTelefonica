using AgendaTelefonica.ViewModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System.Collections.ObjectModel;
using System.Data;
using System;

namespace AgendaTelefonica.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public ObservableCollection<Contato> ContactList { get; set; }
        public Contato _selectedContact;

        public IRelayCommand InsertCommand { get; set; }
        public IRelayCommand UpdateCommand { get; set; }
        public IRelayCommand DeleteCommand { get; set; }
        
        public MainViewModel()
        {
            DALAgenda.CriarBancoSQLite();
            DALAgenda.CriarTabelaSQLite();
            InsertCommand = new RelayCommand(ExecuteInsert);
            UpdateCommand = new RelayCommand(ExecuteUpdate, CanExecuteUpdate);
            DeleteCommand = new RelayCommand(ExecuteDelete, CanExecuteDelete);
            ContactList = new ObservableCollection<Contato>();
            PrepareContactList();  
        }

        public Contato SelectedContact
        {
            get => _selectedContact;
            set
            {
                SetProperty(ref _selectedContact, value);
                DeleteCommand.NotifyCanExecuteChanged();
                UpdateCommand.NotifyCanExecuteChanged();
            }
        }

        private void ExecuteInsert()
        {
            var contatoViewModel = new ContactEditVM();
            WeakReferenceMessenger.Default.Send(new OpenWindowMessage(contatoViewModel));
            if (contatoViewModel.contato != null )
            {
                DALAgenda.Adicionar(contatoViewModel.contato);
                this.ContactList.Add(contatoViewModel.contato); // Adicione à lista local após adicionar ao banco de dados
                this.SelectedContact = contatoViewModel.contato;
            } 
             // Limpe a seleção atual criando um novo objeto Contato vazio
        }

        private void ExecuteUpdate()
        {
            
            var contatoViewModel = new ContactEditVM ();
            var cloneContato = (Contato)this.SelectedContact.Clone(); 

            contatoViewModel.contato = cloneContato;
            WeakReferenceMessenger.Default.Send(new OpenWindowMessage(contatoViewModel));
            if (contatoViewModel.contato != null ) 
            {
                this.SelectedContact.Nome = cloneContato.Nome;
                this.SelectedContact.Email = cloneContato.Email;
                this.SelectedContact.Telefone = cloneContato.Telefone;
                this.SelectedContact.TipoContato = cloneContato.TipoContato;
                DALAgenda.Alterar(this.SelectedContact); 
            }
           
        }

        private bool CanExecuteUpdate()
        {
            return SelectedContact != null;
        }

        private void ExecuteDelete()
        {
            
            DALAgenda.Deletar(SelectedContact.Id);
            ContactList.Remove(SelectedContact);
            
            if ( ContactList.Count > 0 )
                SelectedContact = ContactList[0];
            else 
                SelectedContact = null; // Após a exclusão, limpe a seleção atual
        }

         private bool CanExecuteDelete()
        {
            return SelectedContact != null;
        }

        private void PrepareContactList ()
        {
            DataTable dt = new DataTable ();
            dt = DALAgenda.GetContatos();

            foreach (DataRow row in dt.Rows )
            {
                Contato contact = new Contato
                {
                    Id = Convert.ToInt32(row["id"]),
                    Nome = row["nome"].ToString(),
                    Email = row["email"].ToString(),
                    Telefone = row["telefone"].ToString(),
                    TipoContato = row["tipoContato"].ToString()
                };

                ContactList.Add(contact); 
            }
        }
    }

    public class OpenWindowMessage : ValueChangedMessage<ContactEditVM>
    {
        public OpenWindowMessage(ContactEditVM contatoViewModel) : base (contatoViewModel) {}
    }
}
