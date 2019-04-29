<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="allstudent-details.aspx.cs" Inherits="allstudent_details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="row">
            <div class="col-sm-6">
                <%--User Information--%>
                <div class="x_panel">
                    <div class="x_title">
                        <h2>User Information</h2>
                        <ul class="nav navbar-right panel_toolbox">
                            <li>
                                <asp:LinkButton ID="btnEditInfo" runat="server" Text="Edit" Font-Bold="true" OnClick="btnEditInfo_Click" /></li>
                            <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                            </li>
                            <li><a class="close-link"><i class="fa fa-close"></i></a>
                            </li>
                        </ul>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content">
                        <div class="form-horizontal" role="form" runat="server">
                            <div id="div_msg" runat="server" class="" style="text-align: center; margin-top: 10px;">
                            </div>
                            <div class="x_panel">
                                <div class="table-responsive text-nowrap">
                                    <table class="table table-striped">
                                        <thead>
                                            <tr>
                                                <td>
                                                    <label>Column Names</label></td>
                                                <td>
                                                    <label>Details</label></td>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <label>
                                                        Name</label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td>
                                                    <label>
                                                        User Type</label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblUsertype" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        Email Id</label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblEmailId" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        Password</label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPassword" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td>
                                                    <label>
                                                        Academic Level</label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblAcademicLevel" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        Street Number</label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblStreetNo" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        State</label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblState" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        City</label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCity" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        Zipcode</label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblZipcode" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        Contact Number</label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblContactNo" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-sm-6">
                <%--Course Information--%>
                <div class="x_panel">
                    <div class="x_title">
                        <h2>Course Information</h2>
                        <ul class="nav navbar-right panel_toolbox">
                            <li>
                                <asp:LinkButton ID="btnEditCourse" runat="server" Text="Edit" Font-Bold="true" OnClick="btnEditCourse_Click" /></li>
                            <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                            </li>
                            <li><a class="close-link"><i class="fa fa-close"></i></a>
                            </li>
                        </ul>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content">
                        <div class="form-horizontal" role="form" runat="server">
                            <div id="div1" runat="server" class="" style="text-align: center; margin-top: 10px;">
                            </div>
                            <div class="x_panel">
                                <div class="table-responsive text-nowrap">
                                    <table class="table table-striped">
                                        <thead>
                                            <tr>
                                                <td>
                                                    <label>Column Names</label></td>
                                                <td>
                                                    <label>Details</label></td>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <label>
                                                        Course Number</label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCourseNumber" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        Course Name</label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCourseName" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        Course Level</label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCourseLevel" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        Term</label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblTerm" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
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
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gridCourseStatus" runat="server" AutoGenerateColumns="false"
                            AllowPaging="True" CssClass="table"
                            Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="gridCourseStatus_RowCommand" OnPageIndexChanging="gridCourseStatus_PageIndexChanging">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="#F7F6F3" VerticalAlign="Top" ForeColor="#333333" />
                            <Columns>
                                <asp:BoundField DataField="courseId" HeaderText="Course Id" SortExpression="courseId" />
                                <asp:BoundField DataField="userId" HeaderText="User Id" SortExpression="userId" />
                                <asp:BoundField DataField="fName" HeaderText="First Name" SortExpression="fName" />
                                <asp:BoundField DataField="lName" HeaderText="Last Name" SortExpression="lName" />
                                <asp:BoundField DataField="emailId" HeaderText="Email Id" SortExpression="emailId" />
                                <asp:BoundField DataField="courseLevel" HeaderText="Course Level" SortExpression="courseLevel" />
                                <asp:BoundField DataField="courseNumber" HeaderText="Course Number" SortExpression="courseNumber" />
                                <asp:BoundField DataField="courseName" HeaderText="Course Name" SortExpression="courseName" />
                                <asp:BoundField DataField="courseStatus" HeaderText="Course Status" SortExpression="courseStatus" />
                                <asp:TemplateField HeaderText="Approve/Decline">
                                    <ItemTemplate>
                                        <asp:Button ID="btnApprove" CssClass="btn btn-success btn-sm" Visible='<%#  (Eval("courseStatus").ToString()) == "Decline" %>'
                                            runat="server" Text="Approve" OnClientClick="if ( ! CourseApproved()) return false;"
                                            CommandArgument='<%# Eval("userId") + "," +  Eval("courseId")%>' CommandName="false" />

                                        <asp:Button ID="btnDecline" CssClass="btn btn-success btn-sm" Visible='<%#  (Eval("courseStatus").ToString()) == "Approved" %>'
                                            runat="server" Text="Decline" OnClientClick="if ( ! CourseDecline()) return false;"
                                            CommandArgument='<%# Eval("userId") + "," +  Eval("courseId")%>' CommandName="true" />
                                        <%--<asp:Label ID="lbl_approve" CssClass="" Visible='<%#  (Eval("courseStatus").ToString()) == "Approved" %>'
                                                             runat="server" Text="Approved"></asp:Label>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

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
                <div id="divError" runat="server" class="" style="text-align: center; margin-top: 10px;"></div>
                <div class="form-horizontal" role="form" runat="server">
                    <div class="row">
                        <div class="col-sm-12">
                            <label class="control-label">
                                Message :
                   
                            </label>

                            <asp:TextBox ID="txtMessage" TextMode="MultiLine" CssClass="form-control" runat="server" Columns="50"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="row" style="margin-top: 10px">
                    <div class=" col-sm-offset-4 col-sm-4">
                        <asp:Button ID="btnSave" class="btn btn-primary btn-block" runat="server" Text="Save"
                            OnClick="btnSave_Click" ValidationGroup="a" />
                    </div>
                </div>
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




