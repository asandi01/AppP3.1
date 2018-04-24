using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppP3 {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecurrenceTypeAdd : ContentPage {   
        public RecurrenceTypeAdd() {
            InitializeComponent();
        }

        async void OnSubmitData(object sender, EventArgs args) {
            try {                                                                                          
                ServiceReference1.RecurenceTypeModel payment = new ServiceReference1.RecurenceTypeModel();

                payment.detail = detail.Text;
                payment.days = Int32.Parse(days.Text);   
                                                                                 
                var wsPayment = new ServiceReference1.RecurenceTypeSoapClient();
                wsPayment.AddAsync(payment);
                wsPayment.AddCompleted += wsPaymentAddCompleted;

            } catch (Exception ex) {
                throw ex;
            }
        }
                                                                                                              
        private async void wsPaymentAddCompleted(object sender, ServiceReference1.AddCompletedEventArgs e) {
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