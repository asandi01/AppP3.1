using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppP3 {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecurrenceTypeEdit : ContentPage { 
        public RecurrenceTypeEdit(int idSelected) {
            InitializeComponent();
            id.Text = idSelected.ToString();
                                                                 
            var wsPayment = new ServiceReference1.RecurenceTypeSoapClient();
            wsPayment.GetDetailsByIdAsync(idSelected);
            wsPayment.GetDetailsByIdCompleted += wsPaymentCompleted;
        }

        private async void wsPaymentCompleted(object sender, ServiceReference1.GetDetailsByIdCompletedEventArgs e) {
            var payment = e.Result;
            Device.BeginInvokeOnMainThread(() => {
                detail.Text = payment.detail;
                days.Text = payment.days.ToString();   
            });
        }

        async void OnSubmitData(object sender, EventArgs args) {
            try {
                ServiceReference1.RecurenceTypeModel payment = new ServiceReference1.RecurenceTypeModel();

                payment.id = Int32.Parse(id.Text);
                payment.detail = detail.Text;
                payment.days = Int32.Parse(days.Text); 
                                                               
                var wsPayment = new ServiceReference1.RecurenceTypeSoapClient();
                wsPayment.UpdateDetailsAsync(payment);
                wsPayment.UpdateDetailsCompleted += wsPaymentUpdateCompleted; 

            } catch (Exception ex) {
                throw ex;
            }
        }

        private async void wsPaymentUpdateCompleted(object sender, ServiceReference1.UpdateDetailsCompletedEventArgs e) {
            var payment = e.Result;
            Device.BeginInvokeOnMainThread(() => {
                if (payment) {
                    Navigation.PushModalAsync(new RecurrenceTypeList());
                } else {
                    DisplayAlert("Error", "Ocurrio un error", "Ok");
                }
            });
        }

        async void OnDelete(object sender, EventArgs args) {
            try {                                                                                          
                ServiceReference1.RecurenceTypeModel payment = new ServiceReference1.RecurenceTypeModel();

                payment.id = Int32.Parse(id.Text);                                
                var wsPayment = new ServiceReference1.RecurenceTypeSoapClient();
                wsPayment.DeleteAsync(payment.id);
                wsPayment.DeleteCompleted += wsPaymentDeleteCompleted;

            } catch (Exception ex) {
                throw ex;
            }
        }
                                                                                                                   
        private async void wsPaymentDeleteCompleted(object sender, ServiceReference1.DeleteCompletedEventArgs e) {
            var payment = e.Result;
            Device.BeginInvokeOnMainThread(() => {
                if (payment) {
                    Navigation.PushModalAsync(new RecurrenceTypeList());
                } else {
                    DisplayAlert("Error", "Ocurrio un error", "Ok");
                }
            });
        }
    }
}