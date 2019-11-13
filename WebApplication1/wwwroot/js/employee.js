//query class for employee page
//author: Minh Nguyen 
//last update 11/11/19
$(function () {//auto run when load employee web page
    //validation the user input
    document.addEventListener("keyup", e => {
        $("#modalstatus").removeClass();//remove any existing css onn divvalid
        if ($("#EmployeeModalForm").valid()) {
            $("#modalstatus").attr("class", "badge badge-success");
            $("#modalstatus").text("data entered is valid");
            $("#actionbutton").prop('disabled', false);
        }
        else {
            $("#modalstatus").attr("class", "badge badge-danger");
            $("#modalstatus").text("fix errors");
            $("#actionbutton").prop('disabled', true);
        }
    });
    //input rule
    $("#EmployeeModalForm").validate({
        rules: {
            TextBoxTitle: { maxlength: 4, required: true, validTitle: true },
            TextBoxFirstname: { maxlength: 25, required: true },
            TextBoxLastname: { maxlength: 25, required: true },
            TextBoxEmail: { maxlength: 40, required: true, email: true },
            TextBoxPhone: { maxlength: 15, required: true }
        },
        errorElement: 'div',
        messages:
        {
            TextBoxTitle:
            {
                required: "required 1-4 chars.", maxlength: "required 1-4 chars.", validTitle: "Mr. Ms. Mrs. or Dr."
            },
            TextBoxFirstname:
            {
                required: "required 1-25 chars.", maxlength: "required 1-25 chars."
            },
            TextBoxLastname:
            {
                required: "required 1-25 chars.", maxlength: "required 1-25 chars."
            },
            TextBoxPhone:
            {
                required: "required 1-15 chars.", maxlength: "required 1-15 chars."
            },
            TextBoxEmail:
            {
                required: "required 1-40 chars.", maxlength: "required 1-40 chars.", email: "need vaild email format"
            }
        }
    }); // EmployeeModalForm.validate
    $.validator.addMethod("validTitle", (value) => {//custom rule 
        return (value === "Mr." || value === "Ms." || value === "Mrs." || value === "Dr.");
    }, "");// .validator.addMethod
    //create a list that show all the employee
    const getAll = async (msg) => {
        try {
            $("#employeeList").text("Finding Employee Information...");
            let response = await fetch(`api/employee`);//ask for data from the server
            if (!response.ok)//check server response
                throw new Error(`Status - ${response.status}, Text - ${response.statusText}`);
            let data = await response.json();
            buildEmployeeList(data,true);
            msg === "" ?
                $("#status").text("Employees Loaded") : $("#status").text(`${msg}`);
            response = await fetch(`api/department`);//ask for data from the server
            if (!response.ok)//check server response
                throw new Error(`Status - ${response.status}, Text - ${response.statusText}`);
            let dep = await response.json();
            sessionStorage.setItem('allDepartments', JSON.stringify(dep));//store Department data
        } catch (error) {
            $("#status").text(error.message);
        }
    }; //getAll
    //prepare for update
    const setupForUpdate = (id, data) => {
        $("#deletebutton").show();//show the delete btn
        $("#actionbutton").val("Update");
        $("#modalTitle").html("<h4>Update Employee</h4>");
        clearModalFields();
        // show employee data for user to modify
        data.map(each => {
            if (each.id === parseInt(id)) {
                $("#TextBoxEmail").val(each.email);
                $("#TextBoxTitle").val(each.title);
                $("#TextBoxFirstname").val(each.firstName);
                $("#TextBoxLastname").val(each.lastName);
                $("#TextBoxPhone").val(each.phoneNo);
                sessionStorage.setItem("Id", each.id);
                sessionStorage.setItem("DepartmentId", each.departmentId);
                sessionStorage.setItem("Timer", each.timer);
                $("#modalstatus").text("Update Data");
                loadDepartmentDDL(each.departmentId.toString());
                $("#theModal").modal("toggle");
            }
        });
    };
    //prepare for add
    const setupForAdd = () => {
        $("#deletebutton").hide();// hide delete btn
        $("#actionbutton").val("Add");//change btn name to Add
        $("#modalTitle").html("<h4>Add Employees</h4>");
        $("#theModal").modal("toggle");
        $("#modalstatus").text("Add new employee");
        loadDepartmentDDL(-1);
        clearModalFields();
    };
    // use to clear txt box and data store in seesion
    const clearModalFields = () => {
        $("#TextBoxTitle").val("");
        $("#TextBoxFirstname").val("");
        $("#TextBoxLastname").val("");
        $("#TextBoxPhone").val("");
        $("#TextBoxEmail").val("");
        sessionStorage.removeItem("Id");
        sessionStorage.removeItem("DepartmentId");
        sessionStorage.removeItem("Timer");
        console.log($("#EmployeeModalForm").validate());
        $("#EmployeeModalForm").validate().resetForm();
    };
    // create dynamic employee List
    const buildEmployeeList = (data, usealldata ) => {
        $("#employeeList").empty();
        //header 
        div = $(`<div class="list-group-item text-white bg-secondary row d-flex" id="status">Employee Info</div>
                    <div class= "list-group-item row d-flex text-center" id="heading">
                    <div class="col-4 h4">Title</div>
                    <div class="col-4 h4">First Name</div>
                    <div class="col-4 h4">Last Name</div>
                </div>`);
        div.appendTo($("#employeeList"));
        usealldata ? sessionStorage.setItem("allemployees", JSON.stringify(data)) : null;
        //allow Add function
        btn = $(`<button class="list-group-item row d-fex" id="0"><div class="col-12 text-left">...click to add employee</div></button>`);
        btn.appendTo($("#employeeList"));
        // employee info
        data.map(each => {
            btn = $(`<button class="list-group-item row d-flex" id="${each.id}">`);
            btn.html(`<div class="col-4" id="employeetitle${each.id}">${each.title}</div>
                        <div class="col-4" id="employeefirstname${each.id}">${each.firstName}</div>
                        <div class="col-4" id="employeelastname${each.id}">${each.lastName}</div>`
            );
            btn.appendTo($("#employeeList"));
        }); // map
    };

    const update = async () => {
        try {
            //set up a new client side instance of Employee
            employee = new Object();
            employee.title = $("#TextBoxTitle").val();
            employee.firstName = $("#TextBoxFirstname").val();
            employee.lastName = $("#TextBoxLastname").val();
            employee.phoneNo = $("#TextBoxPhone").val();
            employee.email = $("#TextBoxEmail").val();
            //we stored these 3 in session storage earlier
            employee.id = sessionStorage.getItem("Id");
            employee.departmentId = $('#ddlDep').val();;
            employee.timer = sessionStorage.getItem("Timer");
            employee.Picture64 = null;
            //send the updated Employee back to server
            let response = await fetch("/api/employee", {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json; charset=utf-8"
                },
                body: JSON.stringify(employee)
            });

            if (response.ok)//check server response
            {
                let data = await response.json();
                getAll(data.msg);
            }
            else
            {
                throw new Error(`Status - ${response.status}, Problem sever side, see server console.`);
            }
            $("#theModal").modal("toggle");
        } catch (error) {
            $("#status").text(error.message);
        }
    };

    const add = async () => {
        try {
            //set up a new client side instance of Employee
            employee = new Object();
            employee.title = $("#TextBoxTitle").val();
            employee.firstName = $("#TextBoxFirstname").val();
            employee.lastName = $("#TextBoxLastname").val();
            employee.phoneNo = $("#TextBoxPhone").val();
            employee.email = $("#TextBoxEmail").val();
            //we stored these 3 in session storage earlier
            employee.id = -1;
            employee.departmentId = $('#ddlDep').val();
            employee.timer = null;
            employee.Picture64 = null;
            
            //send the updated Employee back to server
            //let response = await fetch("/api/employees", {
            let response = await fetch("/api/employee", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json; charset=utf-8"
                },
                body: JSON.stringify(employee)
            });

            if (response.ok) {
                let data = await response.json();
                getAll(data.msg);
            }
            else {
                throw new Error(`Status - ${response.status}, Problem sever side, see server console.`);
            }
            $("#theModal").modal("toggle");
        } catch (error) {
            $("#status").text(error.message);
        }
    };
    const _delete = async () => {
        try {
            
            //send the updated Employee back to server
            //let response = await fetch("/api/employees", {
            let response = await fetch(`/api/employee/${sessionStorage.getItem('Id')}`, {
                method: 'DELETE',
                headers: {
                    "Content-Type": "application/json; charset=utf-8"
                }
            });

            if (response.ok) {
                let data = await response.json();
                getAll(data.msg);
            }
            else {
                throw new Error(`Status - ${response.status}, Problem sever side, see server console.`);
            }
            $("#theModal").modal("toggle");
        }
        catch (error) {
            $("#status").text(error.message);
        }
    };
    //create a dynamic list that show Department data
    const loadDepartmentDDL = (empDep) => {
        html = '';
        $('#ddlDep').empty();
        //take the Department data we store earlier
        let allDepartments = JSON.parse(sessionStorage.getItem('allDepartments'));

        allDepartments.map(dep => html += `<option value="${dep.departmentId}">${dep.departmentName}</option>`);
        $('#ddlDep').append(html);
        $('#ddlDep').val(empDep);
    };

    $("#actionbutton").click(() => {
        $("#actionbutton").val() === "Update" ? update() : add();
    }); // actionbutton click
    $('[data-toggle=confirmation]').confirmation({ rootSelector: '[data-toggle=confirmation]'});
    $('#deletebutton').click(() => _delete());//call _delete when user hit delete btn
    //action listener when user hit employee list
    //create a search function to find employee using their last name
    $("#srch").keyup(() => {
        console.log("srch1");
        let alldata = JSON.parse(sessionStorage.getItem("allemployees"));
        console.log("srch");
        let filtereddata = alldata.filter((emp) => emp.lastName.match(new RegExp($("#srch").val(), 'i')));
        buildEmployeeList(filtereddata, false);
    });
    $("#employeeList").click((e) => {
        clearModalFields();
        $("#modalstatus").attr("class", "");
        if (!e) e = window.event;
        let Id = e.target.parentNode.id;
        if (Id === "employeeList" || Id === "") {
            Id = e.target.id;
        }
        if (Id !== "status" && Id != "heading") {
            let data = JSON.parse(sessionStorage.getItem("allemployees"));
            Id === "0" ? setupForAdd() : setupForUpdate(Id, data);
        } else {
            return false;
        }
    });

    //loadDepartmentDDL("");
    getAll("");
});
