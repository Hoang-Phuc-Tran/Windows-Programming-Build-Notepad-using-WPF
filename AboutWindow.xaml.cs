/*
 * FILE:        AboutWindow.xaml.cs
 * Project:	    A02 – WPF APPLICATION
 * Author:	    Hoang Phuc Tran
 * Student ID:  8789102
 * Date:		September 30, 2022
 * Description: This file contains a Window1 class, its properties and event handlers.
 */
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
using System.Windows.Shapes;

namespace A02_Notepad
{
    /// <summary>
    /// CLASS NAME:  Window1
    /// PURPOSE : The MainWindow class is inherited from the Window classes.It has properties
    /// and event handlers. This class is used to invoke a new window which contains the standard information about the application
    /// </summary>
    /// 
    public partial class Window1 : Window
    {
        /*  -- Method Header Comment
        Name	: Window1 -- CONSTRUCTOR
        Purpose : to instantiate a new Window1 object - given a set of attribute values
        Inputs	: NONE
        Outputs	: NONE
        Returns	: Nothing
        */
        public Window1()
        {
            InitializeComponent();
        }

        /*  -- Event handler Header Comment
      Name	: ok_click
      Purpose : this event handler is used to open a new window which contains the standard information about the application
      to save a new file. 
      Inputs	: sender               object
                 RoutedEventArgs       e
      Outputs	: NONE
      Returns	: NONE
      */
        private void ok_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
