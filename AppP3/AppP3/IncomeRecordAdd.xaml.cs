using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppP3 {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IncomeRecordAdd : ContentPage {
        public IncomeRecordAdd() {
            InitializeComponent();
        }

        async void OnSubmitData(object sender, EventArgs args) {
            try {                                                                                            
                ServiceReference3.IncomeRecordModel payment = new ServiceReference3.IncomeRecordModel();

                payment.detail = detail.Text;
                payment.amount = Double.Parse(amount.Text);

                bool send = true;
                DateTime temp;
                if (DateTime.TryParse(paymentDate.Text, out temp)) {  
                    payment.paymentDate = DateTime.Parse(paymentDate.Text);
                } else {
                    send = false;
                    Device.BeginInvokeOnMainThread(() => {  
                        DisplayAlert("Error", "Error de fecha", "Ok");     
                    });
                }

                if (send) {   
                    var wsPayment = new ServiceReference3.IncomeRecordSoapClient();
                    wsPayment.AddAsync(payment);
                    wsPayment.AddCompleted += wsPaymentAddCompleted;
                }

            } catch (Exception ex) {
                throw ex;
            }
        }
                                                                                                             
        private async void wsPaymentAddCompleted(object sender, ServiceReference3.AddCompletedEventArgs e) {
            var payment = e.Result;
            Device.BeginInvokeOnMainThread(() => {
                if (payment) {
                    Navigation.PushModalAsync(new IncomeRecordList());
                } else {
                    DisplayAlert("Error", "Ocurrio un error", "Ok");
                }
            });
        }
    }
}