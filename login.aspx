<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>University of Bridgeport | Course Registration</title>
    <link href="css/style1.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap-theme.min.css" rel="stylesheet" type="text/css" />
    <link href="css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap-social.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="images/favicons.ico" type="image/x-icon" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" />
    <style type="text/css">
        .checkb {
            margin-left: 20px;
        }

        .fontc {
            font-size: 12px;
        }

        .backgrd {
            /*background: linear-gradient(rgba(255,255,255,.5), rgba(255,255,255,.5)),url(images/login2.jpg);*/
            background: url(images/home-page-landing-arch-min.jpg) no-repeat center fixed;
            background-size: cover;
        }
    </style>
    <%--Favicon--%>
    <link rel="icon" href="images/fevicon.ico" type="image/x-icon" />
</head>
<body class="backgrd">
    <form id="form1" runat="server">
        <div class="container" style="margin-top: 110px;">
            <div class="row">
                <div class="col-md-4 col-sm-12 col-xs-12">
                    <h1 class="text-center login-title" style="color: #ffffff; font-weight: bold;">Sign in to Course Registration</h1>
                    <div class="account-wall" style="background: linear-gradient(rgba(255,255,255,.8), rgba(255,255,255,.8)) center;">
                        <img class="profile-img" src="images/dheya-icon.png"
                            alt="" />
                        <div class="form-signin">
                            <div id="div_msg" runat="server" style="margin-top: 10px; text-align: center; margin-bottom: 25px">
                            </div>
                            <asp:TextBox ID="txt_username" type="text" runat="server" placeholder="Enter Email-Id"
                                class="form-control" TabIndex="1"></asp:TextBox>
                            <asp:TextBox ID="txt_password" type="password" runat="server" placeholder="Password"
                                class="form-control" TabIndex="2"></asp:TextBox>
                            <div style="text-align: center">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please Enter valid Email-ID."
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txt_username"
                                    meta:resourcekey="RegularExpressionValidator1Resource1" Display="None"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="rfv_username" CssClass="Validators" Display="None"
                                    ControlToValidate="txt_username" runat="server" ErrorMessage="Email-ID is required."></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="rfv_password" CssClass="Validators" Display="None"
                                    ControlToValidate="txt_password" runat="server" ErrorMessage="Password is required."></asp:RequiredFieldValidator>
                                <asp:ValidationSummary ID="vs_login" runat="server" ShowMessageBox="true" ShowSummary="false" />
                            </div>
                            <asp:LinkButton ID="btn_login" runat="server"
                                class="btn btn-lg btn-primary btn-block" Style="text-align: center"
                                OnClick="btn_login_Click" TabIndex="3">Login to CDF-Dashboard</asp:LinkButton>

                            <label class="checkbox pull-left fontc row">
                                Remember me
                           
                            </label>
                            <asp:CheckBox ID="chkRememberMe" CssClass="checkb" runat="server" />
                            <asp:LinkButton ID="changepassword" class="pull-right need-help fontc" ValidationGroup="a" runat="server"
                                OnClick="forgotpassword_Click" TabIndex="6"><strong>Forgot Password</strong></asp:LinkButton>
                            <span class="clearfix"></span>

                        </div>
                    </div>
                    <div class="row" style="text-align: center; margin-top: 10px; font-weight: bold;"><u><a href="pre/email-verification.aspx" style="color: #000000;">Create New Account</a></u></div>

                </div>
                <div class="col-md-8 col-sm-12 col-xs-12">
                </div>

            </div>

        </div>
    </form>

    <script src="js/jquery.js" type="text/javascript"></script>
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
</body>
</html>
