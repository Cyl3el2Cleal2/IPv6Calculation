using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace main
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Net.IPNetwork ipnetwork = System.Net.IPNetwork.Parse(inputIIP.Text + "/" + (comboSubnet.SelectedIndex + 1));
                tvIP.Content = "IP address = " + formatIP(inputIIP.Text) + "/" + (comboSubnet.SelectedIndex + 1);
                tvIPfull.Content = "IP address (full) = " + formatFullIP(formatIP("" + inputIIP.Text));
                tvIPFirst.Content = "First = " + formatFullIP(formatIP("" + ipnetwork.FirstUsable));
                tvIPLast.Content = "Last = " + formatFullIP(formatIP("" + ipnetwork.LastUsable));
                tvIPTotal.Content = "Total IP addresses = " + String.Format("{0:n0}", ipnetwork.Total);
                tvNetwork.Content = "Network = " + ipnetwork.Network;
                tvNetmask.Content = "Netmask = " + ipnetwork.Netmask;
                tvIPBroadcast.Content = "Prefix length = " + (comboSubnet.SelectedIndex + 1);
            }

            catch (Exception p)
            {
                MessageBox.Show("Invalid IP address. \n Exaxple: 2221:0db8::");


            }
        }

   
        private void ComboSubnet_Initialized(object sender, EventArgs e)
        {
            
            for (int i = 1; i <= 128; i++)
            {
                System.Net.IPNetwork ipnetwork = System.Net.IPNetwork.Parse("2001:0db8::/" + i);
                if (i < 64)
                {
                    BigInteger biig = new BigInteger();
                    biig = (BigInteger) Math.Pow(2, 64 - i);

                    comboSubnet.Items.Add("" + i + " ( " + String.Format("{0:n0}", biig) + " networks /64 )");
                }
                else
                {
                    comboSubnet.Items.Add("" + i + " ( " + String.Format("{0:n0}", ipnetwork.Usable ) + " Address /64)");
                }
                

            }
            comboSubnet.SelectedIndex = 0;

        }

        private String formatIP(String ip)
        {
            String[] old = ip.Split(':');
            String newIP = "";
            for(int i = 0; i< old.Length; i++)
            {
                if(old[i].Length < 4)
                {
                    String tran = "000000000000000";
                    if (!String.IsNullOrEmpty(old[i]))
                    {
                         tran+= old[i];
                    }
                        
                    old[i] = tran.Substring(tran.Length - 4);
                }
                newIP += old[i];
                if (i != old.Length - 1)
                    newIP += ":";

            }

            return newIP;
        }

        private String formatFullIP(String ip)
        {
            String fullIP = ip + ":0000:0000:0000:0000:0000:0000:0000";
            return fullIP.Substring(0,39);

        }
    }
}
