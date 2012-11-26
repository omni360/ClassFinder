using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using HtmlAgilityPack;

namespace ClassFinder
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            System.Threading.Thread.Sleep(1000); //Delay for 1 second for the splash screen
        }

        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            myTextBlock.Text = "Please enter a CRN below:";
            string crn = myTextBox.Text;
            if (crn != "")
            {
                HtmlWeb.LoadAsync("https://selfservice.mypurdue.purdue.edu/prod/bwckschd.p_disp_detail_sched?term_in=201320&crn_in=" + crn, DownloadCompleted);
            }
        }

        private void DownloadCompleted(object sender, HtmlDocumentLoadCompleted e)
        {
            if (e.Error == null)
            {
                HtmlDocument doc = e.Document;
                if (doc != null)
                {
                    string capacity = doc.DocumentNode.Element("html").
                        Element("body").
                        Elements("div").ElementAt(3).
                        Elements("table").ElementAt(0).
                        Elements("tr").ElementAt(1).
                        Element("td").
                        Element("table").
                        Element("tr").
                        Element("tr").
                        Elements("td").ElementAt(0).InnerText;

                    string actual = doc.DocumentNode.Element("html").
                        Element("body").
                        Elements("div").ElementAt(3).
                        Elements("table").ElementAt(0).
                        Elements("tr").ElementAt(1).
                        Element("td").
                        Element("table").
                        Element("tr").
                        Element("tr").
                        Elements("td").ElementAt(1).InnerText;

                    string remaining = doc.DocumentNode.Element("html").
                        Element("body").
                        Elements("div").ElementAt(3).
                        Elements("table").ElementAt(0).
                        Elements("tr").ElementAt(1).
                        Element("td").
                        Element("table").
                        Element("tr").
                        Element("tr").
                        Elements("td").ElementAt(2).InnerText;

                    capacityTextBlock.Text = capacity;
                    actualTextBlock.Text = actual;
                    remainingTextBlock.Text = remaining;

                }
            }
        }
    }
}