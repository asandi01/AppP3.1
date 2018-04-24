using AppP3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppP3 {
    public partial class MainPage : ContentPage {       
        public MainPage() {
            InitializeComponent();
            paymentImage.Source = "/images/payment.png";
        }
                  
        /*async void OnPayment(object sender, SelectedItemChangedEventArgs e) {       
            await Navigation.PushModalAsync(new PaymentRecordList());
        } */

        void OnImagePayment(object sender, EventArgs args) {
            try {
                Navigation.PushModalAsync(new PaymentRecordList());
            } catch (Exception ex) {
                throw ex;
            }
        }

    }
}
