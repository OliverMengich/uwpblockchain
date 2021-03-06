﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Data.SqlClient;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BL
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void enryptButton_Click(object sender, RoutedEventArgs e)
        {
            string minerAddress = "mineraddress";
            var amount = amountBox.Text;
            
            Blockchain blockchain = new Blockchain(proofOfWorkDifficulty: 2, miningReward: 10);
            blockchain.CreateTransaction(new Transaction(messageBox.Text,recipientBox.Text, Convert.ToDouble(amount)));

            //initiating mining...
            blockchain.MineBlock("Oliver");
            balanceBox.Text = Convert.ToString(blockchain.GetBalance(minerAddress));

        }
    }
}
