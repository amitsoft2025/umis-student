
$(document).ready(function () {
    if ($('#Qualification').val() == art || $('#Qualification').val() == sci || $('#Qualification').val() == comm) {
        $('#SubjectTable').removeAttr("style");
        $("#SubjectTable").attr("style", "display: inline;");

    }
    else {
        $('#SubjectTable').removeAttr("style");
        $("#SubjectTable").attr("style", "display: none;");
    }
    $('#Qualification').change(function () {
        debugger;
        var res = $('#Qualification').val();
        if (res == "")
            res = 0;
        $.ajax({
            url: "/StudentLLB/HomeL/SubjectTable/",
            data: { id: res },
            cache: false,
            type: "POST",
            dataType: "json",
            success: function (result) {

                console.log(result);
                if (result) {
                    //showAlert(result);
                    view = true;
                    $('#SubjectTable').removeAttr("style");
                    $("#SubjectTable").attr("style", "display: inline;");
                    //$('#Percentage').attr('readonly', true);
                    // $('#Perr').html('Please fill subject detail ,Aggregate  Percentage will automatic Calculate');
                }
                else {
                    view = false;
                    $('#SubjectTable').removeAttr("style");
                    $("#SubjectTable").attr("style", "display: none;");
                    //  $('#Percentage').removeAttr("readonly");
                    //  $('#Perr').html(' ');
                }

            }
        });

    });
    $('.MarksObtain').change(function () {
        //$(".subper").each(function () {
        debugger;
        var tr = $(this).closest("tr");
        var tm = tr.find("#TotalMarks").val();
        if ($(this).val() == "0" || $(this).val() == "") {
            showAlert('Please Enter obtained Marks !!!');
            tr.find("#SubjectPercentage").val('');
            $(this).val('');
            $(this).focus();
            return;
        }
        else if (parseInt($(this).val()) >= tm) {
            showAlert('Obtained Marks should be less than Total marks !!!');
            $(this).val('');
            tr.find("#SubjectPercentage").val('');
            $(this).focus();
            return;
        }
        else {
            var per = (parseInt($(this).val()) * 100) / tm;
            var percentage = per.toPrecision(4);
            tr.find("#SubjectPercentage").val(percentage);
        }
        //var paperTotalMarks = $("#TotalMarks").val();
        //var paperMarksObtain = $("#MarksObtain").val();
        //var Percentage = $("#SubjectPercentage ").val();
        //if (parseInt($("#MarksObtain").val()) > parseInt($("#TotalMarks").val())) {
        //    $("#SubjectPercentage").val("");
        //    $("#MarksObtain").val("");
        //    showAlert('Paper Obtain Marks  should be less Paper Total Marks !!');
        //    // $("#btn").attr("style", "display: none;");
        //    return;
        //}
        //paperTotalMarks = (paperTotalMarks == "" ? "0" : paperTotalMarks);
        //paperMarksObtain = (paperMarksObtain == "" ? "0" : paperMarksObtain);
        //Percentage = (paperMarksObtain * 100.00 / paperTotalMarks);
        //Percentage = (Percentage == Infinity ? "0" : Percentage);
        //if (isNaN(Percentage)) {
        //    Percentage = "0";
        //}
        //Percentage = (Percentage == undefined ? "0" : Percentage);
        //$("#SubjectPercentage").val(parseFloat(Percentage).toFixed(2));

        //if ($('#SubjectPercentage').val() == "" || $('#SubjectPercentage').val() == "0") {

        //    //$('#Berr').html("Please select a Qualification  !!");
        //    showAlert('Please Enter Honours Percentage   !!');
        //    return;
        //}




    });
});

function percheck() {
    if (parseInt($("#Percentage").val()) > 99) {
        $("#Percentage").val("");
        showAlert('Percentage should be less then 100 ');
        // $("#btn").attr("style", "display: none;");
        return;
    }
    else {
        $('#btn').removeAttr("style");
        $("#btn").attr("style", "display: inline;");
    }
    if (parseInt($("#Percentage").val()) <= 0) {
        $("#Percentage").val("");
        showAlert('Percentage should be greater then 0 ');

        return;
    }


}

