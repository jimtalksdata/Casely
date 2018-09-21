﻿using System;
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
using System.Collections.ObjectModel;
using CaselyData;

namespace Casely {

    
    /// <summary>
    /// Interaction logic for UCPartEntry.xaml
    /// </summary>
    public partial class UCPartEntry : UserControl {
        /// <summary>
        /// partEntry is used to give access to the author and other fields
        /// that are associated with this part entry control.
        /// </summary>
        public PartEntry partEntry;
        public UCPartEntry(PartEntry part) {
            InitializeComponent();
            this.DataContext = part;
            partEntry = part;
        }

    }
}