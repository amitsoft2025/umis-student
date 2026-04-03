function continuephase() {
    $("#Instructionview").attr("style", "display:none");
    $("#adminissiontype").attr("style", "display:block");

}
function Prevoius1() {
    $("#Instructionview").attr("style", "display:block");
    $("#adminissiontype").attr("style", "display:none");
    $("#divotp").attr("style", "display:none");
    $("#otpbutton").attr("style", "display:block");
    $("#otpverifybutton").attr("style", "display:none");
    $("#nextbutton").attr("style", "display:none");

}
function nextphase() {
    var AdminissionType = $("#AdminissionType").val();
    var Educationtype = $("#Educationtype").val();
    var Coursetype = $("#Coursetype").val();
    var Streamtype = $("#Streamtype").val();
    var previousStreamtype = $("#iastream").val();
    var boardtype = $("#boardtype").val();
    var stmobileno = $("#stmobileno").val();
    if (AdminissionType == '') {
        showAlert("Please Select  Adminission Type !!");
        return;
    }
    if (Educationtype == '') {
        showAlert("Please Select  Programme   !!");
        return;
    }
    if (boardtype == '') {
        showAlert("Please Select  Previous Board !!");
        return;
    }
    if ($("#iastream").val() == '') {
        showAlert("Please Select Previous Stream !!");
        return;
    }
    if (Coursetype == '') {
        showAlert("Please Select Course !!");
        return;
    }
    if (stmobileno == '') {
        showAlert("Please Enter Mobile no!!");
        return;
    }
    $("#adminissiontype").attr("style", "display:none");
    $("#basicinfo").attr("style", "display:block");
    $("#administype1").val(AdminissionType);
    $("#educationtype1").val(Educationtype);
    $("#coursetype1").val(Coursetype);
    $("#stream1").val(Streamtype);
    $("#prevoiustreamid").val(previousStreamtype);
    $("#prevoiusboardid").val(boardtype);
    $("#mobileno").val(stmobileno);
    $("#mobilenoverified").val(stmobileno);
}
function OTP() {
    var AdminissionType = $("#AdminissionType").val();
    var Educationtype = $("#Educationtype").val();
    var Coursetype = $("#Coursetype").val();
    var Streamtype = $("#Streamtype").val();
    var previousStreamtype = $("#iastream").val();
    var boardtype = $("#boardtype").val();
    var stmobileno = $("#stmobileno").val();
    if (AdminissionType == '') {
        showAlert("Please Select  Adminission Type !!");
        return;
    }
    if (Educationtype == '') {
        showAlert("Please Select  Programme   !!");
        return;
    }
    if (boardtype == '') {
        showAlert("Please Select  Previous Board !!");
        return;
    }
    if ($("#iastream").val() == '') {
        showAlert("Please Select Previous Stream !!");
        return;
    }
    if (Coursetype == '') {
        showAlert("Please Select Course !!");
        return;
    }
    if (stmobileno == '') {
        showAlert("Please Enter Mobile no!!");
        return;
    }
    $("#OTP").val('');
    showloader();
    $.ajax({
        url: "/Home/studentsendotp/",
        data: { MobileNo: stmobileno, Coursetype: Coursetype, sessionid: $("#cseesionid").val() },
        cache: false,
        type: "POST",
        dataType: "json",
        success: function (data) {
            //console.log(data);  
            hideloader();
            showAlert(data.msg);
            if (data.status == true) {
                $("#divotp").attr("style", "display:block");
                $("#otpbutton").attr("style", "display:none");
                $("#otpverifybutton").attr("style", "display:block");
                $("#nextbutton").attr("style", "display:none");
            }
            else {
                $("#divotp").attr("style", "display:none");
                $("#otpbutton").attr("style", "display:block");
                $("#otpverifybutton").attr("style", "display:none");
                $("#nextbutton").attr("style", "display:none");
            }

        },
        error: function (data) {
            hideloader();
        }
    });
    //$("#adminissiontype").attr("style", "display:none");
    //$("#basicinfo").attr("style", "display:block");
    //$("#administype1").val(AdminissionType);
    //$("#educationtype1").val(Educationtype);
    //$("#coursetype1").val(Coursetype);
    //$("#stream1").val(Streamtype);
    //$("#prevoiustreamid").val(previousStreamtype);
    //$("#prevoiusboardid").val(boardtype);

}
function resendOTP() {
    var AdminissionType = $("#AdminissionType").val();
    var Educationtype = $("#Educationtype").val();
    var Coursetype = $("#Coursetype").val();
    var Streamtype = $("#Streamtype").val();
    var previousStreamtype = $("#iastream").val();
    var boardtype = $("#boardtype").val();
    var stmobileno = $("#stmobileno").val();
    if (AdminissionType == '') {
        showAlert("Please Select  Adminission Type !!");
        return;
    }
    if (Educationtype == '') {
        showAlert("Please Select  Programme   !!");
        return;
    }
    if (boardtype == '') {
        showAlert("Please Select  Previous Board !!");
        return;
    }
    if ($("#iastream").val() == '') {
        showAlert("Please Select Previous Stream !!");
        return;
    }
    if (Coursetype == '') {
        showAlert("Please Select Course !!");
        return;
    }
    if (stmobileno == '') {
        showAlert("Please Enter Mobile no!!");
        return;
    }
    showloader();
    $("#OTP").val('');
    $.ajax({
        url: "/Home/studentsendotp/",
        data: { MobileNo: stmobileno, Coursetype: Coursetype, sessionid: $("#cseesionid").val() },
        cache: false,
        type: "POST",
        dataType: "json",
        success: function (data) {
            //console.log(data);
            showAlert(data.msg);
            hideloader();
            if (data.status == true) {
                $("#divotp").attr("style", "display:block");
                $("#otpbutton").attr("style", "display:none");
                $("#otpverifybutton").attr("style", "display:block");
                $("#nextbutton").attr("style", "display:none");
            }
            else {
                $("#divotp").attr("style", "display:none");
                $("#otpbutton").attr("style", "display:block");
                $("#otpverifybutton").attr("style", "display:none");
                $("#nextbutton").attr("style", "display:none");
            }
        },
        error: function (data) {
            hideloader();
            $("#divotp").attr("style", "display:none");
            $("#otpbutton").attr("style", "display:none");
            $("#otpverifybutton").attr("style", "display:none");
            $("#nextbutton").attr("style", "display:none");
        }
    });

}
function VerifyOPT() {
    var AdminissionType = $("#AdminissionType").val();
    var Educationtype = $("#Educationtype").val();
    var Coursetype = $("#Coursetype").val();
    var Streamtype = $("#Streamtype").val();
    var previousStreamtype = $("#iastream").val();
    var boardtype = $("#boardtype").val();
    var stmobileno = $("#stmobileno").val();
    var OTP = $("#OTP").val();
    if (AdminissionType == '') {
        showAlert("Please Select  Adminission Type !!");
        return;
    }
    if (Educationtype == '') {
        showAlert("Please Select  Programme   !!");
        return;
    }
    if (boardtype == '') {
        showAlert("Please Select  Previous Board !!");
        return;
    }
    if ($("#iastream").val() == '') {
        showAlert("Please Select Previous Stream !!");
        return;
    }
    if (Coursetype == '') {
        showAlert("Please Select Course !!");
        return;
    }
    if (stmobileno == '') {
        showAlert("Please Enter Mobile no!!");
        return;
    }
    if (OTP == '') {
        showAlert("Please Enter OTP!!");
        return;
    }
    showloader();
    $.ajax({
        url: "/Home/studentverifyotp/",
        data: { MobileNo: stmobileno, otp: OTP },
        cache: false,
        type: "POST",
        dataType: "json",
        success: function (data) {
            //console.log(data);
            hideloader();
            $("#OTP").val('');
            if (data.status == true) {
                $("#divotp").attr("style", "display:block");
                $("#otpbutton").attr("style", "display:none");
                $("#otpverifybutton").attr("style", "display:none");
                $("#nextbutton").attr("style", "display:block");
                $("#adminissiontype").attr("style", "display:none");
                $("#basicinfo").attr("style", "display:block");
                $("#administype1").val(AdminissionType);
                $("#educationtype1").val(Educationtype);
                $("#coursetype1").val(Coursetype);
                $("#stream1").val(Streamtype);
                $("#prevoiustreamid").val(previousStreamtype);
                $("#prevoiusboardid").val(boardtype);
                $("#mobileno").val(stmobileno);
                $("#mobilenoverified").val(stmobileno);
                //
            }
            else {
                hideloader();
                showAlert(data.msg);
                $("#divotp").attr("style", "display:block");
                $("#otpbutton").attr("style", "display:none");
                $("#otpverifybutton").attr("style", "display:block");
                $("#nextbutton").attr("style", "display:none");
            }

        },
        error: function (data) {
            $("#OTP").val('');
            $("#divotp").attr("style", "display:block");
            $("#otpbutton").attr("style", "display:none");
            $("#otpverifybutton").attr("style", "display:block");
            $("#nextbutton").attr("style", "display:none");
        }
    });
    //$("#adminissiontype").attr("style", "display:none");
    //$("#basicinfo").attr("style", "display:block");
    //$("#administype1").val(AdminissionType);
    //$("#educationtype1").val(Educationtype);
    //$("#coursetype1").val(Coursetype);
    //$("#stream1").val(Streamtype);
    //$("#prevoiustreamid").val(previousStreamtype);
    //$("#prevoiusboardid").val(boardtype);

}
$("#Educationtype").change(function (event) {
    //debugger;
    $('#iastream').find("option").remove();
    $("#iastream").append($("<option></option>").val("").html("--Select Previous Stream--"));
    var res = $(this).val();
    if (res == "")
        res = 0;
    showloader();
    $.ajax({
        url: "/VOC/getprevioustream/",
        data: { id: res },
        cache: false,
        type: "POST",
        dataType: "json",
        success: function (data) {
            hideloader();
            //console.log(data);
            $('#iastream').find("option").remove();
            $("#iastream").append($("<option></option>").val("").html("--Select Previous Stream--"));
            $.each(data.data, function (key, value) {
                $("#iastream").append($("<option></option>").val(value.ID).html(value.QualificationType));
            });
        }
        ,
        error: function (err) {
            // debugger;

            hideloader();
            return false;
        }
    });
});
$("#boardtype").change(function (event) {
    //debugger;
    $('#iastream').find("option").remove();
    $("#iastream").append($("<option></option>").val("").html("--Select Previous Stream--"));
    var res = $(this).val();
    if (res == "")
        res = 0;
    showloader();
    $.ajax({
        url: "/Home/getprevioustream/",
        data: { id: $("#Educationtype").val(), prevoiusboradid: res },
        cache: false,
        type: "POST",
        dataType: "json",
        success: function (data) {
            hideloader();
            //console.log(data);
            $('#iastream').find("option").remove();
            $("#iastream").append($("<option></option>").val("").html("--Select Previous Stream--"));
            $.each(data.data, function (key, value) {
                $("#iastream").append($("<option></option>").val(value.ID).html(value.QualificationType));
            });
        }
        ,
        error: function (err) {
            // debugger;

            hideloader();
            return false;
        }
    });
});
$("#iastream").change(function (event) {
  
    //debugger;
    $('#Coursetype').find("option").remove();
    $("#Coursetype").append($("<option></option>").val("").html("--Select Course--"));
    var res = $("#Educationtype").val();
    if (res == "")
        res = 0;
    showloader();
    $.ajax({
        url: "/VOC/getcousrequlification/",
        data: { id: res, quaid: $(this).val() },
        cache: false,
        type: "POST",
        dataType: "json",
        success: function (data) {
            hideloader();
            $('#Coursetype').find("option").remove();
            $("#Coursetype").append($("<option></option>").val("").html("--Select Course--"));
            $.each(data.data, function (key, value) {
                $("#Coursetype").append($("<option></option>").val(value.CourseCategoryID).html(value.CourseCategory));
            });
        },
        error: function (err) {
            //debugger;

            hideloader();
            return false;
        }
    });
});
$("#Coursetype").change(function (event) {
    return;
    $('#Streamtype').find("option").remove();
    $("#Streamtype").append($("<option></option>").val("").html("--Select Stream--"));
    var res = $(this).val();
    if (res == "")
        res = 0;
    showloader();
    $.ajax({
        url: "/VOC/getstream/",
        data: { id: res },
        cache: false,
        type: "POST",
        dataType: "json",
        success: function (data) {
            hideloader();
            // console.log(data);
            $('#Streamtype').find("option").remove();
            $("#Streamtype").append($("<option></option>").val("").html("--Select Stream--"));
            $.each(data.data, function (key, value) {
                $("#Streamtype").append($("<option></option>").val(value.StreamCategoryID).html(value.streamCategory));
            });

        },
        error: function (err) {
            //debugger;

            hideloader();
            return false;
        }
    });
});


