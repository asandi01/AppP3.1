﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppP3 {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IncomeRecordList : ContentPage {
        List<string> listPayment = new List<string>();
        public IncomeRecordList() {
            InitializeComponent();                                           
            var wsPayment = new ServiceReference3.IncomeRecordSoapClient();
            wsPayment.GetDetailsAsync();
            wsPayment.GetDetailsCompleted += wsPaymentCompleted;
        }             
                                                                                                                 
        private async void wsPaymentCompleted(object sender, ServiceReference3.GetDetailsCompletedEventArgs e) {
            var results = e.Result;    
            foreach (var item in results) {
                listPayment.Add(item.id + " => " + item.detail + " $" + item.amount);
            }

            Device.BeginInvokeOnMainThread(() => {
                lst.ItemsSource = listPayment;
            });


        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e) {
            string itemSelected = e.SelectedItem.ToString();
            string[] id = itemSelected.Split(' ');

            int idSelected = Int32.Parse(id[0]);
            await Navigation.PushModalAsync(new IncomeRecordEdit(idSelected));
        }

        async void OnAddPayment(object sender, SelectedItemChangedEventArgs e) {
            await Navigation.PushModalAsync(new IncomeRecordAdd());
        }

    }
}