/*
 * FILE:        MainWindow.xaml.cs
 * Project:	    A02 – WPF APPLICATION
 * Author:	    Hoang Phuc Tran
 * Student ID:  8789102
 * Date:		September 30, 2022
 * Description: This file contains a MainWindow class, its properties and event handlers for the selected elements
 */
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
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
using System.Xml.Linq;

namespace A02_Notepad
{
    /// <summary>
    /// CLASS NAME:  MainWindow
    /// PURPOSE : The MainWindow class is inherited from the Window classes.It has properties
    /// and event handlers for the selected elements such as New, Open, Save As, Exit and About.
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        private string fileName;    // Holds the string of the file name
        private bool changedText;   // Holds the boolean indicating that the text is whether modified or not


        /*  -- Method Header Comment
        Name	: MainWindow -- CONSTRUCTOR
        Purpose : to instantiate a new MainWindow object - given a set of attribute values
        Inputs	: NONE
        Outputs	: NONE
        Returns	: Nothing
        */
        public MainWindow()
        {
            InitializeComponent();
        }

        /*  -- Method Header Comment
        Name	: MainWindow -- CONSTRUCTOR
        Purpose : to instantiate a new MainWindow object - given a set of attribute values
        Inputs	: newFileName    string
        Outputs	: NONE
        Returns	: Nothing
        */
        public MainWindow(string newFileName)
        {
            InitializeComponent();
            fileName = newFileName;
            this.Title = fileName;
        }

        /*  -- Property Header Comment
        Name	: ChangedText 
        Purpose : this property will return and set the data member (changedText).
        Inputs	: NONE
        Outputs	: NONE
        Returns	: bool
        */
        public bool  ChangedText
        {
           get { return changedText; }
           set { changedText = value; }
        }

        /*  -- Property Header Comment
        Name	: FileName
        Purpose : this property will return and set the data member (fileName).
        Inputs	: NONE
        Outputs	: NONE
        Returns	: string
        */
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        /*  -- Event handler Header Comment
       Name	: new_click
       Purpose : this event handler is used to implenment "New" in the menu items in "File". “New” allows the user 
       to start a new file for editing. If there is unsaved text in the work area, the user has a chance to save the file
       Inputs	: sender                object
                  RoutedEventArgs       e
       Outputs	: NONE 
       Returns	: NONE
       */
        private void new_click(object sender, RoutedEventArgs e)
        {
            // Check if the file is modified or not
            if (ChangedText == false)
            {
                // Clean the textbox
                textArea.Text = "";
                this.Title = "*Unititled - Notepad";
            }
            // The file is modifed
            else
            {
                // The user has a chance to save the file
                var option = MessageBox.Show("Do you want to save changes? ", "", MessageBoxButton.YesNoCancel, MessageBoxImage.Information);

                // The user wants to save
                if(option == MessageBoxResult.Yes)
                {
                    savefile();
                }
                // The user dont want to save 
                if(option == MessageBoxResult.No)
                {
                    // Create a new window and close the old on
                    textArea.Text = "";
                    this.Title = "*Unititled - Notepad";
                }
            }

        }

        /*  -- Method Header Comment
      Name	: savefile
      Purpose : this method is used to save a specific file
      Inputs	: NONE
      Outputs	: NONE
      Returns	: NONE
      */
        public void savefile()
        {
            // Create a new savefiledialog
            SaveFileDialog savefiledialog = new SaveFileDialog();

            // Default
            savefiledialog.DefaultExt = ".txt";
            savefiledialog.FileName = "*";
            savefiledialog.Filter = "Text Documents|*.txt|All Files|*.*";

            bool? result = savefiledialog.ShowDialog();

            // Check if the user chooses "Yes" to save the file
            if (result == true)
            {
                // Write the content of file to a specific file
                System.IO.File.WriteAllText(savefiledialog.FileName, textArea.Text.ToString());

                // Create new window and close the old one
                textArea.Text = "";
                this.Title = "*Unititled - Notepad";
            }
            
            
        }

        /*  -- Event handler Header Comment
      Name	: open_click
      Purpose : this event handler is used to implenment "Open" in the menu items in "File". “Open” allows the user 
      to start a new file. If there is unsaved text in the work area, the user has a chance to save the file
      Inputs	: sender               object
                 RoutedEventArgs       e
      Outputs	: A message box to prompt user selecting an option 
      Returns	: NONE
      */
        private void open_click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();

            bool? result;