$("#Title").change(function (event) {
    // debugger;
    $('#Cast').find("option").remove();
    $("#Cast").append($("<option></option>").val("").html("--Select Category--"));
    $('#Gender').find("option").remove();
    $("#Gender").append($("<option></option>").val("").html("--Select Gender--"));
    var res = $(this).val();
    if (res == "")
        res = 0;
    $.ajax({
        url: "/Home/Bind_gender/",
        data: { title: res },
        cache: false,
        type: "POST",
        dataType: "json",
        success: function (data) {

            //console.log(data);
            $('#Gender').find("option").remove();
            $("#Gender").append($("<option></option>").val("").html("--Select Gender--"));
            $.each(data, function (key, value) {
                $("#Gender").append($("<option></option>").val(value.CommonId).html(value.Title));
            });
        }
    });
    $('#typeTitle').find("option").remove();
    $("#typeTitle").append($("<option></option>").val("").html("--Select Title--"));
    $('#Ftitle').find("option").remove();
    $("#Ftitle").append($("<option></option>").val("").html("--Select Title--"));
    $.ajax({
        url: "/Home/Bind_ftitle/",
        data: { title: res },
        cache: false,
        type: "POST",
        dataType: "json",
        success: function (data) {

            //console.log(data);
            $('#typeTitle').find("option").remove();
            $("#typeTitle").append($("<option></option>").val("").html("--Select Title--"));
            $('#Ftitle').find("option").remove();
            $("#Ftitle").append($("<option></option>").val("").html("--Select Title--"));
            $.each(data, function (key, value) {
                $("#typeTitle").append($("<option></option>").val(value.CommonId).html(value.Title));
                $("#Ftitle").append($("<option></option>").val(value.CommonId).html(value.Title));
            });
        }
    });
});
$("#Gender").change(function (event) {
    // debugger;
    $('#Cast').find("option").remove();
    $("#Cast").append($("<option></option>").val("").html("--Select Category--"));
    var res = $(this).val();
    if (res == "")
        res = 0;
    $.ajax({
        url: "/VOC/Bind_caste/",
        data: { gender: res },
        cache: false,
        type: "POST",
        dataType: "json",
        success: function (data) {

            //console.log(data);
            $('#Cast').find("option").remove();
            $("#Cast").append($("<option></option>").val("").html("--Select Category--"));
            $.each(data, function (key, value) {
                $("#Cast").append($("<option></option>").val(value.CommonId).html(value.Title));
            });
        }
    });
});
$("#Country").change(function (event) {
    // debugger;
    $('#State').find("option").remove();
    $("#State").append($("<option></option>").val("").html("--Select State--"));
    var res = $(this).val();
    if (res == "")
        res = 0;
    $.ajax({
        url: "/VOC/State_Bind/",
        data: { id: res },
        cache: false,
        type: "POST",
        dataType: "json",
        success: function (data) {

            //console.log(data);
            $('#State').find("option").remove();
            $("#State").append($("<option></option>").val("").html("--Select State--"));
            $.each(data, function (key, value) {
                $("#State").append($("<option></option>").val(value.Value).html(value.Text));
            });
        }
    });
});
$("#PCountry").change(function (event) {
    // debugger;
    $('#PState').find("option").remove();
    $("#PState").append($("<option></option>").val("").html("--Select State--"));
    var res = $(this).val();
    if (res == "")
        res = 0;
    $.ajax({
        url: "/VOC/State_Bind/",
        data: { id: res },
        cache: false,
        type: "POST",
        dataType: "json",
        success: function (data) {

            //console.log(data);
            $('#PState').find("option").remove();
            $("#PState").append($("<option></option>").val("").html("--Select State--"));
            $.each(data, function (key, value) {
                $("#PState").append($("<option></option>").val(value.Value).html(value.Text));
            });
        }
    });
});
$("#firstnameHindi").change(function (event) {
    debugger;
    var res = $(this).val();
    $.ajax({
        url: "/Home/googletranslate/",
        data: { text: res },
        cache: false,
        type: "POST",
        dataType: "json",
        success: function (data) {
            debugger;
            console.log(data);
            console.log(data.data);
            //$(this).val(data.data);
            //$("#firstnameHindi").html(data.data); if (data.data == undefined) {
            if (data.data == undefined) {
                return;
            };
        
            $("#firstnameHindi").val(data.data);
        }
    });
});
$("#middlenameHindi").change(function (event) {
    debugger;
    var res = $(this).val();
    $.ajax({
        url: "/Home/googletranslate/",
        data: { text: res },
        cache: false,
        type: "POST",
        dataType: "json",
        success: function (data) {
            debugger; if (data.data == undefined) {
                return;
            }
            console.log(data);
            console.log(data.data);
            //$(this).val(data.data);
            //$("#firstnameHindi").html(data.data);
            $("#middlenameHindi").val(data.data);
        }
    });
});
$("#lastnameHindi").change(function (event) {
    debugger;
    var res = $(this).val();
    $.ajax({
        url: "/Home/googletranslate/",
        data: { text: res },
        cache: false,
        type: "POST",
        dataType: "json",
        success: function (data) {
            if (data.data == undefined) {
                return;
            }
            console.log(data);
            console.log(data.data);
            //$(this).val(data.data);
            //$("#firstnameHindi").html(data.data);
            $("#lastnameHindi").val(data.data);
        }
    });
});
$("#fathernameHindi").change(function (event) {
    debugger;
    var res = $(this).val();
    $.ajax({
        url: "/Home/googletranslate/",
        data: { text: res },
        cache: false,
        type: "POST",
        dataType: "json",
        success: function (data) {

            console.log(data);
            console.log(data.data); if (data.data == undefined) {
                return;
            }
            //$(this).val(data.data);
            //$("#firstnameHindi").html(data.data);
            $("#fathernameHindi").val(data.data);
        }
    });
});
$("#mothernameHindi").change(function (event) {
    debugger;
    var res = $(this).val();
    $.ajax({
        url: "/Home/googletranslate/",
        data: { text: res },
        cache: false,
        type: "POST",
        dataType: "json",
        success: function (data) {

            console.log(data);
            console.log(data.data);
            //$(this).val(data.data);
            //$("#firstnameHindi").html(data.data);
            if (data.data == undefined)
            {
                return;
            }
            $("#mothernameHindi").val(data.data);
        }
    });
});

