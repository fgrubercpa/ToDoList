using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ToDoList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TodoListViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _viewModel = new TodoListViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.AddItem(NewItemDescription.Text);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var item = (ToDoListItem)ListOfToDoItems.SelectedItem;
            _viewModel.DeleteItem(item);
        }
    }
    
}
