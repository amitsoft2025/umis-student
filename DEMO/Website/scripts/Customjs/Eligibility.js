
    $(document).ready(function () {

        $("#EducationType").change(function (event) {
            $('#CourseCategoryID').find("option").remove();
            $("#CourseCategoryID").append($("<option></option>").val("").html("--Select Course--"));
            var res = $(this).val();
            if (res == "")
                res = 0;
            $.ajax({
                url: "/Home/getcousre/",
                data: { id: res },
                cache: false,
                type: "POST",
                dataType: "json",
                success: function (data) {

                    console.log(data);
                    $('#CourseCategoryID').find("option").remove();
                    $("#CourseCategoryID").append($("<option></option>").val("").html("--Select Course--"));
                    $.each(data.data, function (key, value) {
                        $("#CourseCategoryID").append($("<option></option>").val(value.CourseCategoryID).html(value.CourseCategory));
                    });
                }
            });
        });
        //Qualification
        $("#Qualification").change(function (event) {
            debugger;
            var eduid = $("#EducationType").val();
            if (eduid == "")
                eduid = 0;
            var courseid = $("#CourseCategoryID").val();
            if (courseid == "")
                courseid = 0;
            var quali = $("#Qualification").val();
            if (quali == "")
        quali = 0;
            $.ajax({
                url: "/Administrator/Home/getpercentage/?eduid=" + eduid + "&courseid=" + courseid + "&quali=" + quali,
               // data: { eduid: eduid, courseid: courseid,quali: quali },
                cache: false,
                type: "POST",
                dataType: "json",
                success: function (data) {

                    console.log(data);
                    if (data == null) {
                        $("#Percentage").val('');
                        $("#ID").val('');
                        showAlert('no record found');
                    }
                    else {
                        $("#Percentage").val(data.Percentage);
                        $("#ID").val(data.ID);
                    }
                },
                Error: function () {

                    console.log(data);
                    showAlert("coming in error");
                }
            });
        });
    });

function Onlynumericvalue(textbox) {
    textbox.value = textbox.value.replace(/[^0-9.]/g, ''); textbox.value = textbox.value.replace(/(\..*)\./g, '$1');
}

function submitapplication() {
    //debugger;
    if (parseInt($("#Percentage").val()) > 100) {
        $("#Percentage").val("");
        showAlert('Percentage should be less then 100 ');
        return;
    }
    if (parseInt($("#Percentage").val()) <= 0) {
        $("#Percentage").val("");
        showAlert('Percentage should be greater then 0 ');
        return;
    }
    var ID = $("#ID").val();
    //showAlert(ID);
    var eduid = $("#EducationType").val();
    if (eduid == "")
        eduid = 0;
    var courseid = $("#CourseCategoryID").val();
    if (courseid == "")
        courseid = 0;
    var quali = $("#Qualification").val();
    if (quali == "")
        quali = 0;
    var Percentage = $("#Percentage").val();
    

    var obj = {
        ID: ID,
        EducationType: eduid,
        CourseCategoryID: courseid,
        QualificationTypeID: quali ,
        Percentage: Percentage,       
    };
    //console.log(obj);
    showloader();
    $.ajax({
        url: "/Administrator/Home/Savepercentage",
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            hideloader();
           // console.log(result);
            if (result.Status) {
                showAlert(result.Msg);
                location.replace('/Administrator/Home/EligibilityCreteriaList');
            }
            else {
                showAlert("Could'not perform edit and insert");
                
            }
            // window.location.reload();

        },
        error: function (errormessage) {
            hideloader();
            showAlert(result);            
            showAlert(errormessage.responseText);
        }


    });
}