            // Check if the file is modified or not
            if (ChangedText == true)
            {
                // The user has a chance to save the file
                var option = MessageBox.Show("Do you want to save changes? ", "", MessageBoxButton.YesNoCancel, MessageBoxImage.Information);

                // If the user chooses "Yes" to save the file
                if (option == MessageBoxResult.Yes)
                {
                    // Create a new savefiledialog
                    SaveFileDialog savefiledialog = new SaveFileDialog();

                    // Default
                    savefiledialog.DefaultExt = ".txt";
                    savefiledialog.FileName = "*";
                    savefiledialog.Filter = "Text Documents|*.txt|All Files|*.*";

                    bool? result1 = savefiledialog.ShowDialog();

                    // Check if the user chooses "Yes" to save the file
                    if (result1 == true)
                    {
                        // Write the content of file to a specific file
                        System.IO.File.WriteAllText(savefiledialog.FileName, textArea.Text.ToString());
                        ChangedText = false;
                    }

                }
                // If the user chooses "No" to open a new file
                if (option == MessageBoxResult.No)
                {
                    result = openFile.ShowDialog();

                    // Check if the user chooses "Open" button to open a file
                    if (result == true)
                    {
                        // Clear the screen
                        textArea.Text = String.Empty;

                        // Load the content of a specific file
                        textArea.AppendText(File.ReadAllText(openFile.FileName).ToString());

                        // Change the title of the window
                        this.Title = System.IO.Path.GetFileNameWithoutExtension(openFile.FileName) + " - Notepad";

                        changedText = false;
                    }
                }
            }
            // Check if the file is not modifled
            else
            {
                result = openFile.ShowDialog();

                // Check if the user chooses "Open" button to open a file
                if (result == true)
                {
                    // Clear the screen
                    textArea.Text = String.Empty;

                    // Load the content of a specific file
                    textArea.AppendText(File.ReadAllText(openFile.FileName).ToString());

                    // Change the title of the window
                    this.Title = System.IO.Path.GetFileNameWithoutExtension(openFile.FileName) + " - Notepad";

                    ChangedText = false;
                }
            }
        }

        /*  -- Event handler Header Comment
      Name	: saveAs_Click
      Purpose : this event handler is used to implenment "Save As" in the menu items in "File". “Save As” allows the user 
      to save a new file. 
      Inputs	: sender               object
                 RoutedEventArgs       e
      Outputs	: NONE
      Returns	: NONE
      */
        private void saveAs_Click(object sender, RoutedEventArgs e)
        {
            // Create a new savefiledialog
            SaveFileDialog savefiledialog = new SaveFileDialog();

            // Default
            savefiledialog.DefaultExt = ".txt";
            savefiledialog.FileName = "*";
            savefiledialog.Filter = "Text Documents|*.txt|All Files|*.*";

            bool? result = savefiledialog.ShowDialog();

            // Check if the user chooses "Save" button to save a file
            if (result == true)
            {
                // Write the content of file to a specific file
                System.IO.File.WriteAllText(savefiledialog.FileName, textArea.Text.ToString());

                changedText = false;
            }
        }

        /*  -- Event handler Header Comment
      Name	: exit_click
      Purpose : this event handler is used to implenment "Exit" in the menu items in "File". “Exit” allows the user 
      to exit the program. If there is unsaved text in the work area, the user has a chance to save the file
      Inputs	: sender               object
                 RoutedEventArgs       e
      Outputs	: NONE
      Returns	: NONE
      */
        private void exit_click(object sender, RoutedEventArgs e)
        {
            // Check the file is modifled or not before closing the application
            if (ChangedText == false)
            {
                Application.Current.Shutdown();
            }
            // If the file is modified
            else
            {
                // The user has a chance to save the file
                var option = MessageBox.Show("Do you want to save changes before exiting? ", "", MessageBoxButton.YesNoCancel, MessageBoxImage.Information);

                // The user want to save the file
                if (option == MessageBoxResult.Yes)
                {
                    // Create a new savefiledialog
                    SaveFileDialog savefiledialog = new SaveFileDialog();

                    // Default
                    savefiledialog.DefaultExt = ".txt";
                    savefiledialog.FileName = "*";
                    savefiledialog.Filter = "Text Documents|*.txt|All Files|*.*";

                    bool? result = savefiledialog.ShowDialog();

                    // Check if the user chooses "Yes" to save the file
                    if (result == true)
                    {
                        // Write the content of file to a specific file
                        System.IO.File.WriteAllText(savefiledialog.FileName, textArea.Text.ToString());
                        Application.Current.Shutdown();
                    }
                }
                // The user want to exit without saving the file
                if (option == MessageBoxResult.No)
                {
                    Application.Current.Shutdown();
                }

            }

        }

        /*  -- Event handler Header Comment
      Name	: textArea_TextChanged
      Purpose : this event handler is used to check whether the file is modifled or not. Furthermore it will count the chareacters in the file
      Inputs	: sender               object
                 RoutedEventArgs       e
      Outputs	: NONE
      Returns	: NONE
      */
        private void textArea_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Count and update the characters in the ifle
            countCol.Text = textArea.Text.Length.ToString();
           
            changedText = true;
        }


        /*  -- Event handler Header Comment
      Name	: about_click
      Purpose : this event handler is used to open a new window which contains the standard information about the application
      Inputs	: sender               object
                 RoutedEventArgs       e
      Outputs	: NONE
      Returns	: NONE
      */
        private void about_click(object sender, RoutedEventArgs e)
        {
            Window1 aboutWindow = new Window1();

            aboutWindow.ShowDialog();
        }

        /*  -- Event handler Header Comment
     Name	: Window_Closing
     Purpose : this event handler is used to close a window when the user presses 'x' button 
     Inputs	:   sender               object
                RoutedEventArgs       e
     Outputs	: NONE
     Returns	: NONE
     */
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (changedText == false)
            {
                Application.Current.Shutdown();
            }
            else
            {
                // The user has a chance to save the file
                var option = MessageBox.Show("Do you want to save changes before exiting? ", "", MessageBoxButton.YesNoCancel, MessageBoxImage.Information);

                // The user want to save the file
                if (option == MessageBoxResult.Yes)
                {
                    // Create a new savefiledialog
                    SaveFileDialog savefiledialog = new SaveFileDialog();

                    // Default
                    savefiledialog.DefaultExt = ".txt";
                    savefiledialog.FileName = "*";
                    savefiledialog.Filter = "Text Documents|*.txt|All Files|*.*";

                    bool? result = savefiledialog.ShowDialog();

                    // Check if the user chooses "Yes" to save the file
                    if (result == true)
                    {
                        // Write the content of file to a specific file
                        System.IO.File.WriteAllText(savefiledialog.FileName, textArea.Text.ToString());
                        
                    }
                    e.Cancel = true;
                }
                // The user want to exit without saving the file
                if (option == MessageBoxResult.No)
                {
                    
                }
                if(option == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                }
               
            }
        }
    }
}
