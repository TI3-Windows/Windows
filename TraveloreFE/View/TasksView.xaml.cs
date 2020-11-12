using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TraveloreFE.ViewModel;
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
        private TaskViewModel taskViewModel;

        public TasksView()
        {
            this.InitializeComponent();
        }

        private async void btnAddTask_Click(object sender, RoutedEventArgs e)
        {
            if(Description.Text != null)
            {
                string description = Description.Text;
                var date = endDateDatePicker.Date;
                var dateTime = date.Value.DateTime;
                /* MessageDialog md = new MessageDialog($"Description {description}    |    Date {date}    |    DateTime {dateTime}","Variables");
                 await md.ShowAsync();*/
                taskViewModel.AddTask(description, dateTime);
            }
        }
    }
}
