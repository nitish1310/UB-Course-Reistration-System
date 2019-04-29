<%@ Page Title="" Language="C#" MasterPageFile="~/StudentMaster.master" AutoEventWireup="true" CodeFile="course-registration.aspx.cs" Inherits="course_registration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
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
                <h2>Course Registration</h2>
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
                    <label class="col-sm-2 col-sm-offset-2  control-label">
                        Department :
                   
                    </label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-1">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Select Department" ControlToValidate="ddlDepartment"
                            ValidationGroup="a" InitialValue="--Select--">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                    <label class="col-sm-2 col-sm-offset-2  control-label">
                        Course Level :</label>
                    <div class="col-sm-4">
                        <asp:DropDownList class="form-control" ID="ddlCourseLevel" runat="server">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem>Graduate</asp:ListItem>
                            <asp:ListItem>Under-Graduate</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-1">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Select Course Level" ControlToValidate="ddlCourseLevel"
                            ValidationGroup="a" InitialValue="--Select--">*</asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row ">
                    <label class="col-sm-2 col-sm-offset-2  control-label">
                        Course Name :</label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlCourseName" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-1">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Select Course Name" ControlToValidate="ddlCourseName"
                            ValidationGroup="a" InitialValue="--Select--">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row" style="margin-top: 20px">
                    <div class=" col-sm-offset-3 col-sm-2">
                        <asp:Button ID="btn_save" class="btn btn-primary btn-block" runat="server" Text="Add"
                            OnClick="btn_save_Click" ValidationGroup="a" />
                    </div>
                    <div class=" col-sm-2">
                        <asp:Button ID="btn_drop" class="btn btn-primary btn-block" runat="server" Text="Drop"
                            OnClick="btn_drop_Click" ValidationGroup="a" />
                    </div>
                    <div class=" col-sm-2">
                        <asp:Button ID="btn_clear" class="btn btn-primary btn-block" runat="server" Text="Clear"
                            OnClick="btn_clear_Click" />
                    </div>
                </div>

                <div align="center" style="margin-top: 20px;">
                    <asp:GridView ID="gridRegistration" runat="server" AutoGenerateColumns="False" DataKeyNames="courseId"
                        OnSelectedIndexChanged="gridRegistration_SelectedIndexChanged" Width="80%" CssClass="table table-responsive"
                        OnPageIndexChanging="gridRegistration_PageIndexChanging" CellPadding="4" ForeColor="#333333"
                        AllowPaging="True" GridLines="None" PageSize="5">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" HeaderText="Select Here" />
                            <asp:BoundField DataField="courseId" HeaderText="Course Id" InsertVisible="False" ReadOnly="True"
                                SortExpression="courseId" Visible="False" />
                            <asp:BoundField DataField="deptName" HeaderText="Department" SortExpression="deptName" />
                            <asp:BoundField DataField="courseLevel" HeaderText="Course Level" SortExpression="courseLevel" />
                            <asp:BoundField DataField="courseNumber" HeaderText="Course Number" SortExpression="courseNumber" />
                            <asp:BoundField DataField="courseName" HeaderText="Course Name" SortExpression="courseName" />
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5d4a82" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5d4a82" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" HorizontalAlign="Center" CssClass="pagination-ys" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>

                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="a" />
                </div>
            </div>
        </div>


    </form>
    <!-- Custom Theme Scripts -->
    <script src="js/custom.min.js"></script>

</asp:Content>

