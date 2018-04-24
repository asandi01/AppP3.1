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
                  
        void OnImagePayment(object sender, EventArgs args) {
            try {
                Navigation.PushModalAsync(new PaymentRecordList());
            } catch (Exception ex) {
                throw ex;
            }
        }
        void OnImageIncome(object sender, EventArgs args) {
            try {
                Navigation.PushModalAsync(new IncomeRecordList());
            } catch (Exception ex) {
                throw ex;
            }
        }

    }
}
