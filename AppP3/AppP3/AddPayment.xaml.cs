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
                payment.paymentDate = DateTime.Parse(paymentDate.Text);
                payment.providerId = Int32.Parse(providerId.Text);
                payment.expenseCategoryId = Int32.Parse(expenseCategoryId.Text);


                var wsPayment = new ServiceReference2.PaymentRecordSoapClient();     
                wsPayment.AddAsync(payment);
                wsPayment.AddCompleted += wsPaymentAddCompleted;

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