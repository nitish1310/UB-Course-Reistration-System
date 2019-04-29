<%@ Page Title="" Language="C#" MasterPageFile="~/AdvisorMaster.master" AutoEventWireup="true" CodeFile="message.aspx.cs" Inherits="message" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/pagination.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .row {
            padding-left: 10px;
            padding-right: 10px;
            padding: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="x_panel">
            <div class="x_title">
                <h2>Personal Message</h2>
                <ul class="nav navbar-right panel_toolbox">
                    <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                    </li>

                    <li><a class="close-link"><i class="fa fa-close"></i></a>
                    </li>
                </ul>
                <div class="clearfix"></div>
            </div>
            <div class="x_content">
                <br />
                <div id="div_msg" runat="server" class="" style="text-align: center; margin-top: 10px;"></div>
                    <div class="row " style="margin-top: 40px;">
                        <label class="control-label">
                            Message :
                   
                        </label>
                        <div class="row ">
                            <asp:TextBox ID="txtMessage" TextMode="MultiLine" CssClass="form-control" runat="server" Columns="50"></asp:TextBox>
                        </div>
                       
                    </div>

                 
                <div class="row" style="margin-top: 20px">
                    <div class=" col-sm-offset-4 col-sm-4">
                         <asp:Button ID="btnSave" class="btn btn-primary btn-block" runat="server" Text="Save"
                            OnClick="btnSave_Click" ValidationGroup="a" />
                    </div>
                </div>

            </div>
        </div>
    </form>
    <!-- Custom Theme Scripts -->
    <script src="/js/custom.min.js"></script>

</asp:Content>