function Onlynumericvalue(textbox) {
    textbox.value = textbox.value.replace(/[^0-9.]/g, ''); textbox.value = textbox.value.replace(/(\..*)\./g, '$1');
}
function OnlyIntvalue(textbox) {
    textbox.value = textbox.value.replace(/[^0-9]/g, ''); textbox.value = textbox.value.replace(/(\..*)\./g, '$1');
}


function resetapplication() {
    $("#Qualification").val("");
    $("#UniversityName").val("");
    $("#Percentage").val("");
    $("#PassingYear").val("");
    $("#file").val("");
    $("#RollNo").val("");
    $("#append-big-btn").val("");
}

check = function () {
    debugger;
    var _validFileExtensions = [".jpg", ".jpeg", ".bmp", ".gif", ".png", ".pdf"];
    var arrInputs = document.getElementsByTagName("input");
    for (var i = 0; i < arrInputs.length; i++) {
        var oInput = arrInputs[i];
        if (oInput.type == "file") {
            var sFileName = oInput.value;
            var sFileNamesize = document.getElementById('file').files[0].size;
            if (sFileName.length > 0) {
                var blnValid = false;
                for (var j = 0; j < _validFileExtensions.length; j++) {
                    var sCurExtension = _validFileExtensions[j];
                    if (sFileName.substr(sFileName.length - sCurExtension.length, sCurExtension.length).toLowerCase() == sCurExtension.toLowerCase()) {
                        blnValid = true;
                    }
                }
                if (!blnValid) {
                    oInput.value = "";
                    document.getElementById("append-big-btn").value = "";
                    showAlert("Sorry, file is invalid ");
                    return false;
                }
                else {
                    if (sFileNamesize > 500000) {
                        oInput.value = "";
                        document.getElementById("append-big-btn").value = "";
                        showAlert("File is too big!");
                        break;
                    }
                }

            }
            var fileSize = document.getElementById('file').files[0].size;

        }
    }

    return true;
}

