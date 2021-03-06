﻿using Foundation;
using Stencil.Native.iOS.Core;
using Stencil.Native.ViewModels;
using Stencil.SDK;
using Stencil.SDK.Models.Requests;
using Stencil.SDK.Models.Responses;
using System;
using UIKit;

namespace Stencil.Native.iOS
{
    public partial class LoginController : BaseUIViewController, IViewModelView
    {
        public LoginController (IntPtr handle)
            : base(handle)
        {
            this.TrackPrefix = "LoginController";
        }

        public const string IDENTIFIER = "LoginController";

        public LoginViewModel ViewModel { get; set; }

        public override void ViewDidLoad()
        {
            base.ExecuteMethod("ViewDidLoad", delegate ()
            {
                base.ViewDidLoad();

                this.ViewModel = new LoginViewModel(this);

                txtName.Placeholder = this.ViewModel.Text_General_EmailWatermark;
                txtPassword.Placeholder = this.ViewModel.Text_General_PasswordWatermark;
                btnSignIn.SetTitle(this.ViewModel.Text_SignIn, UIControlState.Normal);

                this.OnReturnMoveTo(txtName, txtPassword);
                this.OnReturnExecute(txtPassword, this.DoLogin);


                this.ViewModel.Start();
            });
        }

        protected void DoLogin()
        {
            base.ExecuteMethodAsync("DoLogin", async delegate ()
            {
                this.View.EndEditing(true);

                if (!this.IsValid())
                {
                    return;
                }

                HUD.Show(this.ViewModel.Text_LoggingIn);

                ItemResult<AccountInfo> response = await this.StencilApp.LoginAsyncSafe(new AuthLoginInput()
                {
                    password = txtPassword.Text,
                    user = txtName.Text
                });

                if (response.IsSuccess())
                {
                    HUD.ShowSuccessWithStatus(string.Format("Welcome {0}!", this.StencilApp.CurrentAccount.first_name), 1200);
                    //TODO:MUST: Navigate to first screen.   StencilAppDelegate.Current.Launch_____();
                }
                else
                {
                    HUD.ShowErrorWithStatus(response.GetMessage(), 1200);
                }
            });
        }
        protected bool IsValid()
        {
            return base.ExecuteFunction("IsValid", delegate ()
            {
                string errorMessage = this.ViewModel.Validate(txtName.Text, txtPassword.Text);
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    HUD.ShowErrorWithStatus(errorMessage, 1800);
                    return false;
                }
                return true;
            });
        }

        partial void BtnSignIn_TouchUpInside(UIButton sender)
        {
            this.DoLogin();
        }
    }
}