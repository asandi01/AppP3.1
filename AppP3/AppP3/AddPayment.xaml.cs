using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppP3 {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPayment : ContentPage {
        public AddPayment() {
            InitializeComponent();
        }

        async void OnSubmitData(object sender, EventArgs args) {
            try {

                ServiceReference2.PaymentRecordModel payment = new ServiceReference2.PaymentRecordModel();
                                                   
                payment.detail = detail.Text;
                payment.amount = Double.Parse(amount.Text);
                payment.recurrence = Boolean.Parse(recurrence.Text);
                payment.recurrenciaTypeId = Int32.Parse(recurrenciaTypeId.Text); 
                payment.providerId = Int32.Parse(providerId.Text);
                payment.expenseCategoryId = Int32.Parse(expenseCategoryId.Text);
                                 
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
                    var wsPayment = new ServiceReference2.PaymentRecordSoapClient();
                    wsPayment.AddAsync(payment);
                    wsPayment.AddCompleted += wsPaymentAddCompleted;
                }

            } catch (Exception ex) {
                throw ex;
            }
        }                                                                                                                 

        private async void wsPaymentAddCompleted(object sender, ServiceReference2.AddCompletedEventArgs e) {
            var payment = e.Result;
            Device.BeginInvokeOnMainThread(() => {
                if (payment) {
                    Navigation.PushModalAsync(new PaymentRecordList());
                } else {
                    DisplayAlert("Error", "Ocurrio un error", "Ok");
                }
            });
        }   
    }
}