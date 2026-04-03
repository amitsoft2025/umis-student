
$(document).ready(function () {
    //DataTable
    debugger;
    var view = false;

    if ($('#Qualification').val() == art || $('#Qualification').val() == sci || $('#Qualification').val() == comm) {
    //    $('#SubjectTable').removeAttr("style");
    //    $("#SubjectTable").attr("style", "display: inline;");
    //    $('#Percentage').attr('readonly', true);

    //    $('#Perr').html('Please fill subject detail ,Aggregate Percentage will automatic Calculate');

    }
    else {
        $('#SubjectTable').removeAttr("style");
        $("#SubjectTable").attr("style", "display: none;");
        $('#Percentage').removeAttr("readonly");
        $('#Perr').html('');
    }
    $('.subper').change(function () {
        //$(".subper").each(function () {
        debugger;
        // showAlert($(this).val());
        if ($(this).val() == "0" || $(this).val() == "") {
            showAlert('Please Enter Subject Percentage!!!');
            $(this).val('');
            $(this).focus();
            return;
        }
        else if (parseInt($(this).val()) > 99) {
            showAlert('Please Enter Subject Percentage less then 99 !!!');
            $(this).val('');
            $(this).focus();
            return;
        }
        //});

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
        else if (parseInt($(this).val()) > tm) {
            showAlert('Obtained Marks should be less then Total marks !!!');
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
        //});
        var subper = $('input[name="SubjectPercentage"]').map(function () {
            return this.value
        }).get();
        var TotalMarks1 = $('input[name="TotalMarks"]').map(function () {
            return this.value
        }).get();
        var MarksObtain1 = $('input[name="MarksObtain"]').map(function () {
            return this.value
        }).get();
        if (subper.length == 5) {
            debugger;
            var aggper = 0.0;
            var totalm = 0.0;
            for (var i = 0; i < TotalMarks1.length; i++) {
                aggper = aggper + parseFloat(MarksObtain1[i]);
                totalm = totalm + parseFloat(TotalMarks1[i]);
            }
            //showAlert(aggper);
            var final = aggper * 100 / totalm;
            //$('#Percentage').val(final);
            $('#Percentage').val(final.toPrecision(4));
        }

    });

    $('#Qualification').change(function () {
        debugger;
        return;
        var res = $('#Qualification').val();
        if (res == "")
            res = 0;
        $.ajax({
            url: "/StudentV/Homev/SubjectTable/",
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
                    $('#Percentage').attr('readonly', true);
                    $('#Perr').html('Please fill subject detail ,Aggregate  Percentage will automatic Calculate');
                }
                else {
                    view = false;
                    $('#SubjectTable').removeAttr("style");
                    $("#SubjectTable").attr("style", "display: none;");
                    $('#Percentage').removeAttr("readonly");
                    $('#Perr').html(' ');
                }

            }
        });

    });
    $('.Subject').change(function () {

        debugger;
        return;
        var id = $('#Qualification').val();
        var res = $(this).val();
        var tr = $(this).closest("tr");
        var str = '';
        var SubjectID = $('select[name="SubjectID"]').map(function () {

            return this.value
        }).get();
        for (var i = 0; i < SubjectID.length; i++) {
            if (SubjectID[i] != "") {
                str += SubjectID[i] + ',';
            }

        }
        //showAlert(str);
        //tr.find('.Subject').find("option").remove();
        //tr.find('.Subject').append($("<option></option>").val("").html("Select Subject"));
        //tr.find('#compulsory1 ').find("option").remove();
        //tr.find('#compulsory1 ').append($("<option></option>").val("").html("--Select compulsory1--"));
        showloader();
        $.ajax({
            url: "/StudentV/Homev/Subject_bindDanamic/",
            data: { id: id, res: str },
            cache: false,
            type: "POST",
            dataType: "json",
            success: function (result) {
                hideloader();
                debugger;

                var str = '';
                str += '<option value=' + "" + '>' + "--Select Subject--" + '</option>';
                for (var i = 0; i < result.length; i++) {

                    str += '<option value=' + result[i].ID + '>' + result[i].SubjectName + '</option>';
                    //tr.next('tr').find('.Subject ').html($("<option     />").val(result[i].ID).text(result[i].SubjectName));

                }
                tr.next('tr').find('.Subject ').html(str);



            }
        });

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
        // $("#btn").attr("style", "display: none;");
        return;
    }
    else {
        //$('#btn').removeAttr("style");
        //$("#btn").attr("style", "display: inline;");
    }
    //debugger;
    //var Qualification = $("#Qualification").val();
    //var per = parseFloat($("#Percentage").val());
    // showAlert(per);
    //$('#btn').attr('display', 'none');

    //$.ajax({
    //    type: "POST",
    //    url: "/Student/Home/Checkfee",
    //    data: { qual: Qualification },

    //    success: function (response) {

    //        console.log(response);

    //        if (per < parseFloat(response.Percentage)) {

    //            $("#Percentage").val('');
    //            $("#btn").attr("style", "display: none;");
    //            showAlert('you are not eligiable for this course ,you percentage should be more than ' + response.Percentage);

    //             window.location.href = "/Student/Home/FeesSubmit";
    //        }
    //        else {
    //            $('#btn').removeAttr("style");
    //            $("#btn").attr("style", "display: inline;");
    //            window.location.href = "/Student/Home/FeesSubmit";
    //        }
    //    },
    //    Error: function (response) {

    //        $('#btn').attr('display', 'none');
    //        showAlert("coming in error");
    //    }
    //});
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
function encodeImagetoBase64(element) {
    //debugger;
    var file = element.files[0];

    var reader = new FileReader();

    reader.onloadend = function () {

        $(".link").attr("href", reader.result);

        $(".link").text(reader.result);

    }

    reader.readAsDataURL(file);

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


                        // break;
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
    //debugger;
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
    if ($('#Qualification').val() != art || $('#Qualification').val() != sci || $('#Qualification').val() != comm) {
        debugger;
        if (Percentage == "0" || Percentage == "") {

            //$('#Perr').html("Please Enter percentage  !!");
            showAlert('Please Enter Percentage  !!');
            return;
        }
    }

    if (PassingYear == "") {

        // $('#Yerr').html("Please select a Passing year  !!");
        showAlert('Please select  Passing Year  !!');
        return;
    }


    var obj = {
        ID: ID,
        QualicationType: Qualification,
        Board_UniversityName: UniversityName,
        Percentage: Percentage,
        PassingYear: PassingYear,
        FileURl: FileURl,
        file: file,
        hfile: hfile,
        RollNo: RollNo,
        EncriptedID: ID
    };




    var fileData = new FormData();


    var fileUpload = $("#file").get(0);
    var files = fileUpload.files;

    for (var i = 0; i < files.length; i++) {
        fileData.append("file", files[i]);
    }



    fileData.append('ID', ID);
    fileData.append('QualicationType', Qualification);
    fileData.append('Board_UniversityName', UniversityName);
    fileData.append('Percentage', Percentage);
    fileData.append('PassingYear', PassingYear);
    fileData.append('FileURl', FileURl);
    //fileData.append('file', file);
    fileData.append('hfile', hfile);
    fileData.append('RollNo', RollNo);
    fileData.append('EncriptedID', ID);



    //console.log(obj);
    var subper = $('input[name="SubjectPercentage"]').map(function () {
        return this.value
    }).get();
    var SubjectID = $('select[name="SubjectID"]').map(function () {

        return this.value
    }).get();
    var SubID = $('input[name="ID"]').map(function () {

        return this.value
    }).get();
    var TotalMarks = $('input[name="TotalMarks"]').map(function () {

        return this.value
    }).get();
    var MarksObtain = $('input[name="MarksObtain"]').map(function () {

        return this.value
    }).get();
    var no = $('input[name="SubjectPercentage"]');
    var listObj = [];
    var view;
    debugger;


      res = 0;


    showloader();

    $.ajax({
        url: "/StudentV/Homev/AddNewQualification",
        type: "POST",
        contentType: false, // Not to set any content header
        processData: false, // Not to process data
        data: fileData,
        success: function (result) {
            //console.log(result);
            hideloader();
            if (result.Status) {
                if (view) {
                    for (var i = 0; i < subper.length; i++) {

                        debugger;
                        var obj1 = {
                            ID: SubID[i],
                            QualificationMasterID: result.ScopeIdentity,
                            SubjectID: SubjectID[i],
                            SubjectPercentage: subper[i],
                            TotalMarks: TotalMarks[i],
                            MarksObtain: MarksObtain[i]
                        };
                        listObj.push(obj1);
                    }
                   
                    showAlert(" Subject Details  Updated successfully");
                }
                else {
                    showAlert(result.Msg);
                    hideloader();
                    showloader();
                    location.replace('/StudentV/Homev/PreviousyearQualification');
                }
            }
            else {
                hideloader();
                showAlert(result.Msg);
                // location.replace('/Student/Home/StudentQualification');
            }
            //location.replace('/Student/Home/PreviousyearQualification');
            // window.location.reload();

        },
        error: function (errormessage) {

            hideloader();
            showAlert(errormessage.responseText);
        }


    });


}

