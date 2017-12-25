function AjaxPost( formData, baseUrl, type, dataType,loadingDiv)
{
    $(formData).submit(function (e) {
        
        //var formdata = $(formData).serialize();
        
        $(loadingDiv).show();
        $.ajax({
            url: baseUrl,
            type: type,
            data: new FormData(this),
            processData: false,
            dataType:'json',
            contentType: false,
            success: function (result) {
                $(loadingDiv).hide();
                alert(result.Message);
            }
     });
        e.preventDefault();
    });
}

function AjaxGet(formData, baseUrl, type, dataType,outerDiv,formdiv,loadingDiv,ManualBookingId) {
    
        $.ajax({
            url: baseUrl,
            type: type,
            data:{ManualBookingNo:formData},
            dataType: dataType,
            success: function (result) {
                $(loadingDiv).hide();
                $(outerDiv).show();
                
                $(outerDiv).html("<b><h3>LR Details:-</h3></b><br/>" +
                     " <h4><b>LR Number ID :</b> " + result.ResultObject.ManualBookingId + "<br><br>" +
                    " <h4><b>LR Number :</b> " + result.ResultObject.BookingNo + "<br><br>" +
                    "<b>Booking Date :</b> " +  result.ResultObject.BookingDate + "<br><br>" +
                    "<b>From Location :</b> " + result.ResultObject.FromLocation + "<br><br>" +
                    "<b>To Location :</b> " + result.ResultObject.ToLocation + "<br><br>" +
                    "<b>Vehicle No :</b> " + result.ResultObject.VechicleNo + "<br><br>" +
                    "<b>Customer Name :</b> " + result.ResultObject.CustomerName + "<br><br>" +
                    "<b>Booking Weight :</b> " + result.ResultObject.BookingWeight + "<br><br>" +
                    "<b>No Of Packages:</b> " + result.ResultObject.BookingNoOfPackages + "</h4>");
                
                $(formdiv).appendTo(outerDiv);
                $(formdiv).show();
                $(ManualBookingId).val(result.ResultObject.ManualBookingId);

            }

        });    
}

function AjaxGet1(formData, baseUrl, type, dataType, outerDiv, tracediv, loadingDiv, trackingUrl) {

    $.ajax({
        url: baseUrl,
        type: type,
        data: { ManualBookingNo: formData },
        dataType: dataType,
        success: function (result) {
            $(loadingDiv).hide();
            $(outerDiv).show();
            
            $(outerDiv).html("<b><h3>LR Details:-</h3></b><br/>" +
                 " <h4><b>LR Number ID :</b> " + result.ResultObject.ManualBookingId + "<br><br>" +
                " <h4><b>LR Number :</b> " + result.ResultObject.BookingNo + "<br><br>" +
                "<b>Booking Date :</b> " + result.ResultObject.BookingDate + "<br><br>" +
                "<b>From Location :</b> " + result.ResultObject.FromLocation + "<br><br>" +
                "<b>To Location :</b> " + result.ResultObject.ToLocation + "<br><br>" +
                "<b>Vehicle No :</b> " + result.ResultObject.VechicleNo + "<br><br>" +
                "<b>Customer Name :</b> " + result.ResultObject.CustomerName + "<br><br>" +
                "<b>Booking Weight :</b> " + result.ResultObject.BookingWeight + "<br><br>" +
                "<b>No Of Packages:</b> " + result.ResultObject.BookingNoOfPackages + "</h4>");
            var BookingID = result.ResultObject.ManualBookingId;
            
            $.ajax({
                url: trackingUrl,
                type: type,
                data: { ManualBookingID: BookingID },
                dataType: 'json',
                
                success: function (data) {
                    var res = JSON.parse(data);
                    var restable = res.Table;
                    //$(tracediv).append("<table class='table table-bordered'><th>Tracking Date</th><th>Tracking Time</th><th>Location</th><th>Remarks</th><th>Reason</th>");
                    $.each(restable, function (key, value) {
                        //$(tracediv).append(value.Tracking_Time);
                        if (value.Signature == null)
                        {
                            $(tracediv).append("<table class='table table-bordered'><th>Tracking Date</th><th>Tracking Time</th><th>Location</th><th>Remarks</th><th>Reason</th><tr><td>" + value.Tracking_Date + "</td><td>" + value.Tracking_Time + "</td><td>" + value.Location + "</td><td>" + value.Remarks + "</td><td>" + value.Reason + "</td></tr></table>");
                        } 
                        if(value.Signature != null){
                            $(tracediv).append("<img src='" + value.Signature + "'/><br><a class='btn btn-lg btn-success' href='/Attachments/" + value.Attachment_Path + "'>Attachment</a>");

                        }
                    });

                   //$(tracediv).append("</table>");
                    
                    $(tracediv).appendTo(outerDiv);
                    $(tracediv).show();
                }
            });
        }

    });
}

