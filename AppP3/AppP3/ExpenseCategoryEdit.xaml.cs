using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppP3 {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExpenseCategoryEdit : ContentPage {  
        public ExpenseCategoryEdit(int idSelected) {
            InitializeComponent();
            id.Text = idSelected.ToString();
                                                                             
            var wsPayment = new ServiceReference4.ExpenseCategorySoapClient();
            wsPayment.GetDetailsByIdAsync(idSelected);
            wsPayment.GetDetailsByIdCompleted += wsPaymentCompleted;
        }
                                                                                                                      
        private async void wsPaymentCompleted(object sender, ServiceReference4.GetDetailsByIdCompletedEventArgs e) {
            var payment = e.Result;
            Device.BeginInvokeOnMainThread(() => {
                detail.Text = payment.detail;         
            });
        }

        async void OnSubmitData(object sender, EventArgs args) {
            try {                                                                                         
                ServiceReference4.ExpenseCategoryModel payment = new ServiceReference4.ExpenseCategoryModel();

                payment.id = Int32.Parse(id.Text);
                payment.detail = detail.Text;
                                                                                 
                var wsPayment = new ServiceReference4.ExpenseCategorySoapClient();
                wsPayment.UpdateDetailsAsync(payment);
                wsPayment.UpdateDetailsCompleted += wsPaymentUpdateCompleted;

            } catch (Exception ex) {
                throw ex;
            }
        }                                                                                                                  

        private async void wsPaymentUpdateCompleted(object sender, ServiceReference4.UpdateDetailsCompletedEventArgs e) {
            var payment = e.Result;
            Device.BeginInvokeOnMainThread(() => {
                if (payment) {
                    Navigation.PushModalAsync(new ExpenseCategoryList());
                } else {
                    DisplayAlert("Error", "Ocurrio un error", "Ok");
                }
            });
        }

        async void OnDelete(object sender, EventArgs args) {
            try {                                                                                          
                ServiceReference4.ExpenseCategoryModel payment = new ServiceReference4.ExpenseCategoryModel();

                payment.id = Int32.Parse(id.Text);                               
                var wsPayment = new ServiceReference4.ExpenseCategorySoapClient();
                wsPayment.DeleteAsync(payment.id);
                wsPayment.DeleteCompleted += wsPaymentDeleteCompleted;

            } catch (Exception ex) {
                throw ex;
            }
        }
                                                                                                                   
        private async void wsPaymentDeleteCompleted(object sender, ServiceReference4.DeleteCompletedEventArgs e) {
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