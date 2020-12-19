using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using TraveloreFE.Model;
using TraveloreFE.ViewModel;
using TraveloreFE.ViewModel.Commands;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TraveloreFE.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TasksView : Page
    {

        private Task selectedTask;
        public Travellist Travellist { get; set; }
        public TaskViewModel tvm;

        public TasksView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Travellist = (Travellist)e.Parameter;
            DataContext = new TaskViewModel(Travellist);
            tvm = (TaskViewModel)DataContext;
        }

        private async void lvTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView lv = (ListView)sender;
            Task selectedTask = (Task)lvTasks.SelectedItem;
            if (selectedTask != null)
            {
                await tvm.UpdateSelectedTask(selectedTask.Id);
            }

        }

        private async void btnAddTask_Click(object sender, RoutedEventArgs e)
        {
            var description = Description.Text;
            if (Description.Text.Length != 0)
            {
                await tvm.AddNewTask(description);
                Description.Text = "";
            }
            else {
                MessageDialog md = new MessageDialog("Description can't be empty!");
                await md.ShowAsync();
            }
        }

        private void lvTasks_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            ListView listView = (ListView)sender;
            allContactsMenuFlyout.ShowAt(listView, e.GetPosition(listView));
            var a = ((FrameworkElement)e.OriginalSource).DataContext;
            selectedTask = (Task)a;
        }

        private async void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (selectedTask != null)
            {
                tvm.DeleteTaskCommand.Execute(selectedTask.Id);
            }
        }

        private async void Update_Click(object sender, RoutedEventArgs e)
        {
            if(selectedTask != null)
            {
                await tvm.UpdateSelectedTask(selectedTask.Id);
            }
        }

        private async void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (selectedTask != null)
            {
                MessageDialog md = new MessageDialog($"Selected : {selectedTask.Description}");
                await md.ShowAsync();
            }
        }
    }
}
