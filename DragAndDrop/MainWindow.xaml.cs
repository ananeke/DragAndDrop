using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DragAndDrop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListBox ibSender = sender as ListBox;
            ListBoxItem dragElement = ibSender.GetItemAt(e.GetPosition(ibSender));
            if(dragElement != null)
            {
                DataObject data = new DataObject();
                //wersja bez obsługi zawnętrznych appek
                //data.SetData("Format_Lista", ibSender);
                //data.SetData("Format_ElementListy", dragElement); 
                //wersja z unikalnym identyfikatorem - nie uniewrsalnym dla .NET
                //data.SetData("Format_EtykietaElementuListy", dragElement.Content as string);
                data.SetData(DataFormats.StringFormat, dragElement.Content as string);
                DragDrop.DoDragDrop(ibSender, data, DragDropEffects.Copy | DragDropEffects.Move);

                if (!Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
                    ibSender.Items.Remove(dragElement);
            }
        }
        private void ListBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.KeyStates.HasFlag(DragDropKeyStates.ControlKey))
                e.Effects = DragDropEffects.Copy;
            else e.Effects = DragDropEffects.Move;
        }
        private void ListBox_Drop(object sender, DragEventArgs e)
        {
            ListBox ibSender = sender as ListBox;

            //wersja bez obsługi zawnętrznych appek
            //ListBox ibSource = e.Data.GetData("Format_Lista") as ListBox;
            //ListBoxItem dragElement = e.Data.GetData("Format_ElementListy") as ListBoxItem;
            //if (!e.KeyStates.HasFlag(DragDropKeyStates.ControlKey))
            //    ibSource.Items.Remove(dragElement);
            //else dragElement = new ListBoxItem() { Content = dragElement.Content };

            //wersja z unikalnym identyfikatorem - nie uniewrsalnym dla .NET
            //string labelDragElement = e.Data.GetData("Format_EtykietaElementuListy") as string;
            string labelDragElement = e.Data.GetData(DataFormats.StringFormat) as string;
            ListBoxItem dragElement = new ListBoxItem() { Content = labelDragElement };

            int index = ibSender.IndexFromPoint(e.GetPosition(ibSender));
            if (index < 0)
                ibSender.Items.Add(dragElement);
            else ibSender.Items.Insert(index, dragElement);
 
        }
    }
}
