
$(document).on('click', '#PrintRecipt', function () {
    //debugger;
    ExportToPDFApplicationformfeesrec($('#dvPrintRecipt'), [], '', PDFPageType.Portrait, 'EnrollmentFeeReceipt');
});
$(document).ready(function () {

});

$(function () {

    $("#myform").submit(function (e) {
        showloader();
    });

});
function ExportToPDFApplicationformfeesrec(DivName, TableColumnHide, ReportHeading, PageType, Applicationform) {
    //debugger;
    /// <summary>Function to convert HTML to PDF.</summary>
    /// <param name="DivName">Name of the Div  with selector which contains all the html to print.</param>
    /// <param name="TableColumnHide" type="array">Pass array of columns to be hidden like [2,5,6] in a Table.</param>
    /// <param name="ReportHeading">Heading of the Report</param>
    /// <param name="PageType">Provide page type from enum PDFPageType </param>
    /// <returns>PDF File</returns>
    var PDFtable = DivName.find('table');
    if (TableColumnHide) {
        $.each(TableColumnHide, function (index, value) {
            PDFtable.find('td:nth-child(' + value + '),th:nth-child(' + value + ')').hide();  //hiding the columns of the table inside Div
        });
    }
    PDFtable.attr('border', '0');//applied 1 to table attribute border so we can get border in the Table 
    var printContents = '<html><head><title></title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" /></head><body><div style="font-size: 25px;" ><center>' + ReportHeading + '</center></div><br />' + DivName.html() + '</body></html>';

    $('body').prepend("<form method='post' action='/StudentBED/ExamBED/PrintEnrollmentFeeReceipt' id='tempForm2'><input type='hidden' name='data1' value='" + printContents + "' ><input type='hidden' name='dataname1' value='" + Applicationform + "' ><input type='hidden' name='PageType1' value='" + PageType + "' ></form>");
    $('#tempForm2').submit();
    $("tempForm2").remove();
    if (TableColumnHide) {
        $.each(TableColumnHide, function (index, value) {
            PDFtable.find('td:nth-child(' + value + '),th:nth-child(' + value + ')').show(); // //Again Showing the columns of the table inside Div
        });
    }
}
/// <summary>Enum for Page Type</summary>
var PDFPageType = {
    Default: 'Default',
    Portrait: 'Portrait',
    Landscape: 'Landscape'
}