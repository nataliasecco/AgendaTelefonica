using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AgendaTelefonica
{
    public class Contato : ObservableObject, ICloneable
    {
        public int Id { get; set; }
        public string _nome;
        public string _email;
        public string _telefone;
        public string _tipoContato;

        public string Nome 
        {
            get { return _nome;}
            set { SetProperty(ref _nome, value);}
        }

        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }


        public string Telefone
        {
            get { return _telefone; }
            set { SetProperty(ref _telefone, value); }
        }

        public string TipoContato
        {
            get { return _tipoContato; }
            set { SetProperty(ref _tipoContato, value); }
        }

        public object Clone ()
        {
            return this.MemberwiseClone();
        }   
    }
}
