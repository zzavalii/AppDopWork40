using AppDopWork40.Models;
using AppDopWork40.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace AppDopWork40
{
    public partial class MainWindow : Window
    {
        private readonly string PATH = $"{Environment.CurrentDirectory}\\todoDataList.json";
        private BindingList<TodoModel> _todoDataList;
        private FileIOService _fileIOService;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _fileIOService = new FileIOService(PATH);
            try
            {
                _todoDataList = _fileIOService.LoadData();

                var sortedList = _todoDataList.OrderBy(todo => todo.CreationDate).ToList();
                dgTodoList.ItemsSource = sortedList;

                foreach (var todoItem in sortedList)
                {
                    todoItem.PropertyChanged += TodoItem_PropertyChanged;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }
        }

        private void TodoItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Text")
            {
                var todoItem = (sender as TodoModel);
                todoItem.CreationDate = DateTime.Now;

                try
                {
                    _fileIOService.SaveData(_todoDataList);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Close();
                }
            }
        }

        private void dgTodoList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var todoItem = (e.AddedItems[0] as TodoModel);
            todoItem.CreationDate = DateTime.Now;
        }

        private void _todoDataList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemChanged)
            {
                var todoItem = e.NewIndex < _todoDataList.Count ? _todoDataList[e.NewIndex] : null;
                if (todoItem != null)
                {
                    todoItem.CreationDate = DateTime.Now;
                }

                try
                {
                    _fileIOService.SaveData(sender);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Close();
                }
            }
        }
    }
}
