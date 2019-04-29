<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="course-list.aspx.cs" Inherits="course_list" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="css/pagination.css" rel="stylesheet" type="text/css" />
    <link href="/vendors/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" />

    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <style type="text/css">
        .row {
            padding-left: 10px;
            padding-right: 10px;
            padding: 1px;
        }

    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="x_panel">
            <div class="x_title">
                <h2>Course List</h2>
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
                <div>
                    <div class="row form-group " style="padding-top: 20px;">
                        <label style="text-align: right;" class="col-sm-3 col-sm-offset-1  control-label">
                            Search By :</label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txt_name" placeholder="Course Name" class="form-control"
                                runat="server"></asp:TextBox>
                            <%--<ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="LowercaseLetters, UppercaseLetters, Custom"
                                TargetControlID="txt_name" ValidChars=". @ _ " />--%>
                        </div>
                        <div class="col-sm-1">
                        </div>
                    </div>

                    <div class="ln_solid"></div>
                    <div class="row form-group ">
                        <div class=" col-sm-offset-1 col-sm-1">
                        </div>
                        <div class=" col-sm-3">
                            <asp:Button ID="btn_preview" runat="server" CssClass="btn btn-primary btn-block btn1"
                                Text="Preview" OnClick="btn_preview_Click" />
                        </div>
                        <div class=" col-sm-3">
                            <%-- <asp:Button ID="btn_export" Text="Export" runat="server" CssClass="btn btn-primary btn-block btn1"
                                OnClick="btn_export_Click" />--%>
                            <a href="#" data-toggle="modal" class="btn btn-primary btn-block btn1" title="Advance Search" tabindex="5" data-target="#myModal">Advance Search</a>
                        </div>
                         <div class=" col-sm-3">
                             <a href="#" data-toggle="modal" class="btn btn-primary btn-block btn1" title="Add Course" tabindex="5" data-target="#AddCourseModal">Add Course</a>
                        </div>
                    </div>
                </div>

                <div style="height: 20px; width: 100%">
                    <asp:Label ID="lbl_rowcount" CssClass="control-label col-sm-4" runat="server" Text=""></asp:Label>
                    <asp:Label ID="lbl_msg" CssClass="control-label col-sm-10" runat="server" Text=""></asp:Label>
                </div>
                <div>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" GridLines="None" AllowPaging="True" CssClass="table table-responsive"
                        OnDataBound="GridView1_DataBound" Width="100%" DataKeyNames="courseId" 
                        OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="15" OnRowCommand="GridView1_RowCommand"
                        AllowSorting="True">
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <asp:CommandField ShowSelectButton="false" />
                            <asp:TemplateField HeaderText="More Details">
                                <ItemTemplate>
                                    <asp:HyperLink ID="Details" Target="_blank" runat="server" NavigateUrl='<%# Eval("courseId", "course-details.aspx?id={0}") %>'
                                        CssClass="bodytext">More Info...</asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="courseId" HeaderText="Course Id" SortExpression="courseId" />                           
                            <asp:BoundField DataField="deptName" HeaderText="Department" SortExpression="deptName" />
                            <asp:BoundField DataField="courseLevel" HeaderText="Course Level" SortExpression="courseLevel" />
                            <asp:BoundField DataField="courseNumber" HeaderText="Course Number" SortExpression="courseNumber" />
                            <asp:BoundField DataField="courseName" HeaderText="Course Name" SortExpression="courseName" />
                            <asp:BoundField DataField="status" HeaderText="Course Status" SortExpression="status" />
                            <asp:BoundField DataField="credits" HeaderText="Credits" SortExpression="credits" />
                            <asp:BoundField DataField="term" HeaderText="Term" SortExpression="term" />
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
                </div>
            </div>
        </div>
        <!-- Modal -->
        <div class="modal fade" id="myModal" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            &times;</button>
                        <h4 class="modal-title">Advance Search</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-5 col-md-offset-1">
                                <div class="form-group">
                                    <span class="leftalign">Department</span>
                                    <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control">                                       
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
                                    <span class="leftalign">Course Status</span>
                                    <asp:DropDownList ID="ddl_cdfLevel" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="Select">Select an Option</asp:ListItem>
                                        <asp:ListItem Value="1">Approved</asp:ListItem>
                                        <asp:ListItem Value="0">Declined</asp:ListItem>                                        
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        
                
                        <div class="row" style="margin-top: 20px;">
                            <div class="form-group">
                                <div class="col-md-4 col-md-offset-2">
                                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary btn-block btn1"
                                        Text="Preview" OnClick="btn_advance_preview_Click" />
                                </div>
                                <div class="col-md-4">
                                    <asp:Button ID="btn_clear" runat="server" CssClass="btn btn-primary btn-block btn1"
                                        Text="Clear" OnClick="btn_clear_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

         <!-- Modal Add Course -->
        <div class="modal fade" id="AddCourseModal" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            &times;</button>
                        <h4 class="modal-title">Add Course</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-6 col-md-offset-3">
                                <div class="form-group">
                                    <span class="leftalign">Course Number</span>
                                  <asp:TextBox class="form-control" ID="txtCourseNumber" runat="server" placeholder="Course Number"
                            MaxLength="100"></asp:TextBox>
                                   
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 col-md-offset-3">
                                <div class="form-group">
                                    <span class="leftalign">Course Name</span>
                                  <asp:TextBox class="form-control" ID="txtCourseName" runat="server" placeholder="Course Name"
                            MaxLength="100"></asp:TextBox>
                                   
                                </div>
                            </div>
                        </div>
                          <div class="row">
                            <div class="col-md-6 col-md-offset-3">
                                <div class="form-group">
                                    <span class="leftalign">Course Level</span>
                                <asp:DropDownList ID="ddlCLevel" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="Select">Select an Option</asp:ListItem>
                                        <asp:ListItem Value="Under-Graduate">Under-Graduate</asp:ListItem>
                                        <asp:ListItem Value="Graduate">Graduate</asp:ListItem>
                                    </asp:DropDownList>
                                   
                                </div>
                            </div>
                        </div>
                          <div class="row">
                            <div class="col-md-6 col-md-offset-3">
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
                            <div class="col-md-6 col-md-offset-3">
                                <div class="form-group">
                                    <span class="leftalign">Department</span>
                                    <asp:DropDownList ID="ddlCDepartment" runat="server" CssClass="form-control" >                                       
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                          <div class="row">
                            <div class="col-md-6 col-md-offset-3">
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
                        </div>
                
                        <div class="row" style="margin-top: 20px;">
                            <div class="form-group">
                                <div class="col-md-4 col-md-offset-4">
                                   <asp:Button ID="Button2" runat="server" CssClass="btn btn-primary btn-block btn1"
                                Text="Add Course" OnClick="btnAddCourse_Click" />
                                </div>
                               
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <!-- Custom Theme Scripts -->
    <script src="js/custom.min.js"></script>

    <script type="text/javascript">
        function UserApproved() {
            return confirm("Are you sure you want to Approved this user?");
        }
        function UserActive() {
            return confirm("Are you sure you want to Active this user?");
        }
        function UserDeactive() {
            return confirm("Are you sure you want to Deactive this user?");
        }
    </script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script>
        $(function () {
            $("#ctl00_ContentPlaceHolder1_txt_from").datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: "dd/mm/yy",
                yearRange: "-90:+00"
            });

            $("#ctl00_ContentPlaceHolder1_txt_to").datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: "dd/mm/yy",
                yearRange: "-90:+00"
            });

        });
    </script>
</asp:Content>

