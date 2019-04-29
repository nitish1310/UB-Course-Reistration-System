<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="course-details.aspx.cs" Inherits="course_details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="x_panel">
            <div class="x_title">
                <h2>Course Update</h2>
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
                 <div class="row">
                            <div class="col-md-5 col-md-offset-1">
                                <div class="form-group">
                                    <span class="leftalign">Department</span>
                                    <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" >                                       
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-5">
                                <div class="form-group">
                                    <span class="leftalign">Course Level</span>
                                    <asp:DropDownList ID="ddlCourseLevel" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="Select">Select an Option</asp:ListItem>
                                        <asp:ListItem Value="Under-Graduate">Under-Graduate</asp:ListItem>
                                        <asp:ListItem Value="Graduate">Graduate</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-5 col-md-offset-1">
                                <div class="form-group">
                                    <span class="leftalign">Course Number</span>
                                    <asp:DropDownList ID="ddlCourseNumber" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-5">
                                <div class="form-group">
                                    <span class="leftalign">Course Name</span>
                                     <asp:TextBox class="form-control" ID="txtCourseName" runat="server" placeholder="Course Name"
                            MaxLength="100"></asp:TextBox>
                                   
                                </div>
                            </div>
                        </div>
                         <div class="row">
                            <div class="col-md-5 col-md-offset-1">
                                <div class="form-group">
                                    <span class="leftalign">Course Status</span>
                                     <asp:DropDownList ID="ddlCourseStatus" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="Select">Select an Option</asp:ListItem>
                                        <asp:ListItem Value="1">Approved</asp:ListItem>
                                        <asp:ListItem Value="0">Declined</asp:ListItem>                                        
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-5">
                                <div class="form-group">
                                    <span class="leftalign">Credits</span>
                                    <asp:DropDownList ID="ddlCourseCredits" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="Select">Select an Option</asp:ListItem>
                                        <asp:ListItem Value="1">1</asp:ListItem>
                                        <asp:ListItem Value="2">2</asp:ListItem>
                                         <asp:ListItem Value="3">3</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                            </div>
                        </div>
                
                <div class="row">
                            <div class="col-md-5 col-md-offset-1">
                                <div class="form-group">
                                    <span class="leftalign">Term</span>
                                     <asp:DropDownList ID="ddlCourseTerm" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="Select">Select an Option</asp:ListItem>
                                        <asp:ListItem Value="Summer2019">Summer 2019</asp:ListItem>
                                        <asp:ListItem Value="Fall2019">Fall 2019</asp:ListItem>    
                                        <asp:ListItem Value="Spring2020">Spring 2020</asp:ListItem>    
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-5">
                                <div class="form-group">
                                    <span class="leftalign">Class Room Number</span>
                                    <asp:DropDownList ID="ddlClassRoom" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>

                            </div>
                        </div>
                        <div class="row" style="margin-top: 20px;">
                            <div class="form-group">
                                <div class="col-md-4 col-md-offset-4">
                                    <asp:Button ID="btnPreview" runat="server" CssClass="btn btn-primary btn-block btn1"
                                        Text="Update" OnClick="btnUpdate_Click"  />
                                </div>
                                
                            </div>
                        </div>
            </div>
        </div>

        <div class="x_panel">
            <div class="x_title">
                <h2>Course Drop</h2>
                <ul class="nav navbar-right panel_toolbox">
                    <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                    </li>

                    <li><a class="close-link"><i class="fa fa-close"></i></a>
                    </li>
                </ul>
                <div class="clearfix"></div>
            </div>
       <div class="x_content">
                        <div align="center">
                            <table class="table table-striped" style="width: 80%; margin-top: 40px">
                                <tr>
                                    <td>
                                        <label>
                                            Course Activity</label>
                                    </td>
                                    <td>
                                        <label>
                                            Current Status</label>
                                    </td>

                                    <td>
                                        <label>
                                            Change Status</label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblStatus" runat="server">User Status</asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCourseStatus" runat="server" Text="Label"></asp:Label>
                                    </td>

                                    <td>
                                        <asp:Button ID="btnCourseStatus" runat="server" Text="Active" OnClick="btnCourseStatus_Click" />
                                    </td>
                                </tr>
                               <%-- <tr>
                                    <td>
                                        <asp:Label ID="Label2" runat="server">Test Reassign</asp:Label>
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="btn_testReassing" runat="server" class="btn btn-danger btn-sm btn-block" Text="TEST REASSIGN" OnClick="btn_testReassing_Click" Enabled="False" />
                                    </td>
                                </tr>--%>

                              
                            </table>
                        </div>
                    </div>
            </div>



        <%--User Information--%>
        <div class="x_panel">
            <div class="x_title">
                <h2>User Information</h2>
                <ul class="nav navbar-right panel_toolbox">
                    <%--<li>
                        <asp:LinkButton ID="btn_editInfo" runat="server" Text="Edit" Font-Bold="true" OnClick="btn_editInfo_Click" /></li>--%>
                    <li><a href="#" data-toggle="modal" title="Help" data-target="#myModalHelpInformation"><i class="fa fa-question"></i></a>
                    </li>
                    <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                    </li>
                    <li><a class="close-link"><i class="fa fa-close"></i></a>
                    </li>
                </ul>
                <div class="clearfix"></div>
            </div>
            <div class="x_content">
                
            </div>
        </div>
    </form>

    <script src="js/custom.min.js"></script>
    <script type="text/javascript">
        function CourseApproved() {
            return confirm("Are you sure you want to Approved this Course?");
        }
        function CourseDecline() {
            return confirm("Are you sure you want to Decline this Course?");
        }
        function UserDeactive() {
            return confirm("Are you sure you want to Deactive this user?");
        }
    </script>
</asp:Content>

