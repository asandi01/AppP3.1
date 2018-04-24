using AppP3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppP3 {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPayment : ContentPage {
        public EditPayment(int idSelected) {
            InitializeComponent();
            id.Text = idSelected.ToString();


            var wsPayment = new ServiceReference2.PaymentRecordSoapClient();
            wsPayment.GetDetailsByIdAsync(idSelected);
            wsPayment.GetDetailsByIdCompleted += wsPaymentCompleted;
        }
              
        private async void wsPaymentCompleted(object sender, ServiceReference2.GetDetailsByIdCompletedEventArgs e) {
            var payment = e.Result;
            Device.BeginInvokeOnMainThread(() => {
                detail.Text = payment.detail;
                amount.Text = payment.amount.ToString();
                recurrence.Text = payment.recurrence.ToString();
                recurrenciaTypeId.Text = payment.recurrenciaTypeId.ToString();
                paymentDate.Text = payment.paymentDate.ToString();
                providerId.Text = payment.providerId.ToString();
                expenseCategoryId.Text = payment.expenseCategoryId.ToString();
            });
        }
             
        async void OnSubmitData(object sender, EventArgs args) {
            try {

                ServiceReference2.PaymentRecordModel payment = new ServiceReference2.PaymentRecordModel();

                payment.id = Int32.Parse(id.Text);
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
                    wsPayment.UpdateDetailsAsync(payment);
                    wsPayment.UpdateDetailsCompleted += wsPaymentUpdateCompleted;
                }

            } catch (Exception ex) {
                throw ex;
            }
        }

        private async void wsPaymentUpdateCompleted(object sender, ServiceReference2.UpdateDetailsCompletedEventArgs e) {
            var payment = e.Result; 
            Device.BeginInvokeOnMainThread(() => {
                if (payment) {
                    Navigation.PushModalAsync(new PaymentRecordList());
                } else {
                    DisplayAlert("Error", "Ocurrio un error", "Ok");  
                }
            });
        }

        async void OnDelete(object sender, EventArgs args) {
            try {

                ServiceReference2.PaymentRecordModel payment = new ServiceReference2.PaymentRecordModel();

                payment.id = Int32.Parse(id.Text); 
                var wsPayment = new ServiceReference2.PaymentRecordSoapClient();
                wsPayment.DeleteAsync(payment.id);                             
                wsPayment.DeleteCompleted += wsPaymentDeleteCompleted;

            } catch (Exception ex) {
                throw ex;
            }
        }
                                                                                                                           
        private async void wsPaymentDeleteCompleted(object sender, ServiceReference2.DeleteCompletedEventArgs e) {
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