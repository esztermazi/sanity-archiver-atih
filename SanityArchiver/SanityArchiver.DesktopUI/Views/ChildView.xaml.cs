﻿using SanityArchiver.Application.Models;
using SanityArchiver.DesktopUI.ViewModels;
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

namespace SanityArchiver.DesktopUI.Views
{
    /// <summary>
    /// Interaction logic for ChildView.xaml
    /// </summary>
    public partial class ChildView : UserControl
    {
        public CustomDirectory CustomDirectory { get; set; }

        public ChildView()
        {
            CustomDirectory = new CustomDirectory();
            InitializeComponent();
           
        }
    }
}