$("#FirstNameInHindi").change(function (event) {
    debugger;
    var res = $(this).val();
    $.ajax({
        url: "/Home/googletranslate/",
        data: { text: res },
        cache: false,
        type: "POST",
        dataType: "json",
        success: function (data) {
            debugger;
            console.log(data);
            console.log(data.data);
            //$(this).val(data.data);
            //$("#firstnameHindi").html(data.data);
            if (data.data == undefined) {
                return;
            }
            $("#FirstNameInHindi").val(data.data);
        }
    });
});
$("#MiddleNameInHindi").change(function (event) {
    debugger;
    var res = $(this).val();
    $.ajax({
        url: "/Home/googletranslate/",
        data: { text: res },
        cache: false,
        type: "POST",
        dataType: "json",
        success: function (data) {

            console.log(data);
            console.log(data.data); if (data.data == undefined) {
                return;
            }
            //$(this).val(data.data);
            //$("#firstnameHindi").html(data.data);
            $("#MiddleNameInHindi").val(data.data);
        }
    });
});
$("#LastNameInHindi").change(function (event) {
    debugger;
    var res = $(this).val();
    $.ajax({
        url: "/Home/googletranslate/",
        data: { text: res },
        cache: false,
        type: "POST",
        dataType: "json",
        success: function (data) {

            console.log(data);
            console.log(data.data); if (data.data == undefined) {
                return;
            }
            //$(this).val(data.data);
            //$("#firstnameHindi").html(data.data);
            $("#LastNameInHindi").val(data.data);
        }
    });
});
$("#FatherNameInHindi").change(function (event) {
    debugger;
    var res = $(this).val();
    $.ajax({
        url: "/Home/googletranslate/",
        data: { text: res },
        cache: false,
        type: "POST",
        dataType: "json",
        success: function (data) {

            console.log(data);
            console.log(data.data); if (data.data == undefined) {
                return;
            }
            //$(this).val(data.data);
            //$("#firstnameHindi").html(data.data);
            $("#FatherNameInHindi").val(data.data);
        }
    });
});
$("#MotherNameInHindi").change(function (event) {
    debugger;
    var res = $(this).val();
    $.ajax({
        url: "/Home/googletranslate/",
        data: { text: res },
        cache: false,
        type: "POST",
        dataType: "json",
        success: function (data) {

            console.log(data);
            console.log(data.data); if (data.data == undefined) {
                return;
            }
            //$(this).val(data.data);
            //$("#firstnameHindi").html(data.data);
            $("#MotherNameInHindi").val(data.data);
        }
    });
});
$("#parmanentcheckbox").change(function (event) {
    //debugger;
    //var res11 = $("#parmanentcheckbox").check();
    if ($('#parmanentcheckbox').is(":checked")) {


        if ($("#Address").val() == "") {
            showAlert("Please Enter Address !!");
            $(this).val();
            $(this).prop('checked', false);
            return;
        }
        if ($("#city").val() == "") {
            showAlert("Please Enter City !!");
            $(this).prop('checked', false);
            return;
        }
        //if ($("#pincode").val() == "") {
        //    showAlert("Please Enter PinCode !!");
        //    $(this).prop('checked', false);
        //    return;
        //}
        if ($("#pincode").val() == "") {
            showAlert('Please Enter pincode !!');
            $("#pincode").focus();
            $(this).prop('checked', false);
            return false;
        }
        else {
            if ($("#pincode").val().length != 6) {
                showAlert('pincode should be 6 character  !!');
                $("#pincode").focus();
                $(this).prop('checked', false);
                return false;
            }
        }
        if ($("#Country").val() == "") {
            showAlert("Please Select Country !!");
            $(this).prop('checked', false);
            return;
        }
        if ($("#State").val() == "") {
            showAlert("Please Select State !!");
            $(this).prop('checked', false);
            return;
        }
        $("#PAddress").val($("#Address").val());
        $("#PPinCode").val($("#pincode").val());
        $("#Pcity").val($("#city").val());

        $('#PCountry').val($("#Country").val());
        $("#Pcity").attr('readonly', 'readonly');
        $("#PAddress").attr('readonly', 'readonly');
        $("#PPinCode").attr('readonly', 'readonly');
        $("#PState").attr('readonly', 'readonly');


        $("#PState").append($("<option></option>").val("").html("--Select State--"));
        var res = $("#Country").val()
        if (res == "")
            res = 0;
        $.ajax({
            url: "/VOC/State_Bind/",
            data: { id: res },
            cache: false,
            type: "POST",
            dataType: "json",
            success: function (data) {

                // console.log(data);
                $('#PState').find("option").remove();
                $("#PState").append($("<option></option>").val("").html("--Select State--"));
                $.each(data, function (key, value) {
                    $("#PState").append($("<option></option>").val(value.Value).html(value.Text));
                });
                $('#PState').val($("#State").val());
            }
        });

    }
    else {
        $("#PAddress").val('');
        $("#PPinCode").val('');
        $("#Pcity").val('');
        // $('#PCountry').val('');
        $('#PState').val("");
        $("#Pcity").removeAttr('readonly');
        $("#PAddress").removeAttr('readonly');
        $("#PPinCode").removeAttr('readonly');
        $("#PState").removeAttr('disabled');
        $("#PState").removeAttr('readonly');
        //$("#PState").append($("<option></option>").val("").html("--Select State--"));
    }
});
checkphoto = function () {
    //debugger;
    var _validFileExtensions = [".jpg", ".jpeg", ".bmp", ".gif", ".png"];
    var arrInputs = document.getElementById("photo");
    var oInput = arrInputs;
    var sFileName = oInput.value;
    var sFileNamesize = document.getElementById('photo').files[0].size;;
    if (sFileName.length > 0) {
        var blnValid = false;
        for (var j = 0; j < _validFileExtensions.length; j++) {
            var sCurExtension = _validFileExtensions[j];
            if (sFileName.substr(sFileName.length - sCurExtension.length, sCurExtension.length).toLowerCase() == sCurExtension.toLowerCase()) {
                blnValid = true;
                // break;
            }

        }
        if (!blnValid) {
            oInput.value = "";
            document.getElementById('append-big-btn').value = "";
            showAlert("Sorry, file is invalid, allowed extensions are: " + _validFileExtensions.join(", "));
            return false;
        }
        else {
            if (sFileNamesize > 51200) {
                oInput.value = "";
                document.getElementById('append-big-btn').value = "";
                showAlert("File is too big!");
                return false;
            }
        }
    }
    //var fileSize = document.getElementById('file').files[0].size;
    return true;
}
checksigh = function () {
    // debugger;
    var _validFileExtensions = [".jpg", ".jpeg", ".bmp", ".gif", ".png"];
    var arrInputs = document.getElementById("sign");
    var oInput = arrInputs;
    var sFileName = oInput.value;
    var sFileNamesize = document.getElementById('sign').files[0].size;;
    if (sFileName.length > 0) {
        var blnValid = false;
        for (var j = 0; j < _validFileExtensions.length; j++) {
            var sCurExtension = _validFileExtensions[j];
            if (sFileName.substr(sFileName.length - sCurExtension.length, sCurExtension.length).toLowerCase() == sCurExtension.toLowerCase()) {
                blnValid = true;
                // break;
            }

        }
        if (!blnValid) {
            oInput.value = "";
            document.getElementById('append-big-btn2').value = "";
            showAlert("Sorry, file is invalid, allowed extensions are: " + _validFileExtensions.join(", "));
            return false;
        }
        else {
            if (sFileNamesize > 51200) {
                oInput.value = "";
                document.getElementById('append-big-btn2').value = "";
                showAlert("File is too big!");
                return false;
            }
        }
    }
    //var fileSize = document.getElementById('file').files[0].size;
    return true;
}
function Prevoius() {
    $("#adminissiontype").attr("style", "display:block");
    $("#basicinfo").attr("style", "display:none");
    $("#divotp").attr("style", "display:none");
    $("#otpbutton").attr("style", "display:block");
    $("#otpverifybutton").attr("style", "display:none");
    $("#nextbutton").attr("style", "display:none");
}
function onsave() {


   

    var AdminissionType = $("#AdminissionType").val();
    var Educationtype = $("#Educationtype").val();
    var Coursetype = $("#Coursetype").val();
    var previousStreamtype = $("#iastream").val();
    var boardtype = $("#boardtype").val();
    if (AdminissionType == '') {
        showAlert("Please Select  Adminission Type !!");
        return;
    }
    if (Educationtype == '') {
        showAlert("Please Select  Programme   !!");
        return;
    }
    if (boardtype == '') {
        showAlert("Please Select  Previous Board !!");
        return;
    }
    if ($("#iastream").val() == '') {
        showAlert("Please Select Previous Stream !!");
        return;
    }
    if (Coursetype == '') {
        showAlert("Please Select Course !!");
        return;
    }

    if ($("#Title").val() == "") {
        showAlert('Please Select Title !!');
        $("#Title").focus();
        return false;

    }
    if ($("#firstname").val() == "") {
        showAlert('Please Enter Firstname !!');
        $("#firstname").focus();
        return false;

    }
 
    if ($("#firstnameHindi").val() == "") {
        showAlert('Please Enter Firstname in Hindi!!');
        $("#firstnameHindi").focus();
        return false;
    }
    if ($("#Gender").val() == "") {
        showAlert('Please Select Gender !!');
        $("#Gender").focus();
        return false;
    }
    if ($("#dob").val() == "") {
        showAlert('Please Enter DOB !!');
        $("#dob").focus();
        return false;
    }
    else {
        if ($("#dob").val().length != 10) {
            showAlert('DOB Should Be valid !!');
            $("#dob").focus();
            return false;
        }

    }
    if ($("#Cast").val() == "") {
        showAlert('Please Select Category !!');
        $("#Cast").focus();
        return false;
    }

    if ($("#mobileno").val() == "") {
        showAlert('Please Enter Mobile Number !!');
        $("#mobileno").focus();
        return false;
    }
    else {
        if ($("#mobileno").val().length != 10) {
            showAlert('Mobile Number should be 10 character  !!');
            $("#mobileno").focus();
            return false;
        }
    }
    if ($("#email").val() == "") {
        showAlert('Please Enter Email ID !!');
        $("#email").focus();
        return false;
    }
    else {
        var email = $("#email").val();
        var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (!filter.test(email)) {
            showAlert('Please Enter Valid Email ID !!');
            $("#email").focus();
            return false;
        }
    }
    if ($("#Blood_Group").val() == "") {
        showAlert('Please Select Blood Group !!');
        $("#Blood_Group").focus();
        return;
    }
    if ($("#Nationality").val() == "") {
        showAlert('Please Select Nationality !!');
        $("#Nationality").focus();
        return false;
    }
    if ($("#Religion").val() == "") {
        showAlert('Please Select Religion !!');
        $("#Religion").focus();
        return false;
    }
    if ($("#Religion").val() == "28") {
        if ($("#ReligonOther").val() == "") {
            showAlert('Please Enter Religion !!');
            $("#ReligonOther").focus();
            return false;
        }
    }

    if ($("#Address").val() == "") {
        showAlert('Please Enter Address !!');
        $("#Address").focus();
        return false;
    }
    if ($("#city").val() == "") {
        showAlert('Please Enter  City !!');
        $("#city").focus();
        return;
    }
    if ($("#pincode").val() == "") {
        showAlert('Please Enter pincode !!');
        $("#pincode").focus();
        return false;
    }
    else {
        if ($("#pincode").val().length != 6) {
            showAlert('pincode should be 6 character  !!');
            $("#pincode").focus();
            return false;
        }
    }
    if ($("#Country").val() == "") {
        showAlert('Please Select Country !!');
        $("#Country").focus();
        return false;
    }

    if ($("#State").val() == "") {
        showAlert('Please Select State !!');
        $("#State").focus();
        return false;
    }
    if ($("#PAddress").val() == "") {
        showAlert('Please Enter Permanent  Address !!');
        $("#PAddress").focus();

        return false;
    }
    if ($("#Pcity").val() == "") {
        showAlert('Please Enter Permanent  City !!');
        $("#Pcity").focus();
        return;
    }
    if ($("#PPinCode").val() == "") {
        showAlert('Please Enter Permanent  Pincode !!');
        $("#PPinCode").focus();
        return false;
    }
    else {
        if ($("#PPinCode").val().length != 6) {
            showAlert('pincode should be 6 character  !!');
            $("#PPinCode").focus();
            return false;
        }
    }
    if ($("#PCountry").val() == "") {
        showAlert('Please Select Permanent  Country !!');
        $("#PCountry").focus();
        return false;
    }
    if ($("#PState").val() == "") {
        showAlert('Please Select Permanent  State !!');
        $("#PState").focus();
        return false;
    }


    if ($("#typeTitle").val() == "") {
        showAlert('Please Select  Father title');
        $("#typeTitle").focus();
        return false;
    }
    if ($("#fathername").val() == "") {
        showAlert('Please Enter Father Name !!');
        $("#fathername").focus();
        return false;
    }
    if ($("#fathernameHindi").val() == "") {
        showAlert('Please Enter Father Name in Hindi!!');
        $("#fathernameHindi").focus();
        return false;
    }
    if ($("#fathermobile").val() != "") {
        if ($("#fathermobile").val().length != 10) {
            showAlert('Father Mobile Number should be 10 character  !!');
            $("#fathermobile").focus();
            return false;
        }
    }
    if ($("#fatheremail").val() != "") {
        var email = $("#fatheremail").val();
        var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (!filter.test(email)) {
            showAlert('Please Enter Valid Email ID for Father !!');
            $("#fatheremail").focus();
            return false;
        }

    }


  

    if ($("#mothername").val() == "") {
        showAlert('Please Enter Mother Name !!');
        $("#mothername").focus();
        return false;
    }
    if ($("#mothernameHindi").val() == "") {
        showAlert('Please Enter Mother Name in Hindi!!');
        $("#mothernameHindi").focus();
        return false;
    }
    if ($("#mothermobile").val() != "") {
        if ($("#mothermobile").val().length != 10) {
            showAlert('Mother Mobile Number should be 10 character  !!');
            $("#mothermobile").focus();
            return false;
        }
    }
 
    if ($("#motheremail").val() != "") {
        var email = $("#motheremail").val();
        var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (!filter.test(email)) {
            showAlert('Please Enter Valid Email ID for Mother !!');
            $("#motheremail").focus();
            return false;
        }

    }
  
    if ($("#photo").val() == "") {
        showAlert('Please Select photo !!');
        $("#photo").focus();
        return false;
    }
    if ($("#sign").val() == "") {
        showAlert('Please Select Signature !!');
        $("#sign").focus();
        return false;
    } if ($("#checkAgree").prop("checked") == false) {
        showAlert("Please agree to terms and conditions !!");
        $("#checkAgree").focus();
        return false;

    }
    if (window.FormData !== undefined) {

        var fileUpload = $("#photo").get(0);
        var files = fileUpload.files;
        var fileData = new FormData();
        for (var i = 0; i < files.length; i++) {
            fileData.append("photo", files[i]);
        }
        var fileUpload2 = $("#sign").get(0);
        var files2 = fileUpload2.files;
        for (var i = 0; i < files2.length; i++) {
            fileData.append("sign", files2[i]);
        }
        fileData.append('firstname', $("#firstname").val());
        fileData.append('middlename', $("#middlename").val());
        fileData.append('lastname', $("#lastname").val());
        fileData.append('Gender', $("#Gender").val());
        fileData.append('dob', $("#dob").val());
        fileData.append('Cast', $("#Cast").val());
        fileData.append('Blood_Group', $("#Blood_Group").val());
        fileData.append('mobileno', $("#mobileno").val());
        fileData.append('email', $("#email").val());
        fileData.append('Address', $("#Address").val());
        fileData.append('pincode', $("#pincode").val());
        fileData.append('Country', $("#Country").val());
        fileData.append('State', $("#State").val());
        fileData.append('PAddress', $("#PAddress").val());
        fileData.append('Pcity', $("#Pcity").val());
        fileData.append('city', $("#city").val());
        fileData.append('PPinCode', $("#PPinCode").val());
        fileData.append('PCountry', $("#PCountry").val());
        fileData.append('PState', $("#PState").val());
        fileData.append('fathermobile', $("#fathermobile").val());
        fileData.append('fatheremail', $("#fatheremail").val());
        fileData.append('fathername', $("#fathername").val());
        fileData.append('fatherqulification', $("#fatherqulification").val());
        fileData.append('fatheroccupation', $("#fatheroccupation").val());
        fileData.append('mothername', $("#mothername").val());
        fileData.append('mothermobile', $("#mothermobile").val());
        fileData.append('motheremail', $("#motheremail").val());
        fileData.append('motherqulicafication', $("#motherqulicafication").val());
        fileData.append('motheroccupation', $("#motheroccupation").val());
        fileData.append('administype1', $("#administype1").val());
        fileData.append('educationtype1', $("#educationtype1").val());
        fileData.append('coursetype1', $("#coursetype1").val());
        fileData.append('stream1', $("#stream1").val());
        fileData.append('Guardianname', $("#Guardianname").val());
        fileData.append('Guardianmobile', $("#Guardianmobile").val());
        fileData.append('Guardianrelation', $("#Guardianrelation").val());
        fileData.append('title', $("#Title").val());
        fileData.append('ftitle', $("#typeTitle").val());
        fileData.append('cseesionid', $("#cseesionid").val());
        fileData.append('Nationality', $("#Nationality").val());
        fileData.append('Religion', $("#Religion").val());
        fileData.append('MotherTongue', $("#MotherTongue").val());
        fileData.append('ishandicapped', $("input[name='ishandicapped']:checked").val());
        fileData.append('isex_service_man', $("input[name='isex_service_man']:checked").val());
        fileData.append('is_ncc_candidate', $("input[name='is_ncc_candidate']:checked").val());
        fileData.append('aadharno', '');
        fileData.append('prevoiustreamid', $("#prevoiustreamid").val());
        fileData.append('prevoiusboardid', $("#prevoiusboardid").val());
        //  debugger;
        var tttt = document.getElementById("firstnameHindi").innerHTML;

        fileData.append('FirstNameInHindi', $("#firstnameHindi").val());
        fileData.append('MiddleNameInHindi', $("#middlenameHindi").val());
        fileData.append('LastNameInHindi', $("#lastnameHindi").val());
        fileData.append('ReligonOther', $("#ReligonOther").val());
        fileData.append('IsSports', $("input[name='IsSports']:checked").val());
        fileData.append('IsStaff', $("input[name='IsStaff']:checked").val());
        fileData.append('FatherNameInHindi', $("#fathernameHindi").val());
        fileData.append('MotherNameInHindi', $("#mothernameHindi").val());


        $("#privious").attr("style", "display:none");
        $("#btnUpload").attr("style", "display:none");
        fileData.append('is_GEW', $("input[name='is_GEW']:checked").val());
        var parmanentcheckbox1 = "False";
        if ($('#parmanentcheckbox').is(":checked")) {
            parmanentcheckbox1 = "True";
        }
        fileData.append('is_permanentaddress', parmanentcheckbox1);

        showloader();
        $.ajax({
            url: '/VOC/UploadSaveFiles',
            type: "POST",
            contentType: false, // Not to set any content header
            processData: false, // Not to process data
            data: fileData,
            success: function (result) {
                //debugger;
                //console.log(result);
                if (result.status == true) {
                    $("#adminissiontype").attr("style", "display:none");
                    $("#basicinfo").attr("style", "display:none");
                    $("#privious").attr("style", "display:none");
                    $("#btnUpload").attr("style", "display:none");
                    $("#welcome").attr("style", "display:block");
                    $("#welnamename").html(result.FirstName);
                    $("#applicationno").html(result.ApplicationNo);
                    $("#welMobileno").html(result.MobileNo);
                    $("#showpassword").html(result.Password);
                    hideloader();
                    //  showAlert(result);
                }
                else {
                    $("#adminissiontype").attr("style", "display:none");
                    $("#basicinfo").attr("style", "display:block");
                    $("#welcome").attr("style", "display:none");
                    $("#privious").attr("style", "display:block");
                    $("#btnUpload").attr("style", "display:block");
                    showAlert(result.Message);
                    hideloader();
                }
                return false;
            },
            error: function (err) {
                //debugger;
                showAlert(err.statusText);
                $("#adminissiontype").attr("style", "display:none");
                $("#basicinfo").attr("style", "display:block");
                $("#welcome").attr("style", "display:none");
                $("#privious").attr("style", "display:block");
                $("#btnUpload").attr("style", "display:block");
                hideloader();
                return false;
            }
        });
    }
    else {
        showAlert("FormData is not supported.");
        $("#adminissiontype").attr("style", "display:none");
        $("#basicinfo").attr("style", "display:block");
        $("#welcome").attr("style", "display:none");
        $("#privious").attr("style", "display:block");
        $("#btnUpload").attr("style", "display:block");; hideloader();
        return false;
    }
}