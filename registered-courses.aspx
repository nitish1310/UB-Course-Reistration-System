<%@ Page Title="" Language="C#" MasterPageFile="~/AdvisorMaster.master" AutoEventWireup="true" CodeFile="registered-courses.aspx.cs" Inherits="registered_courses" %>

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
                <h2>Course Registered</h2>
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
                <div id="divContainer" runat="server"> 
                <div class="row " style="margin-top: 40px;">
                    <label class="col-sm-2 col-sm-offset-2  control-label">
                        User Id :
                    </label>
                    <div class="col-sm-4">
                        <asp:Label ID="lblUserId" runat="server" ></asp:Label>
                    </div>
                    <div class="col-sm-1">
                        
                    </div>
                </div>

                <div class="row " style="margin-top: 40px;">
                    <label class="col-sm-2 col-sm-offset-2  control-label">
                        Email Id :
                    </label>
                    <div class="col-sm-4">
                        <asp:Label ID="lblEmail" runat="server" ></asp:Label>
                    </div>
                    <div class="col-sm-1">
                        
                    </div>
                </div>

                 <div class="row " style="margin-top: 40px;">
                    <label class="col-sm-2 col-sm-offset-2  control-label">
                        Course Id :
                    </label>
                    <div class="col-sm-4">
                        <asp:Label ID="lblCourseId" runat="server" ></asp:Label>
                    </div>
                    <div class="col-sm-1">
                        
                    </div>
                </div>
                    </div>
                <div class="row " style="margin-top: 40px;">
                    <label class="col-sm-2 col-sm-offset-2  control-label">
                        Course Status :
                    </label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlCourseStatus" class="form-control"  runat="server">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem value="true">Approve</asp:ListItem>
                            <asp:ListItem value="false">Decline</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-1">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Select Course Status" ControlToValidate="ddlCourseStatus"
                            ValidationGroup="a" InitialValue="--Select--">*</asp:RequiredFieldValidator>
                    </div>
                </div>
               
                <div class="row" style="margin-top: 20px">
                    <div class=" col-sm-offset-3 col-sm-2">
                        <asp:Button ID="btn_update" class="btn btn-primary btn-block" runat="server" Text="Update"
                            OnClick="btn_update_Click" ValidationGroup="a" />
                    </div>
                   <%-- <div class=" col-sm-2">
                        <asp:Button ID="btn_update" class="btn btn-primary btn-block" runat="server" Text="Update"
                            OnClick="btn_update_Click" ValidationGroup="a" />
                    </div>--%>
                    <div class=" col-sm-2">
                        <asp:Button ID="btn_clear" class="btn btn-primary btn-block" runat="server" Text="Clear"
                            OnClick="btn_clear_Click" />
                    </div>
                </div>

                <div align="center" style="margin-top:20px;">
                    <asp:GridView ID="gridRegistration" runat="server" AutoGenerateColumns="False" DataKeyNames="courseId"
                        OnSelectedIndexChanged="gridRegistration_SelectedIndexChanged" Width="80%" CssClass="table table-responsive"
                        OnPageIndexChanging="gridRegistration_PageIndexChanging" CellPadding="4" ForeColor="#333333"
                        AllowPaging="True" GridLines="None" PageSize="5">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" HeaderText="Select Here" />
                            <asp:BoundField DataField="courseId" HeaderText="Course Id" SortExpression="courseId" />
                             <asp:BoundField DataField="userId" HeaderText="User Id" SortExpression="userId" />
                            <asp:BoundField DataField="fName" HeaderText="First Name" SortExpression="fName" />
                            <asp:BoundField DataField="lName" HeaderText="Last Name" SortExpression="lName" />
                             <asp:BoundField DataField="emailId" HeaderText="Email Id" SortExpression="emailId" />
                            <asp:BoundField DataField="deptName" HeaderText="Department" SortExpression="deptName" />
                            <asp:BoundField DataField="courseLevel" HeaderText="Course Level" SortExpression="courseLevel" />
                            <asp:BoundField DataField="courseNumber" HeaderText="Course Number" SortExpression="courseNumber" />
                            <asp:BoundField DataField="courseName" HeaderText="Course Name" SortExpression="courseName" />
                            <asp:BoundField DataField="courseStatus" HeaderText="Course Status" SortExpression="courseStatus" />
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
    <script src="../js/custom.min.js"></script>

</asp:Content>