function submitapplication() {
    debugger;
    var ID = $("#hid").val();
    //showAlert(ID);
    var Qualification = $("#Qualification").val();
    var UniversityName = $("#UniversityName").val();
    var Percentage = $("#Percentage").val();
    var PassingYear = $("#PassingYear").val();
    var hfile = $("#hfile").val();
    var file1 = $("#file").val();
    var RollNo = $("#RollNo").val();
    var FileURl;
    var file;
    if ($("#file").val() == null) {

        FileURl = $("#hfile").val();

        file = $(".link").attr("href");
    }
    else {
        var str = $("#file").val().split('\\');
        FileURl = str.pop();
        file = $(".link").attr("href");
    }


    //showAlert(file);
    if (Qualification == "") {

        //$('#Berr').html("Please select a Qualification  !!");
        showAlert('Please select Qualification  !!');
        return;
    }
    if (ID == "0") {
        if (file1 == "") {
            showAlert("Please Upload Document");
            return;
        }
    }
    if (UniversityName == "") {

        //$('#Uerr').html("Please Enter University Name  !!");
        showAlert('Please Enter Board/University Name  !!');
        return;
    }
    if (RollNo == "") {

        // $('#Yerr').html("Please select a Passing year  !!");
        showAlert('Please Enter  Roll Number  !!');
        return;
    }
    //if ($('#Qualification').val() != art || $('#Qualification').val() != sci || $('#Qualification').val() != comm) {
    //    //debugger;
        if (Percentage == "0" || Percentage == "") {

            //$('#Perr').html("Please Enter percentage  !!");
            showAlert('Please Enter Percentage  !!');
            return;
        }
    //}

    if (PassingYear == "") {

        // $('#Yerr').html("Please select a Passing year  !!");
        showAlert('Please select  Passing Year  !!');
        return;
    }
    if ($('#Qualification').val() == art || $('#Qualification').val() == sci || $('#Qualification').val() == comm) {

        if ($('#SubjectID').val() == "") {

            //$('#Berr').html("Please select a Qualification  !!");
            showAlert('Please select Honours Subject  !!');
            return;
        }
        if ($('#TotalMarks').val() == "" || $('#TotalMarks').val() == "0") {

            //$('#Berr').html("Please select a Qualification  !!");
            showAlert('Please Enter Honours TotalMarks   !!');
            return;
        }
        if ($('#MarksObtain').val() == "" || $('#MarksObtain').val() == "0") {

            //$('#Berr').html("Please select a Qualification  !!");
            showAlert('Please Enter Honours MarksObtain   !!');
            return;
        }
       
        var paperTotalMarks = $("#TotalMarks").val();
        var paperMarksObtain = $("#MarksObtain").val();
        var Percentage = $("#SubjectPercentage ").val();
        if (parseInt($("#MarksObtain").val()) > parseInt($("#TotalMarks").val())) {
            $("#SubjectPercentage").val("");
            $("#MarksObtain").val("");
            showAlert('Paper Obtain Marks  should be less Paper Total Marks !!');
            // $("#btn").attr("style", "display: none;");
            return;
        }
        paperTotalMarks = (paperTotalMarks == "" ? "0" : paperTotalMarks);
        paperMarksObtain = (paperMarksObtain == "" ? "0" : paperMarksObtain);
        Percentage = (paperMarksObtain * 100.00 / paperTotalMarks);
        Percentage = (Percentage == Infinity ? "0" : Percentage);
        if (isNaN(Percentage)) {
            Percentage = "0";
        }
        Percentage = (Percentage == undefined ? "0" : Percentage);
        $("#SubjectPercentage").val(parseFloat(Percentage).toFixed(2));

        if ($('#SubjectPercentage').val() == "" || $('#SubjectPercentage').val() == "0") {

            //$('#Berr').html("Please select a Qualification  !!");
            showAlert('Please Enter Honours Percentage   !!');
            return;
        }
    }
    res = 0;
    showloader();
    var EnID = $('#EnID').val();
    if (window.FormData !== undefined) {
        var fileUpload = $("#file").get(0);
        var files = fileUpload.files;
        var fileData = new FormData();
        if (files.length > 0) {
            for (var i = 0; i < files.length; i++) {
                fileData.append("file", files[i]);
            }
        }
        else {
            //showAlert($('#hfile').val());
            fileData.append("hfile", $('#hfile').val());

        }

        // debugger;
        fileData.append('ID', ID);
        fileData.append('Qualification', Qualification);
        fileData.append('UniversityName', UniversityName);
        fileData.append('Percentage', Percentage);
        fileData.append('PassingYear', PassingYear);
        fileData.append('RollNo', RollNo);

        fileData.append('SubjectID', $('#SubjectID').val());
        fileData.append('TotalMarks', $('#TotalMarks').val());
        fileData.append('MarksObtain', $('#MarksObtain').val());
        fileData.append('SubjectPercentage', $('#SubjectPercentage').val());
        fileData.append('Subid', $('#Subid').val());

        showloader();
        $.ajax({
            url: '/StudentLLB/HomeL/AddNewQualification',
            type: "POST",
            contentType: false, // Not to set any content header
            processData: false, // Not to process data
            data: fileData,
            success: function (result) {

                // console.log(result);
                if (result.Status == true) {

                    hideloader();
                    showAlert(result.Msg);
                    showloader();
                    //window.location.reload();
                    //window.location = '/College/Home/PreviousYearQualificationManualAd/?id=' + EnID;
                    location.replace('/StudentLLB/HomeL/PreviousyearQualificationO');
                }
                else {

                    showAlert(result.Msg);
                    hideloader();
                }
                //return false;
            },
            error: function (err) {
                // debugger;
                showAlert(err.statusText);
                hideloader();
                return false;
            }
        });
    }
    else {
        showAlert("FormData is not supported.");

        return false;
    }


}