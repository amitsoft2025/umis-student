$(document).ready(function () {

    $("#EducationTypeID").change(function (event) {
   
        $('#CourseCategoryID').find("option").remove();
        $("#CourseCategoryID").append($("<option></option>").val("").html("--Select Course--"));
        var res = $(this).val();
        if (res == "") {
            res = 0;
            return;
        }
        $.ajax({
            url: "/Home/getcousre/",
            data: { id: res },
            cache: false,
            type: "POST",
            dataType: "json",
            success: function (data) {

                //console.log(data);
                $('#CourseCategoryID').find("option").remove();
                $("#CourseCategoryID").append($("<option></option>").val("").html("--Select Course--"));
                $.each(data.data, function (key, value) {
                    $("#CourseCategoryID").append($("<option></option>").val(value.CourseCategoryID).html(value.CourseCategory));
                });
            } ,
            error: function () {
                $('#CourseCategoryID').find("option").remove();
            }
        });
    });
    $("#chkall").click(function () {
       
        if ($("#chkall:checked")) {
            //showAlert('hii');
            $(".chk").prop('checked', $(this).prop('checked'));
           
        } else {
            $(".chk").prop('checked', $(this).prop('checked'));
        }
       
    });
});

function submitapplication() {
   
    
    var courseid = $("#CourseCategoryID").val(); 
   
    var eduid = $("#EducationTypeID").val();
    var college = $("#CollegeID").val();
   // showAlert(courseid);
    var st =[];
    var st1 = [];
    showloader();
    $.when($.getJSON("/Administrator/Home/GetSubjectData/", { courseid: courseid, eduid: eduid, college: college }),
        $.getJSON("/Administrator/Home/GetSubject/", { courseid: courseid })).
        done(function (res1, res2) {
          
            hideloader();
            debugger;         
               
           
            console.log(res1[0].ID);
            $("#ID").val(res1[0].ID);
            st = res1[0].HonoursSubject.split(',');
           
           
                $('#table').removeAttr("style");
                $("#table").attr("style", "display: block;");
                var result1 = '<div class=""></div>';
               

                //showAlert(st.length);
                debugger;
                var ch = false;
            $.each(res2[0].honourslist, function (index, value) {
                    ///debugger;

                    if ($.inArray(value.StreamCategoryID.toString(), st) !== -1) {
                        //debugger;
                        ch = true;

                    }


                    result1 = result1 + '<div class="col-md-3">';
                    if (ch) {

                        result1 = result1 + '<input type="checkbox" name="chk" class="from-control chk" value="' + value.StreamCategoryID + '" checked>' + value.streamCategory + '</input>';
                    }
                    else {
                        result1 = result1 + '<input type="checkbox" name="chk" class="from-control chk" value="' + value.StreamCategoryID + '">' + value.streamCategory + '</input>';
                    }
                    result1 = result1 + '</div>';


                });

                $("#tbody").html(result1);
         
        }).fail(function (res2) {
            hideloader();
             $.ajax({
        url: "/Administrator/Home/GetSubject/",
        data: { courseid: courseid },
        cache: false,
        type: "POST",
        dataType: "json",
        success: function (data) {

            // console.log(data);
            $('#table').removeAttr("style");
            $("#table").attr("style", "display: block;");
            var result1 = '<div class=""></div>';
            var result2 = '';

            //showAlert(st.length);
            debugger;
            var ch = false;
            $.each(data.honourslist, function (index, value) {
                ///debugger;

                if ($.inArray(value.StreamCategoryID.toString(), st) !== -1) {
                    //debugger;
                    ch = true;

                }


                result1 = result1 + '<div class="col-md-3">';
                if (ch) {

                    result1 = result1 + '<input type="checkbox" name="chk" class="from-control chk" value="' + value.StreamCategoryID + '" checked>' + value.streamCategory + '</input>';
                }
                else {
                    result1 = result1 + '<input type="checkbox" name="chk" class="from-control chk" value="' + value.StreamCategoryID + '">' + value.streamCategory + '</input>';
                }
                result1 = result1 + '</div>';


            });

            $("#tbody").html(result1);        


        },
        error: function () {
            debugger;
            //console.log(data);
            $('#table').removeAttr("style");
            $("#table").attr("style", "display: none;");
            //showAlert('error');
        }
    });
        }); 

    //$.ajax({
    //    url: "/Administrator/Home/GetSubjectData/",
    //    data: { courseid: courseid, eduid: eduid, college: college},
    //    cache: false,
    //    type: "POST",
    //    dataType: "json",
    //    success: function (data1) {
    //        hideloader();
    //        debugger;
    //        //console.log(data);
    //        $("#ID").val(data1.ID);
    //        st = data1.HonoursSubject.split(',');
    //        //console.log(st);
    //        //st1 = data1.SubsidiarySubject.split(',');
          
    //        //console.log(st1);
          
    //    },
    //    error: function () {
    //        hideloader();
    //        //console.log(data1); 
    //        $('#table').removeAttr("style");
    //        $("#table").attr("style", "display: none;");
    //        //showAlert('error');
    //       //showAlert('No Record Found');
    //    }
    //});
    //$.ajax({
    //    url: "/Administrator/Home/GetSubject/",
    //    data: { courseid: courseid },
    //    cache: false,
    //    type: "POST",
    //    dataType: "json",
    //    success: function (data) {

    //        // console.log(data);
    //        $('#table').removeAttr("style");
    //        $("#table").attr("style", "display: block;");
    //        var result1 = '<div class=""></div>';
    //        var result2 = '';

    //        //showAlert(st.length);
    //        debugger;
    //        var ch = false;
    //        $.each(data.honourslist, function (index, value) {
    //            ///debugger;

    //            if ($.inArray(value.StreamCategoryID.toString(), st) !== -1) {
    //                //debugger;
    //                ch = true;

    //            }


    //            result1 = result1 + '<div class="col-md-3">';
    //            if (ch) {

    //                result1 = result1 + '<input type="checkbox" name="chk" class="from-control chk" value="' + value.StreamCategoryID + '" checked>' + value.streamCategory + '</input>';
    //            }
    //            else {
    //                result1 = result1 + '<input type="checkbox" name="chk" class="from-control chk" value="' + value.StreamCategoryID + '">' + value.streamCategory + '</input>';
    //            }
    //            result1 = result1 + '</div>';


    //        });

    //        $("#tbody").html(result1);
    //        //$("#tbody").append('<div class="text-center" style="margin-top:20px;"><h4>Subsidiary Subject List</h4></div>');

    //        //$.each(data.subsidiarylist, function (index, value) {
    //        //    var ch = false;
    //        //    if ($.inArray(value.StreamCategoryID.toString(), st1) !== -1) {
    //        //        debugger;
    //        //        ch = true;

    //        //    }              
    //        //    result2 = result2 + '<div class="col-md-3">';
    //        //    if (ch) {
    //        //        result2 = result2 + '<input type="checkbox" name="chk1" class="from-control" value="' + value.StreamCategoryID + '" checked>' + value.streamCategory + '</input>';
    //        //    }
    //        //    else {
    //        //        result2 = result2 + '<input type="checkbox" name="chk1" class="from-control" value="' + value.StreamCategoryID + '" >' + value.streamCategory + '</input>';

    //        //    }
    //        //    result2 = result2 + '</div>'; 



    //        //});
    //        //$("#tbody").append(result2);
    //    },
    //    error: function () {
    //        debugger;
    //        //console.log(data);
    //        $('#table').removeAttr("style");
    //        $("#table").attr("style", "display: none;");
    //        //showAlert('error');
    //    }
    //});
    
   
}
function Add() {
    debugger;
    var valueschk = $('input[name="chk"]').map(function () {
        
        return this.value
    }).get();
    var chekboxklist = "";
    var i = 0;
    var j = 0;
    $.each($("input[name='chk']:checked"), function () {   
                 chekboxklist += $(this).val()+',';               
    });
    var val = '';
     $.each($("input[name='chk1']:checked"), function () {   
         val += $(this).val()+',';               
    });

    
    //for (var i = 0; i < valueschk.length; i++) {
        
    //    val += valueschk[i] + ',';
    //}   
    //var valueschk1 = $('input[name="chk1"]').map(function () {
    //    return this.value
    //}).get();
    //var val1 = '';
    //for (var i = 0; i < valueschk1.length; i++) {

    //    val1 += valueschk1[i] + ',';
    //}

    var Obj = {
        ID:$("#ID").val(),
        EducationTypeID: $("#EducationTypeID").val(),
        CourseCategoryID: $("#CourseCategoryID").val(),
        HonoursSubject: chekboxklist,
        SubsidiarySubject: val,        
        CollegeID: $("#CollegeID").val()
    };
    showloader();
    $.ajax({
        url: "/Administrator/Home/Add",
        data: JSON.stringify(Obj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            hideloader();
            if (result.Status == 1) {
                showAlert(result.Msg);
                window.location.reload();
            }
            else {
                showAlert(result.Msg);
            }
        },
        error: function (errormessage) {
            hideloader();
            showAlert(errormessage.responseText);
        }
    });
}


    
