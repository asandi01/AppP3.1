using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppP3 {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExpenseCategoryAdd : ContentPage {         
        public ExpenseCategoryAdd() {
            InitializeComponent();
        }

        async void OnSubmitData(object sender, EventArgs args) {
            try {                                                                                          
                ServiceReference4.ExpenseCategoryModel payment = new ServiceReference4.ExpenseCategoryModel();

                payment.detail = detail.Text;           
                                                                                 
                var wsPayment = new ServiceReference4.ExpenseCategorySoapClient();
                wsPayment.AddAsync(payment);
                wsPayment.AddCompleted += wsPaymentAddCompleted;

            } catch (Exception ex) {
                throw ex;
            }
        }
                                                                                                              
        private async void wsPaymentAddCompleted(object sender, ServiceReference4.AddCompletedEventArgs e) {
            var payment = e.Result;
            Device.BeginInvokeOnMainThread(() => {
                if (payment) {
                    Navigation.PushModalAsync(new ExpenseCategoryList());
                } else {
                    DisplayAlert("Error", "Ocurrio un error", "Ok");
                }
            });
        }
    }
}