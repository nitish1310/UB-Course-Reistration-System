<%@ Page Title="" Language="C#" MasterPageFile="~/StudentMaster.master" AutoEventWireup="true" CodeFile="home.aspx.cs" Inherits="home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- Bootstrap -->
    <link href="vendors/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Font Awesome -->
    <link href="http://maxcdn.bootstrapcdn.com/font-awesome/latest/css/font-awesome.min.css" rel="stylesheet" />
    <!-- NProgress -->
    <link href="vendors/nprogress/nprogress.css" rel="stylesheet" />
    <!-- bootstrap-progressbar -->
    <link href="vendors/bootstrap-progressbar/css/bootstrap-progressbar-3.3.4.min.css" rel="stylesheet" />

    <!-- Custom Theme Style -->
    <link href="css/custom.min.css" rel="stylesheet" />
    <link href="css/pagination.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row top_tiles">
        
        <div class="animated flipInY col-lg-6 col-md-6 col-sm-12 col-xs-12">
            <div class="tile-stats">
                <div class="icon"><i class="fa fa-user-plus purple"></i></div>
                <div class="count">
                    <asp:Label ID="leads_count" runat="server">3</asp:Label>
                </div>
                <h3 style="margin-top: 15px;">Course Registered</h3>
            </div>
        </div>
        <div class="animated flipInY col-lg-6 col-md-6 col-sm-12 col-xs-12">
            <div class="tile-stats">
                <div class="icon" id="lbl_lastvisit" runat="server"><i class="fa fa-eye red"></i></div>
                <div class="count">
                    <asp:Label ID="visits_count" runat="server">9</asp:Label>
                </div>
                <h3 style="margin-top: 15px;">Total Credits</h3>
            </div>

        </div>
    </div>
    <br />

   
 
    <%-- <div class="row" style="margin-bottom: 30px;">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2>Dheya Summer Offers</h2>
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
                    <center><img src="images/Dheya-Summer-Bonanza.jpg" alt="Dheya Summer Bonanza" style="height:372px; width:651px;"/></center>
                    </div>
                </div>
            </div>
        </div>--%>

    <!-- jQuery -->
    <script src="js/custom.min.js"></script>
    <script type='text/javascript' src='https://www.google.com/jsapi'></script>
    
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>

    
</asp:Content>

