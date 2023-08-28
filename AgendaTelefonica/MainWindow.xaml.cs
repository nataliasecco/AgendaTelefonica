using System;
using System.Data;
using System.Windows;
using AgendaTelefonica.ViewModel;
using AgendaTelefonica.ViewModels;
using CommunityToolkit.Mvvm.Messaging;

namespace AgendaTelefonica
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
           InitializeComponent();


            WeakReferenceMessenger.Default.Register<OpenWindowMessage>(this, (r,m)=> 
            {
                var fw = new ContactWindow();

                fw.DataContext = m.Value;
                fw.ShowDialog(); 

            });  

            DataContext = new MainViewModel();
        }  
    }
}
